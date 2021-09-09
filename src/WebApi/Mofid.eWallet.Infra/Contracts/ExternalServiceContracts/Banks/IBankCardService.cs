using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks
{
	public interface IBankCardService
	{
		Task CardIssuanceAsync(string accessToken, string nationalCode);

		Task CardActivateAsync(string token, int userid, string pan, string nationalCode);
		Task VerifyCardActivateAsync(string accessToken, string phoneNumber, string otp, string nationalCode);
	}
}
