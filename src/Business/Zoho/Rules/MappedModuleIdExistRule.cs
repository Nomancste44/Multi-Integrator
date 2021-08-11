using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ZohoToInsightIntegrator.Contract.Contracts;

namespace ZohoToInsightIntegrator.Business.Zoho.Rules
{
    public class MappedModuleIdExistRule : IBusinessRule
    {
        private readonly IUniquenessChecker _uniquenessChecker;
        private readonly string _accountId;
        private readonly DataTable _mappedModules;

        internal MappedModuleIdExistRule(
            IUniquenessChecker uniquenessChecker,
            string accountId,
            DataTable mappedModules)
        {
            _uniquenessChecker = uniquenessChecker;
            _accountId = accountId;
            _mappedModules = mappedModules;
        }
        public bool IsBroken() => !_uniquenessChecker.IsMappedModuleIdExist(_accountId, _mappedModules);

        public string Message => "MappedModuleId Does not Exist";
    }
}
