using Microsoft.AspNetCore.Http;

namespace Mofid.eWallet.Api.RequestModels
{
    public class ImportExcelRequest
    {
        public IFormFile WhiteList { get; set; }
    }
}
