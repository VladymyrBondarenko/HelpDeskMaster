using FluentValidation;
using HelpDeskMaster.App.Exceptions;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkDirections.DeleteWorkDirection
{
    internal class DeleteWorkDirectionCommandValidator : AbstractValidator<DeleteWorkDirectionCommand>
    {
        public DeleteWorkDirectionCommandValidator()
        {
            RuleFor(x => x.WorkDirectionId)
                .NotEmpty().WithMessage(ValidationErrorCode.Empty);
        }
    }
}
