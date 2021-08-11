using System;
using System.Collections.Generic;
using System.Text;

namespace ZohoToInsightIntegrator.Contract.Models
{
    public sealed class InsightModuleField
    {
        public Guid InsightModuleFieldId { get; set; }
        public Guid AccountId { get; set; }
        public string IntegratorName { get; set; }
        public Guid InsightModuleId { get; set; }
        public string SectionName { get; set; }
        public int SectionOrder { get; set; }
        public string FieldName { get; set; }
        public string FieldApiName { get; set; }
        public string FieldType { get; set; }
        public string LockUpModuleApiName { get; set; }
        public bool IsMandatory { get; set; }
        public bool ReadOnly { get; set; }
        public int FieldMaxLength { get; set; }
        public int FieldSequenceNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Account Account { get; set; }
        public InsightModule InsightModule { get; set; }
        public ICollection<MappedField> MappedFields { get; set; }
    }
}
