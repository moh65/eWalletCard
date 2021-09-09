using Newtonsoft.Json;
using System;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
	[Serializable()]
	[JsonObject()]
	public class UserResponse : ResponseBase
	{
		[JsonProperty("result")]
		public Result Result { get; set; }
	}
	public class Result
	{
		[JsonProperty("version")]
		public int Version { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("nickName")]
		public string NickName { get; set; }

		[JsonProperty("followingCount")]
		public int FollowingCount { get; set; }

		[JsonProperty("joinDate")]
		public long JoinDate { get; set; }

		[JsonProperty("cellphoneNumber_unverified")]
		public string CellphoneNumberUnverified { get; set; }

		[JsonProperty("userId")]
		public int UserId { get; set; }

		[JsonProperty("guest")]
		public bool Guest { get; set; }

		[JsonProperty("chatSendEnable")]
		public bool ChatSendEnable { get; set; }

		[JsonProperty("chatReceiveEnable")]
		public bool ChatReceiveEnable { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("ssoIssuerCode")]
		public int SsoIssuerCode { get; set; }

		[JsonProperty("legalInfo")]
		public LegalInfoResponse LegalInfo { get; set; }
	}
	public class LegalInfoResponse
	{
	}
}
