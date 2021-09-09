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
using System.Linq;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.Services
{
	public class BankOttService : IBankOttExternalService
	{
		private IUtilityService _utilityService;
		private BankConfiguration _bankSetting;
		public BankOttService(IUtilityService utility, IOptions<BankConfiguration> bankConfig)
		{
			_utilityService = utility;
			_bankSetting = bankConfig.Value;
		}
		public async Task<string> GetOttAsync(string accessToken, int tokenIssuer , string nationalCode)
		{
			var client = RestClient.For<IOtt>(_bankSetting.PlatformAddress);
			client.AccessToken = accessToken;
			client.TokenIssuer = tokenIssuer;
			try
			{
				var response = await client.GetOtt();
				var result = JsonConvert.DeserializeObject<OttResponse>(await response.Content.ReadAsStringAsync());
				return result.Ott;
			}
			catch (ApiException ex)
			{
				throw ex;
			}
		}
	}
}
