using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public static class ApplicationUserTokenMap
    {
        public static void Configure(this EntityTypeBuilder<ApplicationUserToken> userToken)
        {
            userToken.ToTable("user_token");

            userToken.HasKey(p => new
            {
                p.UserId,
                p.LoginProvider,
                p.Name,
                p.TenantId
            })
            .HasName("pk_user_token");

            userToken.Property(p => p.UserId)
                .HasColumnName("user_id");

            userToken.Property(p => p.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired();

            userToken.Property(p => p.LoginProvider)
                .HasColumnName("login_provider")
                .HasMaxLength(50);

            userToken.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(80);

            userToken.Property(p => p.Value)
                .HasColumnName("value")
                .HasMaxLength(1024);
        }
    }
}
