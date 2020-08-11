using FluentValidation;
using QuestionAndAnswer.Application.Questions.Commands;

namespace QuestionAndAnswer.Application.Questions.Validators
{
    public class CreateQuestionCommandValidator: AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty().MaximumLength(100);
            RuleFor(c => c.Content).NotEmpty().WithMessage("Please include some content for question");
        }
    }
}