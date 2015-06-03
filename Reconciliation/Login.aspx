<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!doctype html />
<html>
<head id="Head1" runat="server">
    <title></title>
       <style type="text/css">
        th
        {
            font: italic bold 15px serif; 
            background-color:#D5D5A6;
            color:#666633;
            }
        .style7
        {
            width: 19px;
        }
        </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
                <table align="center" 
            style="border: thick groove #666633; width: 43%; height: 313px;">
            <tr>
                <td colspan="2" align="center" 
                    
                    
                    
                    style="font-family: 'Times New Roman', Times, serif; font-size: large; font-weight: bold; font-style: oblique; font-variant: normal; text-transform: uppercase; color: #666633; background-color: #D5D5A6;">
                    Bank Payments Reconciliation<br />
                    Login</td>
            </tr>
            <tr>
                <td colspan="2" bgcolor="#666633">&nbsp;</td>
            </tr>
            <tr bgcolor="#D5D5A6">
                <td bgcolor="#D5D5A6" style="font-weight: 700" align="right">UserID</td>
                <td bgcolor="#D5D5A6">
                    <asp:TextBox ID="txtUserID" runat="server" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            
            <tr bgcolor="#D5D5A6">
                <td bgcolor="#D5D5A6" style="font-weight: 700" align="right">Password</td>
                <td bgcolor="#D5D5A6">
                    <asp:TextBox ID="txtPass" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            
            <tr bgcolor="#D5D5A6">
                <td bgcolor="#D5D5A6" style="font-weight: 700" align="right">&nbsp;</td>
                <td bgcolor="#D5D5A6">
                    <asp:Button ID="btnGenCode" runat="server" onclick="btnGenCode_Click" 
                        Text="Generate Code" />
                </td>
            </tr>
            
            <tr bgcolor="#D5D5A6">
                <td bgcolor="#D5D5A6" style="font-weight: 700" align="right">&nbsp;</td>
                <td bgcolor="#D5D5A6">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr bgcolor="#D5D5A6" id="rwCode">
                <td bgcolor="#D5D5A6" style="font-weight: 700" align="right">Enter Code</td>
                <td bgcolor="#D5D5A6">
                    <asp:TextBox ID="txtCode" runat="server" MaxLength="20" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            
            <tr bgcolor="#D5D5A6" id="rwButton">
                <td bgcolor="#D5D5A6" style="font-weight: 700">&nbsp;</td>
                <td bgcolor="#D5D5A6">
                    <asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" 
                        Text="Login" Enabled="False" />
                </td>
            </tr>
            
        </table>

    </div>
    </form>
</body>
</html>
