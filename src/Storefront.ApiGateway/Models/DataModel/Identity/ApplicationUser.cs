using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Storefront.ApiGateway.Models.DataModel.Tenants;

namespace Storefront.ApiGateway.Models.DataModel.Identity
{
    public class ApplicationUser : IdentityUser<long>
    {
        public long TenantId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<ApplicationUserClaim> UserClaims { get; set; }
        public ICollection<ApplicationUserLogin> UserLogins { get; set; }
        public ICollection<ApplicationUserToken> UserTokens { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public Tenant Tenant { get; set; }
    }
}
