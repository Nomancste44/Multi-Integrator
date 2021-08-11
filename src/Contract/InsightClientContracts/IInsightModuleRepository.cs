using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Insight.Module;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.InsightClientContracts
{
    public interface IInsightModuleRepository : IRepository<InsightModule>
    {
        Task<IEnumerable<InsightAvailableModuleDto>> GetAvailableInsightModules(CancellationToken cancellationToken);
    }
}
