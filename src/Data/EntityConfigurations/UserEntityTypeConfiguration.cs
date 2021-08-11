using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZohoToInsightIntegrator.Contract.Models;

namespace ZohoToInsightIntegrator.Data.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(u => u.UserId).HasDefaultValueSql();
            builder.HasKey(u => u.UserId);
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();
            builder.Property(u => u.CreatedOn).HasDefaultValueSql();

            builder.HasOne<Account>(u => u.Account)
                .WithMany(a => a.Users)
                .HasForeignKey(u => u.AccountId)
                .IsRequired();

        }
    }
}
