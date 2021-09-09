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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofid.eWallet.UnitTests.Services
{
	[TestClass]
    public class OtpServiceTest
    {
        [TestMethod]
        public async Task KycServiceMustKycNewClient_async()
        {
            int clientAddingCounter = 0;
            int otpSendCounter = 0;
            int PassStateCounter = 0;
            var moqClientRepo = Substitute.For<IClientRepository>();
            moqClientRepo.WhenForAnyArgs(x => x.AddAsync(Arg.Any<Client>())).Do(x => clientAddingCounter++);

            var moqBankRegistration = Substitute.For<IBankRegistrationExternalServcie>();
            moqBankRegistration.HandshakeAsync(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(new Token() { DeviceId = "1122" });
            moqBankRegistration.WhenForAnyArgs(x => x.OtpAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).Do(x => otpSendCounter++);

            var moqBackoffice = Substitute.For<ICustomerBackOfficeService>();
            moqBackoffice.GetCustomerByNationalCodeAsync(Arg.Any<string>()).ReturnsForAnyArgs(new Client());

            var moqClientStateService = Substitute.For<IClientStateService>();

            var moqCache = Substitute.For<ICache>();
            var moqTokenservice = Substitute.For<ITokenService>();
            var moqBankToken = Substitute.For<IBankTokenExternalService>();
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

            await kycService.KycAsync(new Client { NationalCode = "1234567890" }, new Token { DeviceId = "1122" });

            //Assertions
            Assert.AreEqual(clientAddingCounter, 1);
            Assert.AreEqual(otpSendCounter, 1);
        }


        [TestMethod]
        public async Task KycServiceMustKycClient_async()
        {
            int otpSendCounter = 0;
            int PassStateCounter = 0;
            var moqClientRepo = Substitute.For<IClientRepository>();
            moqClientRepo
                .FindByNationalCodeAsync(Arg.Any<string>())
                .Returns(Task.FromResult(new Client
                {
                    NationalCode = "1234567890",
                    Tokens = new List<Token> { new Token { KeyId = "1", DeviceId = "1122" } }
                }));

            var moqBankRegistration = Substitute.For<IBankRegistrationExternalServcie>();
            moqBankRegistration.HandshakeAsync(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(new Token());
            moqBankRegistration.WhenForAnyArgs(x => x.OtpAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).Do(x => otpSendCounter++);

            var moqBackoffice = Substitute.For<ICustomerBackOfficeService>();
            moqBackoffice.GetCustomerByNationalCodeAsync(Arg.Any<string>()).ReturnsForAnyArgs(new Client());

            var moqCache = Substitute.For<ICache>();
            var moqTokenservice = Substitute.For<ITokenService>();
            var moqBankToken = Substitute.For<IBankTokenExternalService>();

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

            await kycService.KycAsync(new Client { NationalCode = "1234567890" }, new Token { DeviceId = "1122" });

            //Assertions
            Assert.AreEqual(otpSendCounter, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(BusinessException), "BusinessException inappropriately allowed.")]
        public async Task KycServiceMustThrowExceptionIfStoredClientHasNoToken_async()
        {
            int PassStateCounter = 0;
            var moqClientRepo = Substitute.For<IClientRepository>();
            moqClientRepo
                .FindByNationalCodeAsync(Arg.Any<string>())
                .Returns(Task.FromResult(new Client
                {
                    NationalCode = "1234567890",
                    Tokens = new List<Token> { new Token { DeviceId = "1" } }
                }));

            var moqClientStateService = Substitute.For<IClientStateService>();
            var moqBankRegistrationExternalServcie = Substitute.For<IBankRegistrationExternalServcie>();
            var moqBankCustomerRemainBalanceService = Substitute.For<IBankCustomerRemainBalanceService>();

            moqBankRegistrationExternalServcie.HandshakeAsync(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult((Token)null));

            moqClientStateService.WhenForAnyArgs(x => x.PassStateAsync(Arg.Any<string>(), Arg.Any<ClientState>())).Do(x => PassStateCounter++);
            IClientKycService kycService = new ClientKycService(
                null,
                moqBankCustomerRemainBalanceService,
                moqClientRepo,
                null,
                null,
                moqBankRegistrationExternalServcie,
                null,
                null,
                moqClientStateService);
            await kycService.KycAsync(new Client { NationalCode = "1234567890" }, new Token { DeviceId = "1122" });
            //Assert..ThrowsException<BusinessException>
            //	(async () => await kycService.KycAsync(new Client { NationalCode = "1234567890" }, new Token { DeviceId = "1122" }));
        }
        [TestMethod]
        public async Task KycServiceMustHandshakeAgain_async()
        {
            int updateRepositoryCounter = 0;
            int updateTokenCounter = 0;
            int otpSendCounter = 0;
            int PassStateCounter = 0;
            var moqClientRepo = Substitute.For<IClientRepository>();
            moqClientRepo
                .FindByNationalCodeAsync(Arg.Any<string>())
                .Returns(Task.FromResult(new Client
                {
                    NationalCode = "1234567890",
                    Tokens = new List<Token>() { new Token { KeyId = null, DeviceId = "1122" } },
                }));
            moqClientRepo.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => updateRepositoryCounter++);
            moqClientRepo.WhenForAnyArgs(x => x.UpdateClientTokenAsync(Arg.Any<Client>(), Arg.Any<Token>())).Do(x => updateTokenCounter++);

            var moqBankRegistration = Substitute.For<IBankRegistrationExternalServcie>();
            moqBankRegistration.HandshakeAsync(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(new Token { DeviceId = "1122" });
            moqBankRegistration.WhenForAnyArgs(x => x.OtpAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).Do(x => otpSendCounter++);
            var moqClientStateService = Substitute.For<IClientStateService>();
            var moqBankCustomerRemainBalanceService = Substitute.For<IBankCustomerRemainBalanceService>();
            moqClientStateService.WhenForAnyArgs(x => x.PassStateAsync(Arg.Any<string>(), Arg.Any<ClientState>())).Do(x => PassStateCounter++);

            IClientKycService kycService = new ClientKycService(
                null,
                moqBankCustomerRemainBalanceService,
                moqClientRepo,
                null,
                null,
                moqBankRegistration,
                null,
                null,
                moqClientStateService);
            await kycService.KycAsync(new Client { NationalCode = "1234567890" }, new Token { DeviceId = "1122" });
            Assert.AreEqual(updateTokenCounter, 1);
            Assert.AreEqual(otpSendCounter, 1);
        }


    }
}
