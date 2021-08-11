using Microsoft.Extensions.DependencyInjection;
using ZohoToInsightIntegrator.Contract.ZohoClientContracts;
using ZohoToInsightIntegrator.ZohoClient.ZohoApi;

namespace ZohoToInsightIntegrator.ZohoClient
{
    public static class DependencyRegister
    {
        public static IServiceCollection AddZohoDependency(this IServiceCollection services)
        {
            services.AddScoped<ZohoHttpContextMiddleware>();
            services.AddHttpClient<ZohoHttpClient>()
                .AddHttpMessageHandler<ZohoHttpContextMiddleware>();
            services.AddScoped<IZohoConnectionApi, ZohoConnectionApi>();
            services.AddScoped<IZohoResourceApi, ZohoResourceApi>();
            return services;
        }
    }
}
