using System.ComponentModel.DataAnnotations;

namespace Storefront.ApiGateway.Models.TransferModel.Auth
{
    public sealed class WriteCredentialsJson
    {
        [Required, EmailAddress, MaxLength(80)]
        public string Email { get; set; }

        [Required, MaxLength(20)]
        public string Password { get; set; }
    }
}
