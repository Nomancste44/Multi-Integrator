using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ZohoToInsightIntegrator.Contract.DataModels.Zoho.Field
{
    public class Layout
    {
        [JsonPropertyName("layouts")]
        public List<Section> Layouts { get; set; }
    }
    public class Section
    {
        [JsonPropertyName("name")]
        public string LayoutName { get; set; }

        [JsonPropertyName("sections")]
        public List<Field> Sections { get; set; }
    }
    public class Field
    {
        [JsonPropertyName("name")]
        public string SectionName { get; set; }

        [JsonPropertyName("sequence_number")]
        public int SectionOrder { get; set; }

        [JsonPropertyName("fields")]
        public List<ZohoAvailableFieldDto> Fields { get; set; }

    }
    public class ZohoAvailableFieldDto
    {
        [JsonPropertyName("field_label")]
        public string FieldName { get; set; }

        [JsonPropertyName("api_name")]
        public string FieldApiName { get; set; }

        [JsonPropertyName("data_type")]
        public string FieldType { get; set; }

        [JsonPropertyName("field_read_only")]
        public bool ReadOnly { get; set; }

        [JsonPropertyName("visible")]
        public bool Visible { get; set; }

        [JsonPropertyName("length")]
        public int FieldMaxLength { get; set; }

        [JsonPropertyName("sequence_number")]
        public int FieldSequenceNumber { get; set; }

        [JsonPropertyName("system_mandatory")]
        public bool IsMandatory { get; set; }

        [JsonPropertyName("lookup")]
        public LookUp LookUp { get; set; }

        [JsonPropertyName("pick_list_values")]
        public object PickListValues { get; set; }
    }

    public class LookUp
    {
        [JsonPropertyName("module")]
        public string Module { get; set; }
    }
}
