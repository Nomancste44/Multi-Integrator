using FluentValidation;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(c => c.RefreshToken)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} can't be null or empty");
        }
    }
}
