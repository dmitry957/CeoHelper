using CeoHelper.Data.Data.Entities.Base;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeoHelper.Data.Data.Repositories.Base
{
    public interface IBaseRepository<T> where T : class, IBaseEntity
    {
        Task<T> GetById(long id, bool asNoTracking = false);
        Task<List<T>> GetByIds(IEnumerable<long> ids);
        Task<long> Insert(T entity);
        Task InsertRange(IList<T> entity);
        Task Update(T entity);
        Task UpdateRange(IEnumerable<T> entities);
        Task Delete(long id);
        Task DeleteRange(IEnumerable<long> ids);
        IDbContextTransaction BeginTransaction();
    }
}
