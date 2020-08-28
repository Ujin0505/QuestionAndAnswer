using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionAndAnswer.Application.Answers.Models;
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
        private readonly IDateTimeService _dateTimeService;

        public GetQuestionQueryHandler(IMediator mediator, ApplicationDbContext dbContext, IDateTimeService dateTimeService)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            _dateTimeService = dateTimeService;
        }
        
        public async Task<QuestionResponce> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Questions
                .Include(q => q.Answers)
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == request.Id,cancellationToken);
            
            if (result == null)
                return null;
            
            var response = new QuestionResponce()
            {
                Id = result.Id,
                Title = result.Title,
                Content = result.Content,
                UserName = result.UserName,
                DateCreated = _dateTimeService.ToResponceFormat( result.Created),
                Answers = result.Answers.Select( a =>  new AnswerResponce()
                {
                    Id = a.Id,
                    Content =  a.Content,
                    Created  = _dateTimeService.ToResponceFormat(a.Created),
                    QuestionId = a.QuestionId,
                    UserName = a.UserName
                })
            };

            await _mediator.Publish(new GetQuestionNotification() {QuestionId = response.Id}, cancellationToken);
            
            return response;
        }
    }
}
