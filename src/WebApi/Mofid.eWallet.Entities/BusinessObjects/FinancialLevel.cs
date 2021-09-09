using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Entities.BusinessObjects
{
	public class FinancialLevel
	{
		public int Id { get; set; }
		public string Level { get; set; }
		public string LevelName { get; set; }
		public int Value { get; set; }
	}

}
