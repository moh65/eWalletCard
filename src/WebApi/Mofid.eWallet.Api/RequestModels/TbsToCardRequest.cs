namespace Mofid.eWallet.Api.DTOs
{
    public class TbsToCardRequest
    {
        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// مبلغ مورد نظر جهت انقال از tbs به مفید کارت
        /// </summary>
        public decimal Amount { get; set; }
    }
}
