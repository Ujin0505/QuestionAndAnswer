using System.Threading.Tasks;

namespace QuestionAndAnswer.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        Task<string> GetName();
    }
}