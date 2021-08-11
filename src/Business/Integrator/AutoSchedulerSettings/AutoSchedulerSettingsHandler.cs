using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.AutoSchedulerSettings;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Integrator.AutoSchedulerSettings;

namespace ZohoToInsightIntegrator.Business.Integrator.AutoSchedulerSettings
{
    public class AutoSchedulerSettingsHandler : IAutoSchedulerSettingsHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public AutoSchedulerSettingsHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<AutoSchdulerTimeDto>> GetAutoSchdulerTimes(CancellationToken cancellationToken)
        {
            return this._unitOfWork.AutoSchedulerSettingsRepository.GetAutoSchdulerTimes(cancellationToken);
        }

        public Task<AutoSchedulerSettingsDto> GetAutoSchedulerSettings(string accountId , CancellationToken cancellationToken)
        {
            return this._unitOfWork.AutoSchedulerSettingsRepository.GetAutoSchedulerSettings(accountId, cancellationToken);
        }

        

        public async Task<IEnumerable<ModuleSyncModeDto>> GetModuleSyncModes(string autoSchedulerSettingsId, CancellationToken cancellationToken)
        {
            return await this._unitOfWork.AutoSchedulerSettingsRepository.GetModuleSyncModes(autoSchedulerSettingsId, cancellationToken);
        }

        public Task SaveAutoSchedulerSettings(AutoSchedularSettingsCommands autoSchedulerSettings, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
