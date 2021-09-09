using Mofid.eWallet.Entities.BusinessObjects;
using System;

namespace Mofid.eWallet.Api.RequestModels
{
    public class AddressClientRequest
    {
        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalCode { get; set; }

        public Client MapToClient()
        {
            return new Client
            {
                NationalCode = NationalCode
            };
        }
    }
}
