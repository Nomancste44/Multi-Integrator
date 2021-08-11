using FluentValidation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.AutoSchedulerSettings
{
    public class AutoSchedularSettingsCommandValidator : AbstractValidator<AutoSchedularSettingsCommands>
    {
        public AutoSchedularSettingsCommandValidator()
        {
            this.RuleFor(c => c.AccountID)
               .Cascade(CascadeMode.Stop)
               .NotNull()
               .NotEmpty()
               .WithErrorCode(HttpStatusCode.BadRequest.ToString());
        }
    }
}
