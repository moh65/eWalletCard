using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
    //public class HandshakeResponse
    //{
    //    [JsonProperty("keyId")]
    //    public string KeyId { get; set; }
    //    [JsonProperty("expire_in")]
    //    public int ExpiresInDays { get; set; }
    //}






























    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ClientResponse
    {
        [JsonProperty("allowedGrantTypes")]
        public List<string> AllowedGrantTypes { get; set; }

        [JsonProperty("allowedRedirectUris")]
        public List<string> AllowedRedirectUris { get; set; }

        [JsonProperty("allowedScopes")]
        public List<string> AllowedScopes { get; set; }

        [JsonProperty("captchaEnabled")]
        public bool CaptchaEnabled { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("loginUrl")]
        public string LoginUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("roles")]
        public List<string> Roles { get; set; }

        [JsonProperty("signupEnabled")]
        public bool SignupEnabled { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }
    }

    public class Location
    {
    }

    public class Device
    {
        [JsonProperty("current")]
        public bool Current { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("lastAccessTime")]
        public long LastAccessTime { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }
    }

    public class HandshakeResponse
    {
        [JsonProperty("algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("client")]
        public ClientResponse Client { get; set; }

        [JsonProperty("device")]
        public Device Device { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresInSecond { get; set; }

        [JsonProperty("keyFormat")]
        public string KeyFormat { get; set; }

        [JsonProperty("keyId")]
        public string KeyId { get; set; }

        [JsonProperty("publicKey")]
        public string PublicKey { get; set; }
    }


}
