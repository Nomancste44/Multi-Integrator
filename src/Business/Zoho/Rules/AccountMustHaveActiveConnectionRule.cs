using System;
using System.Collections.Generic;
using System.Text;
using ZohoToInsightIntegrator.Contract.Contracts;

namespace ZohoToInsightIntegrator.Business.Zoho.Rules
{
    public class AccountMustHaveActiveConnectionRule : IBusinessRule
    {
        private readonly IUniquenessChecker _uniquenessChecker;
        private readonly string _accountId;

        internal AccountMustHaveActiveConnectionRule(
            IUniquenessChecker uniquenessChecker,
            string accountId)
        {
            _uniquenessChecker = uniquenessChecker;
            _accountId = accountId;
        }

        public bool IsBroken() => !_uniquenessChecker.IsConnectionActive(_accountId);

        public string Message => "Account doesn't have active connection";
    }
}
