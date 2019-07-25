using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Storefront.ApiGateway.Models.DataModel.Tenants;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public class ApplicationRole : IdentityRole<long>
    {
        public long TenantId { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
        public Tenant Tenant { get; set; }
    }
}
