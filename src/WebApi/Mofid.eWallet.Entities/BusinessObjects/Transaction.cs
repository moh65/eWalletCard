using Mofid.eWallet.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Entities.BusinessObjects
{
	public class Transaction
	{
		public Transaction()
		{
			this.CreateDate = DateTime.Now;
			this.ModifiedDate = DateTime.Now;
		}
        public string Id { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
		public DateTime CreateDate { get; }
		public DateTime ModifiedDate { get; set; }
		public string NationalCode { get; set; }
		public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string GroupId { get; set; }
        public string Description { get; set; }
		public string DeviceId { get; set; }
	}
}
