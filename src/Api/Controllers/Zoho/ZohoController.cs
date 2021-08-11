using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Api.Configuration.Authentication;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Zoho;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.ModuleMapping;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.SetConnection;
using ZohoToInsightIntegrator.Contract.Utility;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.Field;

namespace ZohoToInsightIntegrator.Api.Controllers.Zoho
{
    [Route("api/[controller]")]
    [ApiController]
    [HasPermission(PolicyClaimTypes.Admin)]
    public class ZohoController : ControllerBase
    {
        private readonly IZohoHandler _zohoAccountHandler;

        public ZohoController(IZohoHandler zohoAccountHandler)
        {
            _zohoAccountHandler = zohoAccountHandler;
        }

        [HttpPost(nameof(SetZohoConnection))]
        public async Task<bool> SetZohoConnection(
            [FromBody] SetZohoConnectionCommand command, CancellationToken cancellationToken)
        {
            return await _zohoAccountHandler
                .SetZohoConnectionAsync(command, cancellationToken);
        }

        [HttpGet(nameof(GetZohoAvailableModules))]
        public async Task<IEnumerable<MappedModuleDto>> GetZohoAvailableModules(string accountId,
            CancellationToken cancellationToken)
        {
            return await _zohoAccountHandler.GetZohoAvailableModulesAsync(accountId, cancellationToken);
        }

        [HttpGet("/GetZohoModuleFields/{moduleApiName}")]
        public async Task<IEnumerable<Section>> GetZohoModuleFields(string accountId, string zohoModuleId,
            string moduleApiName, CancellationToken cancellationToken)
        {
            return await _zohoAccountHandler.GetZohoModuleFieldsAsync(accountId,
                zohoModuleId, moduleApiName, cancellationToken);
        }

    }
}
