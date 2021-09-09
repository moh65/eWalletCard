using System;

namespace Mofid.eWallet.Entities.BusinessObjects
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpire { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpire { get; set; }
        public DateTime TokenAcquire { get; set; }
        public string DeviceId { get; set; }
        public string KeyId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime HandshakeExpireDate { get; set; }

        
        public static string CreateCacheKey(string phoneNumber, string deviceId)
        {
            return $"{phoneNumber}{deviceId}";
        
        }        
    }


  
}
