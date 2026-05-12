using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsDemo
{
	public partial class SiteMaster : MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.IsAuthenticated)
			{
				var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

				var displayName =
					identity.FindFirst("preferred_username")?.Value
					?? identity.FindFirst(ClaimTypes.Name)?.Value
					?? HttpContext.Current.User.Identity.Name;

				var userNameLabel = (Label)FindControlInLoginView("LoginView", "UserName");

				if (userNameLabel != null)
					userNameLabel.Text = Server.HtmlEncode(displayName);
			}
		}


		protected void SignIn_Click(object sender, EventArgs e)
		{
			if (!Request.IsAuthenticated)
			{
				HttpContext.Current.GetOwinContext().Authentication.Challenge(
					new AuthenticationProperties { RedirectUri = "/" },
					OpenIdConnectAuthenticationDefaults.AuthenticationType);
			}
		}

		protected void SignOut_Click(object sender, EventArgs e)
		{
			if (Request.IsAuthenticated)
			{
				HttpContext.Current.GetOwinContext().Authentication.SignOut(
					OpenIdConnectAuthenticationDefaults.AuthenticationType,
					CookieAuthenticationDefaults.AuthenticationType);
			}
		}
		private Control FindControlInLoginView(string loginViewId, string controlId)
		{
			LoginView loginView = FindControl(loginViewId) as LoginView;

			if (loginView == null) return null;

			foreach (Control ctrl in loginView.Controls)
			{
				Control found = ctrl.FindControl(controlId);
				if (found != null) return found;
			}

			return null;
		}
	}
}