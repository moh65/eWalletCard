using Mofid.eWallet.Entities.BusinessObjects;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks
{
	public interface IBankRegistrationExternalServcie
	{
		Task<Token> HandshakeAsync(string deviceId, string nationalCode);
		Task OtpAsync(string keyId, string identity, string nationalCode);
		Task<string> VerifyOtpAsync(string keyId, string identity, string otpCode, string nationalCode);


		//ToLevel4
		Task<bool> CheckLevel4Async(string accessToken, string nationalCode);
		Task UpgradeToLevel4Async(string accessToken, string exchangecode, string nationalCode);


		//Profile
		Task<Client> GetProfileAsync(Token accessToken, int tokenIssuer, string nationalCode);
		Task<bool> EditProfileAsync(string nationalCode, string birthDate, string nikname, string accessToken, int tokenIssuer = 1);
		Task<Client> EditProfileWithConfirmAsync(string accessToken, int tokenIssuer, string nationalCode);


		//physicalVerify
		Task<bool> PhysicalVerifyAsync(string userId, string nationalCode);
	}
}
