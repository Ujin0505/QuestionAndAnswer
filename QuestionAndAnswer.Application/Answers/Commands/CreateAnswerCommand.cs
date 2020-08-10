using System;
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
        public int UserId { get; set; }
        public string UserName { get; set; }
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
        
        public Task<AnswerResponce> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = new Answer()
            {
                QuestionId =  request.QuestionId,
                Content = request.Content,
                UserId = request.UserId,
                UserName = request.UserName,
                Created = DateTime.Now
            };
            
            _dbContext.Add(answer);
            if (_dbContext.SaveChanges() == 1)
            {
                var result = new AnswerResponce()
                {
                    Id = answer.Id,
                    Content = answer.Content,
                    Created = answer.Created.ToLongDateString(),
                    UserName = answer.UserName,
                    QuestionId = answer.QuestionId
                };
                return Task.FromResult(result);
            }
            else
                return Task.FromResult<AnswerResponce>(null);
        }
    }
}