using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuestionAndAnswer.Application.Answers.Models;
using QuestionAndAnswer.Data.Entities;

namespace QuestionAndAnswer.Application.Models
{
    public class QuestionDto: INotification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string DateCreated { get; set; }

        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}