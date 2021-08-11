using System;

namespace ZohoToInsightIntegrator.Contract.Models
{
    public sealed class IntegratingCredential
    {
        public Guid IntegratingCredentialId { get; set; }
        public Guid AccountId { get; set; }
        public string ZohoClientId { get; set; }
        public string ZohoClientSecret { get; set; }
        public string ZohoAccessToken { get; set; }
        public string ZohoRefreshToken { get; set; }
        public string ZohoApiDomain { get; set; }
        public string ZohoTokenType { get; set; }
        public string ZohoWfTriggerOption { get; set; }
        public string ZohoTimeZone { get; set; }
        public string InsightClientId { get; set; }
        public string InsightClientSecret { get; set; }
        public string InsightTimeZone { get; set; }
        public string InsightApiDomain { get; set; }
        public string NofityEmail { get; set; }
        public DateTime? CutOffDateTime { get; set; }
        public int LogRetentionDays { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Account Account { get; set; }
    }
}
