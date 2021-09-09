using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Mofid.eWallet.Api;
using Mofid.eWallet.Infra.Responses;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mofid.eWallet.Test
{

    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
           return Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseStartup<FakeStartup>()
                    .UseIISIntegration()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseDefaultServiceProvider(p => p.ValidateScopes = false).UseTestServer();
                    
            });

         
        }
    }

    public class IntegrationTest
    {
        
        protected readonly HttpClient TestClient;
        public IntegrationTest()
        {

            var appfactory = new TestWebApplicationFactory<FakeStartup>();
            TestClient = appfactory.CreateClient();
        }

        protected async Task Authenticate()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJWT());
        }

        private async Task<string> GetJWT()
        {
            var payload = "{\"username\": \"m.parsa\",\"password\": \"123456\"}";

            HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await TestClient.PostAsync("http://localhost:5000/api/v1/user/auth", c);

            ApiResponse<dynamic> receive = JsonConvert.DeserializeObject<ApiResponse<dynamic>>(await response.Content.ReadAsStringAsync());
            return receive.Result.token;
        }
    }
}
