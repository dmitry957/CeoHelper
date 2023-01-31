using CeoHelper.Data.Data.Repositories.Interfaces;
using CeoHelper.Data.Entities;
using CeoHelper.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeoHelper.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> GetCurrentUserAvailableTokens()
        {
            var currentUser = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUser is null)
            {
                throw new ApplicationException("User not found.");
            }

            return currentUser.Tokens;
        }
    }
}
