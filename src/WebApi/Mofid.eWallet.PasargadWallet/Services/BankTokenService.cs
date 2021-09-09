using Microsoft.Extensions.Options;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Configurations;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Exceptions;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.PasargadWallet.ApiHelper;
using Mofid.eWallet.PasargadWallet.ResponseModels;
using Newtonsoft.Json;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.Services
{
	public class BankTokenService : IBankTokenExternalService
	{
		private IUtilityService _utilityService;
		private BankConfiguration _bankSetting;
		public BankTokenService(IUtilityService utility, IOptions<BankConfiguration> bankConfig)
		{
			_utilityService = utility;
			_bankSetting = bankConfig.Value;
		}

		public async Task<Token> AcuireTokenAsync(string code , string nationalCode)
		{
			try
			{
				var nowTime = _utilityService.GetNow();
				var response = await _AcuireTokenAsync(code , nationalCode);
				return new Token()
				{
					AccessToken = response.AccessToken,
					RefreshToken = response.RefreshToken,
					AccessTokenExpire = nowTime.AddSeconds(response.ExpiresIn),
					RefreshTokenExpire = nowTime.AddDays(365)
				};
			}
            catch (ApiException)
            {
				throw new InvalidDataBusinessException(ExceptionErrorCodes.BankTokenFailed);
            }
		}

		private async Task<TokenResponse> _AcuireTokenAsync(string code, string nationalCode)
		{
			var client = RestClient.For<IToken>(_bankSetting.AccountAddress);
			client.BearerToken = "Bearer " + _bankSetting.ApiToken;

			try
			{
				var data = new Dictionary<string, object>()
				{
					{ "client_id", _bankSetting.ClientId},
					{ "client_secret", _bankSetting.ClientSecret},
					{ "grant_type", "authorization_code"},
					{ "code", code},
				};
				var watch = _utilityService.StartNewWatch();
				var response = await client.GetTokenAsync(data);
				_utilityService.PodAudit(response, $"AcuireTokenAsync " + JsonConvert.SerializeObject(data), nationalCode, time: _utilityService.TotalTime(watch));
				return response;
			}
			catch (ApiException ex)
			{
				throw new ExternalServiceCorruptionException(ExceptionErrorCodes.BankTokenFailed, ex.Message);
			}
		}
		public async Task<Token> RefreshTokenAsync(Token token, string nationalCode)
		{
			try
			{
				var now = _utilityService.GetNow();
				var resonse = await _RefreshTokenAsync(token.RefreshToken,nationalCode);
				token.AccessToken = resonse.AccessToken;
				token.AccessTokenExpire = now.AddSeconds(resonse.ExpiresIn);
				return token;
			} catch(ApiException)
            {
				throw new InvalidDataBusinessException(ExceptionErrorCodes.RefreshTokenFailed);
            }
		}

		private async Task<TokenResponse> _RefreshTokenAsync(string refreshToken, string nationalCode)
		{
			var client = RestClient.For<IToken>(_bankSetting.AccountAddress);
			client.BearerToken = "Bearer " + _bankSetting.ApiToken;

			try
			{
				var data = new Dictionary<string, object>()
				{
					{ "client_id", _bankSetting.ClientId},
					{ "client_secret", _bankSetting.ClientSecret},
					{ "grant_type", "refresh_token"},
					{ "refresh_token", refreshToken},
				};
				var watch = _utilityService.StartNewWatch();
				var response = await client.GetTokenAsync(data);
				_utilityService.PodAudit(response, $"RefreshTokenAsync " + JsonConvert.SerializeObject(data), nationalCode, time: _utilityService.TotalTime(watch));
				return response;
			}
			catch (ApiException ex)
			{
				throw new ExternalServiceCorruptionException(ExceptionErrorCodes.RefreshTokenFailed, ex.Message);
			}
		}
	}
}
