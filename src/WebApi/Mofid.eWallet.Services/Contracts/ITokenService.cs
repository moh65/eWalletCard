using Mofid.eWallet.Entities.BusinessObjects;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services
{

	public interface ITokenService
	{
		Task<Token> AcquireTokenAsync(string phoneNumber, string deviceId);

	}

}
