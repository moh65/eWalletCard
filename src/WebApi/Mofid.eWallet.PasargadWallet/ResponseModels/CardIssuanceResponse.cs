using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
    public class CardIssuanceResult
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("shahabCode")]
        public string ShahabCode { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("requestDate")]
        public long RequestDate { get; set; }

        [JsonProperty("cardType")]
        public string CardType { get; set; }
    }

    public class CardIssuanceResponse
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
        public List<CardIssuanceResult> Result { get; set; }
    }
}
