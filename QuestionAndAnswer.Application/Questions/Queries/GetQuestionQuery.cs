using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionAndAnswer.Application.Answers.Models;
using QuestionAndAnswer.Application.Common.Handlers;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Questions.Queries
{
    public class GetQuestionQuery: IRequest<QuestionDto>
    {
        public int Id { get; }
        public GetQuestionQuery(int id)
        {
            Id = id;
        }
    }
    public class GetQuestionQueryHandler: IRequestHandler<GetQuestionQuery, QuestionDto>
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetQuestionQueryHandler(IMediator mediator, ApplicationDbContext dbContext, IMapper mapper)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<QuestionDto> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Questions
                .Include(q => q.Answers)
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == request.Id,cancellationToken);
            
            if (result == null)
                return null;

            var response = _mapper.Map<QuestionDto>(result);
            await _mediator.Publish(new GetQuestionNotification() {QuestionId = response.Id}, cancellationToken);
            
            return response;
        }
    }
}
