using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using FluentValidation;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.ModuleMapping
{
    public class ModuleMappingCommandValidator : AbstractValidator<ModuleMappingCommand>
    {
        public ModuleMappingCommandValidator()
        {

            RuleFor(c => c.ZohoModuleId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} can't null or empty")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());

            RuleFor(c => c.InsightModuleId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} can't null or empty")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());
        }
    }
}
