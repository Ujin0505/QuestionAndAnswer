using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _dbContext;

        public GetQuestionQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<QuestionResponce> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Id == request.Id);
            if (result == null)
                return null;

            return new QuestionResponce()
            {
                Id = result.Id,
                Title = result.Title,
                Content = result.Content,
                UserName = result.UserName,
                DateCreated = result.Created.ToLongDateString()
            };
        }
    }
}
