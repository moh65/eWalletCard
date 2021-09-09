using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services.Contracts
{
    public interface ICardService
    {
        Task RegisterCardAsync(string nationalCode, string phoneNumber);
        Task ActivateAsync(string nationalCode, string phoneNumber, string pan);
        Task VerifyActivateAsync(string nationalCode, string phoneNumber , string otp);
    }
}
