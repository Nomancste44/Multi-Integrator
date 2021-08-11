using System;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Account
{
    public class IntegratingCredentialDto
    {
        public string ZohoClientId { get; set; }
        public string ZohoClientSecret { get; set; }
        public string ZohoWfTriggerOption { get; set; }
        public string ZohoTimeZone { get; set; }
        public string InsightClientId { get; set; }
        public string InsightClientSecret { get; set; }
        public string InsightTimeZone { get; set; }
        public string InsightApiDomain { get; set; }
        public string NofityEmail { get; set; }
        public DateTime? CutOffDateTime { get; set; }
        public int LogRetentionDays { get; set; }
        public bool IsCrmConnectionActive { get; set; }
    }
}
