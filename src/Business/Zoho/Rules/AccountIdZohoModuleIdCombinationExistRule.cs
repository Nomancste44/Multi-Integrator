using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ZohoToInsightIntegrator.Contract.Contracts;

namespace ZohoToInsightIntegrator.Business.Zoho.Rules
{
    public class AccountIdZohoModuleIdCombinationExistRule : IBusinessRule
    {
        private readonly IUniquenessChecker _uniquenessChecker;
        private readonly string _accountId;
        private readonly DataTable _mappedModules;

       internal AccountIdZohoModuleIdCombinationExistRule(
            IUniquenessChecker uniquenessChecker,
            string accountId,
            DataTable mappedModules)
        {
            _uniquenessChecker = uniquenessChecker;
            _accountId = accountId;
            _mappedModules = mappedModules;
        }
        public bool IsBroken() => _uniquenessChecker.IsModuleMapExist(_accountId,_mappedModules);

        public string Message => "ZohoModuleId Already Exists";
    }
}
