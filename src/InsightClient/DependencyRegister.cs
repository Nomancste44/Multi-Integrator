using Microsoft.Extensions.DependencyInjection;
using ZohoToInsightIntegrator.Contract.InsightClientContracts;
using ZohoToInsightIntegrator.InsightClient.InsightApi;

namespace ZohoToInsightIntegrator.InsightClient
{
    public static class DependencyRegister
    {
        public static IServiceCollection AddInsightDependency(this IServiceCollection services)
        {
            services.AddHttpClient<InsightHttpClient>();
            services.AddScoped<IInsightResourceApi,InsightResourceApi>();
            return services;
        }
    }
}
