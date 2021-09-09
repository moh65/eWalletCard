using RestEase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.ApiHelper
{
	public interface IUserInfo
	{
		[Header("Authorization")]
		string AccessToken { get; set; }
	
		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Get("users")]
		Task<HttpResponseMessage> GetUserInfo2Async();

		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Post("users")]
		Task<HttpResponseMessage> UpdateUserInfo2Async([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string,object> data);
	}
}
