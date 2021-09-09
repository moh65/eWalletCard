using Mofid.eWallet.Entities.BusinessObjects;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks
{
	public interface IBankTransferExternalService
	{
		Task<bool> TransferAsync(string accessToken, int tokenIssuer, int userId, string guildCode, decimal amount, string description, string wallet, string uniqueId, string currencyCode = "IRR", string nationalCode = "");
	}
}
