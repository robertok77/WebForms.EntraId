using System;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;

namespace WebFormsDemo.Pages
{
	public partial class Protected : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Request.IsAuthenticated)
			{
				HttpContext.Current.GetOwinContext().Authentication.Challenge(
					new AuthenticationProperties { RedirectUri = Request.Url.ToString() },
					OpenIdConnectAuthenticationDefaults.AuthenticationType);

				Response.End();
				return;
			}

			var identity = (ClaimsIdentity)User.Identity;

			var displayName = identity.FindFirst("preferred_username")?.Value
			                  ?? identity.FindFirst(ClaimTypes.Name)?.Value
			                  ?? User.Identity.Name;

			UserLabel.Text = Server.HtmlEncode(displayName);

			var signedInAt = identity.FindFirst("app:SignedInAt")?.Value;
			SignInTimeLabel.Text = string.IsNullOrEmpty(signedInAt)
				? "N/A"
				: DateTime.Parse(signedInAt).ToLocalTime().ToString("f");
		}
	}
}