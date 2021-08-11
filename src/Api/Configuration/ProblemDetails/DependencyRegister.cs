using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Api.Configuration.ProblemDetails
{
    public static class DependencyRegister
    {
        public static IServiceCollection AddProblemDetailsDependency(
            this IServiceCollection services)
        {
            services.AddProblemDetails(opt =>
            {
                opt.IncludeExceptionDetails = (ctx, ex) =>
                {
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment() || env.IsStaging();
                };
                opt.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));

            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new CommandValidationErrorDetails()
                    {
                        Title = "Command validation error",
                        Status = StatusCodes.Status400BadRequest,
                        Type = "https://somedomain/validation-error",
                        Detail = context
                            .ModelState
                            .Values.SelectMany(v => v.Errors).First().ErrorMessage
                    };
                    return new BadRequestObjectResult(problemDetails);
                };
            });
            return services;
        }
    }
}
