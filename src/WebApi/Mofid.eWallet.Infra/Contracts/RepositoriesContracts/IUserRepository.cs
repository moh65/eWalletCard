using Mofid.eWallet.Entities.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.RepositoriesContracts
{
	public interface IUserRepository
	{
		Task<User> GetByIdAsync(string userId);
		Task<User> GetByUsernameAsync(string username);
	}
}
