using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionAndAnswer.Application.Common.Handlers;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Questions.Queries
{
    public class GetQuestionQuery: IRequest<QuestionResponce>
    {
        public int Id { get; }
        public GetQuestionQuery(int id)
        {
            Id = id;
        }
    }
    public class GetQuestionQueryHandler: IRequestHandler<GetQuestionQuery, QuestionResponce>
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;

        public GetQuestionQueryHandler(IMediator mediator, ApplicationDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }
        
        public async Task<QuestionResponce> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Id == request.Id,cancellationToken);
            if (result == null)
                return null;
            
            var responce = new QuestionResponce()
            {
                Id = result.Id,
                Title = result.Title,
                Content = result.Content,
                UserName = result.UserName,
                DateCreated = result.Created.ToLongDateString()
            };

            await _mediator.Publish(new GetQuestionNotification() {QuestionId = responce.Id}, cancellationToken);
            
            return responce;
        }
    }
}
