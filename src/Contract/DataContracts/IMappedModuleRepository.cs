using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.ModuleMapping;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.DataContracts
{
    public interface IMappedModuleRepository : IRepository<MappedModule>
    {
        Task<IEnumerable<MappedModule>> GetMappedModulesByAccountId(string accountId,
            CancellationToken cancellationToken); 
        Task<IEnumerable<MappedModuleDto>> GetMappedModules(string accountId, 
            CancellationToken cancellationToken);
        Task InsertMappedModules(string accountId, DataTable mappedModules);
    }
}
