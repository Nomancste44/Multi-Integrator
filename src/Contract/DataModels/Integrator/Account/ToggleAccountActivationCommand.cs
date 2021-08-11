using System;
using System.Collections.Generic;
using System.Text;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account
{
    public class ToggleAccountActivationCommand
    {
        public string AccountId { get; set; }

        public bool IsActive { get; set; }
    }
}
