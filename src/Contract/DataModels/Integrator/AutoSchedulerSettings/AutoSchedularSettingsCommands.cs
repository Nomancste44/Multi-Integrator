using System;
using System.Collections.Generic;
using System.Text;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.AutoSchedulerSettings
{
    public class AutoSchedularSettingsCommands
    {
        public Guid AutoSchedulerSettingId { get; set; }
        public Guid AccountID { get; set; }
        public bool IsAutoSchedulerOn { get; set; }
        public Guid SchedulerRunDaily { get; set; }
        public string SchedulerRunAt { get; set; }
        public bool IsSendNotification { get; set; }
    }
}
