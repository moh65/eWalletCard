
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Moq;
using System;
using System.Threading.Tasks;

namespace Mofid.eWallet.Moq.Bank
{
	public class MoqBankRegistrationServcie
	{
		public IBankRegistrationExternalServcie GenerateMoqClass()
		{
			var mock = new Mock<IBankRegistrationExternalServcie>();

			mock.Setup(s => s.HandshakeAsync(It.IsAny<string>(), It.IsAny<string>())).Callback(async () =>
			{
				await Task.Delay(2000);
			}).Returns(Task.FromResult(Constants.Token));

			mock.Setup(s => s.OtpAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback(async () =>
		   {
			   await Task.Delay(2000);
		   });

			mock.Setup(s => s.VerifyOtpAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback(async () =>
		  {
			  await Task.Delay(2000);
		  }).Returns(Task.FromResult("ok"));


			mock.Setup(s => s.CheckLevel4Async(It.IsAny<string>(), It.IsAny<string>())).Callback(async () =>
			{
				await Task.Delay(2000);
			}).Returns(Task.FromResult(true));

			mock.Setup(s => s.UpgradeToLevel4Async(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback(async () =>
		 {
			 await Task.Delay(2000);
		 }).Returns(Task.FromResult(true));

			mock.Setup(s => s.UpgradeToLevel4Async(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback(async () =>
			{
				await Task.Delay(2000);
			}).Returns(Task.FromResult(true));


			mock.Setup(s => s.GetProfileAsync(It.IsAny<Token>(), It.IsAny<int>(), It.IsAny<string>())).Callback(async () =>
			{
				await Task.Delay(2000);
			}).Returns(Task.FromResult(Constants.Client));

			mock.Setup(s => s.EditProfileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Callback(async () =>
		{
			await Task.Delay(2000);
		}).Returns(Task.FromResult(true));

			mock.Setup(s => s.EditProfileWithConfirmAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Callback(async () =>
			{
				await Task.Delay(2000);
			}).Returns(Task.FromResult(Constants.Client));

			mock.Setup(s => s.PhysicalVerifyAsync(It.IsAny<string>(), It.IsAny<string>())).Callback(async () =>
			{
				await Task.Delay(2000);
			}).Returns(Task.FromResult(true));


			return mock.Object;
		}
	}
}
