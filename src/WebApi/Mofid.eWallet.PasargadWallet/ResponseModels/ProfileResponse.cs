using Mofid.eWallet.Entities.BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
	public class ProfileResponse
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
		public ProfileResult Result { get; set; }

		public Client MapToClient()
			=> new Client()
			{
				BirthDate = Result.BirthDate,
				//CellphoneNumber = Result.CellphoneNumber,
				//CellphoneNumberUnverified = Result.CellphoneNumberUnverified,
				//Email = Result.Email,
				FinancialLevel = new FinancialLevel()
				{
					Id = Result.FinancialLevel.Id,
					Level = Result.FinancialLevel.Level,
					LevelName = Result.FinancialLevel.LevelName,
					Value = Result.FinancialLevel.Value,
				},
				FirstName = Result.FirstName,
				LastName = Result.LastName,
				//Name = Result.Name,
				NationalCode = Result.NationalCode,
				NickName = Result.NickName,
				PhoneNumber = Result.PhoneNumber,
				//Sheba = Result.Sheba,
				UserId = Result.UserId,
				Username = Result.Username,
				//Version = Result.Version,
				//Gender = Result.Gender,
				//SsoIssuerCode = Result.SsoIssuerCode,
				//SsoId = Result.SsoId,
				//AddressCity = Result.AddressSrv.City,
				//Address = Result.AddressSrv.AddressString,
				//PostalCode = Result.AddressSrv.Postalcode,
				Gender = Result.Gender ,
				
			};
	}

	public class ProfileResult
	{
		[JsonProperty("version")]
		public int Version { get; set; }

		[JsonProperty("firstName")]
		public string FirstName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("email_unverified")]
		public string EmailUnverified { get; set; }

		[JsonProperty("phoneNumber")]
		public string PhoneNumber { get; set; }

		[JsonProperty("nationalCode")]
		public string NationalCode { get; set; }

		[JsonProperty("nationalCode_verified")]
		public string NationalCodeVerified { get; set; }

		[JsonProperty("gender")]
		public string Gender { get; set; }

		[JsonProperty("addressSrv")]
		public Address AddressSrv { get; set; }

		[JsonProperty("nickName")]
		public string NickName { get; set; }

		[JsonProperty("birthDate")]
		public string BirthDate { get; set; }

		[JsonProperty("score")]
		public int Score { get; set; }

		[JsonProperty("followingCount")]
		public int FollowingCount { get; set; }

		[JsonProperty("imageInfo")]
		public ImageInfo ImageInfo { get; set; }

		[JsonProperty("profileImage")]
		public string ProfileImage { get; set; }

		[JsonProperty("joinDate")]
		public int JoinDate { get; set; }

		[JsonProperty("cellphoneNumber")]
		public string CellphoneNumber { get; set; }

		[JsonProperty("cellphoneNumber_unverified")]
		public string CellphoneNumberUnverified { get; set; }

		[JsonProperty("userId")]
		public int UserId { get; set; }

		[JsonProperty("sheba")]
		public string Sheba { get; set; }

		[JsonProperty("guest")]
		public bool Guest { get; set; }

		[JsonProperty("chatSendEnable")]
		public bool ChatSendEnable { get; set; }

		[JsonProperty("chatReceiveEnable")]
		public bool ChatReceiveEnable { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("ssoId")]
		public string SsoId { get; set; }

		[JsonProperty("ssoIssuerCode")]
		public int SsoIssuerCode { get; set; }

		[JsonProperty("client_metadata")]
		public string ClientMetadata { get; set; }

		[JsonProperty("legalInfo")]
		public LegalInfo LegalInfo { get; set; }

		[JsonProperty("financialLevelSrv")]
		public FinancialLevelSrv FinancialLevel { get; set; }

		[JsonProperty("readOnlyFields")]
		public string ReadOnlyFields { get; set; }

		[JsonProperty("pasargadCustomerNumber")]
		public string PasargadCustomerNumber { get; set; }

		[JsonProperty("follower")]
		public bool Follower { get; set; }
	}
	public class LegalInfo
	{
	}
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
		public bool Default { get; set; }
	}
	public class ImageInfo
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("hashCode")]
		public string HashCode { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("actualWidth")]
		public int ActualWidth { get; set; }

		[JsonProperty("actualHeight")]
		public int ActualHeight { get; set; }

		[JsonProperty("width")]
		public int Width { get; set; }

		[JsonProperty("height")]
		public int Height { get; set; }
	}
	public class FinancialLevelSrv
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("level")]
		public string Level { get; set; }

		[JsonProperty("levelName")]
		public string LevelName { get; set; }

		[JsonProperty("value")]
		public int Value { get; set; }
	}
}