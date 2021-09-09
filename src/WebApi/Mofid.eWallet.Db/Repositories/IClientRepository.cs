using Mofid.eWallet.Domain.Models;
using System.Threading.Tasks;

namespace Mofid.eWallet.Db.Repositories
{
	public interface IClientRepository
	{
		Task<Client> FindByPhoneNumberAsync(string phoneNumber);
		Task AddAsync(Client client);   
		Task UpdateClientTokenAsync(Client client, Token token);
		Task UpdateClientTokenDeviceAsync(Client client, Token token);

	}
}
