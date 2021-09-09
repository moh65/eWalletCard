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
	public class HandshakeStage : StageBase<Client>
	{
		private readonly IClientRepository clientRepository;
		private readonly IBankRegistrationExternalServcie bankRegistrationExternal;

		public HandshakeStage(
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
			if (!Data.Tokens.Any(x =>
					x.KeyId != null &&
					x.HandshakeExpireDate > DateTime.Now &&
					x.DeviceId == DeviceId))
				Data = await HandshakeAgain(Data, DeviceId, Data.NationalCode);

			if (!Data.ClientStates.Any(x => x == ClientState.HandshakedWithBank))
				await clientRepository.InsertToClientStateAsync(Data.NationalCode, ClientState.HandshakedWithBank);

			return true;
		}

		public override Task<bool> Undo()
		{
			throw new NotImplementedException();
		}
		private async Task<Client> HandshakeAgain(Client kyc, string deviceId, string nationalCode)
		{
			var token = await bankRegistrationExternal.HandshakeAsync(deviceId, nationalCode);
			if (token == null)
				throw new BusinessException(ExceptionErrorCodes.HandshakeFaileException);
			if (kyc.Tokens != null)
			{
				var oldToken = kyc.Tokens.FirstOrDefault(f => f.DeviceId == token.DeviceId);
				if (oldToken != null)
				{
					oldToken.KeyId = token.KeyId;
					oldToken.HandshakeExpireDate = token.HandshakeExpireDate;
					oldToken.AccessToken = token.AccessToken;
					oldToken.AccessTokenExpire = token.AccessTokenExpire;
					oldToken.RefreshToken = token.RefreshToken;
					oldToken.RefreshTokenExpire = token.RefreshTokenExpire;
				}
				else kyc.Tokens.Add(token);
			}
			else
			{
				kyc.Tokens = new List<Token>();
				kyc.Tokens.Add(token);
			}
			await clientRepository.UpdateClientTokenAsync(kyc, token);
			return kyc;
		}
	}
}
