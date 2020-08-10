using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Questions.Commands
{
    public class UpdateQuestionCommand: IRequest<QuestionResponce>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, QuestionResponce>
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;

        public UpdateQuestionCommandHandler(IMediator mediator, ApplicationDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public Task<QuestionResponce> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var result = _dbContext.Questions.FirstOrDefault(q => q.Id == request.Id);
            if (result == null)
                return Task.FromResult<QuestionResponce>(null);

            result.Title = request.Title;
            result.Content = request.Content;

            if (_dbContext.SaveChanges() == 1)
            {
                var responce = new QuestionResponce
                {
                    Id = result.Id,
                    Title = result.Title,
                    Content = result.Content,
                    DateCreated = result.Created.ToLongDateString(),
                    UserName = result.UserName
                };
                return Task.FromResult<QuestionResponce>(responce);
            }
            
            return Task.FromResult<QuestionResponce>(null);
        }
    }
    
}