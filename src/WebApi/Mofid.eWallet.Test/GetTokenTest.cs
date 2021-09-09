using FluentAssertions;
using Mofid.eWallet.Infra.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mofid.eWallet.Test
{
    public class GetTokenTest : IntegrationTest
    {
        [Fact]
        public async Task GetTokenAsync()
        {
            await Authenticate();

            var payload = @"{ ""phoneNumber"": ""09223593741"", ""deviceId"": ""d5210629098"" }";

            HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await TestClient.PostAsync("http://localhost:5000/api/v1/token", c);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var receive = JsonConvert.DeserializeObject<ApiResponse<object>>(await response.Content.ReadAsStringAsync());
            dynamic token = JObject.Parse(receive.Result.ToString());
            var b = token.accessToken;
            var guid = Guid.Empty;


            Assert.True(Guid.TryParse(b.ToString(), out guid), "can not get token");
        } 
    }
}
