namespace Mofid.eWallet.Api.RequestModels
{
    public class CardActivateRequest
    {
        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// شماره موبایل
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// شماره کارت
        /// </summary>
        public string Pan { get; set; }
    }
}
