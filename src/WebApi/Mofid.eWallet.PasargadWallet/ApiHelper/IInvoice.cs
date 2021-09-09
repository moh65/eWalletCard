using RestEase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.ApiHelper
{
	public interface IInvoice
	{
		[Header("_token_")]
		string AccessToken { get; set; }

		[Header("_token_issuer_")]
		int TokenIssuer { get; set; }
		[Header("_ott_")]
		string Ott { get; set; }

		[Header("Content-Type", "application/x-www-form-urlencoded")]
		[Get("nzh/biz/issueInvoice")]
		Task<HttpResponseMessage> InvoiceAsync([Query(QuerySerializationMethod.Default)] Dictionary<string, object> data);
		//09128893955


	}

}
