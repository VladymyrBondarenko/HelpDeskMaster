using FluentValidation;
using HelpDeskMaster.App.Exceptions;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.DeleteWorkCategory
{
    internal class DeleteWorkCategoryCommandValidator : AbstractValidator<DeleteWorkCategoryCommand>
    {
        public DeleteWorkCategoryCommandValidator()
        {
            RuleFor(x => x.WorkCategoryId)
                .NotEmpty().WithMessage(ValidationErrorCode.Empty);
        }
    }
}
