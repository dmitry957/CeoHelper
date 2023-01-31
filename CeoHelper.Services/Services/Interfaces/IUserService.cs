using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeoHelper.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> GetCurrentUserAvailableTokens();
    }
}
