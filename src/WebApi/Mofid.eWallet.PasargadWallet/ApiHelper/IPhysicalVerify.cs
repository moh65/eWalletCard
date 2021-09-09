using RestEase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.ApiHelper
{
	public interface IPhysicalVerify
	{
		[Header("_token_")]
		string ApiToken { get; set; }
		[Header("_token_issuer_")]
		int TokenIssuer { get; set; }

		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Get("nzh/biz/verifyCustomer/")]
		Task<HttpResponseMessage> GetPhysicalVerifyAsync(string userId);
	}
}
