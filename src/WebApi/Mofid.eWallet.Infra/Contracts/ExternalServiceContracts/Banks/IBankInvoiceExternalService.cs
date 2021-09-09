using Mofid.eWallet.Entities.BusinessObjects;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks
{
	public interface IBankInvoiceExternalService
	{
		Task<Invoice> SetInvoice(string userId, string ott, double prices, string nationalCode);
	}
}
