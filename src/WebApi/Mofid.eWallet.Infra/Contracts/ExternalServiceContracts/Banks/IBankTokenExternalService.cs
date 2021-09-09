using Mofid.eWallet.Entities.BusinessObjects;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks
{
	public interface IBankTokenExternalService
	{
		Task<Token> RefreshTokenAsync(Token token, string nationalCode);
		Task<Token> AcuireTokenAsync(string code, string nationalCode);
	}
}
