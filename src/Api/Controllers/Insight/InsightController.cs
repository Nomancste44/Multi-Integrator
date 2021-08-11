using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ZohoToInsightIntegrator.Api.Configuration.Authentication;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Insight;
using ZohoToInsightIntegrator.Contract.DataModels.Insight.Module;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Api.Controllers.Insight
{
    [Route("api/[controller]")]
    [ApiController]
    [HasPermission(PolicyClaimTypes.Admin)]
    public class InsightController : ControllerBase
    {
        private readonly IInsightHandler _insightHandler;
        public InsightController(IInsightHandler insightHandler)
        {
            _insightHandler = insightHandler;
        }

        [HttpGet("/GetAllData/{moduleName}")]
        public async Task<string> GetAllData(string moduleName,
            string accountId, CancellationToken cancellationToken)
        {
            return await _insightHandler.GetAllData(accountId, moduleName, cancellationToken);
        }

        [HttpGet(nameof(GetAvailableInsightModules))]
        public async Task<IEnumerable<InsightAvailableModuleDto>> GetAvailableInsightModules(CancellationToken cancellationToken)
        {
            return await _insightHandler.GetAvailableInsightModules(cancellationToken);
        }
    }
}
