using Mofid.eWallet.PasargadWallet.ResponseModels;
using RestEase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.ApiHelper
{
	public interface IStatus
	{
		[Header("_token_")]
		string AccessToken { get; set; }

		[Header("_token_issuer_")]
		int TokenIssuer { get; set; }

		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Get("nzh/getUserInquiryStatus/")]
		Task<StatusResponse> GetStatusAsync([Query(QuerySerializationMethod.Default)] Dictionary<string, object> data);


		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Get("nzh/getUserInquiryStatus/")]
		Task<HttpResponseMessage> GetStatusHttpAsync([Query(QuerySerializationMethod.Default)] Dictionary<string, object> data);
	}
}
