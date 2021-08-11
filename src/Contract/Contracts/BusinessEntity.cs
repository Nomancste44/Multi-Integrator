using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Contract.Contracts
{
   
    public abstract class BusinessEntity
    {
        /// <summary>
        /// Check the business rule, using this method
        /// </summary>
        /// <param name="rule"> Rule</param>
        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken()) throw new BusinessRuleValidationException(rule);
        }
    }
}
