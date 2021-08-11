using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.ModuleMapping;

namespace ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Mapping
{
    public interface IModuleMappingHandler
    {
        Task SaveMappedModulesAsync(string accountId,
            List<ModuleMappingCommand> moduleMappingCommands, CancellationToken cancellationToken);
        Task<IEnumerable<MappedModuleDto>> GetMappedModules(string accountId, CancellationToken cancellationToken);
    }
}
