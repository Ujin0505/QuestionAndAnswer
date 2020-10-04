using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Data.Entities;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Application.Questions.Commands
{
    public class CreateQuestionCommand: IRequest<QuestionDto>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        //public int UserId { get; set; }
        //public string UserName { get; set; }
    }
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, QuestionDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CreateQuestionCommandHandler(ApplicationDbContext dbContext, IMediator mediator, ICurrentUserService currentUserService, IMapper mapper)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        
        public async Task<QuestionDto> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            string userName = await _currentUserService.GetName();
            
            var question = new Question()
            {
                Title = request.Title,
                Content = request.Content,
                UserId = _currentUserService.UserId,
                UserName = userName,
                Created = DateTime.UtcNow
            };
            
            _dbContext.Add(question);
            var added = await _dbContext.SaveChangesAsync() == 1;
            
            if (added)
            {
                var result = _mapper.Map<QuestionDto>(question);
                await _mediator.Publish(result, cancellationToken);
                
                return result;
            }
            else
                return null;
        }
    }
}