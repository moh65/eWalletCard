using Microsoft.Extensions.Options;
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
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.Services
{
	class BankCardService : IBankCardService
    {
        private readonly BankConfiguration bankSetting;
        private readonly IUtilityService utilityService;

        public BankCardService(IOptions<BankConfiguration> bankSetting, IUtilityService utilityService)
        {
            this.utilityService = utilityService;
            this.bankSetting = bankSetting.Value;
        }
        public async Task CardIssuanceAsync(string accessToken, string nationalCode)
        {
            var client = RestClient.For<ICardIssuance>(bankSetting.PlatformAddress);
            client.AccessToken = accessToken;
            //client.ApiToken = _bankSetting.apiToken;
            client.TokenIssuer = bankSetting.TokenIssuer;
            try
            {
                var data = new Dictionary<string, object>()
                {
                    { "walletCode", "PODLAND_WALLET" },
                    { "currencyCode", "IRR" },
                };

                var watch = utilityService.StartNewWatch();
                var response = await client.CardIssuanceAsync(data);
                var result = await response.Content.ReadAsStringAsync();
                var cardResponse = JsonConvert.DeserializeObject<CardIssuanceResponse>(result);

                utilityService.PodAudit(cardResponse, "CardIssuanceAsync " + JsonConvert.SerializeObject(data), nationalCode, time: utilityService.TotalTime(watch));

                //TODO: Log
            }
            catch (ApiException ex)
            {
                //TODO: Log

                throw;
            }
        }

        public async Task CardActivateAsync(string token, int userid, string pan, string nationalCode)
        {
            var client = RestClient.For<ICardActivate>(bankSetting.PlatformAddress);
            client.AccessToken = token;
            client.TokenIssuer = bankSetting.TokenIssuer;

            var data = new Dictionary<string, object>()
                {
                    { "pan", pan },
                    { "userid", userid },
                };

            var watch = utilityService.StartNewWatch();
            var response = await client.ActivateCard(data);
            var result = await response.Content.ReadAsStringAsync();
            var cardResponse = JsonConvert.DeserializeObject<ActiveCardResponse<ActiveCardResult>>(result);
            utilityService.PodAudit(cardResponse, "CardActivateAsync " + JsonConvert.SerializeObject(data), nationalCode, time: utilityService.TotalTime(watch));

            if (cardResponse.HasError)
                throw new BankException(cardResponse.ErrorCode);
            //TODO: Log


        }



        public async Task VerifyCardActivateAsync(string accessToken, string phoneNumber, string otp, string nationalCode)
        {
            var client = RestClient.For<ICardActivate>(bankSetting.PlatformAddress);
            client.AccessToken = accessToken;
            client.TokenIssuer = bankSetting.TokenIssuer;


            var data = new Dictionary<string, object>()
                {
                    { "cellphoneNumber", phoneNumber },
                    { "code", otp },
                };

            var watch = utilityService.StartNewWatch();
            var response = await client.VerifyActivateCard(data);
            var result = await response.Content.ReadAsStringAsync();
            var cardResponse = JsonConvert.DeserializeObject<ActiveCardResponse<bool>>(result);
            utilityService.PodAudit(cardResponse, "VerifyCardActivateAsync " + JsonConvert.SerializeObject(data), nationalCode, time: utilityService.TotalTime(watch));
            if (cardResponse.Result != true && cardResponse.HasError)
                throw new BankException(cardResponse.ErrorCode);
            //TODO: Log

        }
    }
}
