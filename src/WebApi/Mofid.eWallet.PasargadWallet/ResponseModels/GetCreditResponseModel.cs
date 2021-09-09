using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
	[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
	public class GetCreditResponseModel
	{
		public bool HasError { get; set; }
		public int ErrorCode{ get; set; }
		public string ReferenceNumber { get; set; }
		public int Count{ get; set; }
		public string Ott{ get; set; }
	}
}
