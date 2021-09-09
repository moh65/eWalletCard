namespace Mofid.eWallet.Api.RequestModels
{
    public class AcquireRequest
	{
		/// <summary>
		/// شماره تماس
		/// </summary>
		public string PhoneNumber { get; set; }
		/// <summary>
		/// شناسه دستگاه
		/// </summary>
		public string DeviceId { get; set; }
	}
}
