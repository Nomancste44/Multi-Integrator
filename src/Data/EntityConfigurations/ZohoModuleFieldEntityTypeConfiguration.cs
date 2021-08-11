using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Data.EntityConfigurations
{
    public class ZohoModuleFieldEntityTypeConfiguration : IEntityTypeConfiguration<ZohoModuleField>
    {
        public void Configure(EntityTypeBuilder<ZohoModuleField> builder)
        {
            builder.ToTable("ZohoModuleFields");
            builder.Property(e => e.ZohoModuleFieldId).HasDefaultValueSql();
            builder.HasKey(e => e.ZohoModuleFieldId);

            builder.HasOne<Account>(m => m.Account)
                .WithMany(a => a.ZohoModuleFields)
                .HasForeignKey(m => m.AccountId)
                .IsRequired();

            builder.HasOne<ZohoModule>(m => m.ZohoModule)
                .WithMany(a => a.ZohoModuleFields)
                .HasForeignKey(m => m.ZohoModuleId)
                .IsRequired();

            builder.Property(a => a.CreatedOn).HasDefaultValueSql();
            builder.Property(a => a.UpdatedOn);
        }
    }
}
