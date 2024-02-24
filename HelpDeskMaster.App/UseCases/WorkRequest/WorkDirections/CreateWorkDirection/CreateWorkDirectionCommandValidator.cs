using FluentValidation;
using HelpDeskMaster.App.Exceptions;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.CreateWorkDirection
{
    internal class CreateWorkDirectionCommandValidator : AbstractValidator<CreateWorkDirectionCommand>
    {
        public CreateWorkDirectionCommandValidator()
        {
            RuleFor(x => x.Title).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(ValidationErrorCode.Empty)
                .MaximumLength(75).WithMessage(ValidationErrorCode.TooLong)
                .Custom(containsLetters);
        }

        private void containsLetters(string value, ValidationContext<CreateWorkDirectionCommand> context)
        {
            if (!value.Any(char.IsLetter))
            {
                context.AddFailure("Title", ValidationErrorCode.RequireLetters);
            }
        }
    }
}
