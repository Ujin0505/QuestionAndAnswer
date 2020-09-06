using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using QuestionAndAnswer.Application.Common.Handlers;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Application.Questions.Commands;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace QuestionAndAnswer.Tests.Integration.Controllers
{
    public class QuestionsController: TestBaseController
    {
        public QuestionsController (TestWebApplicationFactory testWebApplicationFactory): base(testWebApplicationFactory)
        {
        }

        [Fact]
        public async Task GetQuestions_WithoutSearchFilter_ReturnContent()
        {
            var response = await _client.GetAsync("api/questions");
            var content = await response.Content.ReadAsStringAsync();
            var questions = JsonSerializer.Deserialize<List<QuestionDto>>(content, new JsonSerializerOptions(){PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.NotEmpty(questions);
        }

        [Fact]
        public async Task GetQuestion_ValidId_ReturnStatusOkWithContent()
        {
            await Auth();
            string url = "api/questions/-2"; 
            
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var question = JsonSerializer.Deserialize<QuestionDto>(content, new JsonSerializerOptions(){PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(question.Id == -2);
        }
        
        [Fact]
        public async Task GetQuestion_InvalidId_ReturnStatusNotFound()
        {
            await Auth();
            string url = "api/questions/-999";
            
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task DeleteQuestion_ValidId_ReturnStatusNoContent()
        {
            await Auth();
            string url = "api/questions/-1";
            
            var response = await _client.DeleteAsync(url);
            
            Assert.True(response.StatusCode == HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task CreateQuestion_ValidData_ReturnStatusCreated()
        {
            await Auth();
            string url = "api/questions";
            var createCommand = new CreateQuestionCommand()
            {
                Title = "TestTitle",
                Content = "TestContent"
            };
            var content = new StringContent(JsonSerializer.Serialize(createCommand), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync( url, content);
            
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }
        
        
        [Fact]
        public async Task UpdateQuestion_ValidData_ReturnStatusContentCreated()
        {
            await Auth();
            string url = "api/questions/-2";
            var createCommand = new UpdateQuestionCommand()
            {
                Id =  -2,
                Title = "NewTestTitle",
                Content = "NewTestContent"
            };
            var content = new StringContent(JsonSerializer.Serialize(createCommand), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(url, content);

            Assert.True(response.StatusCode == HttpStatusCode.NoContent);
        }
        
        
    }
}