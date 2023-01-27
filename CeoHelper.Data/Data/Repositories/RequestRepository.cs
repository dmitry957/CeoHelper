using CeoHelper.Data.Data.Entities;
using CeoHelper.Data.Data.Repositories.Base;
using CeoHelper.Data.Data.Repositories.Interfaces;

namespace CeoHelper.Data.Data.Repositories
{
    public class RequestRepository : BaseRepository<Request>, IRequestRepository
    {
        public RequestRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
