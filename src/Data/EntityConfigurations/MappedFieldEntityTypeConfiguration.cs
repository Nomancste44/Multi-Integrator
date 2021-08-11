using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Data.EntityConfigurations
{
    public class MappedFieldEntityTypeConfiguration : IEntityTypeConfiguration<MappedField>
    {
        public void Configure(EntityTypeBuilder<MappedField> builder)
        {
            builder.ToTable("MappedFields");
            builder.Property(e => e.MappedFieldId).HasDefaultValueSql();
            builder.HasKey(e => e.MappedFieldId);

            builder.HasOne<Account>(m => m.Account)
                .WithMany(a => a.MappedFields)
                .HasForeignKey(m => m.AccountId)
                .IsRequired();

            builder.HasOne<MappedModule>(m => m.MappedModule)
                .WithMany(a => a.MappedFields)
                .HasForeignKey(m => m.MappedModuleId)
                .IsRequired();

            builder.HasOne<ZohoModuleField>(m => m.ZohoModuleField)
                .WithMany(a => a.MappedFields)
                .HasForeignKey(m => m.ZohoModuleFieldId)
                .IsRequired();

            builder.HasOne<InsightModuleField>(m => m.InsightModuleField)
                .WithMany(a => a.MappedFields)
                .HasForeignKey(m => m.InsightModuleFieldId)
                .IsRequired();

            builder.Property(a => a.CreatedOn).HasDefaultValueSql();
            builder.Property(a => a.UpdatedOn);
        }
    }
}
