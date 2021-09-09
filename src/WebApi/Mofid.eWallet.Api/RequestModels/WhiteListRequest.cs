
using System.ComponentModel.DataAnnotations;

namespace Mofid.eWallet.Api.RequestModels
{
    public class WhiteListRequest
    {
        /// <summary>
        /// کد ملی
        /// </summary>
        [Required]
        public string NationalCode { get; set; }
    }
}
