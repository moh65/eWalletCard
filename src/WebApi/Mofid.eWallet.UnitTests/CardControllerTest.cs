using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using NSubstitute;
using Mofid.eWallet.Services.Contracts;
using Mofid.eWallet.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Mofid.eWallet.Infra.Exceptions;
using Mofid.eWallet.Infra.Utils;

namespace Mofid.eWallet.UnitTests
{
    [TestClass]
    public class CardControllerTest
    {


        [TestMethod]
        public async Task RegisterActionSuccessfully_async()
        {
            int cardCounter = 0;
            var moqCardService = Substitute.For<ICardService>();
            moqCardService.WhenForAnyArgs(x => x.RegisterCardAsync(Arg.Any<string>(), Arg.Any<string>())).Do(x => cardCounter++);
            var actionParametter = new Api.RequestModels.CardRegisterRequest { NationalCode = "4900729671", PhoneNumber = "09124258049" };
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetCurrentRefCodePerScope().Returns("604198463213204");

            var controller = new CardController(moqCardService, moqUtilityService);
            var res = await controller.Register(actionParametter);


            Assert.AreEqual(cardCounter, 1);
            Assert.AreEqual(res.Value.StatusCode, StatusCodes.Status200OK);
            Assert.AreEqual(res.Value.ReferenceCode, "604198463213204");

        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundBusinessException))]
        public async Task RegisterActionException_async()
        {

            var moqCardService = Substitute.For<ICardService>();
            moqCardService.WhenForAnyArgs(x => x.RegisterCardAsync(Arg.Any<string>(), Arg.Any<string>())).Do(x => throw new NotFoundBusinessException(ExceptionErrorCodes.ClientNotFound));
            var actionParametter = new Api.RequestModels.CardRegisterRequest { NationalCode = "4900729671", PhoneNumber = "09124258049" };
            var moqUtilityService = Substitute.For<IUtilityService>();
            moqUtilityService.GetCurrentRefCodePerScope().Returns("604198463213204");

            var controller = new CardController(moqCardService, moqUtilityService);

            var res = await controller.Register(actionParametter);

            Assert.AreEqual(res.Value.StatusCode, StatusCodes.Status500InternalServerError);
            Assert.AreEqual(res.Value.ReferenceCode, "604198463213204");
        }
    }
}
