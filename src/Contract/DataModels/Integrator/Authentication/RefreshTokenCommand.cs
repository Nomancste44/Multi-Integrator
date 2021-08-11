using Microsoft.AspNetCore.Mvc;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication
{
    public class RefreshTokenCommand
    {
        [FromHeader]
        public string RefreshToken { get; set; }
    }
}
