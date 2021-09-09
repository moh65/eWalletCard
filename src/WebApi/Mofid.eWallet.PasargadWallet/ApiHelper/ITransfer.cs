using RestEase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.ApiHelper
{
	public interface ITransfer
	{
		[Header("_token_")]
		string AccessToken { get; set; }

		[Header("_token_issuer_")]
		int TokenIssuer { get; set; }

		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Post("nzh/biz/transferToFollower/")]
		Task<HttpResponseMessage> TransferAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);
	}
}
