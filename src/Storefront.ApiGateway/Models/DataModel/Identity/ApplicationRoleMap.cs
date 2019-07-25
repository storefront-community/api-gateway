using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public static class ApplicationRoleMap
    {
        public static void Configure(this EntityTypeBuilder<ApplicationRole> role)
        {
            role.ToTable("role");

            role.HasKey(p => new
            {
                p.Id,
                p.TenantId
            })
            .HasName("pk_role");

            role.HasIndex(p => p.NormalizedName)
                .HasName("ix_role_name")
                .IsUnique();

            role.Property(p => p.Id)
                .HasColumnName("id")
                .UseNpgsqlSerialColumn();

            role.Property(p => p.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired();

            role.Property(p => p.ConcurrencyStamp)
                .HasColumnName("concurrency_stamp")
                .IsConcurrencyToken();

            role.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(80);

            role.Property(p => p.NormalizedName)
                .HasColumnName("normalized_name")
                .HasMaxLength(80);

            role.HasMany(p => p.UserRoles)
                .WithOne(p => p.Role)
                .HasForeignKey(p => new
                {
                    p.RoleId,
                    p.TenantId
                })
                .HasConstraintName("fk_user_role__role")
                .IsRequired();

            role.HasMany(p => p.RoleClaims)
                .WithOne(p => p.Role)
                .HasForeignKey(p => new
                {
                    p.RoleId,
                    p.TenantId
                })
                .HasConstraintName("fk_role_claim__role")
                .IsRequired();
        }
    }
}
