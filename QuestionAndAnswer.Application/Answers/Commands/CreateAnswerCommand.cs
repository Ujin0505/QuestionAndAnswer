using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QuestionAndAnswer.Application.Answers.Models;
using QuestionAndAnswer.Application.Common.Handlers;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Data.Entities;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Answers.Commands
{
    public class CreateAnswerCommand: IRequest<AnswerDto>
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        //public int UserId { get; set; }
        //public string UserName { get; set; }
    }
    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, AnswerDto>
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public CreateAnswerCommandHandler(IMediator mediator, ApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }
        
        public async Task<AnswerDto> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = new Answer()
            {
                QuestionId =  request.QuestionId,
                Content = request.Content,
                UserId =  _currentUserService.UserId,
                UserName = await _currentUserService.GetName(),
                Created = DateTime.Now
            };
            
            _dbContext.Add(answer);
            var added = await _dbContext.SaveChangesAsync();
            if (added == 1)
            {
                var result = new AnswerDto()
                {
                    Id = answer.Id,
                    Content = answer.Content,
                    Created = answer.Created.ToLongDateString(),
                    UserName = answer.UserName,
                    QuestionId = answer.QuestionId
                };

                await _mediator.Publish(new CreateAnswerNotification() { QuestionId = result.QuestionId}, cancellationToken);
                return result;
            }
            else
                return null;
            
            
        }
    }

    
}