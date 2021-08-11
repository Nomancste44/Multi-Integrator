using System.Net;
using FluentValidation;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.DataModels.Zoho.SetConnection
{
    public class SetZohoConnectionCommandValidator 
        :AbstractValidator<SetZohoConnectionCommand>
    {
        public SetZohoConnectionCommandValidator(IUniquenessChecker uniquenessChecker)
        {

            RuleFor(c => c.AccountId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .Must(accountId=>!uniquenessChecker
                    .IsUniqueValue($"{nameof(IntegratingCredential)}s", 
                        nameof(IntegratingCredential.AccountId),accountId))
                .WithMessage("Account doesn't have credentials")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            RuleFor(c => c.code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} can't null or empty")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

        }
    }
}
