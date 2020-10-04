using AutoMapper;
using QuestionAndAnswer.Application.Answers.Models;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Data.Entities;

namespace QuestionAndAnswer.Application.Answers.Mappings
{
    public class AnswerToAnswerDto: Profile
    {
        public AnswerToAnswerDto()
        {
            CreateMap<Answer, AnswerDto>();
            /*.ForMember(to => to.Created, map => map.MapFrom(from => from.Created.ToString()));*/
        }
    }
}