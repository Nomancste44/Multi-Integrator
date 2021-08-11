using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ZohoToInsightIntegrator.Api.Configuration.Authentication;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Mapping;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.ModuleMapping;
using ZohoToInsightIntegrator.Contract.Utility;


namespace ZohoToInsightIntegrator.Api.Controllers.Integrator
{
    [Route("api/[controller]")]
    [ApiController]
    [HasPermission(PolicyClaimTypes.Admin)]
    public class IntegratorModuleMappingController : ControllerBase
    {
        private readonly IModuleMappingHandler _moduleMappingHandler;

        public IntegratorModuleMappingController(IModuleMappingHandler moduleMappingHandler)
        {
            _moduleMappingHandler = moduleMappingHandler;
        }

        [HttpPost(nameof(SaveMappedModules))]
        public async Task<IActionResult> SaveMappedModules(string accountId,
            List<ModuleMappingCommand> moduleMappingCommands,
            CancellationToken cancellationToken)
        {
            await _moduleMappingHandler.SaveMappedModulesAsync(accountId,
                moduleMappingCommands, cancellationToken);
            return Ok(HttpStatusCode.Created);
        }

        [HasPermission(PolicyClaimTypes.ViewOnly)]
        [HttpGet(nameof(GetMappedModules))]
        public async Task<IEnumerable<MappedModuleDto>> GetMappedModules(string accountId, CancellationToken cancellationToken)
        {
            return await _moduleMappingHandler.GetMappedModules(accountId, cancellationToken);
        }
    }
}
