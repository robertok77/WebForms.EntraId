using Microsoft.Owin;
using Owin;
using WebFormsDemo;

[assembly: OwinStartup(typeof(Startup))]

namespace WebFormsDemo
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}