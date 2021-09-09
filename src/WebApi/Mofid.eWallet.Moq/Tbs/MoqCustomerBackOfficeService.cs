using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.ExternalServiceContracts.Tbs;
using Moq;
using System;
using System.Threading.Tasks;

namespace Mofid.eWallet.Moq.Tbs
{
	public class MoqCustomerBackOfficeService
    {
        public ICustomerBackOfficeService GenerateMoqClass()
        {
            var mock = new Mock<ICustomerBackOfficeService>();
    
            mock.Setup(s => s.GetCustomerByNationalCodeAsync(It.IsAny<string>())).Callback(async () => {
                await Task.Delay(2000);
            }).Returns(Task.FromResult(Constants.Client));

            mock.Setup(s => s.GetCustomerRemain(It.IsAny<string>())).Callback(async () => {
                await Task.Delay(2000);
            }).Returns(Task.FromResult(new Api.Dto.ClientRemainDto { 
                CurrentRemain = 9000000000,
            }));
            mock.Setup(s => s.IncreaseTbsBalanceAsync(It.IsAny<decimal>() , It.IsAny<string>())).Callback(async () => {
                await Task.Delay(2000);
            }).Returns(Task.FromResult(true));

            mock.Setup(s => s.DecreaseTbsBalanceAsync(It.IsAny<decimal>(), It.IsAny<string>())).Callback(async () => {
                await Task.Delay(2000);
            }).Returns(Task.FromResult(true));

            return mock.Object;
        }
    }
}
