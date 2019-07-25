using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public static class ApplicationUserRoleMap
    {
        public static void Configure(this EntityTypeBuilder<ApplicationUserRole> userRole)
        {
            userRole.ToTable("user_role");

            userRole.HasKey(p => new
            {
                p.UserId,
                p.RoleId,
                p.TenantId
            })
            .HasName("pk_user_role");

            userRole.Property(p => p.UserId)
                .HasColumnName("user_id");

            userRole.Property(p => p.RoleId)
                .HasColumnName("role_id");

            userRole.Property(p => p.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired();
        }
    }
}
