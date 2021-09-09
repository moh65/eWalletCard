using Mofid.eWallet.PasargadWallet.ResponseModels;
using RestEase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.ApiHelper
{
	public interface IToken
	{
		[Header("Authorization")]
		string BearerToken { get; set; }

		[Header("host", "accounts.pod.ir")]
		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Post("oauth2/token")]
		Task<TokenResponse> GetTokenAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> dic);
	}
}
