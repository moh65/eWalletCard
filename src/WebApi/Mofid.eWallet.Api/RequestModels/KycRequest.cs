using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.DTOs;
using Mofid.eWallet.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mofid.eWallet.Api.RequestModels
{
    public class KycRequest
    {
        /// <summary>
        /// کد ملی
        /// </summary>
        [Required]
        public string NationalCode { get; set; }
        /// <summary>
        /// شماره سریال کارت ملی
        /// </summary>
        [Required]
        public string NationalCardSerial { get; set; }
        /// <summary>
        /// شماره موبایل
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// شماره دستگاه
        /// </summary>
        [Required]
        public string DeviceId { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        [MaxLength(10, ErrorMessage = "کد پستی 10 رقم است.")]
        public string PostalCode { get; set; }
        /// <summary>
        /// آدرس پستی
        /// </summary>
        public string AddressString { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public string BirthDate { get; set; }

        public Client ToClient()
        {
            var client = new Client();

            client.NationalCode = NationalCode;
            client.NationalCardSerial = NationalCardSerial;
            client.PhoneNumber = PhoneNumber;
            if (!(string.IsNullOrWhiteSpace(AddressString) && string.IsNullOrWhiteSpace(PostalCode)))
            {
                client.Addresses.Add(new Address { AddressString = AddressString, Postalcode = PostalCode, Source = AddressSourceEnum.FromUser });
            }
            client.BirthDate = BirthDate;

            return client;
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
