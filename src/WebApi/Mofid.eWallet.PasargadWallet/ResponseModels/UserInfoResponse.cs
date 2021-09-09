using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
	public class UserInfoResponse
	{
		[JsonProperty("birthdate")]
		public string Birthdate { get; set; }

		[JsonProperty("email_verified")]
		public bool EmailVerified { get; set; }

		[JsonProperty("exchangecode_verified")]
		public bool ExchangecodeVerified { get; set; }

		[JsonProperty("family_name")]
		public string FamilyName { get; set; }

		[JsonProperty("foreigncode_verified")]
		public bool ForeigncodeVerified { get; set; }

		[JsonProperty("given_name")]
		public string GivenName { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("legalInquireStatus")]
		public List<LegalInquireStatus> LegalInquireStatus { get; set; }

		[JsonProperty("nationalcode")]
		public string Nationalcode { get; set; }

		[JsonProperty("nationalcode_verified")]
		public bool NationalcodeVerified { get; set; }

		[JsonProperty("nationalcode_verifiers")]
		public List<int> NationalcodeVerifiers { get; set; }

		[JsonProperty("phone_number_verified")]
		public bool PhoneNumberVerified { get; set; }

		[JsonProperty("physical_verified")]
		public bool PhysicalVerified { get; set; }

		[JsonProperty("preferred_username")]
		public string PreferredUsername { get; set; }

		[JsonProperty("scope")]
		public string Scope { get; set; }

		[JsonProperty("sejamcode_verified")]
		public bool SejamcodeVerified { get; set; }

		[JsonProperty("sub")]
		public string Sub { get; set; }

		[JsonProperty("updated_at")]
		public long UpdatedAt { get; set; }

		[JsonProperty("user_metadata")]
		public string UserMetadata { get; set; }

		[JsonProperty("zoneinfo")]
		public string Zoneinfo { get; set; }
	}
	public class LegalInquireStatus
	{
		[JsonProperty("inquiryTime")]
		public long InquiryTime { get; set; }

		[JsonProperty("insertTime")]
		public long InsertTime { get; set; }

		[JsonProperty("lastUpdateTime")]
		public long LastUpdateTime { get; set; }

		[JsonProperty("legalAuthority")]
		public string LegalAuthority { get; set; }

		[JsonProperty("nationalcode")]
		public string Nationalcode { get; set; }

		[JsonProperty("phoneNumber")]
		public string PhoneNumber { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("tryCount")]
		public int TryCount { get; set; }

		[JsonProperty("birthdate")]
		public string Birthdate { get; set; }
	}
}
