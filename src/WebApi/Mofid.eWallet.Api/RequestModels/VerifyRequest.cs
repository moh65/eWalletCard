using Mofid.eWallet.Entities.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Api.RequestModels
{
	public class VerifyRequest
	{
		/// <summary>
		/// شماره دستگاه
		/// </summary>
		public string DeviceId{ get; set; }
		/// <summary>
		/// شماره موبایل
		/// </summary>
		public string PhoneNumber { get; set; }
		/// <summary>
		/// کد رمز یکبار مصرف
		/// </summary>
		public string Otp { get; set; }

		public Client ToClient()
        {
			return new Client()
			{
				PhoneNumber = PhoneNumber
			};
        }

		public Token ToToken()
        {
			return new Token()
			{
				DeviceId = DeviceId
			};
        }
	}
}
