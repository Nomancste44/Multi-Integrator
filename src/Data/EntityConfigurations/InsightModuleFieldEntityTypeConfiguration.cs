using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Data.EntityConfigurations
{
    public class InsightModuleFieldEntityTypeConfiguration : IEntityTypeConfiguration<InsightModuleField>
    {
        public void Configure(EntityTypeBuilder<InsightModuleField> builder)
        {
            builder.ToTable("InsightModuleFields");
            builder.Property(e => e.InsightModuleId).HasDefaultValueSql();
            builder.HasKey(e => e.InsightModuleId);

            builder.HasOne<Account>(m => m.Account)
                .WithMany(a => a.InsightModuleFields)
                .HasForeignKey(m => m.AccountId)
                .IsRequired();

            builder.HasOne<InsightModule>(m => m.InsightModule)
                .WithMany(a => a.InsightModuleFields)
                .HasForeignKey(m => m.InsightModuleId)
                .IsRequired();

            builder.Property(a => a.CreatedOn).HasDefaultValueSql();
            builder.Property(a => a.UpdatedOn);

        }
    }
}
