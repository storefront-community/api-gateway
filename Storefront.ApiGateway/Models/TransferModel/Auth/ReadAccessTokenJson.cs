using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Storefront.ApiGateway.Models.TransferModel.Auth
{
    public sealed class ReadAccessTokenJson : IActionResult
    {
        public ReadAccessTokenJson() { }

        public ReadAccessTokenJson(string token)
        {
            Token = token;
        }

        public string Token { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
