using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.FieldMapping;


namespace ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Mapping
{
    public interface IFieldMappingHandler
    {
        Task<IEnumerable<MappedModuleNameDto>> GetMappedModuleName(string accountId,
            CancellationToken cancellationToken);
    }
}
