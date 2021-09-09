namespace Mofid.eWallet.Api.RequestModels
{
    public class ClientListRequest
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 20;
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
    }
}
