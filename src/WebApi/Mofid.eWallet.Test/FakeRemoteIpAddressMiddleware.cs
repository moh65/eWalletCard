using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace Mofid.eWallet.Test
{
    public class FakeRemoteIpAddressMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IPAddress fakeIpAddress = IPAddress.Parse("127.0.0.1");

        public FakeRemoteIpAddressMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Connection.RemoteIpAddress = fakeIpAddress;

            await this.next(httpContext);
        }
    }
}
