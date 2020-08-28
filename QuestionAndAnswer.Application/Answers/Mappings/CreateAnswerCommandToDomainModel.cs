using AutoMapper;
using QuestionAndAnswer.Application.Answers.Commands;
using QuestionAndAnswer.Data.Entities;

namespace QuestionAndAnswer.Application.Answers.Mappings
{
    public class CreateAnswerCommandToDomainModel: Profile
    {
        public CreateAnswerCommandToDomainModel()
        {
            CreateMap<CreateAnswerCommand, Answer>();
        }
    }
}