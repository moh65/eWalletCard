using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Infra.Security
{
    public interface IJWTService
    {
        string ValidateTokenAndGetClaim(string token, string claim);
        string GenerateToken(string claim, object value);
    }
}
