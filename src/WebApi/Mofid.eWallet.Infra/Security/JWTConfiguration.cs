using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Infra.Security
{
    public class JWTConfiguration
    {
        [JsonProperty("secret")]
        public string Secret { get; set; }
    }
}
