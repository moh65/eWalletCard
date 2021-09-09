using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using Mofid.eWallet.Infra.Exceptions;
using Mofid.eWallet.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services.Implementations
{
	class ClientStateService : IClientStateService
    {
        private readonly IClientRepository clientRepository;

        public ClientStateService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }
        public async Task<List<ClientState>> GetStates(string nationalCode)
        {
            var client = await clientRepository.FindByNationalCodeAsync(nationalCode);

            if (client is null)
                throw new BusinessException(ExceptionErrorCodes.ClientNotFound);

            return client.ClientStates;
        }

        public async Task PassStateAsync(string nationalCode, ClientState state)
        {
            if (!await IsStatePassed(nationalCode, state))
                await clientRepository.InsertToClientStateAsync(nationalCode, state);
        }
        public async Task<bool> IsStatePassed(string nationalCode, ClientState state)
        {
            var client = await clientRepository.FindByNationalCodeAsync(nationalCode);
            if (client is null)
            {
                throw new NotFoundBusinessException(ExceptionErrorCodes.ClientNotFound);
            }
            var result = client.ClientStates.Any(x => x == state);
            return result;
        }
    }
}
