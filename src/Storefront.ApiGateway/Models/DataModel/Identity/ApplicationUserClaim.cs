using Microsoft.AspNetCore.Identity;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<long>
    {
        public long TenantId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
