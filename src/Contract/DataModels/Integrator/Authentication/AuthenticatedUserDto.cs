using System;
using System.Collections.Generic;
using System.Security.Claims;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication
{
    public class AuthenticatedUserDto
    {
        public AuthenticatedUserDto()
        {
            Claims = new List<Claim>();
        }
        public string RoleName { get; set; }
        public string RoleLabel { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public bool IsAccountActive { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
