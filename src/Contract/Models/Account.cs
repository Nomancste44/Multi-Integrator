using System;
using System.Collections.Generic;

namespace ZohoToInsightIntegrator.Contract.Models
{
    public sealed class Account
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public bool IsActive { get; set; }
        public bool IsConnectionActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public IntegratingCredential IntegratingCredential { get; set; }    
        public ICollection<User> Users { get; set; }
        public ICollection<ZohoModule> ZohoModules { get; set; }
        public ICollection<MappedModule> MappedModules { get; set; }
        public ICollection<ZohoModuleField> ZohoModuleFields { get; set; }
        public ICollection<InsightModuleField> InsightModuleFields { get; set; }
        public ICollection<MappedField> MappedFields { get; set; }

    }
}

