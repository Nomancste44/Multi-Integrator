using FluentValidation;
using System.Net;
using System.Text.RegularExpressions;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.User
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator(IUniquenessChecker uniquenessChecker)
        {
            RuleFor(c => c.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("{PropertyValue} isn't a valid Email")
                .Must(email =>
                    uniquenessChecker.IsUniqueValue($"{nameof(Models.User)}s", nameof(Models.User.Email),
                        email))
                .WithMessage("{PropertyValue} is already exist")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            RuleFor(c => c.RoleId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Matches(new Regex(Common.GuidRegexPattern))
                .WithMessage("{PropertyValue} isn't a valid Guid")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            RuleFor(c => c.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Your password can't be null.")
                .NotEmpty()
                .WithMessage("Your password can't be empty.")
                .MinimumLength(16)
                .WithMessage("Your password length must be at least 16.")
                .Matches(@"[A-Z]+")
                .WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+")
                .WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+")
                .WithMessage("Your password must contain at least one number.")
                .Matches(@"[!@#$%^&*\(\)_\+\-\={}<>,\.\|'~`:;\\?\/\[\]]+")
                .WithMessage("Your password must contain at least one special (!@#$%^&*()_+-+{}<>,|'~`:;?[] ) character.");


            RuleFor(c => c.AccountId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Matches(new Regex(Common.GuidRegexPattern))
                .WithMessage("{PropertyValue} isn't a valid Guid")
                .Must(accountId =>
                    !uniquenessChecker.IsUniqueValue($"{nameof(Models.Account)}s", nameof(Models.Account.AccountId),
                        accountId))
                .WithMessage("Account doesn't in exist")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());
        }
    }
}
