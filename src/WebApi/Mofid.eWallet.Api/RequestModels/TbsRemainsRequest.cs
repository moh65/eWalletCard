
using System.ComponentModel.DataAnnotations;

namespace Mofid.eWallet.Api.RequestModels
{
    public class TbsRemainsRequest
    {
        /// <summary>
        /// کد ملی
        /// </summary>
        [Required]
        public string NationalCode { get; set; }
    }
}
