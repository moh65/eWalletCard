using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Banks;
using Moq;
using System;
using System.Threading.Tasks;

namespace Mofid.eWallet.Moq.Bank
{
	public class MoqBankOttService 
    {
        public IBankOttExternalService GenerateMoqClass()
        {
            var mock = new Mock<IBankOttExternalService>();

            mock.Setup(s => s.GetOttAsync(It.IsAny<string>() , It.IsAny<int>(), It.IsAny<string>())).Callback(async () => {
                await Task.Delay(2000);
            }).Returns(Task.FromResult("ott"));


            return mock.Object;
        }
    } 

  
}
