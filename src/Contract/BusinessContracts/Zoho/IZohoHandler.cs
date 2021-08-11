using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.ModuleMapping;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.Field;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.SetConnection;

namespace ZohoToInsightIntegrator.Contract.BusinessContracts.Zoho
{
    public interface IZohoHandler
    {
        Task<bool> SetZohoConnectionAsync(SetZohoConnectionCommand command, CancellationToken cancellationToken);
        Task<IEnumerable<MappedModuleDto>> GetZohoAvailableModulesAsync(string accountId, CancellationToken cancellationToken);
        Task<IEnumerable<Section>> GetZohoModuleFieldsAsync(string accountId,
            string zohoModuleId, string moduleApiName, CancellationToken cancellationToken);

    }
}
