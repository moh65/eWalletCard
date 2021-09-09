
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Moq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Moq.Bank
{
	public class MoqBankCustomerRemainBalanceService
    {
        public IBankCustomerRemainBalanceService GenerateMoqClass()
        {
            var mock = new Mock<IBankCustomerRemainBalanceService>();


            mock.Setup(s => s.GetRemain(It.IsAny<string>(), It.IsAny<string>())).Callback(async () =>
            {
                await Task.Delay(2000);
            }).Returns(Task.FromResult((decimal)0));


            return mock.Object;
        }
    }
}
