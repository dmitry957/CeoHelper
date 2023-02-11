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
using OpenAI_API.Completions;
using OpenAI_API.Models;
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
        if (currentUser.Tokens < model.TextSize)
        {
            throw new ApplicationException("Not enough tokens");
        }

        try
        {
            SearchResultModel resultModel = new();
            string payload = GetTemplatedRequest(model);
            var result = await _openAIAPI.Completions.CreateCompletionAsync(new CompletionRequest(payload, model: Model.DavinciText,  temperature: 1, max_tokens: model.TextSize));

            if (result.Completions.Count > 0)
            {
                resultModel.GeneratedContent = result.Completions.FirstOrDefault()!.Text;
            }
            //mock generated content from Open AI
            //resultModel.GeneratedContent = "Lorem ipsum dolor sit amet, consectetur adipisicing elit,\r\n" +
            //                               "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.\r\n" +
            //                               "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut\r\n" +
            //                               "aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit\r\n" +
            //                               "in voluptate velit esse cillum dolore.";

            using var transaction = _requestRepository.BeginTransaction();

            var newRequest = new Request()
            {
                Body = payload,
                UserId = currentUser.Id,
                CreationDate = DateTime.UtcNow,
                TokensUsed = model.TextSize,
                Result = resultModel.GeneratedContent
            };
            await _requestRepository.Insert(newRequest);

            //Choice? choice = result.Completions.FirstOrDefault();
            //if (choice is not null)
            //{
            int tokens = model.TextSize;
            currentUser.Tokens -= tokens;
            //}

            IdentityResult updateUserResult = await _userManager.UpdateAsync(currentUser);
            if (!updateUserResult.Succeeded)
            {
                throw new ApplicationException(updateUserResult.ToString());
            }
            await transaction.CommitAsync();
            resultModel.AvailableTokens = currentUser.Tokens;
            resultModel.RequestId = newRequest.Id;
            return resultModel;
            //}
        }
        catch
        {
            throw new ApplicationException("Something went wrong");
        }
        return new SearchResultModel();
    }

    private string GetTemplatedRequest(SearchRequestModel model) {
        if (model.Language.Equals("en-US")) {
            return $"generate SEO text with the following requirements: " +
                    $"text size {model.TextSize} symbols " +
                    $"include keywords: {model.Keywords} " +
                    $"keywords must be {model.KeywordDensity}% off all symbols in the text " +
                    $"keywords must remain as they are, don't change them " +
                    $"include {model.Personalization} ";
        }
        return $"сгенерируй СЕО текст с следующими требованиями: \r\n" +
               $"размер текста {model.TextSize} символов\r\n" +
               $"включая ключевые слова: {model.Keywords}\r\n" +
               $"ключевые слова должны быть {model.KeywordDensity}% от всего текста\r\n" +
               $"ключевые слова должны оставатся как они есть, не меняй их\r\n" +
               $"включая {model.Personalization}\r\n";
    }
}
