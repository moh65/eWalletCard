using Microsoft.Extensions.Options;
using Mofid.eWallet.Entities.Configurations;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.PasargadWallet.ApiHelper;
using Mofid.eWallet.PasargadWallet.ResponseModels;
using Newtonsoft.Json;
using RestEase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.Services
{
	public class BankCustomerRemainBalanceService : IBankCustomerRemainBalanceService
	{
		private IUtilityService _utilityService;
		private BankConfiguration _bankSetting;
		public BankCustomerRemainBalanceService(IUtilityService utility, IOptions<BankConfiguration> bankConfig)
		{
			_utilityService = utility;
			_bankSetting = bankConfig.Value;
		}


		public async Task<decimal> GetRemain(string nationalCode ,string deviceId)
		{
			var client = RestClient.For<ICustomerRemainBalance>(_bankSetting.PlatformAddress);
			client.AccessToken = _bankSetting.ApiToken;
			try
			{
				
				var watch = _utilityService.StartNewWatch();
				var response = await client.GetRemain(nationalCode , deviceId);
				_utilityService.PodAudit(response  , $"GetRemain?nationalCode={nationalCode}&deviceId={deviceId}" , nationalCode, time: _utilityService.TotalTime(watch));

				return response.Balance;
			}
			catch (ApiException ex)
			{
				throw ex;
			}
		}

        
    }
}
