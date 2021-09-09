namespace Mofid.eWallet.Entities.BusinessObjects
{
	public class InvoiceItem
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Price { get; set; }
		public int Quantity { get; set; }
		public string Description { get; set; }
	}
}