using Microsoft.AspNetCore.Http;
using Mofid.eWallet.Entities.BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofid.eWallet.Services
{
    public interface ITemporaryClientService
    {
		Task Import(IFormFile file);
        Task<(long , List<TemporaryClient>)> List(int skip, int take, string nationalCode, string mobile);
        Task Delete(string id);
    }
}