using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Data.Entities;

/*using System.Net.Authorization;*/

namespace QuestionAndAnswer.Services
{
    public class CurrentUserService: ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public string UserId => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        public async Task<string> GetName()
        {
            string message = $"{_configuration["Auth0:Authority"]}userInfo";
            var request = new HttpRequestMessage(HttpMethod.Get, message);
            string header = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            request.Headers.Add("Authorization", header);

            var client = _httpClientFactory.CreateClient();
            var responce = await client.SendAsync(request);
            if (responce.IsSuccessStatusCode)
            {
                var data = await responce.Content.ReadAsStringAsync();
                var user =  JsonSerializer.Deserialize<User>(data,
                    new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
                return user.Name;
            }
            return String.Empty;
        }

        
    }
}