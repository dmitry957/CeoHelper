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

        public async Task Like(Request request)
        {
            request.IsLiked = true;
            request.ModifyDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task Dislike(Request request)
        {
            request.IsLiked = false;
            request.ModifyDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
