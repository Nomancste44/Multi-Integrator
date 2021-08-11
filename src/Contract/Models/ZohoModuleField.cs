using System;
using System.Collections.Generic;
using System.Text;

namespace ZohoToInsightIntegrator.Contract.Models
{
    public sealed class ZohoModuleField
    {
        public Guid ZohoModuleFieldId { get; set; }
        public Guid AccountId { get; set; }
        public string IntegratorName { get; set; }
        public Guid ZohoModuleId { get; set; }
        public string SectionName { get; set; }
        public int SectionOrder { get; set; }
        public string FieldName { get; set; }
        public string FieldApiName { get; set; }
        public string FieldType { get; set; }
        public string LockUpModuleApiName { get; set; }
        public string PickListValues { get; set; }
        public bool IsMandatory { get; set; }
        public bool ReadOnly { get; set; }
        public bool Visible { get; set; }
        public int FieldMaxLength { get; set; }
        public int FieldSequenceNumber { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Account Account { get; set; }
        public ZohoModule ZohoModule { get; set; }
        public ICollection<MappedField> MappedFields { get; set; }
    }
}
