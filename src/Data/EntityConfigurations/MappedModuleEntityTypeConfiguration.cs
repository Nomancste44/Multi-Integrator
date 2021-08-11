using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Data.EntityConfigurations
{
    public class MappedModuleEntityTypeConfiguration : IEntityTypeConfiguration<MappedModule>
    {
        public void Configure(EntityTypeBuilder<MappedModule> builder)
        {
            builder.ToTable("MappedModules");
            builder.Property(e => e.MappedModuleId).HasDefaultValueSql();
            builder.HasKey(e => e.MappedModuleId);

            builder.HasOne<Account>(m => m.Account)
                .WithMany(a => a.MappedModules)
                .HasForeignKey(m => m.AccountId)
                .IsRequired();

            builder.HasOne<ZohoModule>(m => m.ZohoModule)
                .WithMany(a => a.MappedModules)
                .HasForeignKey(m => m.ZohoModuleId)
                .IsRequired();

            builder.HasOne<InsightModule>(m => m.InsightModule)
                .WithMany(a => a.MappedModules)
                .HasForeignKey(m => m.InsightModuleId)
                .IsRequired();


            builder.Property(a => a.CreatedOn).HasDefaultValueSql();
            builder.Property(a => a.UpdatedOn);

        }
    }
}
