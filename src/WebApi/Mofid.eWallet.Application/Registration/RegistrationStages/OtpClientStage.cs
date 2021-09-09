using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Application.Registration.RegistrationStages
{
	public class OtpClientStage : StageBase<Client>
	{
		private readonly IClientRepository clientRepository;
		private readonly IBankRegistrationExternalServcie bankRegistrationExternal;

		public OtpClientStage(IClientRepository clientRepository, IBankRegistrationExternalServcie bankRegistrationExternal, string deviceId)
		{
			this.clientRepository = clientRepository;
			this.bankRegistrationExternal = bankRegistrationExternal;
			DeviceId = deviceId;
		}

		public string DeviceId { get; }

		public async override Task<bool> Do()
		{
			var storedToken = Data.Tokens.First(w => w.DeviceId == DeviceId);

			await bankRegistrationExternal.OtpAsync(
				storedToken.KeyId,
				Data.PhoneNumber,
				Data.NationalCode);


			if (!Data.ClientStates.Any(x => x == ClientState.RegiteredNotVerified))
				await clientRepository.InsertToClientStateAsync(Data.NationalCode, ClientState.RegiteredNotVerified);

			return true;
		}

		public override Task<bool> Undo()
		{
			throw new NotImplementedException();
		}
	}
}
