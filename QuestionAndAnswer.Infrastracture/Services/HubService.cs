using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Infrastracture.Hubs;

namespace QuestionAndAnswer.Infrastracture.Services
{
    public class HubService: IHubService
    {
        private readonly IHubContext<QuestionsHub> _hubContext;

        public HubService(IHubContext<QuestionsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task SendAsync(int questionId)
        {
            return _hubContext.Clients
                .Group($"Question-{questionId}")
                .SendAsync("ReceiveQuestion", questionId);
        }
    }
}