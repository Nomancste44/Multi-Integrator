using System;
using System.Collections.Generic;

namespace ZohoToInsightIntegrator.Contract.Models
{
    public sealed class MappedModule
    {
        public Guid MappedModuleId { get; set; }
        public Guid AccountId { get; set; }
        public Guid ZohoModuleId { get; set; }
        public Guid InsightModuleId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Account Account { get; set; }
        public ZohoModule ZohoModule { get; set; }
        public InsightModule InsightModule { get; set; }
        public ICollection<MappedField> MappedFields { get; set; }
    }
}
