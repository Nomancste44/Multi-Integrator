using System;
using ZohoToInsightIntegrator.Contract.Contracts;

namespace ZohoToInsightIntegrator.Contract.Utility
{
    public class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(IBusinessRule brokenRule)
            :base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
            this.Details = brokenRule.Message;
        }

        public IBusinessRule BrokenRule { get;}

        public string Details { get;}
        public override string ToString() =>  $"{BrokenRule.GetType().FullName} : {BrokenRule.Message}";
        
    }

}
