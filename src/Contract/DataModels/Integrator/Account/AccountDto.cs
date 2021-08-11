using System;
using System.Collections.Generic;
using System.Text;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account
{
    public class AccountDto
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public bool IsActive { get; set; }
    }

    public class ActiveAccountDto
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
    }
}
