using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Data.EntityConfigurations
{
    public class InsightModuleEntityTypeConfiguration : IEntityTypeConfiguration<InsightModule>
    {
        public void Configure(EntityTypeBuilder<InsightModule> builder)
        {
            builder.ToTable("InsightModules");
            builder.Property(e => e.InsightModuleId).HasDefaultValueSql();
            builder.HasKey(e => e.InsightModuleId);
        }
    }
}
