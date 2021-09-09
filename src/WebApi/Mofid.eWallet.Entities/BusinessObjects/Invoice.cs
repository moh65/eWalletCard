using System;
using System.Collections.Generic;
using System.Text;

namespace Mofid.eWallet.Entities.BusinessObjects
{
	public class Invoice
	{
		public object UserId { get; set; }
		public List<InvoiceItem> Items { get; set; }

	}
}
