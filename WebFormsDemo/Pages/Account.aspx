<%@ Page Title="My Account" Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Account.aspx.cs"
Inherits="WebFormsDemo.Pages.Account" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	My Account
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<h2>👤 My Account — JWT Claims</h2>
	<p class="text-muted">
		These claims were extracted from the Entra ID id_token and stored in your session.
	</p>

	<asp:GridView ID="ClaimsGrid" runat="server"
	              CssClass="table table-striped table-bordered mt-3"
	              AutoGenerateColumns="False"
	              GridLines="None">
		<HeaderStyle CssClass="table-dark" />
		<Columns>
			<asp:BoundField DataField="Type"  HeaderText="Claim Type"  />
			<asp:BoundField DataField="Value" HeaderText="Claim Value" />
		</Columns>
	</asp:GridView>

</asp:Content>