using System.Threading.Tasks;
using MediatR;
using QuestionAndAnswer.Application.Models;

namespace QuestionAndAnswer.Application.Common.Interfaces
{
    public interface IHubService
    {
        Task SendAsync(int questionId);
    }
}