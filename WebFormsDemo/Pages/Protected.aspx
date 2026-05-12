<%@ Page Title="Protected Page" Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Protected.aspx.cs"
Inherits="WebFormsDemo.Pages.Protected" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Protected Page
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<h2>🔒 Protected Page</h2>
	<p>Only authenticated users can see this content.</p>

	<div class="card mt-3">
		<div class="card-header bg-success text-white">
			✅ Authentication Successful
		</div>
		<div class="card-body">
			<p>Welcome, <strong><asp:Label ID="UserLabel" runat="server" /></strong>!</p>
			<p>Your sign-in time: <asp:Label ID="SignInTimeLabel" runat="server" /></p>
			<a href="Account.aspx" class="btn btn-primary">View All My Claims</a>
		</div>
	</div>

</asp:Content>