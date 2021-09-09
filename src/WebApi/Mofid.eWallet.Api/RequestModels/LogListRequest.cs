namespace Mofid.eWallet.Api.RequestModels
{
    public class LogListRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public int Skip { get; set; }
        /// <summary>
        /// تعداد رکورد های بازگشتی
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalCode { get; set; }
    }
}
