using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services.Contracts
{
    public interface IPaymentService
    {
        Task TbsToCard(string nationalCode , decimal amount);
        Task CardToTbs(string nationalCode, decimal amount);
    }
}
