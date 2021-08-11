using System;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Contract.Models
{
    public sealed class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => Common.UtcDateTime >= Expires;
        public DateTime CreatedOn { get; set; }
        public string CreatedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;

        public  Guid UserId { get; set; }
        public User User { get; set; }
    }
}
