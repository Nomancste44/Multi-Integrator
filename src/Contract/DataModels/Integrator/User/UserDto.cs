using System;
using System.Collections.Generic;
using System.Text;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.User
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public Guid AccountId { get; set; }

        public string AccountName { get; set; }
    }
}
