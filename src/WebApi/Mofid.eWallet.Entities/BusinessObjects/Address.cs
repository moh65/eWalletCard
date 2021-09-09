using Mofid.eWallet.Entities.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Entities.BusinessObjects
{
    public class Address
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("address")]
        public string AddressString { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("faxNumber")]
        public string FaxNumber { get; set; }

        [JsonProperty("postalcode")]
        public string Postalcode { get; set; }

        [JsonProperty("latitude")]
        public int Latitude { get; set; }

        [JsonProperty("longitude")]
        public int Longitude { get; set; }

        [JsonProperty("simpleAddress")]
        public string SimpleAddress { get; set; }

        [JsonProperty("default")]
        public bool IsDefault { get; set; }
        public AddressSourceEnum Source { get; set; }
    }
}
