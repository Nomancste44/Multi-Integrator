using Microsoft.Extensions.DependencyInjection;
using ZohoToInsightIntegrator.Business.Insight;
using ZohoToInsightIntegrator.Business.Integrator.Account;
using ZohoToInsightIntegrator.Business.Integrator.AutoSchedulerSettings;
using ZohoToInsightIntegrator.Business.Integrator.Mapping;
using ZohoToInsightIntegrator.Business.Zoho;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Account;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.AutoSchedulerSettings;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Data.Mapping;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Insight;
using ZohoToInsightIntegrator.Contract.BusinessContracts.Zoho;

namespace ZohoToInsightIntegrator.Business
{
    public static class DependencyRegister
    {
        public static IServiceCollection AddBusinessDependency(
            this IServiceCollection services)
        {
            services.AddScoped<IUserHandler, UserHandler>();
            services.AddScoped<IAccountHandler, AccountHandler>();
            services.AddScoped<IUserRoleHandler, UserRoleHandler>();
            services.AddScoped<IZohoHandler, ZohoHandler>();
            services.AddScoped<IModuleMappingHandler, ModuleMappingHandler>();
            services.AddScoped<IFieldMappingHandler, FieldMappingHandler>();
            services.AddScoped<IAutoSchedulerSettingsHandler, AutoSchedulerSettingsHandler>();
            services.AddScoped<IInsightHandler, InsightHandler>();
            return services;
        }
    }
}
