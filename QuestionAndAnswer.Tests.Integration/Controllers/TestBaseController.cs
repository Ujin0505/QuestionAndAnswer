using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;


namespace QuestionAndAnswer.Tests.Integration.Controllers
{
    public class TestBaseController: IClassFixture<TestWebApplicationFactory>
    {
        protected HttpClient _client;

        public TestBaseController(TestWebApplicationFactory testWebApplicationFactory)
        {
            _client = testWebApplicationFactory.CreateClient();
        }

        public async Task Auth()
        {
            var client = new HttpClient();
            var content = new StringContent(
                "{\"client_id\":\"IRCzaPMIuVPgC8ppKt5QhGQ13ZZ4Xlrm\",\"client_secret\":\"BU7jHMqBD61oElEM92loMmfr2Dw3r0H_h5vQa0kub0xP5HwQXSLbUnlE6Tzck1tw\",\"audience\":\"https://qanda\",\"grant_type\":\"client_credentials\"}",
                Encoding.UTF8,
                "application/json");
            
            var response = await  client.PostAsync("https://dev-u9daq4bk.eu.auth0.com/oauth/token", content);
            var data = await response.Content.ReadAsStringAsync();
            var tokenData =  JsonSerializer.Deserialize<TokenData>(data, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenData.Token_type, tokenData.Access_token);
        }

        private class TokenData
        {
            public string Access_token { get; set; }
            public int Expires_in { get; set; }
            public string Token_type { get; set; }

        }
            
                
        
    }
}