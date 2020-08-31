using QuestionAndAnswer.Application.Models;

namespace QuestionAndAnswer.Application.Common.Interfaces
{
    public interface IQuestionMemoryCacheService
    {
        QuestionDto Get(int questionId);     
        void Remove(int questionId);     
        void Set(QuestionDto questionDto); 
    }
}