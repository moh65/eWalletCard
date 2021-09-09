using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
    public class TransferResponse
    {
        [JsonProperty("hasError")]
        public bool HasError { get; set; }

        [JsonProperty("messageId")]
        public int MessageId { get; set; }

        [JsonProperty("referenceNumber")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("ott")]
        public string Ott { get; set; }

        [JsonProperty("result")]
        public TransferResult Result { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class CurrencySrv
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class CustomerAmountSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currencySrv")]
        public CurrencySrv CurrencySrv { get; set; }

        [JsonProperty("isAutoSettle")]
        public bool IsAutoSettle { get; set; }

        [JsonProperty("wallet")]
        public string Wallet { get; set; }

        [JsonProperty("walletName")]
        public string WalletName { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("block")]
        public bool Block { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("sheba")]
        public string Sheba { get; set; }

        [JsonProperty("hasWillBeBlock")]
        public bool HasWillBeBlock { get; set; }

        [JsonProperty("hasWithdrawBlock")]
        public bool HasWithdrawBlock { get; set; }
    }

    //public class ImageInfo
    //{
    //    [JsonProperty("id")]
    //    public int Id { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("hashCode")]
    //    public string HashCode { get; set; }

    //    [JsonProperty("description")]
    //    public string Description { get; set; }

    //    [JsonProperty("actualWidth")]
    //    public int ActualWidth { get; set; }

    //    [JsonProperty("actualHeight")]
    //    public int ActualHeight { get; set; }

    //    [JsonProperty("width")]
    //    public int Width { get; set; }

    //    [JsonProperty("height")]
    //    public int Height { get; set; }
    //}

    //public class GuildSrv
    //{
    //    [JsonProperty("id")]
    //    public int Id { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("code")]
    //    public string Code { get; set; }

    //    [JsonProperty("imageInfo")]
    //    public ImageInfo ImageInfo { get; set; }

    //    [JsonProperty("selected")]
    //    public bool Selected { get; set; }
    //}

    public class CurrencySrv2
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class MainBusinessAmountSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("withdrawableAmount")]
        public int WithdrawableAmount { get; set; }

        [JsonProperty("notWithdrawableAmount")]
        public int NotWithdrawableAmount { get; set; }

        [JsonProperty("guildSrv")]
        public GuildSrv GuildSrv { get; set; }

        [JsonProperty("currencySrv")]
        public CurrencySrv2 CurrencySrv { get; set; }

        [JsonProperty("isAutoSettle")]
        public bool IsAutoSettle { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("sheba")]
        public string Sheba { get; set; }

        [JsonProperty("aliasName")]
        public string AliasName { get; set; }
    }

    public class ImageInfo2
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("hashCode")]
        public string HashCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("actualWidth")]
        public int ActualWidth { get; set; }

        [JsonProperty("actualHeight")]
        public int ActualHeight { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class GuildSrv2
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("imageInfo")]
        public ImageInfo2 ImageInfo { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }
    }

    public class CurrencySrv3
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class BlockedBusinessAmountSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("withdrawableAmount")]
        public int WithdrawableAmount { get; set; }

        [JsonProperty("notWithdrawableAmount")]
        public int NotWithdrawableAmount { get; set; }

        [JsonProperty("guildSrv")]
        public GuildSrv2 GuildSrv { get; set; }

        [JsonProperty("currencySrv")]
        public CurrencySrv3 CurrencySrv { get; set; }

        [JsonProperty("isAutoSettle")]
        public bool IsAutoSettle { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("sheba")]
        public string Sheba { get; set; }

        [JsonProperty("aliasName")]
        public string AliasName { get; set; }
    }

    public class CurrencySrv4
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class BlockAmountSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currencySrv")]
        public CurrencySrv4 CurrencySrv { get; set; }

        [JsonProperty("isAutoSettle")]
        public bool IsAutoSettle { get; set; }

        [JsonProperty("wallet")]
        public string Wallet { get; set; }

        [JsonProperty("walletName")]
        public string WalletName { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("block")]
        public bool Block { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("sheba")]
        public string Sheba { get; set; }

        [JsonProperty("hasWillBeBlock")]
        public bool HasWillBeBlock { get; set; }

        [JsonProperty("hasWithdrawBlock")]
        public bool HasWithdrawBlock { get; set; }
    }

    public class TransferResult
    {
        [JsonProperty("customerAmountSrvs")]
        public List<CustomerAmountSrv> CustomerAmountSrvs { get; set; }

        [JsonProperty("mainBusinessAmountSrvs")]
        public List<MainBusinessAmountSrv> MainBusinessAmountSrvs { get; set; }

        [JsonProperty("blockedBusinessAmountSrvs")]
        public List<BlockedBusinessAmountSrv> BlockedBusinessAmountSrvs { get; set; }

        [JsonProperty("blockAmountSrvs")]
        public List<BlockAmountSrv> BlockAmountSrvs { get; set; }
    }
}
