using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebFormsDemo
{
	public class Global : HttpApplication
	{
		void Application_Start(object sender, EventArgs e)
		{
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		void Application_BeginRequest(object sender, EventArgs e)
		{
			if (!Request.IsLocal && !Request.IsSecureConnection)
			{
				var secureUrl = Request.Url.ToString().Replace("http://", "https://");
				Response.Redirect(secureUrl, true);
			}
		}
	}
}