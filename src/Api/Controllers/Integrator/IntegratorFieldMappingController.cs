using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ZohoToInsightIntegrator.Api.Configuration.Authentication;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Mapping;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.FieldMapping;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Api.Controllers.Integrator
{
    [Route("api/[controller]")]
    [ApiController]
    [HasPermission(PolicyClaimTypes.Admin)]
    public class IntegratorFieldMappingController : ControllerBase
    {
        private readonly IFieldMappingHandler _fieldMappingHandler;
        public IntegratorFieldMappingController(IFieldMappingHandler fieldMappingHandler)
        {
            _fieldMappingHandler = fieldMappingHandler;
        }

        [HasPermission(PolicyClaimTypes.ViewOnly)]
        [HttpGet(nameof(GetMappedModuleName))]
        public async Task<IEnumerable<MappedModuleNameDto>> GetMappedModuleName(string accountId, CancellationToken cancellationToken)
        {
            return await _fieldMappingHandler.GetMappedModuleName(accountId, cancellationToken);
        }
    }
}
