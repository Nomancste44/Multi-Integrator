using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Api.Configuration.Authentication;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.AutoSchedulerSettings;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.AutoSchedulerSettings;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Api.Controllers.Integrator
{
    [Route("api/[controller]")]
    [ApiController]
    [HasPermission(PolicyClaimTypes.ViewOnly)]
    public class SchedulerSettingsController : ControllerBase
    {
        private IAutoSchedulerSettingsHandler _autoSchedulerSettings;
        public SchedulerSettingsController(IAutoSchedulerSettingsHandler autoSchedulerSettings)
        {
            this._autoSchedulerSettings = autoSchedulerSettings;
        }

        [HttpGet(nameof(GetAutoSchedulerSettings))]
        public async Task<AutoSchedulerSettingsDto> GetAutoSchedulerSettings(string accountId, CancellationToken cancellationToken)
        {
            return await this._autoSchedulerSettings.GetAutoSchedulerSettings(accountId, cancellationToken);
        }

        [HttpGet(nameof(GetModuleSyncModes))]
        public async Task<IEnumerable<ModuleSyncModeDto>> GetModuleSyncModes(string autoSchedulerSettingId, CancellationToken cancellationToken)
        {
            return await this._autoSchedulerSettings.GetModuleSyncModes(autoSchedulerSettingId, cancellationToken);
        }


        [HttpGet(nameof(GetAutoSchdulerTimes))]

        public async Task<IEnumerable<AutoSchdulerTimeDto>> GetAutoSchdulerTimes(CancellationToken cancellationToken)
        {
            return await this._autoSchedulerSettings.GetAutoSchdulerTimes(cancellationToken);
        }

        [HasPermission(PolicyClaimTypes.Admin)]
        [HttpPost(nameof(SaveAutoSchedularSettings))]
        public async Task<IActionResult> SaveAutoSchedularSettings(AutoSchedularSettingsCommands autoSchedularSettingsCommands, CancellationToken cancellationToken)
        {
            await this._autoSchedulerSettings.SaveAutoSchedulerSettings(autoSchedularSettingsCommands, cancellationToken);
            return Ok(HttpStatusCode.OK);
        }




    }
}
