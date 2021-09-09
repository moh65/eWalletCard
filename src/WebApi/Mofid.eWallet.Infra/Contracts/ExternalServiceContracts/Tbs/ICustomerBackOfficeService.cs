using Mofid.eWallet.Api.Dto;
using Mofid.eWallet.Entities.BusinessObjects;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Tbs
{
	public interface ICustomerBackOfficeService
	{
		Task<Client> GetCustomerByNationalCodeAsync(string nationalCode);
		Task<ClientRemainDto> GetCustomerRemain(string nationalCode);
		Task<bool> IncreaseTbsBalanceAsync(decimal amount , string nationalCode);
		Task<bool> DecreaseTbsBalanceAsync(decimal amount, string nationalCode);
	}
}
