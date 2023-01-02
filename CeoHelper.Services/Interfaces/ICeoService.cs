using OpenAI_API;

namespace CeoHelper.Services.Interfaces
{
    public interface ICeoService
    {
        Task<CompletionResult> ExecuteOpenAiRequest(string request, int tokens);
    }
}
