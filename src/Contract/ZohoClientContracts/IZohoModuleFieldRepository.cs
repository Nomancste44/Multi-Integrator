using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.FieldMapping;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.ZohoClientContracts
{
    public interface IZohoModuleFieldRepository : IRepository<ZohoModuleField>
    {
        Task<IEnumerable<MappedModuleNameDto>> GetMappedModuleName(string accountId,
            CancellationToken cancellationToken);
        Task InsertZohoFields(string accountId,string zohoModuleId, DataTable mappedModules);
    }
}
