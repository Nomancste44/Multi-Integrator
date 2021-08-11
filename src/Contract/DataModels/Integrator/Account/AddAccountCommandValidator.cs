using FluentValidation;
using System.Net;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account
{
    public class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
    {
        public AddAccountCommandValidator(IUniquenessChecker uniquenessChecker)
        {
            RuleFor(c => c.AccountName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} can't be null or empty")
                .Must(account => uniquenessChecker.IsUniqueValue(
                    $"{nameof(Models.Account)}s",
                    nameof(Models.Account.AccountName), account))
                .WithMessage("{PropertyValue} is already exist")
                .Must(accountName => accountName != Common.DefaultAccountName)
                .WithMessage("{PropertyValue} is not allowed")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

        }
    }
}
