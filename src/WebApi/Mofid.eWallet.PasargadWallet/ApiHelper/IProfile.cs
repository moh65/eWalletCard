using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.PasargadWallet.ResponseModels;
using RestEase;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.ApiHelper
{
	public interface IProfile
	{
		[Header("_token_")]
		string AccessToken { get; set; }

		[Header("_token_issuer_")]
		int TokenIssuer { get; set; }

		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Get("nzh/getUserProfile/")]
		Task<HttpResponseMessage> GetProfileAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);
		//09128893955
		[Post("nzh/editProfile/")]
		Task<HttpResponseMessage> EditProfileAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

		[Post("nzh/editProfileWithConfirmation/")]
		Task<HttpResponseMessage> EditProfileWithConfirmAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

		[Post("nzh/editProfileWithConfirmation/")]
		Task<HttpResponseMessage> ConfirmationOfEditAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);
	}
}
