using System;
using System.Collections.Generic;
using Storefront.ApiGateway.Models.DataModel.Identity;

namespace Storefront.ApiGateway.Models.DataModel.Tenants
{
    public class Tenant
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string BusinessName { get; set; }
        public string BillingEmail { get; set; }
        public ICollection<ApplicationRole> Roles { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
