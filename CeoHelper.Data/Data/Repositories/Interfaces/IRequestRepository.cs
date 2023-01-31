using CeoHelper.Data.Data.Entities;
using CeoHelper.Data.Data.Repositories.Base;

namespace CeoHelper.Data.Data.Repositories.Interfaces
{
    public interface IRequestRepository : IBaseRepository<Request>
    {
        Task Like(Request request);
        Task Dislike(Request request);
    }
}
