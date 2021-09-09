using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks
{
	public interface IBankCustomerRemainBalanceService
	{
		Task<decimal> GetRemain(string nationalCode, string deviceId);
	}
}
