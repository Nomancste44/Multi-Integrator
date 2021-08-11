using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataModels.Insight.Module;

namespace ZohoToInsightIntegrator.Contract.BusinessContracts.Insight
{
    public interface IInsightHandler
    {
        Task<string> GetAllData(string accountId, string moduleName, CancellationToken cancellationToken);
        Task<IEnumerable<InsightAvailableModuleDto>> GetAvailableInsightModules(CancellationToken cancellationToken);
    }
}
