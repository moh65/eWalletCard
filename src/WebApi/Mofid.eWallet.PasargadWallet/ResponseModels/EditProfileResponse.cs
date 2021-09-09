using Mofid.eWallet.Entities.BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
	public class EditProfileResponse
	{
		[JsonProperty("hasError")]
		public bool HasError { get; set; }

		[JsonProperty("messageId")]
		public int MessageId { get; set; }
		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("referenceNumber")]
		public string ReferenceNumber { get; set; }

		[JsonProperty("errorCode")]
		public int ErrorCode { get; set; }

		[JsonProperty("count")]
		public int Count { get; set; }

		[JsonProperty("ott")]
		public string Ott { get; set; }


	}
}