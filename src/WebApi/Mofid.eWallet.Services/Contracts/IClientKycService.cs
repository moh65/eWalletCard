using Mofid.eWallet.Api.Dto;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.DTOs;
using Mofid.eWallet.Entities.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services
{
	public interface IClientKycService
	{
		Task KycAsync(Client request, Token token);
		Task Verify(Client client, Token token, string otp);
        Task<Client> GetAddress(Client client);
		Task PhysicalVerifing(string nationalCode);
		Task<ClientRemainDto> GetTbsRemain(string nationalCode);
		Task<ClientRemainDto> GetBankRemain(string nationalCode , string deviceId);
		Task<bool> IsLegal(string nationalCode);
        Task<List<ClientState>> GetClientStates(string nationalCode);
        Task<(long , List<ClientDto>)> ListAsync(int skip, int take, string nationalCode, string mobile);
        Task<ClientDto> GetClientAsync(string nationalCode);
    }

}
