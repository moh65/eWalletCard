using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
	public class VerifyResponse
	{
		[JsonProperty("code")]
		public string Code { get; set; }
		[JsonProperty("state")]
		public string State { get; set; }
	}
}
