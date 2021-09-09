
using System.ComponentModel.DataAnnotations;

namespace Mofid.eWallet.Api.RequestModels
{
    public class BankRemainsRequest
    {
        /// <summary>
        /// کد ملی
        /// </summary>
        [Required]
        public string NationalCode { get; set; }

        /// <summary>
        /// شماره دستگاه
        /// </summary>
        [Required]
        public string DeviceId { get; set; }
    }
}
