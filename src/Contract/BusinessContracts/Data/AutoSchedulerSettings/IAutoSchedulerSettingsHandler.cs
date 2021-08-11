using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.AutoSchedulerSettings;

namespace ZohoToInsightIntegrator.Contract.BusinessContracts.Data.AutoSchedulerSettings
{
    public interface IAutoSchedulerSettingsHandler
    {
        Task<IEnumerable<AutoSchdulerTimeDto>> GetAutoSchdulerTimes(CancellationToken cancellationToken);

        Task<AutoSchedulerSettingsDto> GetAutoSchedulerSettings(string accountId , CancellationToken cancellationToken);

        Task<IEnumerable<ModuleSyncModeDto>> GetModuleSyncModes(string autoSchedulerSettingsId, CancellationToken cancellationToken);

        Task SaveAutoSchedulerSettings(AutoSchedularSettingsCommands autoSchedulerSettings,CancellationToken cancellationToken);
    }
}
