﻿﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using QuestionAndAnswer.Application.Common.Interfaces;

namespace QuestionAndAnswer.Application.Common.Behaviours
{
    
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            
            _logger.LogInformation($"Request: {requestName} {userId}{request}",
                requestName, userId, request);
            
            return Unit.Task;
        }
    }
}