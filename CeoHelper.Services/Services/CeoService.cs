using CeoHelper.Data;
using CeoHelper.Data.Data.Entities;
using CeoHelper.Data.Entities;
using CeoHelper.Services.Interfaces;
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
    private readonly ApplicationDbContext _applicationDbContext;

    public CeoService(OpenAIAPI openAIAPI, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext applicationDbContext)
    {
        _openAIAPI = openAIAPI;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _applicationDbContext = applicationDbContext;
    }
    public async Task<CompletionResult> ExecuteOpenAiRequest(string request, int tokens)
    {
        var currentUser = await _applicationDbContext.Users.FirstOrDefaultAsync( x => x.Email == _httpContextAccessor.HttpContext.User.Identity.Name);
        if (currentUser.Tokens < tokens)
        {
            throw new ApplicationException("Not enough tokens");
        }

        try
        {
            var result = await _openAIAPI.Completions.CreateCompletionAsync(new CompletionRequest(request, temperature: 0, max_tokens: 100));
            if (result.Completions.Count > 0)
            {
                using var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

                var newRequest = new Request()
                {
                    Body = request,
                    UserId = currentUser.Id,
                    CreationDate = DateTime.UtcNow,
                    TokensUsed  = tokens
                };
                await _applicationDbContext.AddAsync(newRequest);

                currentUser.Tokens -= tokens;
               
                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
        }
        catch
        {
            throw new ApplicationException("Something went wrong");
        }
        return new CompletionResult();
    }
}
