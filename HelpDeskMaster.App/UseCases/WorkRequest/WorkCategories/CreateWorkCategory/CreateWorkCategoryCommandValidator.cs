﻿using FluentValidation;
using HelpDeskMaster.App.Exceptions;

namespace HelpDeskMaster.App.UseCases.WorkRequest.WorkCategories.CreateWorkCategory
{
    internal class CreateWorkCategoryCommandValidator : AbstractValidator<CreateWorkCategoryCommand>
    {
        public CreateWorkCategoryCommandValidator()
        {
            RuleFor(x => x.Title).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(ValidationErrorCode.Empty)
                .MaximumLength(75).WithMessage(ValidationErrorCode.TooLong)
                .Custom(containsLetters);
        }

        private void containsLetters(string value, ValidationContext<CreateWorkCategoryCommand> context)
        {
            if (!value.Any(char.IsLetter))
            {
                context.AddFailure("Title", ValidationErrorCode.RequireLetters);
            }
        }
    }
}