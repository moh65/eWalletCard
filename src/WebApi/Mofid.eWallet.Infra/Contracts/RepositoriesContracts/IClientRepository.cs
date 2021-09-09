using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.RepositoriesContracts
{
	public interface IClientRepository
	{
		Task<Client> FindByPhoneNumberAsync(string phoneNumber);
		Task<Client> FindByNationalCodeAsync(string nationalCode);
		Task AddAsync(Client client);
		Task UpdateClientTokenAsync(Client client, Token token);
		Task UpdateClientTokenDeviceAsync(Client client, Token token);
		Task UpdateClientAsync(Client client);
		Task InsertToClientStateAsync(string nationalCode, ClientState clientState);
		Task<(long, List<Client>)> TakeAsync(int skip, int take, string nationalCode, string mobile);
	}
}
