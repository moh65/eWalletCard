using Mofid.eWallet.Entities.Enum;

namespace Mofid.eWallet.Entities.Configurations
{
	public class BankConfiguration
	{
		//public string WalletCode { get; set; } = "MOFID_WALLET";
		public string ApiToken { get; set; } = "32d5e96ff6534ed6b24811d6d41a9668";
		public int TokenIssuer { get; set; } = 1;
		//public string BaseAddress { get; set; } = "http://sandbox.pod.ir/srv/basic-platform/nzh";
		public string AccountAddress { get; set; } = "https://accounts.pod.ir";
		public string PlatformAddress { get; set; } = "http://sandbox.pod.ir/srv/basic-platform";
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string Signature { get; set; }
		public BanksEnum Type { get; set; }

	}
}
