using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Storefront.ApiGateway.Models.DataModel.Identity;
using Storefront.ApiGateway.Models.DataModel.Tenants;

namespace Storefront.ApiGateway.Models.DataModel
{
    public class ApiDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        public const string Schema = "identity";

        public ApiDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(Schema);

            builder.Entity<ApplicationRole>().Configure();
            builder.Entity<ApplicationUser>().Configure();
            builder.Entity<ApplicationRoleClaim>().Configure();
            builder.Entity<ApplicationUserClaim>().Configure();
            builder.Entity<ApplicationUserLogin>().Configure();
            builder.Entity<ApplicationUserRole>().Configure();
            builder.Entity<ApplicationUserToken>().Configure();
            builder.Entity<Tenant>().Configure();
        }
    }
}
