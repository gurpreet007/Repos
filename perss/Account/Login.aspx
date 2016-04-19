<%@ Page Title="Log In" Language="VB" MasterPageFile="~/Site1.Master" AutoEventWireup="false"
    CodeFile="Login.aspx.vb" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Button ID="Button1" runat="server" Text="Bypass Login" Visible="False" />
    <br />
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <br />
    <div align=center>
    <asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" 
        BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" 
        DestinationPageUrl="~/Mainpage.aspx" 
        EnableTheming="True" Font-Names="Verdana" Font-Size="1em" ForeColor="#333333" 
        Height="210px" Width="376px" LoginButtonText="Enter" 
            RememberMeText="Dummy Login" DisplayRememberMe="False">
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <LoginButtonStyle BackColor="#616F92" BorderColor="#CCCCCC" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" Font-Size="1em" ForeColor="White" 
            Height="30px" Width="160px" Font-Bold="True" />
        <TextBoxStyle Font-Size="1em" Height="15px" Width="160px" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" 
            ForeColor="White" />
    </asp:Login>
    </div>
</asp:Content>