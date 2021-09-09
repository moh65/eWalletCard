using Mofid.eWallet.Entities.BusinessObjects;
using System.Collections.Generic;

namespace Mofid.eWallet.Entities.DTOs
{
	public class ClientDto
	{
		/// <summary>
		/// کد ملی
		/// </summary>
		public string NationalCode { get; set; }
		/// <summary>
		/// شماره تماس
		/// </summary>
		public string PhoneNumber { get; set; }
		/// <summary>
		/// شناسه دستگاه
		/// </summary>
		public string DeviceId { get; set; }
		/// <summary>
		/// نام دستگاه
		/// </summary>
		public string DeviceName { get; set; }

		/// <summary>
		/// نام 
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// نام خانوادگی
		/// </summary>
		public string LastName { get; set; }

		public string Otp { get; set; }

		public string NationalCardSerial { get; set; }

		/// <summary>
		/// وضعیت
		/// </summary>
        public List<int> States { get; set; }

		public string BirthDate { get; set; }
		public string BourseCode { get; set; }
		public List<Address> Addresses { get; set; }
		public string FatherName { get; set; }
		public Card MofidCard { get; set; }
		public Client MapToClient()
		{
			return new Client()
			{
				NationalCode = NationalCode,
				FirstName = FirstName,
				LastName = LastName,
				PhoneNumber = PhoneNumber,
				Tokens = new List<Token>() {
					 new Token(){ DeviceId= DeviceId}
				 }
			};
		}
	}
}
