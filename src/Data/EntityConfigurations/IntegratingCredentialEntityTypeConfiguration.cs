using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Data.EntityConfigurations
{
    public class IntegratingCredentialEntityTypeConfiguration :
        IEntityTypeConfiguration<IntegratingCredential>
    {
        public void Configure(EntityTypeBuilder<IntegratingCredential> builder)
        {
            builder.ToTable("IntegratingCredentials");
            builder.Property(ic => ic.IntegratingCredentialId).HasDefaultValueSql();
            builder.HasKey(ic => ic.IntegratingCredentialId);

            builder.HasOne(ic => ic.Account)
                .WithOne(a => a.IntegratingCredential)
                .HasForeignKey<IntegratingCredential>(ic => ic.AccountId);
            builder.Property(ic => ic.CreatedOn).HasDefaultValueSql();
        }
    }
}
