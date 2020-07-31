using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionAndAnswer.Application.Answers.Models;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Answers.Queries
{
    public class GetAnswerQuery: IRequest<AnswerResponce>
    {
        public int Id { get; }

        public GetAnswerQuery(int id)
        {
            Id = id;
        }
    }

    public class GetAnswerQueryHandler : IRequestHandler<GetAnswerQuery, AnswerResponce>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAnswerQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<AnswerResponce> Handle(GetAnswerQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Answers.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (result == null)
                return null;

            return new AnswerResponce()
            {
                Id =  result.Id,
                QuestionId =  result.QuestionId,
                Created = result.Created.ToLongDateString(),
                Content = result.Content,
                UserName = result.UserName
            };
        }
    }
    
}