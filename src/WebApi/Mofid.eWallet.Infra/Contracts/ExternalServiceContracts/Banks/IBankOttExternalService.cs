using Mofid.eWallet.Entities.BusinessObjects;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks
{
	public interface IBankOttExternalService
	{
		Task<string> GetOttAsync(string accessToken, int tokenIssuer, string nationalCode);
	}
}
