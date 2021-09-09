using Mofid.eWallet.Entities.DTOs;
using Mofid.eWallet.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mofid.eWallet.Entities.BusinessObjects
{	
	public class Client
	{
		public string Id { get; set; }
		public string PhoneNumber { get; set; }
		public string NationalCode { get; set; }
		public string NationalCardSerial { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string BirthDate { get; set; }
		public string NickName { get; set; }
		public int UserId { get; set; }
		public FinancialLevel FinancialLevel { get; set; }
		public List<Token> Tokens { get; set; }
		public string BourseCode { get; set; }
		public List<Address> Addresses { get; set; }
		public string BirthCertificateCity { get; set; }
		public string FatherName { get; set; }
		public string Gender { get; set; }
        public CardStatusEnum CardStatus { get; set; }
        public Card MofidCard { get; set; }
        //public ClientRegistrationStatus RegistrationStatus { get; set; }
        public List<ClientState> ClientStates { get; set; }

        public override bool Equals(object obj)
		{
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			var c = (Client)obj;

			return this.NationalCode == c.NationalCode;
		}

		public Client()
        {
			Tokens ??= new List<Token>();
			FinancialLevel = new FinancialLevel();
			ClientStates ??= new List<ClientState>();
			Addresses ??= new List<Address>();
		}
		public void ChangeDefaultAddress(Address address)
        {
			var addressItem = Addresses.First(x => x.AddressString == address.AddressString && x.Postalcode == address.Postalcode);
            foreach (var item in Addresses)
				item.IsDefault = false;

			addressItem.IsDefault = true;
		}
		public ClientDto MapToDto(string deviceId, string deviceName)
		{
			return new ClientDto()
			{
				FirstName = FirstName,
				LastName = LastName,
				NationalCode = NationalCode,
				PhoneNumber = PhoneNumber,
				DeviceId = deviceId,
				DeviceName = deviceName
			};
		}

		public ClientDto MapToDto()
		{
			return new ClientDto()
			{
				FirstName = FirstName,
				LastName = LastName,
				NationalCode = NationalCode,
				States = ClientStates?.Select(s => (int)s).ToList(),
				Addresses = Addresses,
				BirthDate  = BirthDate,
				BourseCode = BourseCode,
				MofidCard = MofidCard,
				FatherName  = FatherName,
				NationalCardSerial = NationalCardSerial,
				PhoneNumber = PhoneNumber,
			};
		}
	}



}
