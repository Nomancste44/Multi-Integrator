using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ZohoToInsightIntegrator.Api.Configuration.Authentication
{
    internal class HasPermissionAuthorizationHandler : AttributeAuthorizationHandler<
        HasPermissionAuthorizationRequirement, HasPermissionAttribute>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HasPermissionAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasPermissionAuthorizationRequirement requirement,
            HasPermissionAttribute attribute)
        {

            if (!await AuthorizeAsync(attribute.Name))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }

        private Task<bool> AuthorizeAsync(string permission)
        {
            return Task.FromResult(_httpContextAccessor
                .HttpContext
                .User
                .HasClaim(c=>c.Value == permission));
        }
    }
}