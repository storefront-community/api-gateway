using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public static class ApplicationUserMap
    {
        public static void Configure(this EntityTypeBuilder<ApplicationUser> user)
        {
            user.ToTable("user");

            user.HasKey(p => new
            {
                p.Id,
                p.TenantId
            })
            .HasName("pk_user");

            user.HasIndex(p => p.NormalizedUserName)
                .HasName("ix_user_name")
                .IsUnique();

            user.HasIndex(p => p.TenantId)
                .HasName("ix_tenant_id");

            user.HasIndex(p => p.NormalizedEmail)
                .HasName("ix_email")
                .IsUnique();

            user.Property(p => p.Id)
                .HasColumnName("id")
                .UseNpgsqlSerialColumn();

            user.Property(p => p.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired();

            user.Property(p => p.Email)
                .HasColumnName("email")
                .HasMaxLength(80)
                .IsRequired();

            user.Property(p => p.PhoneNumber)
                .HasColumnName("phone_number")
                .HasMaxLength(20);

            user.Property(p => p.NormalizedEmail)
                .HasColumnName("normalized_email")
                .HasMaxLength(80)
                .IsRequired();

            user.Property(p => p.UserName)
                .HasColumnName("user_name")
                .HasMaxLength(50)
                .IsRequired();

            user.Property(p => p.NormalizedUserName)
                .HasColumnName("normalized_user_name")
                .HasMaxLength(50)
                .IsRequired();

            user.Property(p => p.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(1024)
                .IsRequired();

            user.Property(p => p.TwoFactorEnabled)
                .HasColumnName("two_factor_enabled")
                .IsRequired();

            user.Property(p => p.PhoneNumberConfirmed)
                .HasColumnName("phone_number_confirmed")
                .IsRequired();

            user.Property(p => p.EmailConfirmed)
                .HasColumnName("email_confirmed")
                .IsRequired();

            user.Property(p => p.LockoutEnabled)
                .HasColumnName("lockout_enabled")
                .IsRequired();

            user.Property(p => p.LockoutEnd)
                .HasColumnName("lockout_end");

            user.Property(p => p.AccessFailedCount)
                .HasColumnName("access_failed_count")
                .IsRequired();

            user.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            user.Property(p => p.ConcurrencyStamp)
                .HasColumnName("concurrency_stamp")
                .IsConcurrencyToken();

            user.Property(p => p.SecurityStamp)
                .HasColumnName("security_stamp");

            user.HasMany(p => p.UserClaims)
                .WithOne(p => p.User)
                .HasForeignKey(p => new
                {
                    p.UserId,
                    p.TenantId
                })
                .HasConstraintName("fk_user_claim__user")
                .IsRequired();

            user.HasMany(p => p.UserLogins)
                .WithOne(p => p.User)
                .HasForeignKey(p => new
                {
                    p.UserId,
                    p.TenantId
                })
                .HasConstraintName("fk_user_login__user")
                .IsRequired();

            user.HasMany(p => p.UserTokens)
                .WithOne(p => p.User)
                .HasForeignKey(p => new
                {
                    p.UserId,
                    p.TenantId
                })
                .HasConstraintName("fk_user_token__user")
                .IsRequired();

            user.HasMany(p => p.UserRoles)
                .WithOne(p => p.User)
                .HasForeignKey(p => new
                {
                    p.UserId,
                    p.TenantId
                })
                .HasConstraintName("fk_user_role__user")
                .IsRequired();
        }
    }
}
