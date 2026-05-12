using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace WebFormsDemo
{
	public partial class Startup
	{
		private static readonly string ClientId =
			ConfigurationManager.AppSettings["ida:ClientId"];

		private static readonly string ClientSecret =
			ConfigurationManager.AppSettings["ida:ClientSecret"];

		private static readonly string Authority =
			ConfigurationManager.AppSettings["ida:Authority"];

		private static readonly string RedirectUri =
			ConfigurationManager.AppSettings["ida:RedirectUri"];

		private static readonly string PostLogoutRedirectUri =
			ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];

		public void ConfigureAuth(IAppBuilder app)
		{
			app.SetDefaultSignInAsAuthenticationType(
				CookieAuthenticationDefaults.AuthenticationType);

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				ExpireTimeSpan = TimeSpan.FromHours(8),
				SlidingExpiration = true,
				CookieSecure = CookieSecureOption.Always,   
				CookieHttpOnly = true
			});

			app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
			{
				ClientId = ClientId,
				ClientSecret = ClientSecret,   
				Authority = Authority,
				RedirectUri = RedirectUri,
				PostLogoutRedirectUri = PostLogoutRedirectUri,

				ResponseType = "code id_token",

				Scope = "openid profile email",

				TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					NameClaimType = "preferred_username"
				},

				Notifications = new OpenIdConnectAuthenticationNotifications
				{
					AuthenticationFailed = OnAuthenticationFailed,

					RedirectToIdentityProvider = OnRedirectToIdentityProvider,

					SecurityTokenValidated = OnSecurityTokenValidated
				}
			});

			app.UseStageMarker(PipelineStage.Authenticate);
		}

		private Task OnAuthenticationFailed(
			AuthenticationFailedNotification<
				Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectMessage,
				OpenIdConnectAuthenticationOptions> context)
		{
			context.HandleResponse();

			context.Response.Redirect("/Error.aspx?message=" +
				Uri.EscapeDataString(context.Exception.Message));
			return Task.CompletedTask;
		}

		private Task OnRedirectToIdentityProvider(
			RedirectToIdentityProviderNotification<
				Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectMessage,
				OpenIdConnectAuthenticationOptions> context)
		{
			if (context.OwinContext.Response.StatusCode == 401 &&
				context.OwinContext.Authentication.User?.Identity?.IsAuthenticated == true)
			{
				context.OwinContext.Response.StatusCode = 403;
				context.HandleResponse();
			}
			return Task.CompletedTask;
		}

		private Task OnSecurityTokenValidated(
			SecurityTokenValidatedNotification<
				Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectMessage,
				OpenIdConnectAuthenticationOptions> context)
		{

			var redirectUri = new Uri(
				context.AuthenticationTicket.Properties.RedirectUri,
				UriKind.RelativeOrAbsolute);

			if (redirectUri.IsAbsoluteUri)
				context.AuthenticationTicket.Properties.RedirectUri =
					redirectUri.PathAndQuery;

			var identity = context.AuthenticationTicket.Identity;
			identity.AddClaim(new System.Security.Claims.Claim(
				"app:SignedInAt",
				DateTime.UtcNow.ToString("o")));

			return Task.CompletedTask;
		}
	}
}