using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Caches;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using Mofid.eWallet.Infra.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services.Implementations
{
	public class TokenService : ITokenService
	{
		readonly ICache _clientCache;
		readonly IUtilityService _utilityService;
		readonly IBankTokenExternalService _bankTokenService;
		readonly IClientRepository _clientRepository;

		public TokenService(IUtilityService utilityService, ICache clientCache, IBankTokenExternalService tokenExternalService, IClientRepository clientRepository)
		{
			_utilityService = utilityService;
			_clientCache = clientCache;
			_bankTokenService = tokenExternalService;
			_clientRepository = clientRepository;
		}
		//check chach
		//call refresh token
		public async Task<Token> AcquireTokenAsync(string phoneNumber, string deviceId)
		{
			// for log select nationalCode
			var client = await _clientRepository.FindByPhoneNumberAsync(phoneNumber);
			//TO DO cache 

			var token = await _clientCache.GetOrSetAsync<Token>(Token.CreateCacheKey(phoneNumber, deviceId),
				async () => { return await GetCientTokenByPhonenumber(phoneNumber, deviceId); }
			);
			var now = _utilityService.GetNow();
			if (token == null)
			{
				//throw exception
				return null;
			}
			if (token.AccessTokenExpire > now)
				return token;
			if (token.RefreshTokenExpire > now)
				return await RefreshTokenAsync(token, phoneNumber , client.NationalCode);
			return null;//throw exception
		}

		private async Task<Token> GetCientTokenByPhonenumber(string phonenumber, string deviceId)
		{
			Token token = null;
			var client = await _clientRepository.FindByPhoneNumberAsync(phonenumber);
			if (client != null && client.Tokens != null && client.Tokens.Any(a => a.DeviceId == deviceId))
			{
				token = client.Tokens.First(f => f.DeviceId == deviceId);
			}
			return token;
		}

		private async Task<Token> RefreshTokenAsync(Token token, string phoneNumber , string nationalCode)
		{
			var refreshToken = await _bankTokenService.RefreshTokenAsync(token , nationalCode);
			var client = new Client { PhoneNumber = phoneNumber };
			await _clientRepository.UpdateClientTokenAsync(client, refreshToken);
			_clientCache.Set(Token.CreateCacheKey(phoneNumber, token.DeviceId), token);
			return token;
		}


	}
}
