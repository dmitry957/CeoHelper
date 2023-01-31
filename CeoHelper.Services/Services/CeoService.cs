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

    public async Task<(CompletionResult completionResult, int availableTokens)> ExecuteOpenAiRequest(SearchRequestModel model)
    {
        var currentUser = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
        if (currentUser.Tokens < model.Tokens)
        {
            throw new ApplicationException("Not enough tokens");
        }

        try
        {
            var result = await _openAIAPI.Completions.CreateCompletionAsync(new CompletionRequest(model.Text, temperature: 0, max_tokens: 100));
            if (result.Completions.Count > 0)
            {
                using var transaction = _requestRepository.BeginTransaction();

                var newRequest = new Request()
                {
                    Body = model.Text,
                    UserId = currentUser.Id,
                    CreationDate = DateTime.UtcNow,
                    TokensUsed  = model.Tokens
                };
                await _requestRepository.Insert(newRequest);

                currentUser.Tokens -= model.Tokens;
                IdentityResult updateUserResult = await _userManager.UpdateAsync(currentUser);
                if (!updateUserResult.Succeeded)
                {
                    throw new ApplicationException(updateUserResult.ToString());
                }
                await transaction.CommitAsync();
                return (result, currentUser.Tokens);
            }
        }
        catch
        {
            throw new ApplicationException("Something went wrong");
        }
        return (new CompletionResult(), currentUser.Tokens);
    }
}
