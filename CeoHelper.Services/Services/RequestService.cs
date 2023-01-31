using CeoHelper.Data.Data.Entities;
using CeoHelper.Data.Data.Repositories.Interfaces;
using CeoHelper.Services.Services.Interfaces;

namespace CeoHelper.Services.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task Like(long requestId)
        {
            Request request = await _requestRepository.GetById(requestId);
            if (request is null)
            {
                throw new ApplicationException("Request not found.");
            }
            await _requestRepository.Like(request);
        }

        public async Task Dislike(long requestId)
        {
            Request request = await _requestRepository.GetById(requestId);
            if (request is null)
            {
                throw new ApplicationException("Request not found.");
            }
            await _requestRepository.Dislike(request);
        }
    }
}
