using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Questions.Queries
{
    public class GetQuestionsQuery: IRequest<IEnumerable<QuestionResponce>>
    {
    }
    public class GetQuestionsQueryHandler: IRequestHandler<GetQuestionsQuery,IEnumerable<QuestionResponce>>
    {
        private readonly ApplicationDbContext _dbContext;
        
        public GetQuestionsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<QuestionResponce>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Questions.AsNoTracking().ToListAsync();
            var responce = result.Select(q => new QuestionResponce()
            {
                Id = q.Id,
                Title =  q.Title,
                Content =  q.Content,
                DateCreated = q.Created.ToLongDateString(),
                UserName = q.UserName
            });
            return responce;
        }
    }
}
