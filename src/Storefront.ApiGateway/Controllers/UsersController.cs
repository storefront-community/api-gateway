using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Storefront.ApiGateway.Authorization;
using Storefront.ApiGateway.Models.DataModel.Identity;
using Storefront.ApiGateway.Models.TransferModel.Auth;

namespace Storefront.ApiGateway.Controllers
{
    [Route("auth")]
    public sealed class UsersController : Controller
    {
        private readonly JwtOptions _jwtOptions;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IOptions<JwtOptions> jwtOptions,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost, Route(""), AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] CredentialsModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.IsNotAllowed)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
            );

            token.Payload.Add("userId", user.Id);
            token.Payload.Add("tenantId", user.TenantId);

            return new AccessTokenJson(
                new JwtSecurityTokenHandler().WriteToken(token)
            );
        }
    }
}
