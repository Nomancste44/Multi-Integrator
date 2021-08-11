using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZohoToInsightIntegrator.Contract.DataModels.Zoho.Module
{
    public class module
    {
        [JsonPropertyName("modules")]
        public List<ZohoAvailableModuleDto> Modules { get; set; }

    }
    public class ZohoAvailableModuleDto
    {
        [JsonPropertyName("api_name")]
        public string ApiName { get; set; }

        [JsonPropertyName("plural_label")]
        public string PluralLabel { get; set; }

        [JsonPropertyName("sequence_number")]
        public int SequenceNumber { get; set; }

    }
}
