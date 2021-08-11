using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.ModuleMapping
{
    public class MappedModuleDto
    {
        public Guid MappedModuleId { get; set; }
        public Guid AccountId { get; set; }
        public Guid ZohoModuleId { get; set; }
        public Guid InsightModuleId { get; set; }
        [JsonPropertyName("plural_label")]
        public string ZohoModuleName { get; set; }
        [JsonPropertyName("sequence_number")]
        public int SequenceNumber { get; set; }
    }
}
