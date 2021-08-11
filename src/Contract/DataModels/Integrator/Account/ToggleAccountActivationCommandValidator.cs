using FluentValidation;
using System.Net;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account
{
    public class ToggleAccountActivationCommandValidator : AbstractValidator<ToggleAccountActivationCommand>
    {
        public ToggleAccountActivationCommandValidator()
        {
            RuleFor(c => c.AccountId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("{PropertyValue} isn't valid {PropertyName}");
        }
    }
}
