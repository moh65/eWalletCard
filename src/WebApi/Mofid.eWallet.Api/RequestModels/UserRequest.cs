using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Api.DTOs
{
    public class UserRequest
    {
        /// <summary>
        /// نام کاربری
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// رمز عبور
        /// </summary>
        public string Password { get; set; }
    }
}
