using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.Common.ExtensionFramework;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Infrastracture.Hubs;

namespace QuestionAndAnswer.Tests.Integration.Hubs
{
    public class QuestionsHub: IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _testWebApplicationFactory;

        public QuestionsHub(TestWebApplicationFactory testWebApplicationFactory)
        {
            _testWebApplicationFactory = testWebApplicationFactory;
        }
        
        [Fact]
        public async Task ReceiveQuestionMethod_WithId_ExpectReceivedId()
        {
            int questionId = 1;
            string message = null;
            string expectedMessage = "Successfully subscribe";
            var connection = new HubConnectionBuilder()
                .WithUrl(
                    "http://localhost/questionshub",
                    o => o.HttpMessageHandlerFactory = _ => _testWebApplicationFactory.Server.CreateHandler())
                .Build();
            connection.On<string>("Message", receivedMessage =>
            {
                message = receivedMessage;
            });
            
            await connection.StartAsync();
            await connection.InvokeAsync("SubscribeQuestion", questionId);
            
            Assert.Equal(expectedMessage, message);
        }
    }
}