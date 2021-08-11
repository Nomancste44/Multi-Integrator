using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.Field;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.Module;


namespace ZohoToInsightIntegrator.Contract.ZohoClientContracts
{
    public interface IZohoResourceApi
    {
        Task<List<ZohoAvailableModuleDto>> GetZohoAvailableModulesAsync(string accountId, 
            CancellationToken cancellationToken);
        Task<IEnumerable<Section>> GetZohoModuleFieldsAsync(string accountId,
            string moduleApiName, CancellationToken cancellationToken);
    }
}
