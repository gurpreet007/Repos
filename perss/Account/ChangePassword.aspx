<%@ Page Title="Change Password" Language="VB" MasterPageFile="~/Site1.Master" AutoEventWireup="false"
    CodeFile="ChangePassword.aspx.vb" Inherits="Account_ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        &nbsp;</h2>
    <p>
        &nbsp;</p>
    <p>
        <div align=center>
        <asp:ChangePassword ID="ChangePassword1" runat="server" BackColor="#F7F6F3" 
            BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" 
            CancelDestinationPageUrl="~/Mainpage.aspx" 
            ContinueDestinationPageUrl="~/Mainpage.aspx" Font-Names="Verdana" 
            Font-Size="1em" Height="247px" 
            InstructionText="New Password should be of minimum 6 characters" Width="405px">
            <CancelButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
                ForeColor="#284775" Height="30px" Width="160px" />
            <ChangePasswordButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
                ForeColor="#284775" Height="30px" Width="160px" />
            <ContinueButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
                ForeColor="#284775" />
            <InstructionTextStyle Font-Italic="True" Font-Size="Small" ForeColor="Black" />
            <PasswordHintStyle Font-Italic="True" ForeColor="#888888" />
            <TextBoxStyle Font-Size="1em" Font-Strikeout="True" Height="15px" 
                Width="160px" />
            <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" 
                ForeColor="White" />
        </asp:ChangePassword>
        </div>
    </p>
</asp:Content>