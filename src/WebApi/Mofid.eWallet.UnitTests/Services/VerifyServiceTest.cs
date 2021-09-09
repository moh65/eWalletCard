using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Caches;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Tbs;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using Mofid.eWallet.Infra.Exceptions;
using Mofid.eWallet.Services;
using Mofid.eWallet.Services.Contracts;
using Mofid.eWallet.Services.Implementations;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.UnitTests.Services
{

	[TestClass]
    public class VerifyServiceTest
    {
        [TestMethod]
        public async Task verifyMustReturnResponseObject_async()
        {
            int clientAddingCounter = 0;
            int otpSendCounter = 0;
            int leve4CallCounter = 0;
            int PassStateCounter = 0;
            int cliendUpdateCounter = 0;
            int _cliendTokenUpdateCounter = 0;
            var moqClientRepo = Substitute.For<IClientRepository>();
            
            moqClientRepo.WhenForAnyArgs(x => x.AddAsync(Arg.Any<Client>())).Do(x => clientAddingCounter++);
            moqClientRepo.When(x => x.UpdateClientTokenAsync(Arg.Any<Client>(), Arg.Any<Token>())).Do(x => _cliendTokenUpdateCounter++);
            moqClientRepo.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => cliendUpdateCounter++);
            moqClientRepo
                .FindByPhoneNumberAsync(Arg.Any<string>())
                .Returns(Task.FromResult(new Client
                {
                    NationalCode = "1234567890",
                    Tokens = new List<Token> { new Token { KeyId = "1", DeviceId = "1122" } }
                }));

            var moqBankRegistration = Substitute.For<IBankRegistrationExternalServcie>();
            moqBankRegistration.VerifyOtpAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(""));
            moqBankRegistration.HandshakeAsync(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(new Token()));
            moqBankRegistration.WhenForAnyArgs(x => x.OtpAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).Do(x => otpSendCounter++);
            moqBankRegistration.PhysicalVerifyAsync(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(true);
            moqBankRegistration.WhenForAnyArgs(x => x.UpgradeToLevel4Async(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).Do(x => leve4CallCounter++);
            moqBankRegistration.GetProfileAsync(Arg.Any<Token>(), Arg.Any<int>(), Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult(new Client() { FinancialLevel = new FinancialLevel() { Value = 4  } }));
            
            var moqBackoffice = Substitute.For<ICustomerBackOfficeService>();
            moqBackoffice.GetCustomerByNationalCodeAsync(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(new Client()));
          
            var moqCache = Substitute.For<ICache>();
            moqCache.WhenForAnyArgs(x => x.Set(Arg.Any<string>(), Arg.Any<Token>())).Do(x => _cliendTokenUpdateCounter++);
            var moqTokenservice = Substitute.For<ITokenService>();
            var moqBankToken = Substitute.For<IBankTokenExternalService>();
            moqBankToken.AcuireTokenAsync(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(new Token()));
            var moqClientStateService = Substitute.For<IClientStateService>();
            var moqBankCustomerRemainBalanceService = Substitute.For<IBankCustomerRemainBalanceService>();
            moqClientStateService.WhenForAnyArgs(x => x.PassStateAsync(Arg.Any<string>(), Arg.Any<ClientState>())).Do(x => PassStateCounter++);
          
            IClientKycService kycService = new ClientKycService(
                null,
                moqBankCustomerRemainBalanceService,
                moqClientRepo,
                moqCache,
                moqTokenservice,
                moqBankRegistration,
                moqBankToken,
                moqBackoffice,
                moqClientStateService);

            await kycService.Verify(new Client { NationalCode = "1234567890" }, new Token { DeviceId = "1122" }, "123456");

            //Assertions
            Assert.AreEqual(clientAddingCounter, 0);
            Assert.AreEqual(otpSendCounter, 0);
            Assert.AreEqual(leve4CallCounter, 0);
        }

        [TestMethod]
        public async Task ClientAddressServiceGetAddressSuccess_Async()
        {
            var moqCustomerBackOfficeService = Substitute.For<ICustomerBackOfficeService>();
            var client = new Client()
            {
                NationalCode = "4900729671",
                Addresses = new List<Address> { new Address { City = "تهران" } },
            };
            int PassStateCounter = 0;
            moqCustomerBackOfficeService.GetCustomerByNationalCodeAsync(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(client));
            var moqClientStateService = Substitute.For<IClientStateService>();
            var moqBankCustomerRemainBalanceService = Substitute.For<IBankCustomerRemainBalanceService>();
            moqClientStateService.WhenForAnyArgs(x => x.PassStateAsync(Arg.Any<string>(), Arg.Any<ClientState>())).Do(x => PassStateCounter++);

            var addressService = new ClientKycService(null, moqBankCustomerRemainBalanceService ,null, null, null, null, null, moqCustomerBackOfficeService, moqClientStateService);
            var result = await addressService.GetAddress(client);

            Assert.AreEqual(result.Addresses.FirstOrDefault().City, "تهران");
        }


        [TestMethod]
        public async Task ClientAddressServiceGetAddressNotFound_Async()
        {
            var moqCustomerBackOfficeService = Substitute.For<ICustomerBackOfficeService>();
            var client = new Client()
            {
                NationalCode = "4900729671",
                Addresses = new List<Address> { new Address { City = "تهران" } },
            };
            int PassStateCounter = 0;
            moqCustomerBackOfficeService.GetCustomerByNationalCodeAsync(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult((Client)null));
            var moqClientStateService = Substitute.For<IClientStateService>();
            var moqBankCustomerRemainBalanceService = Substitute.For<IBankCustomerRemainBalanceService>();
            moqClientStateService.WhenForAnyArgs(x => x.PassStateAsync(Arg.Any<string>(), Arg.Any<ClientState>())).Do(x => PassStateCounter++);

            var addressService = new ClientKycService(null, moqBankCustomerRemainBalanceService, null, null, null, null, null, moqCustomerBackOfficeService,moqClientStateService);
            var result = await addressService.GetAddress(client);

            Assert.IsNull(result?.Addresses);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task ClientTbsRemainNotFound_Async()
        {
            var moqCustomerBackOfficeService = Substitute.For<ICustomerBackOfficeService>();
            var client = new Client()
            {
                NationalCode = "4900729671",
                Addresses = new List<Address> { new Address { City = "تهران" } },
            };
            var param = new Api.Dto.ClientRemainDto { CurrentRemain = 500 };

            moqCustomerBackOfficeService.GetCustomerRemain(Arg.Any<string>()).Throws(new BusinessException(ExceptionErrorCodes.ClientNotFound));
            var moqClientStateService = Substitute.For<IClientStateService>();

            var addressService = new ClientKycService(null, null, null, null, null, null, null, moqCustomerBackOfficeService, moqClientStateService);
            var result = await addressService.GetTbsRemain("123");

            Assert.IsNull(result);
        }


        [TestMethod]
        public async Task ClientTbsRemainSuccess_Async()
        {
            var moqCustomerBackOfficeService = Substitute.For<ICustomerBackOfficeService>();
            var client = new Client()
            {
                NationalCode = "4900729671",
                Addresses = new List<Address> { new Address { City = "تهران" } },
            };
            var param = new Api.Dto.ClientRemainDto { CurrentRemain = 500 };

            moqCustomerBackOfficeService.GetCustomerRemain(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult(param));
            var moqClientStateService = Substitute.For<IClientStateService>();
            
            var addressService = new ClientKycService(null, null, null, null, null, null, null, moqCustomerBackOfficeService, moqClientStateService);
            var result = await addressService.GetTbsRemain("123");

            Assert.AreEqual(param.CurrentRemain , result.CurrentRemain);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task ClientBankRemainNotFound_Async()
        {
            var moqCustomerBackOfficeService = Substitute.For<IBankCustomerRemainBalanceService>();
            var moqClientRepository = Substitute.For<IClientRepository>();
            var client = new Client()
            {
                NationalCode = "4900729671",
                Addresses = new List<Address> { new Address { City = "تهران" } },
            };
            var param = new Api.Dto.ClientRemainDto { CurrentRemain = 500 };
            moqClientRepository.FindByNationalCodeAsync(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult((Client)null));
            moqCustomerBackOfficeService.GetRemain(Arg.Any<string>(), Arg.Any<string>()).Throws(new BusinessException(ExceptionErrorCodes.ClientNotFound));
            var moqClientStateService = Substitute.For<IClientStateService>();

            var addressService = new ClientKycService(null, moqCustomerBackOfficeService, moqClientRepository, null, null, null, null, null, moqClientStateService);
            var result = await addressService.GetBankRemain("123", "23");

            
            Assert.IsNull(result);
        }


        [TestMethod]
        public async Task ClientBankRemainSuccess_Async()
        {
            var moqCustomerBackOfficeService = Substitute.For<IBankCustomerRemainBalanceService>();
            var moqClientRepository = Substitute.For<IClientRepository>();
            var client = new Client()
            {
                NationalCode = "4900729671",
                Addresses = new List<Address> { new Address { City = "تهران" } },
            };
            var param = new Api.Dto.ClientRemainDto { CurrentRemain = 0 };
            moqClientRepository.FindByNationalCodeAsync(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(new Client()));
            moqCustomerBackOfficeService.GetRemain(Arg.Any<string>() , Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult((decimal)0));
            var moqClientStateService = Substitute.For<IClientStateService>();

            var addressService = new ClientKycService(null, moqCustomerBackOfficeService, moqClientRepository, null, null, null, null, null, moqClientStateService);
            var result = await addressService.GetBankRemain("123" , "23");

            Assert.AreEqual(param.CurrentRemain, result.CurrentRemain);
        }

        [TestMethod]
        public async Task ClientIsLegalSuccess_Async()
        {
           
            var moqClientRepository = Substitute.For<ITemporaryClientRepository>();
            var client = new Client()
            {
                NationalCode = "4900729671",
                Addresses = new List<Address> { new Address { City = "تهران" } },
            };
            
            moqClientRepository.IsInWhiteList(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(true));
            var moqClientStateService = Substitute.For<IClientStateService>();

            var addressService = new ClientKycService(moqClientRepository, null, null, null, null, null, null, null, moqClientStateService);
            var result = await addressService.IsLegal("123" );

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task ClientIsLegalFaild_Async()
        {

            var moqClientRepository = Substitute.For<ITemporaryClientRepository>();
            var client = new Client()
            {
                NationalCode = "4900729671",
                Addresses = new List<Address> { new Address { City = "تهران" } },
            };

            moqClientRepository.IsInWhiteList(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(false));
            var moqClientStateService = Substitute.For<IClientStateService>();

            var addressService = new ClientKycService(moqClientRepository, null, null, null, null, null, null, null, moqClientStateService);
            var result = await addressService.IsLegal("123");

            Assert.AreEqual(false, result);
        }


        [TestMethod]
        public async Task ListAsyncMustReturnClients_Async()
        {

            var moqClientRepository = Substitute.For<IClientRepository>();
            

            moqClientRepository.TakeAsync (Arg.Any<int>() , Arg.Any<int>() , Arg.Any<string>() ,Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(((long)0 , new List<Client>())));
            var moqClientStateService = Substitute.For<IClientStateService>();

            var service = new ClientKycService(null, null, moqClientRepository, null, null, null, null, null, null);
            var result = await service.ListAsync(0 , 20 , string.Empty , string.Empty);

            Assert.AreEqual(0, result.Item1);
            Assert.AreEqual(0, result.Item2.Count);
        }


        [TestMethod]
        public async Task InfoAsyncMustReturnClientsInfo_Async()
        {

            var moqClientRepository = Substitute.For<IClientRepository>();


            moqClientRepository.FindByNationalCodeAsync(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(new Client { NationalCode = "1234567890" }));

            var service = new ClientKycService(null, null, moqClientRepository, null, null, null, null, null, null);
            var result = await service.GetClientAsync(string.Empty);

            Assert.AreEqual("1234567890", result.NationalCode);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundBusinessException))]
        public async Task InfoAsyncMustThrowException_Async()
        {

            var moqClientRepository = Substitute.For<IClientRepository>();


            moqClientRepository.FindByNationalCodeAsync(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult((Client)null));

            var service = new ClientKycService(null, null, moqClientRepository, null, null, null, null, null, null);
            var result = await service.GetClientAsync(string.Empty);

            Assert.AreEqual(null, result);
        }
    }
}
