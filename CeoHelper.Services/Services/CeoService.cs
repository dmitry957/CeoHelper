using CeoHelper.Data;
using CeoHelper.Data.Data.Entities;
using CeoHelper.Data.Data.Repositories.Interfaces;
using CeoHelper.Data.Entities;
using CeoHelper.Services.Services.Interfaces;
using CeoHelper.Shared.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenAI_API;
using System.Text;
using System.Text.RegularExpressions;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace CeoHelper.Services.Services;

public class CeoService : ICeoService
{
    private readonly OpenAIAPI _openAIAPI;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRequestRepository _requestRepository;

    public CeoService(OpenAIAPI openAIAPI,
                      UserManager<ApplicationUser> userManager,
                      IHttpContextAccessor httpContextAccessor,
                      IRequestRepository requestRepository)
    {
        _openAIAPI = openAIAPI;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _requestRepository = requestRepository;
    }

    public async Task<SearchResultModel> ExecuteOpenAiRequest(SearchRequestModel model)
    {
        var currentUser = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
        if (currentUser.Tokens < model.Tokens)
        {
            throw new ApplicationException("Not enough tokens");
        }

        try
        {
            SearchResultModel resultModel = new();
            string payload = $"generate SEO text with the following requirements: \r\n" +
                $"{model.TextSize}\r\n" +
                $"include keywords: {model.Keywords}\r\n" +
                $"keywords must be {model.KeywordDensity}% off all symbols in the text\r\n" +
                $"keywords must remain as they are, don't change them\r\n" +
                $"include {model.Personalization}\r\n";
            var result = await _openAIAPI.Completions.CreateCompletionAsync(new CompletionRequest(payload, temperature: 0, max_tokens: 100));
            if (result.Completions.Count > 0)
            {
                using var transaction = _requestRepository.BeginTransaction();

                var newRequest = new Request()
                {
                    Body = model.Keywords,
                    UserId = currentUser.Id,
                    CreationDate = DateTime.UtcNow,
                    TokensUsed = model.Tokens
                };
                await _requestRepository.Insert(newRequest);

                Choice? choice = result.Completions.FirstOrDefault();
                if (choice is not null)
                {
                    int tokens = Regex.Matches(choice.Text, @"[\S]+").Count;
                    currentUser.Tokens -= tokens;
                }
                IdentityResult updateUserResult = await _userManager.UpdateAsync(currentUser);
                if (!updateUserResult.Succeeded)
                {
                    throw new ApplicationException(updateUserResult.ToString());
                }
                await transaction.CommitAsync();
                resultModel.AvailableTokens = currentUser.Tokens;
                resultModel.RequestId = newRequest.Id;
                return resultModel;
            }
        }
        catch
        {
            throw new ApplicationException("Something went wrong");
        }
        return new SearchResultModel();
    }
}
