using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using QuestionAndAnswer.Application.Answers.Models;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Questions.Queries
{
    public class GetQuestionsQuery: IRequest<IEnumerable<QuestionResponce>>
    {
        public string Search { get; set; }
    }
    public class GetQuestionsQueryHandler: IRequestHandler<GetQuestionsQuery,IEnumerable<QuestionResponce>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDateTimeService _dateTimeService;

        public GetQuestionsQueryHandler(ApplicationDbContext dbContext, IDateTimeService dateTimeService)
        {
            _dbContext = dbContext;
            _dateTimeService = dateTimeService;
        }
        
        public async Task<IEnumerable<QuestionResponce>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions = _dbContext.Questions.Include(q => q.Answers);
            
            var result =  string.IsNullOrEmpty(request.Search) 
                ? await questions.AsNoTracking().ToListAsync(cancellationToken)
                : await questions.Where(q => q.Title.Contains(request.Search)).AsNoTracking().ToListAsync(cancellationToken);
            
            var response = result.Select(q => new QuestionResponce()
            {
                Id = q.Id,
                Title =  q.Title,
                Content =  q.Content,
                DateCreated = _dateTimeService.ToResponceFormat(q.Created),
                UserName = q.UserName,
                Answers = q.Answers.Select( a =>  new AnswerResponce()
                {
                    Id = a.Id,
                    Content =  a.Content,
                    Created  = _dateTimeService.ToResponceFormat(a.Created),
                    QuestionId = a.QuestionId,
                    UserName = a.UserName
                })
                
            });
            return response;
        }
    }
}
