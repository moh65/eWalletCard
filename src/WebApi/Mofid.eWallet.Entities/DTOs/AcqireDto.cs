using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Api.Dto
{
	public class AcquireDto
	{
		/// <summary>
		/// شماره تماس
		/// </summary>
		public string PhoneNumber { get; set; }
		/// <summary>
		/// شناسه دستگاه
		/// </summary>
		public string DeviceId { get; set; }
	}
}
