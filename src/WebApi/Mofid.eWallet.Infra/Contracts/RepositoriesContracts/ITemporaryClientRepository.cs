using Mofid.eWallet.Entities.BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.RepositoriesContracts
{
	public interface ITemporaryClientRepository
	{
		Task<bool> IsInWhiteList(string nationalCode);
		Task<TemporaryClient> FindByNationCodeAsync(string nationalCode);
		Task AddAsync(TemporaryClient item);
		Task AddAsync(List<TemporaryClient> items);
		Task UpdateAsync(TemporaryClient client);
		Task<(long, List<TemporaryClient>)> List(int skip, int take, string nationalCode, string mobile);
		Task DeleteById(string id);
	}
}
