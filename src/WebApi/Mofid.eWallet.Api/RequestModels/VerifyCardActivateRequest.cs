namespace Mofid.eWallet.Api.RequestModels
{
    public class VerifyCardActivateRequest
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
        /// کد ارسالی به تلفن
        /// </summary>
        public string Otp { get; set; }
    }
}
