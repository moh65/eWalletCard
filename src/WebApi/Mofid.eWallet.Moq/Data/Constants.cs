using Mofid.eWallet.Entities.BusinessObjects;

namespace Mofid.eWallet.Moq {
    public class Constants {
        public static readonly Token Token;
        public static readonly Client Client;
        static Constants(){
            Token = new Token(){
                AccessToken = "11",
                AccessTokenExpire = new System.DateTime(2100, 1, 1),
                CreateDate = System.DateTime.Now,
                DeviceId = "11",
                HandshakeExpireDate = new System.DateTime(2100, 1, 1),
                KeyId = "11",
                RefreshToken = "11",
                RefreshTokenExpire = new System.DateTime(2100, 1, 1),
                TokenAcquire = System.DateTime.Now
            };

            var client = new Client
            {
                NationalCode = "1234567891",
                PhoneNumber = "09123456789",
                BirthCertificateCity = "tehran",
                MofidCard = new Card(){CardNumber="123123123123", ActivateDate=new System.DateTime(2020, 1, 1)},
                FinancialLevel = new FinancialLevel(){LevelName = "level1"},
                Tokens = new System.Collections.Generic.List<Token>(){Token},                
                BirthDate = "2020-01-01",                
                BourseCode = "test bourse",
                CardStatus = Entities.Enum.CardStatusEnum.Registered,
                ClientStates = new System.Collections.Generic.List<Entities.Enum.ClientState>(){Entities.Enum.ClientState.TbsVerified},
                FatherName = "father",
                FirstName = "Mohammad",
                LastName = "Parsa",
                NationalCardSerial = "Serial Number",
                Username = "m.parsa",
                NickName = "mohi",
                Gender = "male",                                
                Addresses = new System.Collections.Generic.List<Address>()
                   {
                       new Address()
                       {
                            AddressString = "تستی",
                            City = "تهران",
                            Postalcode = "11111111",
                       }
                   }
            };

        }

    }
}