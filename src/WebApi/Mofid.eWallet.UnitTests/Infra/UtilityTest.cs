
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mofid.eWallet.Infra.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.UnitTests.Infra
{
	[TestClass]
	public class UtilityTest
	{
		[TestMethod]
		public void UtitityTestMustConvertGerogirianDateToPersian()
		{
			var logger = NSubstitute.Substitute.For<ILogger<UtilityService>>();
			var accessor = NSubstitute.Substitute.For<IHttpContextAccessor>();
			IUtilityService utilityService = new UtilityService(logger, accessor);

			var date = new DateTime(1991, 08, 11);
			var persianDate = utilityService.ConvertToPersian(date);

			Assert.AreEqual(persianDate, "1370/05/20");

		}
	}
}
