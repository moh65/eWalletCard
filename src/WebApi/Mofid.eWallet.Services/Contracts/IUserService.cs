using Mofid.eWallet.Entities.BusinessObjects;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services
{
	public interface IUserService
	{
		Task<User> GetByIdAsync(string userId);
		Task<(bool, User)> AuthenticateAsync(string username, string password);
	}
}