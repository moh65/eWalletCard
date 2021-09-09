using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
	public class StatusResponse
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
		public StatusRoot Result { get; set; }
	}


	public class CardImageInquiryResult
	{
		[JsonProperty("confidence")]
		public int Confidence { get; set; }

		[JsonProperty("isLive")]
		public string IsLive { get; set; }

		[JsonProperty("liveConfidence")]
		public int LiveConfidence { get; set; }

		[JsonProperty("verifyStatus")]
		public int VerifyStatus { get; set; }
	}

	public class LegalInquireStatu
	{
		[JsonProperty("nationalCode")]
		public string NationalCode { get; set; }

		[JsonProperty("phoneNumber")]
		public string PhoneNumber { get; set; }

		[JsonProperty("birthDate")]
		public string BirthDate { get; set; }

		[JsonProperty("nationalCardSerialNumber")]
		public string NationalCardSerialNumber { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("lastUpdateTime")]
		public long LastUpdateTime { get; set; }

		[JsonProperty("inquiryTime")]
		public long InquiryTime { get; set; }

		[JsonProperty("insertTime")]
		public long InsertTime { get; set; }

		[JsonProperty("result")]
		public string Result { get; set; }

		[JsonProperty("inquiryServiceName")]
		public string InquiryServiceName { get; set; }

		[JsonProperty("cardImageInquiryResult")]
		public CardImageInquiryResult CardImageInquiryResult { get; set; }
	}

	public class StatusRoot
	{
		[JsonProperty("firstName")]
		public string FirstName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("ssoId")]
		public string SsoId { get; set; }

		[JsonProperty("phone_number")]
		public string PhoneNumber { get; set; }

		[JsonProperty("phone_number_verified")]
		public bool PhoneNumberVerified { get; set; }

		[JsonProperty("phone_number_pending")]
		public string PhoneNumberPending { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("email_verified")]
		public bool EmailVerified { get; set; }

		[JsonProperty("email_pending")]
		public string EmailPending { get; set; }

		[JsonProperty("nationalcode")]
		public string Nationalcode { get; set; }

		[JsonProperty("legalInquireStatus")]
		public List<LegalInquireStatu> LegalInquireStatus { get; set; }
	}
}