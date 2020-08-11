using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Questions.Commands
{
    public class UpdateQuestionCommand: IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;

        public UpdateQuestionCommandHandler(IMediator mediator, ApplicationDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var result = _dbContext.Questions.FirstOrDefault(q => q.Id == request.Id);
            if (result == null)
                return false;

            result.Title = request.Title;
            result.Content = request.Content;

            var updated = await _dbContext.SaveChangesAsync() == 1;
            return updated;
        }
    }
    
}