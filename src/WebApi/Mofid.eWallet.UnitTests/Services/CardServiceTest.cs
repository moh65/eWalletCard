using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Entities.Enum;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using Mofid.eWallet.Infra.Exceptions;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.Services.Contracts;
using Mofid.eWallet.Services.Implementations;
using Moq;
using NSubstitute;
using System;
using System.Threading.Tasks;

namespace Mofid.eWallet.UnitTests.Services
{
	[TestClass]
    public class CardServiceTest
    {
        [TestMethod]
        public async Task ActivateAsyncSuccess()
        {
            int conter = 0;
            int bankCounter = 0;
            var moqClientRepository = Substitute.For<IClientRepository>();            moqClientRepository.FindByPhoneNumberAsync(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult(new Client { CardStatus = Entities.Enum.CardStatusEnum.Registered , Tokens = new System.Collections.Generic.List<Token> { new Token { AccessToken = "1" } } }));
            moqClientRepository.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => conter++);
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetNow().ReturnsForAnyArgs(x => DateTime.Now);
            var moqClientStateService = Substitute.For<IClientStateService>();
            moqClientStateService.IsStatePassed(Arg.Any<string>(), Arg.Any<ClientState>()).ReturnsForAnyArgs(true);
            var moqBankService = Substitute.For<IBankCardService>();
            moqBankService.WhenForAnyArgs(x => x.CardActivateAsync(Arg.Any<string>() , Arg.Any<int>() , Arg.Any<string>() , Arg.Any<string>())).Do(x => bankCounter++);



            var service = new CardService(moqClientRepository, moqUtilityService, moqBankService, moqClientStateService);
            await service.ActivateAsync("4", "4", "1");

            Assert.AreEqual(conter, 1);
            Assert.AreEqual(bankCounter, 1);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataBusinessException))]
        public async Task ActivateExceptionInputParameterAsyn()
        {
            int conter = 0;
            int bankCounter = 0;
            var moqClientRepository = Substitute.For<IClientRepository>();
            moqClientRepository.FindByPhoneNumberAsync(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult(new Client { CardStatus = Entities.Enum.CardStatusEnum.Registered }));
            moqClientRepository.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => conter++);
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetNow().ReturnsForAnyArgs(x => DateTime.Now);
            var moqClientStateService = Substitute.For<IClientStateService>();
            moqClientStateService.IsStatePassed(Arg.Any<string>(), Arg.Any<ClientState>()).ReturnsForAnyArgs(true);
            var moqBankService = Substitute.For<IBankCardService>();
            moqBankService.WhenForAnyArgs(x => x.CardIssuanceAsync(Arg.Any<string>() , Arg.Any<string>())).Do(x => bankCounter++);



            var service = new CardService(moqClientRepository, moqUtilityService, moqBankService, moqClientStateService);
            await service.ActivateAsync("", "", "1");

            Assert.AreEqual(conter, 1);
            Assert.AreEqual(bankCounter, 1);

        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundBusinessException))]
        public async Task ActivateExceptionClientNotFoundAsync()
        {
            int conter = 0;
            int bankCounter = 0;
            var moqClientRepository = Substitute.For<IClientRepository>();
            moqClientRepository.FindByPhoneNumberAsync(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult((Client)null));
            moqClientRepository.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => conter++);
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetNow().ReturnsForAnyArgs(x => DateTime.Now);
            var moqClientStateService = Substitute.For<IClientStateService>();
            moqClientStateService.IsStatePassed(Arg.Any<string>(), Arg.Any<ClientState>()).ReturnsForAnyArgs(true);
            var moqBankService = Substitute.For<IBankCardService>();
            moqBankService.WhenForAnyArgs(x => x.CardIssuanceAsync(Arg.Any<string>() , Arg.Any<string>())).Do(x => bankCounter++);



            var service = new CardService(moqClientRepository, moqUtilityService, moqBankService, moqClientStateService);
            await service.ActivateAsync("4", "5", "1");

            Assert.AreEqual(conter, 0);

        }

        [TestMethod]
        public async Task RegisterCardSuccess_Async()
        {
            int conter = 0;
            int bankCounter = 0;
            var moqClientRepository = Substitute.For<IClientRepository>();
            moqClientRepository.FindByPhoneNumberAsync(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult(new Client { Tokens = new System.Collections.Generic.List<Token> { new Token { AccessToken = "" } } }));
            moqClientRepository.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => conter++);
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetNow().ReturnsForAnyArgs(x => DateTime.Now);
            var moqClientStateService = Substitute.For<IClientStateService>();
            moqClientStateService.IsStatePassed(Arg.Any<string>(), Arg.Any<ClientState>()).ReturnsForAnyArgs(true);
            var moqBankService = Substitute.For<IBankCardService>();
            moqBankService.WhenForAnyArgs(x => x.CardIssuanceAsync(Arg.Any<string>(), Arg.Any<string>())).Do(x => bankCounter++);



            var service = new CardService(moqClientRepository, moqUtilityService, moqBankService, moqClientStateService);
            await service.RegisterCardAsync("4", "4");

            Assert.AreEqual(bankCounter, 1);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataBusinessException))]
        public async Task RegisterCardInputParamException_Async()
        {
            int conter = 0;
            int bankCounter = 0;
            var moqClientRepository = Substitute.For<IClientRepository>();
            moqClientRepository.FindByPhoneNumberAsync(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult(new Client { }));
            moqClientRepository.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => conter++);
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetNow().ReturnsForAnyArgs(x => DateTime.Now);
            var moqClientStateService = Substitute.For<IClientStateService>();
            moqClientStateService.IsStatePassed(Arg.Any<string>(), Arg.Any<ClientState>()).ReturnsForAnyArgs(true);
            var moqBankService = Substitute.For<IBankCardService>();
            moqBankService.WhenForAnyArgs(x => x.CardIssuanceAsync(Arg.Any<string>() , Arg.Any<string>())).Do(x => bankCounter++);



            var service = new CardService(moqClientRepository, moqUtilityService, moqBankService, moqClientStateService);
            await service.RegisterCardAsync("", "");

            Assert.AreEqual(conter, 0);

        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundBusinessException))]
        public async Task RegisterCardClientNotFoundException_Async()
        {
            int conter = 0;
            int bankCounter = 0;
            var moqClientRepository = Substitute.For<IClientRepository>();
            moqClientRepository.FindByPhoneNumberAsync(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult((Client)null));
            moqClientRepository.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => conter++);
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetNow().ReturnsForAnyArgs(x => DateTime.Now);
            var moqClientStateService = Substitute.For<IClientStateService>();
            moqClientStateService.IsStatePassed(Arg.Any<string>(), Arg.Any<ClientState>()).ReturnsForAnyArgs(true);
            var moqBankService = Substitute.For<IBankCardService>();
            moqBankService.WhenForAnyArgs(x => x.CardIssuanceAsync(Arg.Any<string>() , Arg.Any<string>())).Do(x => bankCounter++);



            var service = new CardService(moqClientRepository, moqUtilityService, moqBankService, moqClientStateService);
            await service.RegisterCardAsync("a", "s");

            Assert.AreEqual(conter, 0);

        }


        [TestMethod]
        public async Task VerifyCardActivateSuccess_Async()
        {
            int conter = 0;
            int bankCounter = 0;
            var moqClientRepository = Substitute.For<IClientRepository>();
            moqClientRepository.FindByPhoneNumberAsync(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult(new Client { CardStatus = CardStatusEnum.Delivered , Tokens = new System.Collections.Generic.List<Token> { new Token() { AccessToken = "1" } } }));
            moqClientRepository.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => conter++);
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetNow().ReturnsForAnyArgs(x => DateTime.Now);
            var moqClientStateService = Substitute.For<IClientStateService>();
            moqClientStateService.IsStatePassed(Arg.Any<string>(), Arg.Any<ClientState>()).ReturnsForAnyArgs(true);
            var moqBankService = Substitute.For<IBankCardService>();
            moqBankService.WhenForAnyArgs(x => x.VerifyCardActivateAsync(Arg.Any<string>(), Arg.Any<string>() , Arg.Any<string> () , Arg.Any<string>())).Do(x => bankCounter++);


            var service = new CardService(moqClientRepository, moqUtilityService, moqBankService, moqClientStateService);
            await service.VerifyActivateAsync("4", "4" , "444");
            Assert.AreEqual(conter, 1);
            Assert.AreEqual(bankCounter, 1);

        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundBusinessException))]
        public async Task VerifyCardActivateNotFoundException_Async()
        {
            int conter = 0;
            int bankCounter = 0;
            var moqClientRepository = Substitute.For<IClientRepository>();
            moqClientRepository.FindByPhoneNumberAsync(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult((Client) null));
            moqClientRepository.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => conter++);
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetNow().ReturnsForAnyArgs(x => DateTime.Now);
            var moqClientStateService = Substitute.For<IClientStateService>();
            moqClientStateService.IsStatePassed(Arg.Any<string>(), Arg.Any<ClientState>()).ReturnsForAnyArgs(true);
            var moqBankService = Substitute.For<IBankCardService>();
            moqBankService.WhenForAnyArgs(x => x.VerifyCardActivateAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).Do(x => bankCounter++);


            var service = new CardService(moqClientRepository, moqUtilityService, moqBankService, moqClientStateService);
            await service.VerifyActivateAsync("4", "4", "444");
            Assert.AreEqual(conter, 0);
            Assert.AreEqual(bankCounter, 0);

        }


        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task VerifyCardActivateExceptionAlreadyIsActive_Async()
        {
            int conter = 0;
            int bankCounter = 0;
            var moqClientRepository = Substitute.For<IClientRepository>();
            moqClientRepository.FindByPhoneNumberAsync(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult(new Client { CardStatus = CardStatusEnum.Activated }));
            moqClientRepository.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => conter++);
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetNow().ReturnsForAnyArgs(x => DateTime.Now);
            var moqClientStateService = Substitute.For<IClientStateService>();
            moqClientStateService.IsStatePassed(Arg.Any<string>(), Arg.Any<ClientState>()).ReturnsForAnyArgs(true);
            var moqBankService = Substitute.For<IBankCardService>();
            moqBankService.WhenForAnyArgs(x => x.VerifyCardActivateAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).Do(x => bankCounter++);


            var service = new CardService(moqClientRepository, moqUtilityService, moqBankService, moqClientStateService);
            await service.VerifyActivateAsync("4", "4", "444");
            Assert.AreEqual(conter, 0);
            Assert.AreEqual(bankCounter, 0);

        }


        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task VerifyCardActivateExceptionNotDelivered_Async()
        {
            int conter = 0;
            int bankCounter = 0;
            var moqClientRepository = Substitute.For<IClientRepository>();
            moqClientRepository.FindByPhoneNumberAsync(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult(new Client { CardStatus = CardStatusEnum.Registered }));
            moqClientRepository.WhenForAnyArgs(x => x.UpdateClientAsync(Arg.Any<Client>())).Do(x => conter++);
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetNow().ReturnsForAnyArgs(x => DateTime.Now);
            var moqClientStateService = Substitute.For<IClientStateService>();
            moqClientStateService.IsStatePassed(Arg.Any<string>(), Arg.Any<ClientState>()).ReturnsForAnyArgs(true);
            var moqBankService = Substitute.For<IBankCardService>();
            moqBankService.WhenForAnyArgs(x => x.VerifyCardActivateAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).Do(x => bankCounter++);

            var service = new CardService(moqClientRepository, moqUtilityService, moqBankService, moqClientStateService);
            await service.VerifyActivateAsync("4", "4", "444");
            Assert.AreEqual(conter, 0);
            Assert.AreEqual(bankCounter, 0);

        }
    }
}
