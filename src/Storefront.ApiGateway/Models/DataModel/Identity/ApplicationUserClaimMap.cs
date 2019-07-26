using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public static class ApplicationUserClaimMap
    {
        public static void Configure(this EntityTypeBuilder<ApplicationUserClaim> userClaim)
        {
            userClaim.ToTable("user_claim");

            userClaim.HasKey(p => new
            {
                p.Id,
                p.TenantId
            })
            .HasName("pk_user_claim");

            userClaim.Property(p => p.Id)
                .HasColumnName("id")
                .UseNpgsqlSerialColumn();

            userClaim.Property(p => p.UserId)
                .HasColumnName("user_id");

            userClaim.Property(p => p.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired();

            userClaim.Property(p => p.ClaimType)
                .HasColumnName("claim_type")
                .HasMaxLength(255);

            userClaim.Property(p => p.ClaimValue)
                .HasColumnName("claim_value")
                .HasMaxLength(255);
        }
    }
}
