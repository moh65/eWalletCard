using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Api.RequestModels
{
    public class CardRegisterRequest
    {
        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// شماره موبایل
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
