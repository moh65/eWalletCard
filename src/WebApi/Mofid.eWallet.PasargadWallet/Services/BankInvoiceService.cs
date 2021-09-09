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
	public class BankInvoiceService : IBankInvoiceExternalService
	{
		private IUtilityService _utilityService;
		private BankConfiguration _bankSetting;
		public BankInvoiceService(IUtilityService utility, IOptions<BankConfiguration> bankConfig)
		{
			_utilityService = utility;
			_bankSetting = bankConfig.Value;
		}
		public async Task<Invoice> SetInvoice(string userId, string ott, double prices, string nationalCode)
		{
			var client = RestClient.For<IInvoice>(_bankSetting.PlatformAddress);
			client.AccessToken = _bankSetting.ApiToken;
			client.TokenIssuer = _bankSetting.TokenIssuer;
			client.Ott = ott;

			var dic = new Dictionary<string, object>()
			{
				{ "userId", userId},
				{ "productId", 0},
				{ "price", prices},
				{ "productDescription", "first transaction"},
				//{ "productId", productsId.ToArray()},
				//{ "price", prices.ToArray()},
				//{ "productDescription", descriptions.ToArray()},
				{ "guildCode", "FINANCIAL_GUILD"},
				{ "preferredTaxRate", "0"},
				{ "addressId", ""},
			};

			try
			{
				var response = await client.InvoiceAsync(dic);
				var result = JsonConvert.DeserializeObject<InvoiceResponse>(await response.Content.ReadAsStringAsync());
				return result.MapToInvoice();
			}
			catch (ApiException ex)
			{
				throw ex;
			}
		}
	}
}
