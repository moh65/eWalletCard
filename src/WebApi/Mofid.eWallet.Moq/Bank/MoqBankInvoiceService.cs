
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Moq;
using System.Threading.Tasks;

namespace Mofid.eWallet.Moq.Bank
{
	public class MoqBankInvoiceService
    {
        public IBankInvoiceExternalService GenerateMoqClass()
        {
            var mock = new Mock<IBankInvoiceExternalService>();

            var invoice = new Invoice();
            mock.Setup(s => s.SetInvoice(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>())).Callback(async () =>
            {
                await Task.Delay(2000);
            }).Returns(Task.FromResult(invoice));


            return mock.Object;
        }
    }
}
