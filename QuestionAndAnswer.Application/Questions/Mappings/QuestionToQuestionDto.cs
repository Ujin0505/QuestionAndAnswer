using AutoMapper;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Data.Entities;

namespace QuestionAndAnswer.Application.Questions.Mappings
{
    public class QuestionToQuestionDto: Profile
    {
        public QuestionToQuestionDto()
        {
            CreateMap<Question, QuestionDto>();
        }
        
        
    }
}