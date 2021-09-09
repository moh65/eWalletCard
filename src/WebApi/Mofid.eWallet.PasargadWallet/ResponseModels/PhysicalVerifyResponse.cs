using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{

    public class PhysicalVerifyResponse
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

        //[JsonProperty("result")]
        //public Result Result { get; set; }
    }
    //public class Result
    //{
    //    [JsonProperty("version")]
    //    public int Version { get; set; }

    //    [JsonProperty("firstName")]
    //    public string FirstName { get; set; }

    //    [JsonProperty("lastName")]
    //    public string LastName { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("nationalCode")]
    //    public string NationalCode { get; set; }

    //    [JsonProperty("nationalCode_verified")]
    //    public string NationalCodeVerified { get; set; }

    //    [JsonProperty("gender")]
    //    public string Gender { get; set; }

    //    [JsonProperty("nickName")]
    //    public string NickName { get; set; }

    //    [JsonProperty("birthDate")]
    //    public long BirthDate { get; set; }

    //    [JsonProperty("followingCount")]
    //    public int FollowingCount { get; set; }

    //    [JsonProperty("joinDate")]
    //    public long JoinDate { get; set; }

    //    [JsonProperty("cellphoneNumber")]
    //    public string CellphoneNumber { get; set; }

    //    [JsonProperty("userId")]
    //    public int UserId { get; set; }

    //    [JsonProperty("sheba")]
    //    public string Sheba { get; set; }

    //    [JsonProperty("guest")]
    //    public bool Guest { get; set; }

    //    [JsonProperty("chatSendEnable")]
    //    public bool ChatSendEnable { get; set; }

    //    [JsonProperty("chatReceiveEnable")]
    //    public bool ChatReceiveEnable { get; set; }

    //    [JsonProperty("username")]
    //    public string Username { get; set; }

    //    [JsonProperty("ssoId")]
    //    public string SsoId { get; set; }

    //    [JsonProperty("ssoIssuerCode")]
    //    public int SsoIssuerCode { get; set; }

    //    [JsonProperty("financialLevelSrv")]
    //    public FinancialLevelSrv FinancialLevelSrv { get; set; }

    //    [JsonProperty("readOnlyFields")]
    //    public string ReadOnlyFields { get; set; }

    //    [JsonProperty("jobs")]
    //    public List<object> Jobs { get; set; }

    //    [JsonProperty("pasargadCustomerNumber")]
    //    public string PasargadCustomerNumber { get; set; }
    //}
}