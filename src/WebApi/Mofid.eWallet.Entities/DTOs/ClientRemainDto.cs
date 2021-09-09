using System;

namespace Mofid.eWallet.Api.Dto
{
    public class ClientRemainDto
    {
		public decimal AdjustedRemain;

		public decimal BlockedRemain;

		public decimal Credit;

		public DateTime? CreditDate;

		public decimal CurrentRemain;
    }
}
