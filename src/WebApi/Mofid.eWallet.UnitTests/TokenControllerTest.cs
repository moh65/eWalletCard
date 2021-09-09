using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mofid.eWallet.Api.Controllers;
using Mofid.eWallet.Api.RequestModels;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Utils;
using Mofid.eWallet.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.UnitTests
{
	[TestClass]
	public class TokenControllerTest
	{
		[TestMethod]
		public async Task AcquireActionMustReturnSuccessResponseObject_async()
		{
			var moqTokenService = Substitute.For<ITokenService>();
			moqTokenService
				.AcquireTokenAsync(Arg.Any<string>(), Arg.Any<string>())
				.ReturnsForAnyArgs(x => new Token { AccessToken = "123513510890aso92" });
			var moqUtilityService = Substitute.For<IUtilityService>();
			var controller = new TokenController(moqTokenService , null , moqUtilityService);
			var acquireRequest = new AcquireRequest { DeviceId = "1122", PhoneNumber = "091234567890" };

			var response = await controller.Acquire(acquireRequest);

			Assert.AreEqual(response.Value.StatusCode, 200);
			Assert.AreEqual(response.Value.IsSuccess, true);
		}
	}
}
