using Microsoft.AspNetCore.Http;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Api.Configuration.ProblemDetails
{
    public class BusinessRuleValidationExceptionProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            Title = "Business rule broken";
            Status = StatusCodes.Status409Conflict;
            Detail = exception.Message;
            Type = "https://somedomain/business-rule-validation-error";
        }
    }
}