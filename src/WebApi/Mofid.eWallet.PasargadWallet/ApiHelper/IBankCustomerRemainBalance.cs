using Mofid.eWallet.PasargadWallet.ResponseModels;
using RestEase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.PasargadWallet.ApiHelper
{
    public interface ICustomerRemainBalance
    {
        [Header("Authorization")]
        string AccessToken { get; set; }


        [Get("oauth2/clients/remainBalance/?nationalCode={nationalCode}&deviceId={deviceId}")]
        Task<CustomerRemainBalanceResponse> GetRemain([Path]string nationalCode ,[Path] string deviceId);
    }
}
