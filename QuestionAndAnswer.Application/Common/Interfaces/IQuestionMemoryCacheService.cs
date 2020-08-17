using QuestionAndAnswer.Application.Models;

namespace QuestionAndAnswer.Application.Common.Interfaces
{
    public interface IQuestionMemoryCacheService
    {
        QuestionResponce Get(int questionId);     
        void Remove(int questionId);     
        void Set(QuestionResponce questionResponce); 
    }
}