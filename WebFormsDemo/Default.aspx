<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Default.aspx.cs"
Inherits="WebFormsDemo.Default" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Home
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<div class="jumbotron p-4 bg-light rounded">
		<h1>Welcome to WebFormsDemo</h1>
		<p class="lead">
			This application uses <strong>Microsoft Entra ID</strong> for authentication
			via the <strong>OWIN OpenID Connect</strong> middleware.
		</p>

		<asp:Panel ID="AuthenticatedPanel" runat="server" Visible="false"
		           CssClass="alert alert-success">
			✅ You are signed in as <strong>
				<asp:Label ID="UserGreeting" runat="server" />
			</strong>
		</asp:Panel>

		<asp:Panel ID="AnonymousPanel" runat="server" Visible="false"
		           CssClass="alert alert-info">
			ℹ️ You are browsing anonymously. <a href="Pages/Protected.aspx">
				Visit the protected page
			</a> to trigger a sign-in.
		</asp:Panel>
	</div>

</asp:Content>