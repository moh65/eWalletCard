using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
    public class OtpResponse
    {
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("identity")]
        public string Identity { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("sent_before")]
        public bool SentBefore { get; set; }
    }
}
