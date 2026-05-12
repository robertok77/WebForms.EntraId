using System;
using System.Security.Claims;
using System.Web.UI;

namespace WebFormsDemo
{
	public partial class Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.IsAuthenticated)
			{
				AuthenticatedPanel.Visible = true;
				AnonymousPanel.Visible = false;

				var identity = (ClaimsIdentity)User.Identity;
				var displayName = identity.FindFirst("preferred_username")?.Value
				                  ?? User.Identity.Name;

				UserGreeting.Text = Server.HtmlEncode(displayName);
			}
			else
			{
				AuthenticatedPanel.Visible = false;
				AnonymousPanel.Visible = true;
			}
		}
	}
}