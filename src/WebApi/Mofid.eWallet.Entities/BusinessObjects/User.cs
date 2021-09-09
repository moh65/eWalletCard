using Newtonsoft.Json;

namespace Mofid.eWallet.Entities.BusinessObjects
{
	public class User
	{
		public string Id { get; set; }
		public string Username { get; set; }
		public string Title { get; set; }
		[JsonIgnore]
		public string Password { get; set; }
	}
}
