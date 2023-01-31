using CeoHelper.Shared.Models.Request;
using OpenAI_API;

namespace CeoHelper.Services.Services.Interfaces
{
    public interface ICeoService
    {
        Task<SearchResultModel> ExecuteOpenAiRequest(SearchRequestModel model);
    }
}
