using Mofid.eWallet.Api.Dto;
using Mofid.eWallet.BO.Tbs.TBSServices;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Tbs;
using Mofid.eWallet.Infra.Exceptions;
using Mofid.eWallet.Infra.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Mofid.eWallet.BO.Tbs
{
	[ExcludeFromCodeCoverage]
    class CustomerBackOfficeService : ICustomerBackOfficeService
    {
        private CustomerClubExternalService _tbsService;
        private readonly IUtilityService utilityService;

        public CustomerBackOfficeService(CustomerClubExternalService tbsService, IUtilityService utilityService)
        {
            _tbsService = tbsService;
            this.utilityService = utilityService;
        }

        public async Task<Client> GetCustomerByNationalCodeAsync(string nationalCode)
        {
            var customerClub = await _tbsService.Instance.GetCustomerByNationalCodeAsync(nationalCode);
            if (customerClub is null)
                throw new NotFoundBusinessException(ExceptionErrorCodes.TbsClientNotFound);
            var client = new Client();

            client.NationalCode = customerClub.NationalCode;
            client.FirstName = customerClub.FirstName;
            client.LastName = customerClub.LastName;
            client.FatherName = customerClub.FatherName;
            client.PhoneNumber = customerClub.Mobile;
            client.BourseCode = customerClub.BourseCode;
            client.Addresses.Add(new Address
            {
                AddressString = customerClub.Address,
                Postalcode = customerClub.PostalCode,
                City = customerClub.AddressCity,
                Source = Entities.Enum.AddressSourceEnum.FromTbs
            });
            client.BirthDate = utilityService.ConvertToPersian(customerClub.BirthDate);
            client.BirthCertificateCity = customerClub.BirthCertificateCity;
            client.Gender = customerClub.Sex ? "female" : "male";

            return client;
        }

        public async Task<ClientRemainDto> GetCustomerRemain(string nationalCode)
        {
            var customerRemain = await _tbsService.Instance.GetCustomerRemainAsync(nationalCode, BackofficeService1.MarketEnumDto.TSE);
            if (customerRemain is null)
                throw new NotFoundBusinessException(ExceptionErrorCodes.TbsClientNotFound);

            return new ClientRemainDto
            {
                AdjustedRemain = customerRemain.AdjustedRemain,
                BlockedRemain = customerRemain.BlockedRemain,
                CreditDate = customerRemain.CreditDate,
                Credit = customerRemain.Credit,
                CurrentRemain = customerRemain.CurrentRemain,
            };
        }

        public async Task<bool> IncreaseTbsBalanceAsync(decimal amount, string nationalCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DecreaseTbsBalanceAsync(decimal amount, string nationalCode)
        {
            throw new NotImplementedException();
        }

        //public async Task<Domain.Dto.RemainDto> GetRemainFromBOAsync(string customerCode)
        //{
        //	var remain2 = await _tbsService.Instance.GetCustomerRemainAsync(customerCode, MarketEnumDto.TSE);
        //	return new Domain.Dto.RemainDto()
        //	{
        //		Balance = (long)remain2.CurrentRemain
        //	};
        //}
    }
}
