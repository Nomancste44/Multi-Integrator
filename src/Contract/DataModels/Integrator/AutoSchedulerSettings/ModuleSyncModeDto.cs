using System;
using System.Collections.Generic;
using System.Text;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.AutoSchedulerSettings
{
    public class ModuleSyncModeDto
    {
        public Guid SyncModuleId { get; set; }
        public string SyncMode { get; set; }
        public string ModuleName { get; set; }
    }
}
