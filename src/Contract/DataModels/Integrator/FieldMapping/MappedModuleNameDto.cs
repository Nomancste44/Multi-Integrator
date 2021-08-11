using System;
using System.Collections.Generic;
using System.Text;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.FieldMapping
{
    public class MappedModuleNameDto
    {
        public Guid MappedModuleId { get; set; }
        public Guid AccountId { get; set; }
        public Guid ZohoModuleId { get; set; }
        public Guid InsightModuleId { get; set; }
        public string MappedModuleName { get; set; }
    }
}
