using CeoHelper.Shared.Models.Request;
using OpenAI_API;

namespace CeoHelper.Services.Services.Interfaces
{
    public interface ICeoService
    {
        Task<(CompletionResult completionResult, int availableTokens)> ExecuteOpenAiRequest(SearchRequestModel model);
    }
}
