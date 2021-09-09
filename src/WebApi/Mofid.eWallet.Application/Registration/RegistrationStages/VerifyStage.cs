using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Caches;
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
	public class VerifyStage : StageBase<Client>
	{
		private readonly IClientRepository clientRepository;
		private readonly IBankRegistrationExternalServcie bankRegistrationExternal;
		private readonly IBankTokenExternalService bankTokenService;
		private readonly ICache cache;

		public VerifyStage(
			IClientRepository clientRepository,
			IBankRegistrationExternalServcie bankRegistrationExternal,
			IBankTokenExternalService bankTokenService,
			ICache cache,
			string deviceId,
			string otp)
		{
			this.clientRepository = clientRepository;
			this.bankRegistrationExternal = bankRegistrationExternal;
			this.bankTokenService = bankTokenService;
			this.cache = cache;
			DeviceId = deviceId;
			Otp = otp;
		}

		public string DeviceId { get; }
		public string Otp { get; }

		public async override Task<bool> Do()
		{
			var token = Data.Tokens.FirstOrDefault(x => x.DeviceId == DeviceId);
			if (token == null)
				throw new NotFoundBusinessException(ExceptionErrorCodes.NullTokenSemantic);

			var code = await bankRegistrationExternal.VerifyOtpAsync(token.KeyId, Data.PhoneNumber, Otp, Data.NationalCode);
			Token acquiredToken = await bankTokenService.AcuireTokenAsync(code, Data.NationalCode);
			acquiredToken.DeviceId = token.DeviceId;
			acquiredToken.KeyId = token.KeyId;

			token.AccessToken = acquiredToken.AccessToken;
			token.AccessTokenExpire = acquiredToken.AccessTokenExpire;
			token.RefreshToken = acquiredToken.RefreshToken;
			token.RefreshTokenExpire = acquiredToken.RefreshTokenExpire;
			token.TokenAcquire = acquiredToken.TokenAcquire;

			//Add to cache
			cache.Set(Token.CreateCacheKey(Data.PhoneNumber, acquiredToken.DeviceId), acquiredToken);

			var newKyc = await bankRegistrationExternal.GetProfileAsync(acquiredToken, 1, Data.NationalCode);
			Data.Username = newKyc.Username;
			Data.NickName = newKyc.NickName;
			Data.UserId = newKyc.UserId;
			Data.FinancialLevel.Value = newKyc.FinancialLevel.Value;
			Data.FinancialLevel.Level = newKyc.FinancialLevel.Level;
			Data.FinancialLevel.LevelName = newKyc.FinancialLevel.LevelName;


			if (!Data.ClientStates.Any(x => x == ClientState.OtpVerified))
				await clientRepository.InsertToClientStateAsync(Data.NationalCode, ClientState.OtpVerified);

			await clientRepository.UpdateClientTokenAsync(Data, acquiredToken);
			await clientRepository.UpdateClientAsync(Data);
			return true;
		}

		public override Task<bool> Undo()
		{
			throw new NotImplementedException();
		}
	}
}
