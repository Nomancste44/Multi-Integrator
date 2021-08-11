using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Data.EntityConfigurations
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.Property(e => e.AccountId).HasDefaultValueSql();
            builder.HasKey(e => e.AccountId);

            builder.Property(a => a.IsActive);
            builder.Property(a => a.AccountName).HasMaxLength(100);
            builder.Property(a => a.UpdatedOn);
            builder.Property(a => a.CreatedOn).HasDefaultValueSql();

        }
    }
}
