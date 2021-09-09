using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services.Contracts
{
    public interface IClientStateService
    {
        Task<List<ClientState>> GetStates(string nationalCode);
        Task PassStateAsync(string nationalCode, ClientState state);
        Task<bool> IsStatePassed(string nationalCode, ClientState state);
    }
}
