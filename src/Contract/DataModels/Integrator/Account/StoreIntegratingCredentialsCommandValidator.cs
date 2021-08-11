using System;
using FluentValidation;
using System.Net;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account
{
    public class StoreIntegratingCredentialsCommandValidator
    : AbstractValidator<StoreIntegratingCredentialsCommand>
    {
        public StoreIntegratingCredentialsCommandValidator(IUniquenessChecker uniquenessChecker)
        {
            this.RuleFor(c => c.AccountId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Must(accountId => !uniquenessChecker.IsUniqueValue(
                    $"{nameof(Models.Account)}s",
                    nameof(Models.Account.AccountId), accountId))
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("This account doesn't exist");

            this.RuleFor(c => c.ZohoClientId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            this.RuleFor(c => c.ZohoClientSecret)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            this.RuleFor(c => c.ZohoTimeZone)
                .Cascade(CascadeMode.Stop)
                .Must(timezone =>
                {
                    try { return TimeZoneInfo.FindSystemTimeZoneById(timezone).Id == timezone; }
                    catch { return false; }
                })
                .WithMessage("{PropertyValue} isn't valid time zone")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            this.RuleFor(c => c.InsightClientId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            this.RuleFor(c => c.InsightClientSecret)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());


            this.RuleFor(c => c.InsightTimeZone)
           .Cascade(CascadeMode.Stop)
           .Must(timezone =>
           {
               try { return TimeZoneInfo.FindSystemTimeZoneById(timezone).Id == timezone; }
               catch { return false; }
           })
           .WithMessage("{PropertyValue} isn't valid time zone")
           .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            this.RuleFor(c => c.InsightApiDomain)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Url()
                .WithMessage("{PropertyValue} is not valid URI")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            this.RuleFor(c => c.NofityEmail)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());
        }
    }
}