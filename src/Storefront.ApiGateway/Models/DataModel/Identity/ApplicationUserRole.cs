using Microsoft.AspNetCore.Identity;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public class ApplicationUserRole : IdentityUserRole<long>
    {
        public long TenantId { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
