using System.Net;
using FluentValidation;
using ZohoToInsightIntegrator.Contract.Contracts;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication
{
    public class AuthenticateUserCommandValidator
        : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator(IUniquenessChecker uniquenessChecker)
        {
            RuleFor(c => c.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("{PropertyValue} isn't a valid Email")
                .Must(email =>
                    !uniquenessChecker.IsUniqueValue($"{nameof(Models.User)}s", nameof(Models.User.Email),
                        email))
                .WithMessage("{PropertyValue} doesn't exist")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            RuleFor(c => c.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Your password can't be null.")
                .NotEmpty()
                .WithMessage("Your password can't be empty.");
        }
    }
}
