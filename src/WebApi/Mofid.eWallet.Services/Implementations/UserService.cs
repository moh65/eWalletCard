using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using Mofid.eWallet.Infra.Exceptions;
using Mofid.eWallet.Infra.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services.Implementations
{
	[ExcludeFromCodeCoverage]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUtilityService utilityServiceService;
        public UserService(IUserRepository userRepository , IUtilityService UtilityServiceService)
        {
            this.utilityServiceService = UtilityServiceService;
            _userRepository = userRepository;
        }

        public async Task<(bool, User)> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user != null)
            {
                //first password should be hashed then check against the database
                string hashPassword = utilityServiceService.HashPassword(password);
                if (user.Password == hashPassword)
                {
                    return (true, user);
                }
            }
            return (false, null);
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user;
        }
    }
}
