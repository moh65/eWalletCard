using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.PasargadWallet.ResponseModels
{
    public class InvoiceResponse
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

        [JsonProperty("result")]
        public Root Result { get; set; }

		internal Entities.BusinessObjects.Invoice MapToInvoice()
		{
            return new Entities.BusinessObjects.Invoice()
            {
                UserId = Result.UserSrv.Id,
            };
		}
	}

    public class BankGateway
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("merchantCode")]
        public string MerchantCode { get; set; }

        [JsonProperty("terminalCode")]
        public string TerminalCode { get; set; }
    }
    public class RateObject
    {
        [JsonProperty("myRate")]
        public int MyRate { get; set; }

        [JsonProperty("rate")]
        public int Rate { get; set; }

        [JsonProperty("rateCount")]
        public int RateCount { get; set; }
    }

    public class Business
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageInfo")]
        public ImageInfo ImageInfo { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("numOfProducts")]
        public int NumOfProducts { get; set; }

        [JsonProperty("rate")]
        public RateObject Rate { get; set; }

        [JsonProperty("sheba")]
        public string Sheba { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("serviceCallName")]
        public string ServiceCallName { get; set; }
    }

    public class UserSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ssoId")]
        public string SsoId { get; set; }

        [JsonProperty("ssoIssuerCode")]
        public int SsoIssuerCode { get; set; }

        [JsonProperty("profileImage")]
        public string ProfileImage { get; set; }
    }

    public class UserPostInfo
    {
        [JsonProperty("postId")]
        public int PostId { get; set; }

        [JsonProperty("liked")]
        public bool Liked { get; set; }

        [JsonProperty("favorite")]
        public bool Favorite { get; set; }
    }

    public class TagTree
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class AttributeValue
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class RepliedItemSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("timelineId")]
        public int TimelineId { get; set; }

        [JsonProperty("entityId")]
        public int EntityId { get; set; }

        [JsonProperty("forwardedId")]
        public int ForwardedId { get; set; }

        [JsonProperty("numOfLikes")]
        public int NumOfLikes { get; set; }

        [JsonProperty("numOfDisLikes")]
        public int NumOfDisLikes { get; set; }

        [JsonProperty("numOfShare")]
        public int NumOfShare { get; set; }

        [JsonProperty("numOfFavorites")]
        public int NumOfFavorites { get; set; }

        [JsonProperty("numOfComments")]
        public int NumOfComments { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty("enable")]
        public bool Enable { get; set; }

        [JsonProperty("hide")]
        public bool Hide { get; set; }

        [JsonProperty("replyPostConfirmation")]
        public bool ReplyPostConfirmation { get; set; }

        [JsonProperty("business")]
        public Business Business { get; set; }

        [JsonProperty("userSrv")]
        public UserSrv UserSrv { get; set; }

        [JsonProperty("rate")]
        public RateObject Rate { get; set; }

        [JsonProperty("userPostInfo")]
        public UserPostInfo UserPostInfo { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("latitude")]
        public int Latitude { get; set; }

        [JsonProperty("longitude")]
        public int Longitude { get; set; }

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("canComment")]
        public bool CanComment { get; set; }

        [JsonProperty("canLike")]
        public bool CanLike { get; set; }

        [JsonProperty("canRate")]
        public bool CanRate { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("tagTrees")]
        public List<TagTree> TagTrees { get; set; }

        [JsonProperty("attributeValues")]
        public List<AttributeValue> AttributeValues { get; set; }

        [JsonProperty("templateCode")]
        public string TemplateCode { get; set; }
    }


    public class PreviewInfo
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

    public class SaleInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("discountPercent")]
        public int DiscountPercent { get; set; }

        [JsonProperty("startDate")]
        public int StartDate { get; set; }

        [JsonProperty("endDate")]
        public int EndDate { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Guild
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("imageInfo")]
        public ImageInfo ImageInfo { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }
    }

    public class SubProduct
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("previewInfo")]
        public PreviewInfo PreviewInfo { get; set; }

        [JsonProperty("availableCount")]
        public int AvailableCount { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("attributeValues")]
        public List<AttributeValue> AttributeValues { get; set; }
    }

    public class ProductGroup
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sharedAttributeCodes")]
        public List<string> SharedAttributeCodes { get; set; }
    }

    public class Currency
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class ProductSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("timelineId")]
        public int TimelineId { get; set; }

        [JsonProperty("entityId")]
        public int EntityId { get; set; }

        [JsonProperty("forwardedId")]
        public int ForwardedId { get; set; }

        [JsonProperty("numOfLikes")]
        public int NumOfLikes { get; set; }

        [JsonProperty("numOfDisLikes")]
        public int NumOfDisLikes { get; set; }

        [JsonProperty("numOfShare")]
        public int NumOfShare { get; set; }

        [JsonProperty("numOfFavorites")]
        public int NumOfFavorites { get; set; }

        [JsonProperty("numOfComments")]
        public int NumOfComments { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty("enable")]
        public bool Enable { get; set; }

        [JsonProperty("hide")]
        public bool Hide { get; set; }

        [JsonProperty("replyPostConfirmation")]
        public bool ReplyPostConfirmation { get; set; }

        [JsonProperty("business")]
        public Business Business { get; set; }

        [JsonProperty("userSrv")]
        public UserSrv UserSrv { get; set; }

        [JsonProperty("rate")]
        public RateObject Rate { get; set; }

        [JsonProperty("userPostInfo")]
        public UserPostInfo UserPostInfo { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("latitude")]
        public int Latitude { get; set; }

        [JsonProperty("longitude")]
        public int Longitude { get; set; }

        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("canComment")]
        public bool CanComment { get; set; }

        [JsonProperty("canLike")]
        public bool CanLike { get; set; }

        [JsonProperty("canRate")]
        public bool CanRate { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("tagTrees")]
        public List<TagTree> TagTrees { get; set; }

        [JsonProperty("repliedItemSrv")]
        public RepliedItemSrv RepliedItemSrv { get; set; }

        [JsonProperty("attributeValues")]
        public List<AttributeValue> AttributeValues { get; set; }

        [JsonProperty("templateCode")]
        public string TemplateCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("categoryList")]
        public List<string> CategoryList { get; set; }

        [JsonProperty("previewInfo")]
        public PreviewInfo PreviewInfo { get; set; }

        [JsonProperty("preview")]
        public string Preview { get; set; }

        [JsonProperty("unlimited")]
        public bool Unlimited { get; set; }

        [JsonProperty("availableCount")]
        public int AvailableCount { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("saleInfo")]
        public SaleInfo SaleInfo { get; set; }

        [JsonProperty("guild")]
        public Guild Guild { get; set; }

        [JsonProperty("allowUserInvoice")]
        public bool AllowUserInvoice { get; set; }

        [JsonProperty("allowUserPrice")]
        public bool AllowUserPrice { get; set; }

        [JsonProperty("subProducts")]
        public List<SubProduct> SubProducts { get; set; }

        [JsonProperty("productGroup")]
        public ProductGroup ProductGroup { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("relatedProductsId")]
        public List<int> RelatedProductsId { get; set; }

        [JsonProperty("preferredTaxRate")]
        public int PreferredTaxRate { get; set; }
    }

    public class ProductShortSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("attributeValues")]
        public List<AttributeValue> AttributeValues { get; set; }

        [JsonProperty("templateCode")]
        public string TemplateCode { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("business")]
        public Business Business { get; set; }
    }

    public class VoucherUsageSrv
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("consumDate")]
        public int ConsumDate { get; set; }

        [JsonProperty("usedAmount")]
        public int UsedAmount { get; set; }

        [JsonProperty("canceled")]
        public bool Canceled { get; set; }
    }

    public class InvoiceItemSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("productSrv")]
        public ProductSrv ProductSrv { get; set; }

        [JsonProperty("productShortSrv")]
        public ProductShortSrv ProductShortSrv { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("voucherUsageSrvs")]
        public List<VoucherUsageSrv> VoucherUsageSrvs { get; set; }
    }

    public class GuildSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("imageInfo")]
        public ImageInfo ImageInfo { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }
    }

    public class AddressSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("faxNumber")]
        public string FaxNumber { get; set; }

        [JsonProperty("postalcode")]
        public string Postalcode { get; set; }

        [JsonProperty("latitude")]
        public int Latitude { get; set; }

        [JsonProperty("longitude")]
        public int Longitude { get; set; }

        [JsonProperty("simpleAddress")]
        public string SimpleAddress { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("mainStreet")]
        public string MainStreet { get; set; }

        [JsonProperty("alley")]
        public string Alley { get; set; }

        [JsonProperty("floor")]
        public string Floor { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("plaque")]
        public string Plaque { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }
    }

    public class IssuerSrv
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ssoId")]
        public string SsoId { get; set; }

        [JsonProperty("ssoIssuerCode")]
        public int SsoIssuerCode { get; set; }

        [JsonProperty("profileImage")]
        public string ProfileImage { get; set; }
    }

    public class Payer
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ssoId")]
        public string SsoId { get; set; }

        [JsonProperty("ssoIssuerCode")]
        public int SsoIssuerCode { get; set; }

        [JsonProperty("profileImage")]
        public string ProfileImage { get; set; }
    }

    public class Root
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("totalAmountWithoutDiscount")]
        public int TotalAmountWithoutDiscount { get; set; }

        [JsonProperty("delegationAmount")]
        public int DelegationAmount { get; set; }

        [JsonProperty("totalAmount")]
        public int TotalAmount { get; set; }

        [JsonProperty("payableAmount")]
        public int PayableAmount { get; set; }

        [JsonProperty("vat")]
        public int Vat { get; set; }

        [JsonProperty("issuanceDate")]
        public int IssuanceDate { get; set; }

        [JsonProperty("deliveryDate")]
        public int DeliveryDate { get; set; }

        [JsonProperty("billNumber")]
        public string BillNumber { get; set; }

        [JsonProperty("issuancePersianDate")]
        public string IssuancePersianDate { get; set; }

        [JsonProperty("paymentBillNumber")]
        public string PaymentBillNumber { get; set; }

        [JsonProperty("bankGateway")]
        public BankGateway BankGateway { get; set; }

        [JsonProperty("transactionReferenceId")]
        public string TransactionReferenceId { get; set; }

        [JsonProperty("uniqueNumber")]
        public string UniqueNumber { get; set; }

        [JsonProperty("trackerId")]
        public int TrackerId { get; set; }

        [JsonProperty("terminalNumber")]
        public int TerminalNumber { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("paymentDate")]
        public int PaymentDate { get; set; }

        [JsonProperty("payed")]
        public bool Payed { get; set; }

        [JsonProperty("serial")]
        public int Serial { get; set; }

        [JsonProperty("canceled")]
        public bool Canceled { get; set; }

        [JsonProperty("cancelDate")]
        public int CancelDate { get; set; }

        [JsonProperty("business")]
        public Business Business { get; set; }

        [JsonProperty("invoiceItemSrvs")]
        public List<InvoiceItemSrv> InvoiceItemSrvs { get; set; }

        [JsonProperty("guildSrv")]
        public GuildSrv GuildSrv { get; set; }

        [JsonProperty("addressSrv")]
        public AddressSrv AddressSrv { get; set; }

        [JsonProperty("userSrv")]
        public UserSrv UserSrv { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("cellphoneNumber")]
        public string CellphoneNumber { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("verificationNeeded")]
        public bool VerificationNeeded { get; set; }

        [JsonProperty("verificationDate")]
        public int VerificationDate { get; set; }

        [JsonProperty("editedInvoiceId")]
        public int EditedInvoiceId { get; set; }

        [JsonProperty("wallet")]
        public string Wallet { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("subInvoices")]
        public List<object> SubInvoices { get; set; }

        [JsonProperty("safe")]
        public bool Safe { get; set; }

        [JsonProperty("postVoucherEnabled")]
        public bool PostVoucherEnabled { get; set; }

        [JsonProperty("referenceNumber")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("lastFourDigitOfCardNumber")]
        public string LastFourDigitOfCardNumber { get; set; }

        [JsonProperty("maskedCardNumber")]
        public string MaskedCardNumber { get; set; }

        [JsonProperty("issuerSrv")]
        public IssuerSrv IssuerSrv { get; set; }

        [JsonProperty("willBePaidAt")]
        public int WillBePaidAt { get; set; }

        [JsonProperty("willBeBlocked")]
        public bool WillBeBlocked { get; set; }

        [JsonProperty("willBlockedFor")]
        public int WillBlockedFor { get; set; }

        [JsonProperty("willBePaid")]
        public bool WillBePaid { get; set; }

        [JsonProperty("unsafeCloseTimeOut")]
        public int UnsafeCloseTimeOut { get; set; }

        [JsonProperty("paymentToolCode")]
        public string PaymentToolCode { get; set; }

        [JsonProperty("billSettled")]
        public bool BillSettled { get; set; }

        [JsonProperty("withdrawable")]
        public bool Withdrawable { get; set; }

        [JsonProperty("rrn")]
        public string Rrn { get; set; }

        [JsonProperty("shaparakTransactionDate")]
        public int ShaparakTransactionDate { get; set; }

        [JsonProperty("payer")]
        public Payer Payer { get; set; }

        [JsonProperty("depositId")]
        public string DepositId { get; set; }

        [JsonProperty("depositBillId")]
        public string DepositBillId { get; set; }

        [JsonProperty("edited")]
        public bool Edited { get; set; }

        [JsonProperty("waiting")]
        public bool Waiting { get; set; }

        [JsonProperty("subInvoice")]
        public bool SubInvoice { get; set; }
    }
}
