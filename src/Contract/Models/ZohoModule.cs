using System;
using System.Collections.Generic;

namespace ZohoToInsightIntegrator.Contract.Models
{
    public sealed class ZohoModule
    {
        public Guid ZohoModuleId { get; set; }
        public Guid AccountId { get; set; }
        public string IntegratorName { get; set; }
        public string ModuleName { get; set; }
        public string ModuleApiName { get; set; }
        public int SequenceNumber { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Account Account { get; set; }
        public ICollection<MappedModule> MappedModules { get; set; }
        public ICollection<ZohoModuleField> ZohoModuleFields { get; set; }
    }
}
