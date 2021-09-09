using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mofid.eWallet.Api.Controllers;
using Mofid.eWallet.Api.RequestModels;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.Services;
using NSubstitute;
using System;
using System.Threading.Tasks;

using Mofid.eWallet.Api.Dto;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Exceptions;
using NSubstitute.ExceptionExtensions;
using System.Collections.Generic;
using Mofid.eWallet.Entities.Enum;

namespace Mofid.eWallet.UnitTests
{
    [TestClass]
    public class ClientControllerTest
    {
        [TestMethod]
        public async Task KycActionMustReturnSuccessResponseObject_async()
        {
            var counter = 0;
            var moqClientKycService = Substitute.For<IClientKycService>();
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqClientKycService
                .WhenForAnyArgs(x => x.KycAsync(Arg.Any<Client>(), Arg.Any<Token>()))
                .Do(x => counter++);

            moqUtilityService.GetNow().ReturnsForAnyArgs(x =>
            {
                return DateTime.Now;
            });


            var controller = new ClientController(moqClientKycService, moqUtilityService);
            var kycRequest = new KycRequest { DeviceId = "", NationalCardSerial = "", NationalCode = "", PhoneNumber = "" };

            var response = await controller.Kyc(kycRequest);

            Assert.AreEqual(counter, 1);
            Assert.AreEqual(response.Value.StatusCode, 200);
            Assert.AreEqual(response.Value.IsSuccess, true);
        }
        [TestMethod]
        public async Task VerifyActionMustReturnSuccessResponseObject_async()
        {
            var counter = 0;
            var moqClientKycService = Substitute.For<IClientKycService>();
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetNow().ReturnsForAnyArgs(x =>
            {
                return DateTime.Now;
            });
            moqClientKycService
                .WhenForAnyArgs(x => x.Verify(Arg.Any<Client>(), Arg.Any<Token>(), Arg.Any<string>()))
                .Do(x => counter++);

            var controller = new ClientController(moqClientKycService, moqUtilityService);
            var verifyRequest = new VerifyRequest { DeviceId = "", PhoneNumber = "", Otp = "" };

            var response = await controller.Verify(verifyRequest);

            Assert.AreEqual(counter, 1);
            Assert.AreEqual(response.Value.StatusCode, 200);
            Assert.AreEqual(response.Value.IsSuccess, true);
        }


        [TestMethod]
        public async Task AddressActionReturnsNotFound_async()
        {
            var moqClientAddressService = NSubstitute.Substitute.For<IClientKycService>();
            moqClientAddressService.GetAddress(Arg.Any<Client>()).ReturnsForAnyArgs(x => Task.FromResult((Client)null));
            var moqUtilityService = Substitute.For<IUtilityService>();
            var controller = new ClientController(moqClientAddressService, moqUtilityService);
            var response = await controller.Address(new AddressClientRequest { NationalCode = "1222" });


            Assert.AreEqual((int)StatusCodes.Status404NotFound, response.Value.StatusCode);

        }

        [TestMethod]
        public async Task AddressActionReturnsSuccessObject_async()
        {
            var moqClientAddressService = NSubstitute.Substitute.For<IClientKycService>();
            moqClientAddressService.GetAddress(Arg.Any<Client>()).ReturnsForAnyArgs(x => Task.FromResult(new Client() { Addresses = new System.Collections.Generic.List<Address>() { new Address { } } }));
            var moqUtilityService = Substitute.For<IUtilityService>();
            var controller = new ClientController(moqClientAddressService, moqUtilityService);
            var response = await controller.Address(new Api.RequestModels.AddressClientRequest { NationalCode = "1222" });


            Assert.AreEqual((int)StatusCodes.Status200OK, response.Value.StatusCode);

        }


        [TestMethod]
        public async Task TbsRemainActionReturnSuccessObject_Async()
        {
            var moqClientAddressService = NSubstitute.Substitute.For<IClientKycService>();
            var moqTbsRemainDto = new ClientRemainDto { CurrentRemain = 500 };
            moqClientAddressService.GetTbsRemain(Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult(moqTbsRemainDto));
            var moqUtilityService = Substitute.For<IUtilityService>();
            var controller = new ClientController(moqClientAddressService, moqUtilityService);
            var response = await controller.TbsRemain(new Api.RequestModels.TbsRemainsRequest { NationalCode = "1222" });

            Assert.AreEqual((int)StatusCodes.Status200OK, response.Value.StatusCode);
            //Assert.AreEqual(moqTbsRemainDto.CurrentRemain, response.Value.Result.remain);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task TbsRemainActionExceptionClientNotFoundObject_Async()
        {
            var moqClientAddressService = NSubstitute.Substitute.For<IClientKycService>();
            var moqTbsRemainDto = new ClientRemainDto { CurrentRemain = 500 };
            moqClientAddressService.GetTbsRemain(Arg.Any<string>()).Throws(new BusinessException("error"));
            var moqUtilityService = Substitute.For<IUtilityService>();
            var controller = new ClientController(moqClientAddressService, moqUtilityService);
            var response = await controller.TbsRemain(new TbsRemainsRequest { NationalCode = "1222" });

            Assert.AreEqual((int)ExceptionErrorCodes.ClientNotFound, response.Value.StatusCode);
            //Assert.AreEqual(moqTbsRemainDto.CurrentRemain, response.Value.Result.remain);
        }


        [TestMethod]
        public async Task BankRemainActionReturnSuccessObject_Async()
        {
            var moqClientAddressService = NSubstitute.Substitute.For<IClientKycService>();
            var moqTbsRemainDto = new ClientRemainDto { CurrentRemain = 500 };
            moqClientAddressService.GetBankRemain(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(x => Task.FromResult(moqTbsRemainDto));
            var moqUtilityService = Substitute.For<IUtilityService>();
            var controller = new ClientController(moqClientAddressService, moqUtilityService);
            var response = await controller.BankRemain(new Api.RequestModels.BankRemainsRequest { NationalCode = "1222", DeviceId = "111" });

            Assert.AreEqual((int)StatusCodes.Status200OK, response.Value.StatusCode);
            //Assert.AreEqual(moqTbsRemainDto.CurrentRemain, response.Value.Result.remain);
        }


        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task BankRemainActionExceptionClientNotFoundObject_Async()
        {
            var moqClientAddressService = NSubstitute.Substitute.For<IClientKycService>();
            var moqTbsRemainDto = new ClientRemainDto { CurrentRemain = 500 };
            moqClientAddressService.GetBankRemain(Arg.Any<string>(), Arg.Any<string>()).Throws(new BusinessException(ExceptionErrorCodes.ClientNotFound));
            var moqUtilityService = Substitute.For<IUtilityService>();
            var controller = new ClientController(moqClientAddressService, moqUtilityService);
            var response = await controller.BankRemain(new BankRemainsRequest { NationalCode = "1222", DeviceId = "111" });

            Assert.AreEqual((int)ExceptionErrorCodes.ClientNotFound, response.Value.StatusCode);
            //Assert.AreEqual(moqTbsRemainDto.CurrentRemain, response.Value.Result.remain);
        }



        [TestMethod]
        public async Task IsLegalActionReturnsFalse_Async()
        {
            var moqClientAddressService = Substitute.For<IClientKycService>();

            moqClientAddressService.IsLegal(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(false));
            var moqUtilityService = Substitute.For<IUtilityService>();
            var controller = new ClientController(moqClientAddressService, moqUtilityService);
            dynamic response = await controller.IsLegal(new WhiteListRequest { NationalCode = "1222" });
            Assert.AreEqual(false, response.Value.Result);
        }

        [TestMethod]

        public async Task IsLegalActionReturnsTrue_Async()
        {
            var moqClientAddressService = Substitute.For<IClientKycService>();

            moqClientAddressService.IsLegal(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(true));
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.WhenForAnyArgs(a => a.GetNow().ReturnsForAnyArgs(DateTime.Now));
            var controller = new ClientController(moqClientAddressService, moqUtilityService);
            dynamic response = await controller.IsLegal(new WhiteListRequest { NationalCode = "1222" });
            var _result = Convert.ToBoolean(response.Value.Result);
            Assert.AreEqual(true, _result);
        }
        [TestMethod]

        public async Task GetClientStatesActionReturnsCorrectResult_Async()
        {
            var moqClientService = Substitute.For<IClientKycService>();
            moqClientService.GetClientStates(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(new List<ClientState> { ClientState.TbsVerified, ClientState.RegiteredNotVerified, ClientState.OtpVerified }));

            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.WhenForAnyArgs(a => a.GetNow().ReturnsForAnyArgs(DateTime.Now));
            moqUtilityService.GetCurrentRefCodePerScope().ReturnsForAnyArgs("10101010");

            var controller = new ClientController(moqClientService, moqUtilityService);
            dynamic response = await controller.ClientStates(new ClientStatesRequest { NationalCode = "1222" });
            var _result = (ApiResponse<List<string>>)response.Value;
            Assert.AreEqual(true, _result.IsSuccess);
            Assert.AreEqual("10101010", _result.ReferenceCode);
            Assert.AreEqual(StatusCodes.Status200OK, _result.StatusCode);
            Assert.AreEqual(3, _result.Result.Count);

        }
        [TestMethod]
        public async Task ListActionReturnsCorrectResult_Async()
        {
            var moqClientService = Substitute.For<IClientKycService>();
            moqClientService.ListAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(((long)0, new List<Entities.DTOs.ClientDto>())));

            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.WhenForAnyArgs(a => a.GetNow().ReturnsForAnyArgs(DateTime.Now));
            moqUtilityService.GetCurrentRefCodePerScope().ReturnsForAnyArgs("10101010");

            var controller = new ClientController(moqClientService, moqUtilityService);
            dynamic response = await controller.list(new ClientListRequest { NationalCode = "1222", Skip = 0, Take = 10 });
            var total = response.Value.Result;
            Assert.AreEqual(true, response.Value.IsSuccess);
            Assert.AreEqual("10101010", response.Value.ReferenceCode);
            Assert.AreEqual(StatusCodes.Status200OK, response.Value.StatusCode);
        }


        [TestMethod]
        public async Task InfoActionReturnsCorrectResult_Async()
        {
            var moqClientService = Substitute.For<IClientKycService>();
            var client = new Entities.DTOs.ClientDto { NationalCode = "1234567890" };
            moqClientService.GetClientAsync(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(client));

            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.WhenForAnyArgs(a => a.GetNow().ReturnsForAnyArgs(DateTime.Now));
            moqUtilityService.GetCurrentRefCodePerScope().ReturnsForAnyArgs("10101010");

            var controller = new ClientController(moqClientService, moqUtilityService);
            dynamic response = await controller.info(new ClientInfoRequest { NationalCode = "1234567890" });
            Assert.AreEqual(true, response.Value.IsSuccess);
            Assert.AreEqual("10101010", response.Value.ReferenceCode);
            Assert.AreEqual(StatusCodes.Status200OK, response.Value.StatusCode);
            Assert.AreEqual(client.NationalCode, response.Value.Result.NationalCode);

        }
    }
}
