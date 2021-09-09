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
	public class BankTransferService : IBankTransferExternalService
	{
		private IUtilityService _utilityService;
		private BankConfiguration _bankSetting;
		public BankTransferService(IUtilityService utility, IOptions<BankConfiguration> bankConfig)
		{
			_utilityService = utility;
			_bankSetting = bankConfig.Value;
		}
		public async Task<bool> TransferAsync(
			string accessToken,
			int tokenIssuer,
			int userId,
			string guildCode,
			decimal amount,
			string description,
			string wallet,
			string uniqueId,
			string currencyCode = "IRR", string nationalCode = "")
		{
			var client = RestClient.For<ITransfer>(_bankSetting.PlatformAddress);
			client.AccessToken = _bankSetting.ApiToken;
			client.TokenIssuer = tokenIssuer;
			try
			{
				var data = new Dictionary<string, object>()
				{
					{ "userId", userId },
					{ "guildCode", guildCode },
					{ "amount", amount },
					{ "description", description},
					{ "currencyCode",currencyCode },
					{ "wallet", wallet},   // کد کیف پول*
					{ "uniqueId", uniqueId},    // ارسال شناسه دلخواه و یکتا به منظور امکان جستجو در لیست ها بر اساس شناسه یکتا 
				};
				var response = await client.TransferAsync(data);
				var result = await response.Content.ReadAsStringAsync();

				var aa = JsonConvert.DeserializeObject<TransferResponse>(result);
				return await Task.FromResult(true);
			}
			catch (ApiException ex)
			{
				throw ex;
			}
		}
	}
}
