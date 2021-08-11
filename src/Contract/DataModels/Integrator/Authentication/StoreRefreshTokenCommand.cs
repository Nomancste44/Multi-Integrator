using System;
using System.Collections.Generic;
using System.Text;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication
{
    public class StoreRefreshTokenCommand
    {
        public string Email { get; set; }
        public string OldRefreshToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
