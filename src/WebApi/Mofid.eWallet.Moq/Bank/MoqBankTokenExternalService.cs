using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Moq.Bank
{
	public class MoqBankTokenExternalService
    {
        public IBankTokenExternalService GenerateMoqClass()
        {
            var mock = new Mock<IBankTokenExternalService>();

            mock.Setup(s => s.AcuireTokenAsync(It.IsAny<string>(), It.IsAny<string>())).Callback(async () => {
                await Task.Delay(2000);
            }).Returns(Task.FromResult(Constants.Token));


            mock.Setup(s => s.RefreshTokenAsync(It.IsAny<Token>(), It.IsAny<string>())).Callback(async () => {
                await Task.Delay(2000);
            }).Returns(Task.FromResult(Constants.Token));

            return mock.Object;
        }
    }
}
