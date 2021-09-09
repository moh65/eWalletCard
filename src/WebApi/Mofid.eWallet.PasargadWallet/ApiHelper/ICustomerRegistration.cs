using Mofid.eWallet.PasargadWallet.ResponseModels;
using RestEase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.ApiHelper
{
	public interface ICustomerRegistration
	{
		[Header("Authorization")]
		string BearerToken { get; set; }

		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Post("oauth2/clients/handshake/{identity}")]
		Task<HttpResponseMessage> HandshakeAsync([Path] string identity, [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);
		//09128893955


		[Header("Authorization")]
		string AuthorizeHeader { get; set; }

		[Header("host", "accounts.pod.ir")]
		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Post("oauth2/otp/authorize/{identity}")]
		Task<OtpResponse> AuthorizeAsync([Path] string identity, [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);


		[Header("host", "accounts.pod.ir")]
		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Post("oauth2/otp/verify/{identity}")]
		Task<VerifyResponse> VerifyOtpAsync([Path] string identity, [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> dic);


		

	}
}
