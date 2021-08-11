using System;
using System.Collections.Generic;

namespace ZohoToInsightIntegrator.Contract.Models
{
    public sealed class Role
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleLabel { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
