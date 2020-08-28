using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Data.Entities;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Questions.Commands
{
    public class CreateQuestionCommand: IRequest<QuestionResponce>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        //public int UserId { get; set; }
        //public string UserName { get; set; }
    }
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, QuestionResponce>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public CreateQuestionCommandHandler(ApplicationDbContext dbContext, IMediator mediator, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }
        
        public async Task<QuestionResponce> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            string userName = await _currentUserService.GetName();
            
            var question = new Question()
            {
                Title = request.Title,
                Content = request.Content,
                UserId = _currentUserService.UserId,
                UserName = userName,
                Created = _dateTimeService.DateTimeNow
            };
            
            _dbContext.Add(question);
            var added = await _dbContext.SaveChangesAsync() == 1;
            
            if (added)
            {
                var result = new QuestionResponce()
                {
                    Id = question.Id,
                    Title = question.Title,
                    Content = question.Content,
                    DateCreated = question.Created.ToString("yyyy-MM-ddTHH:mm:ss"),
                    UserName = question.UserName,
                };

                await _mediator.Publish(result, cancellationToken);
                
                return result;
            }
            else
                return null;
        }
    }
}