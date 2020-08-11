﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QuestionAndAnswer.Application.Answers.Models;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Data.Entities;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Answers.Commands
{
    public class CreateAnswerCommand: IRequest<AnswerResponce>
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        //public int UserId { get; set; }
        //public string UserName { get; set; }
    }
    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, AnswerResponce>
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;

        public CreateAnswerCommandHandler(IMediator mediator, ApplicationDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }
        
        public async Task<AnswerResponce> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = new Answer()
            {
                QuestionId =  request.QuestionId,
                Content = request.Content,
                UserId = /*request.UserId*/ 1,
                UserName = /*request.UserName*/ "user@qanda.ru",
                Created = DateTime.Now
            };
            
            _dbContext.Add(answer);
            var added = await _dbContext.SaveChangesAsync();
            if (added == 1)
            {
                var result = new AnswerResponce()
                {
                    Id = answer.Id,
                    Content = answer.Content,
                    Created = answer.Created.ToLongDateString(),
                    UserName = answer.UserName,
                    QuestionId = answer.QuestionId
                };
                return result;
            }
            else
                return null;
        }
    }
}