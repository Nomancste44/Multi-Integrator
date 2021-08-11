using System;

namespace ZohoToInsightIntegrator.Contract.Models
{
    public sealed class MappedField
    {
        public Guid MappedFieldId { get; set; }
        public Guid AccountId { get; set; }
        public Guid MappedModuleId { get; set; }
        public Guid ZohoModuleFieldId { get; set; }
        public Guid InsightModuleFieldId { get; set; }
        public string ZohoDefaultValue { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Account Account { get; set; }
        public MappedModule MappedModule { get; set; }
        public ZohoModuleField ZohoModuleField { get; set; }
        public InsightModuleField InsightModuleField { get; set; }

    }
}
