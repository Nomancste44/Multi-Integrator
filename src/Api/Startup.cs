using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hellang.Middleware.ProblemDetails;
using ZohoToInsightIntegrator.Api.Configuration.Authentication;
using ZohoToInsightIntegrator.Api.Configuration.ExecutionContext;
using ZohoToInsightIntegrator.Api.Configuration.ProblemDetails;
using ZohoToInsightIntegrator.Api.Extensions;
using ZohoToInsightIntegrator.Business;
using ZohoToInsightIntegrator.Contract.Contracts;
using ZohoToInsightIntegrator.Contract.DataModels.Zoho.SetConnection;
using ZohoToInsightIntegrator.Contract.Models;
using ZohoToInsightIntegrator.Contract.Utility;
using ZohoToInsightIntegrator.Data;
using ZohoToInsightIntegrator.Data.DbContext;
using ZohoToInsightIntegrator.InsightClient;
using ZohoToInsightIntegrator.ZohoClient;

namespace ZohoToInsightIntegrator.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Common.ZohoRedirectHeaderValue = configuration.GetValue<string>("ZohoRedirectUrl");
            AuthenticationConfig.ValidIssuer = configuration.GetValue<string>("ValidIssuer");
            AuthenticationConfig.ValidAudience = configuration.GetValue<string>("ValidAudience");
            AuthorizationChecker.CheckAllEndpoints();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerDocumentation();
            services.AddCors();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddControllers()
                .AddFluentValidation(fvc =>
                    fvc.RegisterValidatorsFromAssemblyContaining<SetZohoConnectionCommandValidator>());
            services.AddDbContextPool<IntegratorDbContext>(
                options =>
                    options.UseSqlServer(
                        Configuration
                            .GetConnectionString(Common.ConnectionName)));

            services.AddRepositoryDependency();
            services.AddBusinessDependency();
            services.AddZohoDependency();
            services.AddInsightDependency();
            services.AddProblemDetailsDependency();
            services.AddSingleton(Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfigurationOptions>());
            services.AddScoped<EmailService>();
            ConfigureAuthenticationWithJwt(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseProblemDetails();

            app.UseSwaggerDocumentation();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureAuthenticationWithJwt(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidAudience = AuthenticationConfig.ValidAudience,
                        ValidIssuer = AuthenticationConfig.ValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(AuthenticationConfig.SecretKey))
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
                {
                    policyBuilder
                        .RequireAuthenticatedUser()
                        .Requirements
                        .Add(new HasPermissionAuthorizationRequirement());
                });
                options.InvokeHandlersAfterFailure = false;
            });
            services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();

        }

    }
}
