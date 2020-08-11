using FluentValidation;
using QuestionAndAnswer.Application.Answers.Commands;

namespace QuestionAndAnswer.Application.Answers.Validators
{
    public class CreateAnswerCommandValidator: AbstractValidator<CreateAnswerCommand>
    {
        public CreateAnswerCommandValidator()
        {
            RuleFor(c => c.QuestionId).NotEmpty();
            RuleFor(c => c.Content).NotEmpty().WithMessage("Please include some content for question");
        }
    }
}