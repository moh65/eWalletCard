using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mofid.eWallet.Api.Controllers;
using Mofid.eWallet.Api.DTOs;
using Mofid.eWallet.Api.RequestModels;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Responses;
using Mofid.eWallet.Infra.Security;
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
	public class UserControllerTest
	{
		[TestMethod]
		public async Task AuthenticateActionMustReturnSuccessResponseObject_async()
		{
			var moqUserService = Substitute.For<IUserService>();
			moqUserService
				.AuthenticateAsync(Arg.Any<string>(), Arg.Any<string>())
				.ReturnsForAnyArgs(x => (true, new User { Title = "ali"}));
			var moqJwtService = Substitute.For<IJWTService>();
			moqJwtService
				.GenerateToken(Arg.Any<string>(), Arg.Any<int>())
				.ReturnsForAnyArgs(x => "123513510890aso92");
			var moqUtilityService = Substitute.For<IUtilityService>();
			var controller = new UserController(moqUserService, moqJwtService , moqUtilityService);
			var userRequest = new UserRequest { Username = "aaa", Password = "bbb"};

			var response = await controller.Authenticate(userRequest);

			Assert.AreEqual(response.Value.StatusCode, 200);
			Assert.AreEqual(response.Value.IsSuccess, true);
		}
	}
}
