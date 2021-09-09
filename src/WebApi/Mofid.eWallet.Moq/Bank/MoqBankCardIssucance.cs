using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Moq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Moq.Bank
{
	public class MoqBankCardIssucanceService
	{
		public IBankCardService GenerateMoqClass()
		{
			var mock = new Mock<IBankCardService>();


			mock.Setup(s => s.CardIssuanceAsync(It.IsAny<string>(), It.IsAny<string>())).Callback(async () =>
			{
				await Task.Delay(2000);
			});

			mock.Setup(s => s.CardActivateAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Callback(async () =>
		   {
			   await Task.Delay(2000);
		   });

			mock.Setup(s => s.VerifyCardActivateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback(async () =>
			{
				await Task.Delay(2000);
			});


			return mock.Object;
		}
	}
}
