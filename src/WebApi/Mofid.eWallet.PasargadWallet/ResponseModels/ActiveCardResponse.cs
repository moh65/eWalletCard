
using Newtonsoft.Json;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{

    public class ActiveCardResponse<T>
    {
        [JsonProperty("hasError")]
        public bool HasError { get; set; }

        [JsonProperty("messageId")]
        public int MessageId { get; set; }

        [JsonProperty("referenceNumber")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("ott")]
        public string Ott { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }
    }

    public class ActiveCardResult
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("pan")]
        public string Pan { get; set; }

        [JsonProperty("sheba")]
        public string Sheba { get; set; }
    }
}
