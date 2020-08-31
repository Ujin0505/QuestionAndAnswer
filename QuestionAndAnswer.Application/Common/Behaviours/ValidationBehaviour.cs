using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace QuestionAndAnswer.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        private IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var results = _validators.Select(v => v.Validate(context));
                var failures = results.SelectMany(v => v.Errors).Where(v => v != null).ToList();
                
                if (failures.Count > 0)
                {
                    throw new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}