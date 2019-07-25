using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public static class ApplicationUserLoginMap
    {
        public static void Configure(this EntityTypeBuilder<ApplicationUserLogin> userLogin)
        {
            userLogin.ToTable("user_login");

            userLogin.HasKey(p => new
            {
                p.LoginProvider,
                p.ProviderKey,
                p.TenantId
            })
            .HasName("pk_user_login");

            userLogin.Property(p => p.UserId)
                .HasColumnName("user_id");

            userLogin.Property(p => p.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired();

            userLogin.Property(p => p.LoginProvider)
                .HasColumnName("login_provider")
                .HasMaxLength(128);

            userLogin.Property(p => p.ProviderKey)
                .HasColumnName("provider_key")
                .HasMaxLength(128);

            userLogin.Property(p => p.ProviderDisplayName)
                .HasColumnName("provider_display_name")
                .HasMaxLength(128);
        }
    }
}
