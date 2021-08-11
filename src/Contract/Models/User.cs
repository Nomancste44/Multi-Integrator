using System;
using System.Collections.Generic;

namespace ZohoToInsightIntegrator.Contract.Models
{
    public sealed class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid RoleId { get; set; }
        public  Role Role { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}
