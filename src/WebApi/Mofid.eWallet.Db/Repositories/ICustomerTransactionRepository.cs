using Mofid.eWallet.Domain.Models;
using System.Threading.Tasks;

namespace Mofid.eWallet.Db.Repositories
{
	public interface ICustomerTransactionRepository : IRepository<CustomerTransaction>
	{
		Task UpdateTransactionBOResponseTime(CustomerTransaction transaction);
	}
}
