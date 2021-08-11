using FluentValidation;
using System.Net;
using System.Text.RegularExpressions;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.User
{
    public class UpdateUserCommadValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommadValidator(
            IUniquenessChecker uniquenessChecker)
        {

            RuleFor(c => c.UserId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Matches(new Regex(Common.GuidRegexPattern))
                .Must(userId => !uniquenessChecker
                    .IsUniqueValue($"{nameof(Models.User)}s", nameof(Models.User.UserId), userId))
                .WithMessage("{PropertyValue} isn't a valid UserId")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            RuleFor(c => c.RoleId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Matches(new Regex(Common.GuidRegexPattern))
                .WithMessage("{PropertyValue} isn't a valid Guid")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());


            RuleFor(c => c.AccountId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Matches(new Regex(Common.GuidRegexPattern))
                .Must(accountId =>
                    !uniquenessChecker.IsUniqueValue($"{nameof(Models.Account)}s", nameof(Models.Account.AccountId),
                        accountId))
                .WithMessage("Account doesn't in exist")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());
        }

    }
}
