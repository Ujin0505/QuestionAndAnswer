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
                "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImEtN1BLVUxCUE8zMUFRTGRkTXVFYyJ9.eyJpc3MiOiJodHRwczovL2Rldi11OWRhcTRiay5ldS5hdXRoMC5jb20vIiwic3ViIjoiSVJDemFQTUl1VlBnQzhwcEt0NVFoR1ExM1paNFhscm1AY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vcWFuZGEiLCJpYXQiOjE1OTg3Nzg3MjMsImV4cCI6MTU5ODg2NTEyMywiYXpwIjoiSVJDemFQTUl1VlBnQzhwcEt0NVFoR1ExM1paNFhscm0iLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.JMV4__zucPT-xH5aPvEjxyIfyeUp5ZPn3wXBH_rOGrLzZDJbeidPf-SsjNoTO1KULCgJWAQjhe1n2WWtDKWkg2Iti5CWV2-Wc5602g-0sUVnEVe1mOjp1qwXDFmYZj-Y_kJAsF7XUQFIRmveLZclqgOe7yOQebzVlfBTVmEvS2kGvc6gXaAtX9KMFqm2Swlnx1iYKNmqS5rRNlP2SIRVgIJk50c8qt68ftcyJUqrUHFXYg29ZBgly1TaG86y9dxA78lR262n-qh3pgItteqQd2STmmWJB2o_VT8qB_PKTn6hwL3m1qYvIdM_TCppGkX9h9CtoVMimIeClEr7RMxFAQ";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}