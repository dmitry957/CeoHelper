using CeoHelper.Data.Data.Repositories.Interfaces;
using CeoHelper.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CeoHelper.Data.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<ApplicationUser> _entities;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<ApplicationUser>();
        }

        public async Task Deactivate(ApplicationUser user)
        {
            user.IsDeactivated = true;
            user.DeactivationDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task Activate(ApplicationUser user)
        {
            user.IsDeactivated = false;
            user.DeactivationDate = null;
            await _context.SaveChangesAsync();
        }
    }
}
