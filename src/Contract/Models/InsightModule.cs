using System;
using System.Collections.Generic;

namespace ZohoToInsightIntegrator.Contract.Models
{
    public sealed class InsightModule
    {
        public Guid InsightModuleId { get; set; }
        public string ModuleName { get; set; }
        public ICollection<MappedModule> MappedModules { get; set; }
        public ICollection<InsightModuleField> InsightModuleFields { get; set; }
    }
}
