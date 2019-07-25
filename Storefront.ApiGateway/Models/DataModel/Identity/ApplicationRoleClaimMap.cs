using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public static class ApplicationRoleClaimMap
    {
        public static void Configure(this EntityTypeBuilder<ApplicationRoleClaim> roleClaim)
        {
            roleClaim.ToTable("role_claim");

            roleClaim.HasKey(p => new
            {
                p.Id,
                p.TenantId
            })
            .HasName("pk_role_claim");

            roleClaim.Property(p => p.Id)
                .HasColumnName("id");

            roleClaim.Property(p => p.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired();

            roleClaim.Property(p => p.RoleId)
                .HasColumnName("role_id")
                .IsRequired();

            roleClaim.Property(p => p.ClaimType)
                .HasColumnName("claim_type")
                .HasMaxLength(255);

            roleClaim.Property(p => p.ClaimValue)
                .HasColumnName("claim_value")
                .HasMaxLength(255);
        }
    }
}
