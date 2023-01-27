using CeoHelper.Data.Data.Repositories.Base;
using CeoHelper.Data.Entities;

namespace CeoHelper.Data.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task Deactivate(ApplicationUser entity);
        Task Activate(ApplicationUser entity);
    }
}
