using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;

namespace WebFormsDemo.Pages
{
	public partial class Account : Page
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

			if (!IsPostBack)
				LoadClaims();
		}

		private void LoadClaims()
		{
			var identity = (ClaimsIdentity)User.Identity;

			ClaimsGrid.DataSource = identity.Claims
				.OrderBy(c => c.Type)
				.Select(c => new
				{
					Type = c.Type,
					Value = c.Value
				})
				.ToList();

			ClaimsGrid.DataBind();
		}
	}
}