using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Data.Entities;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Questions.Commands
{
    public class DeleteQuestionCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;

        public DeleteQuestionCommandHandler(IMediator mediator, ApplicationDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }
        
        public async Task<bool> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var result = _dbContext.Questions.FirstOrDefault(q => q.Id == request.Id);
            if (result == null)
                return false;

            _dbContext.Remove(result);
            bool isSaved = await _dbContext.SaveChangesAsync() == 1;
            return isSaved;
        }
    }
}