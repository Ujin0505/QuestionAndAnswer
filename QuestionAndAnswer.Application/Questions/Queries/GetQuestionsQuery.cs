using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using QuestionAndAnswer.Application.Answers.Models;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Data.Entities;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Questions.Queries
{
    public class GetQuestionsQuery: IRequest<IEnumerable<QuestionDto>>
    {
        public string Search { get; set; }
    }
    public class GetQuestionsQueryHandler: IRequestHandler<GetQuestionsQuery,IEnumerable<QuestionDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetQuestionsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<QuestionDto>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions = _dbContext.Questions.Include(q => q.Answers);
            
            var result =  string.IsNullOrEmpty(request.Search) 
                ? await questions.AsNoTracking().ToListAsync(cancellationToken)
                : await questions.Where(q => q.Title.Contains(request.Search)).AsNoTracking().ToListAsync(cancellationToken);
            
            var response = _mapper.Map<List<Question>, IEnumerable<QuestionDto>>(result);
            return response;
        }
    }
}
