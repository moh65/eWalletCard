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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.Services
{
	public class BankRegistrationServcie : IBankRegistrationExternalServcie
	{
		private readonly BankConfiguration _bankSetting;
		private readonly IUtilityService _utilityService;

		public BankRegistrationServcie(IOptions<BankConfiguration> bankSetting, IUtilityService utilityService)
		{
			this._bankSetting = bankSetting.Value;
			_utilityService = utilityService;
		}

		public async Task<Token> HandshakeAsync(string deviceId, string nationalCode)
		{
			try
			{
				var watch = _utilityService.StartNewWatch();
				var response = await _HandshakeAsync(deviceId ,nationalCode);
				var token = new Token()
				{
					CreateDate = _utilityService.GetNow(),
					DeviceId = deviceId,
					KeyId = response.KeyId,
					HandshakeExpireDate = _utilityService.GetNow().AddSeconds(response.ExpiresInSecond)
				};

				_utilityService.PodAudit(response, $"token_HandshakeAsync?nationalCode={nationalCode}&deviceId={deviceId}", nationalCode, time: _utilityService.TotalTime(watch));
				return token;
			}
            catch (ApiException)
            {
				throw new InvalidDataBusinessException(ExceptionErrorCodes.BankRegistrationFailed);
            }
		}
		private async Task<HandshakeResponse> _HandshakeAsync(string deviceId, string nationalCode)
		{
			var client = RestClient.For<ICustomerRegistration>(_bankSetting.AccountAddress);
			client.BearerToken = "Bearer " + _bankSetting.ApiToken;

			var dic = new Dictionary<string, object>()
			{
				{ "client_id", _bankSetting.ClientId },
				{ "device_uid", deviceId },
			};


			var response = await client.HandshakeAsync(_bankSetting.ClientId, dic);
			var resultString = await response.Content.ReadAsStringAsync();
			var resutl = JsonConvert.DeserializeObject<HandshakeResponse>(resultString);

			return resutl;

		}

		public async Task OtpAsync(string keyId, string identity, string nationalCode)
		{
			try
			{
				await GetOtpAsync(keyId, identity , nationalCode);
			} catch (ApiException ex)
            {
				throw new InvalidDataBusinessException(ExceptionErrorCodes.OtpSendFailed);
            }
		}
		private async Task<OtpResponse> GetOtpAsync(string keyId, string identity, string nationalCode)
		{
			var client = RestClient.For<ICustomerRegistration>(_bankSetting.AccountAddress);
			client.AuthorizeHeader = $@"Signature keyId=""{keyId}"", signature=""{_bankSetting.Signature}"", headers=""host""";

			var data = new Dictionary<string, object>()
				{
					{ "response_type", "code"},
					{ "scope", "legal_birthdate legal_birthdate_write legal_nationalcode legal_nationalcode_write profile email"}
				};

			var watch = _utilityService.StartNewWatch();
			var response = await client.AuthorizeAsync(identity, data);
			_utilityService.PodAudit(response, $"OtpAsync " +  JsonConvert.SerializeObject(data) , nationalCode, time: _utilityService.TotalTime(watch));
			return response;
		}

		public async Task<string> VerifyOtpAsync(string keyId, string identity, string otpCode, string nationalCode)
		{
            try
            {
				var response = await _VerifyOtpAsync(keyId, identity, otpCode , nationalCode);
				return response.Code;
			}
			catch (ApiException)
            {
				throw new InvalidDataBusinessException(ExceptionErrorCodes.OtpVerificationFailed);
            } 
		}

		private async Task<VerifyResponse> _VerifyOtpAsync(string keyId, string identity, string otpCode, string nationalCode)
		{
			var client = RestClient.For<ICustomerRegistration>(_bankSetting.AccountAddress);
			client.AuthorizeHeader = $@"Signature keyId=""{keyId}"", signature=""{_bankSetting.Signature}"", headers=""host""";

			var dic = new Dictionary<string, object>()
				{
					{ "otp", otpCode }
				};

			var watch = _utilityService.StartNewWatch();
			var response = await client.VerifyOtpAsync(identity, dic);
			_utilityService.PodAudit(response, $"VerifyOtpAsync " + JsonConvert.SerializeObject(dic), nationalCode, time: _utilityService.TotalTime(watch));
			return response;
		}

		public async Task<bool> CheckLevel4Async(string accessToken, string nationalCode)
		{
			var client = RestClient.For<IUserInfo>(_bankSetting.AccountAddress);
			client.AccessToken = "Bearer " + accessToken;
			try
			{
				var watch = _utilityService.StartNewWatch();
				var response = await client.GetUserInfo2Async();
				var result = JsonConvert.DeserializeObject<UserInfoResponse>(await response.Content.ReadAsStringAsync());
				_utilityService.PodAudit(result, $"CheckLevel4Async ", nationalCode, time: _utilityService.TotalTime(watch));
				if (result.ExchangecodeVerified == true && result.SejamcodeVerified == true)
				{
					return true;
				}
			}
			catch (ApiException)
			{
				throw;
			}
			return false;
		}

		public async Task UpgradeToLevel4Async(string accessToken, string exchangecode, string nationalCode)
		{
			var client = RestClient.For<IUserInfo>(_bankSetting.AccountAddress);
			client.AccessToken = "Bearer " + accessToken;
			try
			{
				var data = new Dictionary<string, object>
				{
					{"exchangecode",  exchangecode},
				};
				var watch = _utilityService.StartNewWatch();
				var response = await client.UpdateUserInfo2Async(data);
				var result = await response.Content.ReadAsStringAsync();
				_utilityService.PodAudit(result, $"CheckLevel4Async " + JsonConvert.SerializeObject(data), nationalCode, time: _utilityService.TotalTime(watch));
			}
			catch (ApiException e)
			{

				throw;
			}
			return;
		}

		public async Task<bool> PhysicalVerifyAsync(string userId, string nationalCode)
		{
			var client = RestClient.For<IPhysicalVerify>(_bankSetting.PlatformAddress);
			//client.AccessToken = "Bearer " + accessToken;
			client.ApiToken = _bankSetting.ApiToken;
			client.TokenIssuer = _bankSetting.TokenIssuer;
			try
			{
				var watch = _utilityService.StartNewWatch();
				var response = await client.GetPhysicalVerifyAsync(userId);
				var a = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<PhysicalVerifyResponse>(a);
				_utilityService.PodAudit(result, $"PhysicalVerifyAsync " , nationalCode, time: _utilityService.TotalTime(watch));
				if (response.IsSuccessStatusCode && result.HasError == false)
				{
					return true;
				}
			}
			catch (ApiException ex)
			{
				throw new ExternalServiceCorruptionException(ExceptionErrorCodes.BankPhysicalVerifyFailed, ex.Message);
			}
			return false;
		}
		
		public async Task<Client> GetProfileAsync(Token token, int tokenIssuer, string nationalCode)
		{
			try
			{
				var client = RestClient.For<IProfile>(_bankSetting.PlatformAddress);
				client.AccessToken = token.AccessToken;
				client.TokenIssuer = tokenIssuer;

				var data = new Dictionary<string, object>()
				{
					{ "client_id", _bankSetting.ClientId },
					{ "client_secret", _bankSetting.ClientSecret },
				};
				var watch = _utilityService.StartNewWatch();
				var response = await client.GetProfileAsync(data);
				var result = JsonConvert.DeserializeObject<ProfileResponse>(await response.Content.ReadAsStringAsync());
				_utilityService.PodAudit(result, $"GetProfileAsync " + JsonConvert.SerializeObject(data), nationalCode, time: _utilityService.TotalTime(watch));
				return result.MapToClient();
			}
			catch (ApiException)
			{
				throw new InvalidDataBusinessException(ExceptionErrorCodes.BankProfileFailed);
			}
		}
		
		public async Task<bool> EditProfileAsync(
			string nationalCode,
			string birthDate,
			string nikname,
			string accessToken,
			int tokenIssuer)
		{

			var client = RestClient.For<IProfile>(_bankSetting.PlatformAddress);
			client.AccessToken = accessToken;
			client.TokenIssuer = tokenIssuer;

			var data = new Dictionary<string, object>()
			{
				{ "nationalCode" , nationalCode},
				{ "birthDate" ,birthDate},
				{ "nickName", nikname },
				{ "client_id", _bankSetting.ClientId},
				{ "client_secret", _bankSetting.ClientSecret },
				//{ "sheba", "650570021780013140374101" },
			};
			var watch = _utilityService.StartNewWatch();
			var response = await client.EditProfileAsync(data);
			var a = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<EditProfileResponse>(await response.Content.ReadAsStringAsync());
			_utilityService.PodAudit(result, $"EditProfileAsync " + JsonConvert.SerializeObject(data), nationalCode, time: _utilityService.TotalTime(watch));
			if (result.HasError)
			{
				throw new BankException(ExceptionErrorCodes.BankEditProfileFailed, result.ErrorCode , result.Message);
			}

			return result.HasError;
		}

		public async Task<Client> EditProfileWithConfirmAsync(string accessToken, int tokenIssuer, string nationalCode)
		{
			var client = RestClient.For<IProfile>(_bankSetting.PlatformAddress);
			client.AccessToken = accessToken;
			client.TokenIssuer = tokenIssuer;

			var data = new Dictionary<string, object>()
			{
				{ "firstName" ,"mahdi" },
				{ "lastName" ,"afrad" },
				{ "nickName", "user-16025980370082" },
				{ "gender","MAN_GENDER" },//MAN_GENDER یا WOMAN_GENDER
                { "client_id", _bankSetting.ClientId },
				{ "client_secret", _bankSetting.ClientSecret },
			};
			var watch = _utilityService.StartNewWatch();
			var response = await client.EditProfileWithConfirmAsync(data);
			var result = JsonConvert.DeserializeObject<ProfileResponse>(await response.Content.ReadAsStringAsync());
			_utilityService.PodAudit(result, $"EditProfileWithConfirmAsync " + JsonConvert.SerializeObject(data), nationalCode, time: _utilityService.TotalTime(watch));
			return result.MapToClient();
		}

		public async Task<Client> ConfirmationOfEditAsync(string accessToken, int tokenIssuer, string code, string cellphoneNumber, string nationalCode)
		{

			var client = RestClient.For<IProfile>(_bankSetting.PlatformAddress);
			client.AccessToken = accessToken;
			client.TokenIssuer = tokenIssuer;

			var data = new Dictionary<string, object>()
			{
				{ "code" ,code },
				{ "cellphoneNumber",cellphoneNumber},
                //{ "client_id", _bankSetting.clientId },
                //{ "client_secret", _bankSetting.clientSecret },
            };
			var watch = _utilityService.StartNewWatch();
			var response = await client.ConfirmationOfEditAsync(data);
			var result = JsonConvert.DeserializeObject<ProfileResponse>(await response.Content.ReadAsStringAsync());
			_utilityService.PodAudit(result, $"ConfirmationOfEditAsync " + JsonConvert.SerializeObject(data), nationalCode, time: _utilityService.TotalTime(watch));
			return result.MapToClient();
		}
	}
}
