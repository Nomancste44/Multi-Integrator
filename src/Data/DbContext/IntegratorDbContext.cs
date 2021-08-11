using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Data.DbContext
{
    public sealed class IntegratorDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ZohoModule> ZohoModules { get; set; }
        public DbSet<ZohoModuleField> ZohoModuleFields { get; set; }
        public DbSet<MappedModule> MappedModules { get; set; }
        public DbSet<MappedField> MappedFields { get; set; }
        public DbSet<IntegratingCredential> IntegratingCredentials { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public IntegratorDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
           => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}
