using Newtonsoft.Json;


namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
    public class CustomerRemainBalanceResponse
    {
        [JsonProperty("Balance")]
        public decimal Balance { get; set; }
    }
}
