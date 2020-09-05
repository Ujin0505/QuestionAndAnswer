using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
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

        public void Auth()
        {
            string token =
                "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImEtN1BLVUxCUE8zMUFRTGRkTXVFYyJ9.eyJpc3MiOiJodHRwczovL2Rldi11OWRhcTRiay5ldS5hdXRoMC5jb20vIiwic3ViIjoiSVJDemFQTUl1VlBnQzhwcEt0NVFoR1ExM1paNFhscm1AY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vcWFuZGEiLCJpYXQiOjE1OTkyOTMxMDAsImV4cCI6MTU5OTM3OTUwMCwiYXpwIjoiSVJDemFQTUl1VlBnQzhwcEt0NVFoR1ExM1paNFhscm0iLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.cB_gwbwAsxmYTgdlWUuKyVLCSV4sWiptID3euUem-uBmbr2vP1wYIYb2b166tn-OX98StaWvUrJTncyk6P7WMgpDzHT-0o0HMhIAA55aZBH1HSDQeuqQT4s2nqXDlv9O5CZYJ7on01-s2MOSuDmH3cZP_8eLX2NP0C8yoO1fi2Qk4UQ2mU-dSqv8e4kZeSvx-59q0BDx-Y4d5hsOM_PRIt2MmhihrLnrFeHvd2tt1DkxTf8N7tU0TnGyIg8ffxq4TpCw2lW9n9Vz5vrLBLh3NioHZN07rlEsPJJSK_SqKKxM7JGzfYFmZqwWQ2C2PJ-8HsAHuN9g6A6xMFqZK96_0w";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}