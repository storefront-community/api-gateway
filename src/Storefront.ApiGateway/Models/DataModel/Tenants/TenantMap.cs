using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.ApiGateway.Models.DataModel.Tenants
{
    public static class TenantMap
    {
        public static void Configure(this EntityTypeBuilder<Tenant> tenant)
        {
            tenant.ToTable("tenant");

            tenant.HasKey(p => p.Id)
                .HasName("pk_user");

            tenant.Property(p => p.Id)
                .HasColumnName("id")
                .UseNpgsqlSerialColumn();

            tenant.Property(p => p.BusinessName)
                .HasColumnName("business_name")
                .HasMaxLength(80)
                .IsRequired();

            tenant.Property(p => p.BillingEmail)
                .HasColumnName("billing_email")
                .HasMaxLength(80)
                .IsRequired();

            tenant.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            tenant.HasMany(p => p.Roles)
                .WithOne(p => p.Tenant)
                .HasForeignKey(p => p.TenantId)
                .HasConstraintName("fk_role__tenant")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            tenant.HasMany(p => p.Users)
                .WithOne(p => p.Tenant)
                .HasForeignKey(p => p.TenantId)
                .HasConstraintName("fk_user__tenant")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
