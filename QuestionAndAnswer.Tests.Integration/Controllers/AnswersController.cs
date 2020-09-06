using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;
using QuestionAndAnswer.Application.Answers.Commands;
using QuestionAndAnswer.Application.Answers.Models;
using QuestionAndAnswer.Application.Models;
using Xunit;

namespace QuestionAndAnswer.Tests.Integration.Controllers
{
    public class AnswersController: TestBaseController
    {
        public AnswersController(TestWebApplicationFactory testWebApplicationFactory) : base(testWebApplicationFactory)
        {
        }

        [Fact]
        public async Task GetAnswer_ValidData_ReturnStatusCreated()
        {
            int id = -1;
            string url = $"api/questions/-1/answers/{id}";

            var response = await _client.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            var answer = JsonSerializer.Deserialize<AnswerDto>(content, new JsonSerializerOptions(){PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(answer.Id == id);
        }
        
        
        [Fact]
        public async Task CreateAnswer_ValidData_ReturnStatusCreated()
        {
            await Auth();
            int questionId = -1;
            string url = $"api/questions/{-1}/answers/";
            
            var createCommand = new CreateAnswerCommand()
            {
                QuestionId = questionId, 
                Content = "TestContent"
            };
            var content = new StringContent(JsonSerializer.Serialize(createCommand), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);
            
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }
        
        
    }
}