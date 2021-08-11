using FluentValidation;
using System.Net;
using ZohoToInsightIntegrator.Contract.Contracts;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.User
{
    public class SendPasswordCommandValidator : AbstractValidator<SendPasswordCommand>
    {
        public SendPasswordCommandValidator(IUniquenessChecker uniquenessChecker)
        {
            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .Must(email =>
                    !uniquenessChecker.IsUniqueValue($"{nameof(Models.User)}s", nameof(Models.User.Email),
                        email))
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("{PropertyName} is invalid");
        }
    }
}
