using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace ToDo.Api.Controllers
{
    [Route("/")]
    public class AuthenticationController : Controller
    {
        public AuthenticationController()
        {
            int x = 0;
        }
        // GET: api/authentication/token?...
        [HttpGet("signin-google1")]
        public ActionResult Auth()
        {
            var properties = new AuthenticationProperties()
            {
                // actual redirect endpoint for your app
                RedirectUri = $"/ToDo/v1",
                Items =
                {
                    { "LoginProvider", "GoogleOpenIdConnect" },
                },
                AllowRefresh = true,
            };

            return Challenge(properties, "GoogleOpenIdConnect");
        }

    //    [HttpPost("~/signin")]
    //    public async Task<IActionResult> SignIn([FromForm] string provider)
    //    {
    //        // Note: the "provider" parameter corresponds to the external
    //        // authentication provider choosen by the user agent.
    //        if (string.IsNullOrWhiteSpace(provider))
    //        {
    //            return BadRequest();
    //        }

    //        if (!await HttpContext.IsProviderSupportedAsync(provider))
    //        {
    //            return BadRequest();
    //        }

    //        // Instruct the middleware corresponding to the requested external identity
    //        // provider to redirect the user agent to its own authorization endpoint.
    //        // Note: the authenticationScheme parameter must match the value configured in Startup.cs
    //        return Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
    //    }

    //    [HttpGet("~/signout")]
    //    [HttpPost("~/signout")]
    //    public IActionResult SignOut()
    //    {
    //        // Instruct the cookies middleware to delete the local cookie created
    //        // when the user agent is redirected from the external identity provider
    //        // after a successful authentication flow (e.g Google or Facebook).
    //        return SignOut(new AuthenticationProperties { RedirectUri = "/" },
    //            CookieAuthenticationDefaults.AuthenticationScheme);
    //    }
    //}

    //public static class HttpContextExtensions
    //{
    //    public static async Task<AuthenticationScheme[]> GetExternalProvidersAsync(this HttpContext context)
    //    {
    //        if (context == null)
    //        {
    //            throw new ArgumentNullException(nameof(context));
    //        }

    //        var schemes = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();

    //        return (from scheme in await schemes.GetAllSchemesAsync()
    //                where !string.IsNullOrEmpty(scheme.DisplayName)
    //                select scheme).ToArray();
    //    }

    //    public static async Task<bool> IsProviderSupportedAsync(this HttpContext context, string provider)
    //    {
    //        if (context == null)
    //        {
    //            throw new ArgumentNullException(nameof(context));
    //        }

    //        return (from scheme in await context.GetExternalProvidersAsync()
    //                where string.Equals(scheme.Name, provider, StringComparison.OrdinalIgnoreCase)
    //                select scheme).Any();
    //    }
    }
}
