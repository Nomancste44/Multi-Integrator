using FluentValidation;
using System.Net;
using System.Text.RegularExpressions;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.User
{

    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator(IUniquenessChecker uniqueness)
        {

            RuleFor(c => c.UserId)
             .Cascade(CascadeMode.Stop)
             .NotNull()
             .NotEmpty()
             .Matches(new Regex(Common.GuidRegexPattern))
             .Must(userId =>!uniqueness
                 .IsUniqueValue($"{nameof(Models.User)}s",nameof(Models.User.UserId),userId))
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

        }
    }
}
