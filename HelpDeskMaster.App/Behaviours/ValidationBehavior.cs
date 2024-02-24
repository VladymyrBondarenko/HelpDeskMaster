﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace HelpDeskMaster.App.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationFailures = await Task.WhenAll(
                    _validators.Select(validator => validator.ValidateAsync(context)));

                var errors = validationFailures
                    .Where(validationResult => !validationResult.IsValid)
                    .SelectMany(validationResult => validationResult.Errors)
                    .Select(validationFailure => new ValidationFailure(
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage))
                    .ToList();

                if (errors.Any())
                {
                    throw new ValidationException(errors);
                }
            }

            return await next();
        }
    }
}
