using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Xperters.Admin.Api.Controllers
{
	[Route("[controller]/[action]")]
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult SignIn()
		{
			var redirectUrl = HttpContext.Request.QueryString.HasValue
				? HttpContext.Request.QueryString.Value
				: "/api/metadata";

			redirectUrl = redirectUrl.Replace("?redirect=", "");
			redirectUrl = WebUtility.UrlDecode(redirectUrl);

			return Challenge(
				new AuthenticationProperties {RedirectUri = redirectUrl},
				OpenIdConnectDefaults.AuthenticationScheme);
		}

		[HttpGet]
		public IActionResult SignOut()
		{
			var callbackUrl = Url.Action(nameof(SignedOut), "Account", null, Request.Scheme);
			return SignOut(
				new AuthenticationProperties {RedirectUri = callbackUrl},
				CookieAuthenticationDefaults.AuthenticationScheme,
				OpenIdConnectDefaults.AuthenticationScheme);
		}

		[HttpGet]
		public IActionResult SignedOut() => new RedirectResult("/api/metadata");
	}
}