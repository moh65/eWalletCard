using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using Mofid.eWallet.Infra.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Application.Registration.RegistrationStages
{
	public class Level4Stage : StageBase<Client>
	{
		private readonly IClientRepository clientRepository;
		private readonly IBankRegistrationExternalServcie bankRegistrationExternal;

		public Level4Stage(
			IClientRepository clientRepository,
			IBankRegistrationExternalServcie bankRegistrationExternal,
			string deviceId)
		{
			this.clientRepository = clientRepository;
			this.bankRegistrationExternal = bankRegistrationExternal;
			DeviceId = deviceId;
		}

		public string DeviceId { get; }

		public async override Task<bool> Do()
		{
			if (!Data.ClientStates.Any(x=>x == ClientState.UpgradeToLevel4))
			{
				var acquiredToken = Data.Tokens.First(x => x.DeviceId == DeviceId);
				await bankRegistrationExternal.UpgradeToLevel4Async(acquiredToken.AccessToken, Data.BourseCode, Data.NationalCode);
				await clientRepository.InsertToClientStateAsync(Data.NationalCode, ClientState.UpgradeToLevel4);
			}
			return true;
		}

		public override Task<bool> Undo()
		{
			throw new NotImplementedException();
		}
	}
}
