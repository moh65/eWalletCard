using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mofid.eWallet.Api.Controllers;
using Mofid.eWallet.Api.RequestModels;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.UnitTests
{
    [TestClass]
    public class TemporaryClientControllerTest
    {
		[TestMethod]
		public async Task ImportExcelActionMustReturnSuccessResponseObject_async()
		{
			int counter = 0;
			var moqTClientService = Substitute.For<ITemporaryClientService>();
			moqTClientService
				.WhenForAnyArgs(x => x.Import(Arg.Any<IFormFile>()))
				.Do(x => counter++);
			var moqUtilityService = Substitute.For<IUtilityService>();
			var controller = new TemporaryClientController(moqTClientService, moqUtilityService);
			
			var response = await controller.ImportExcel(null);

			Assert.AreEqual(1, counter);
		}


        [TestMethod]
        public async Task ListActionReturnsCorrectResult_Async()
        {
            var moqClientService = Substitute.For<ITemporaryClientService>();
            moqClientService.List(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(((long)0, new List<TemporaryClient>())));

            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.WhenForAnyArgs(a => a.GetNow().ReturnsForAnyArgs(DateTime.Now));
            moqUtilityService.GetCurrentRefCodePerScope().ReturnsForAnyArgs("10101010");

            var controller = new TemporaryClientController(moqClientService, moqUtilityService);
            dynamic response = await controller.List(new ClientListRequest { NationalCode = "1222", Skip = 0, Take = 10 });
            var total = response.Value.Result;
            Assert.AreEqual(true, response.Value.IsSuccess);
            Assert.AreEqual("10101010", response.Value.ReferenceCode);
            Assert.AreEqual(StatusCodes.Status200OK, response.Value.StatusCode);
        }


        [TestMethod]
        public async Task DeleteActionReturnsCorrectResult_Async()
        {
            int Counter = 0;
            var moqClientService = Substitute.For<ITemporaryClientService>();
            var client = new Entities.DTOs.ClientDto { NationalCode = "1234567890" };
            moqClientService.WhenForAnyArgs(x => x.Delete(Arg.Any<string>())).Do(x => Counter++);

            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.WhenForAnyArgs(a => a.GetNow().ReturnsForAnyArgs(DateTime.Now));
            moqUtilityService.GetCurrentRefCodePerScope().ReturnsForAnyArgs("10101010");

            var controller = new TemporaryClientController(moqClientService, moqUtilityService);
            dynamic response = await controller.Delete(new ClientDeleteRequest { Id = "1" });
            Assert.AreEqual(true, response.Value.IsSuccess);
            Assert.AreEqual("10101010", response.Value.ReferenceCode);
            Assert.AreEqual(StatusCodes.Status200OK, response.Value.StatusCode);
            Assert.AreEqual(1, Counter);

        }

    }
}
