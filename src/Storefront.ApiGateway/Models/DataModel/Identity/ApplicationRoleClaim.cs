using Microsoft.AspNetCore.Identity;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<long>
    {
        public long TenantId { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
