using Microsoft.AspNetCore.Http;
using System;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Api.Configuration.ExecutionContext
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Email
        {
            get
            {
                if (_httpContextAccessor
                    .HttpContext
                    .User
                    .FindFirst(PolicyClaimTypes.Email)
                    .Value != null)
                {
                    return _httpContextAccessor.HttpContext.User.FindFirst(PolicyClaimTypes.Email).Value;
                }

                throw new ApplicationException("User context is not available");
            }
        }


        public bool IsSuperAdmin =>
            _httpContextAccessor
                .HttpContext
                .User
                .HasClaim(c => c.Value == PolicyClaimTypes.SuperAdmin);

        public bool IsAdmin =>
            _httpContextAccessor
                .HttpContext
                .User
                .HasClaim(c => c.Value == PolicyClaimTypes.Admin);

        public bool IsAvailable => _httpContextAccessor.HttpContext != null;

        public string IpAddress
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                    return _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];

                if (_httpContextAccessor
                    .HttpContext
                    .Connection
                    .RemoteIpAddress
                    .MapToIPv4().ToString() != null)
                {
                    return _httpContextAccessor
                        .HttpContext
                        .Connection.RemoteIpAddress.MapToIPv4().ToString();
                }
                throw new ApplicationException("IP is not available");
            }
        }

    }
}