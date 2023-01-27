using CeoHelper.Data.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace CeoHelper.Data.Data.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected DbSet<T> _entities;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> GetById(long id, bool asNoTracking = false)
        {
            IQueryable<T> request = _entities.Where(item => item.Id == id);
            if (asNoTracking)
            {
                request = request.AsNoTracking();
            }
            return await request.SingleOrDefaultAsync();
        }

        public async Task<List<T>> GetByIds(IEnumerable<long> ids)
        {
            return await _entities.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<long> Insert(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }
            _entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task InsertRange(IList<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException();
            }
            await _entities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }
            entity.ModifyDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRange(IEnumerable<T> entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException();
            }
            var modifyDate = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.ModifyDate = modifyDate;
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            T entity = _entities.SingleOrDefault(s => s.Id == id);
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<long> ids)
        {
            IQueryable<T> entities = _entities.Where(entity => ids.Contains(entity.Id));
            _entities.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
