using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;

        public CreateQuestionCommandHandler(IMediator mediator, ApplicationDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }
        
        public async Task<QuestionResponce> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Question()
            {
                Title = request.Title,
                Content = request.Content,
                UserId = /*request.UserId*/ 1,
                UserName = "user@qanda.ru",
                Created = DateTime.Now
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
                    DateCreated = question.Created.ToLongDateString(),
                    UserName = question.UserName,
                };
                return result;
            }
            else
                return null;
        }
    }
}