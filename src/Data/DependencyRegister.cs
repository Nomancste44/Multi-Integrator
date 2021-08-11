using Microsoft.Extensions.DependencyInjection;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataContracts;
using ZohoToInsightIntegrator.Data.DbContext;
using ZohoToInsightIntegrator.Data.Repository;

namespace ZohoToInsightIntegrator.Data
{
    public static class DependencyRegister
    {
        public static IServiceCollection AddRepositoryDependency(
            this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IUniquenessChecker, UniquenessChecker>();
            return services;
        }
    }
}
