using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Data.EntityConfigurations
{
    public class ZohoModuleEntityTypeConfiguration : IEntityTypeConfiguration<ZohoModule>
    {
        public void Configure(EntityTypeBuilder<ZohoModule> builder)
        {
            builder.ToTable("ZohoModules");
            builder.Property(e => e.ZohoModuleId).HasDefaultValueSql();
            builder.HasKey(e => e.ZohoModuleId);
            builder.Property(a => a.CreatedOn).HasDefaultValueSql();
            builder.Property(a => a.UpdatedOn);

            builder.HasOne<Account>(m => m.Account)
                .WithMany(a => a.ZohoModules)
                .HasForeignKey(m => m.AccountId)
                .IsRequired();

        }
    }
}
