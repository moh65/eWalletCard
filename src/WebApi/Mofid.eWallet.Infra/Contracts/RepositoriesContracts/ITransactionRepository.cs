using Mofid.eWallet.Entities.BusinessObjects;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.RepositoriesContracts
{
	public interface ITransactionRepository
	{
		Task AddAsync(Transaction transaction);

		Task UpdateAsync(Transaction transaction);
	}
}
