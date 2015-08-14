<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptabstract.aspx.cs" Inherits="rptabstract" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table align="center" 
            style="border: thick groove #666633; width: 57%; height: 313px;">
            <tr>
                <td colspan="5" align="center" 
                    
                    
                    
                    style="font-family: 'Times New Roman', Times, serif; font-size: large; font-weight: bold; font-style: oblique; font-variant: normal; text-transform: uppercase; color: #666633; background-color: #D5D5A6;">
                    Bank Payments Reconciliation</td>
            </tr>
            <tr>
                <td colspan="5" bgcolor="#666633" align="right">
                    <asp:LinkButton ID="lbhome" runat="server" ForeColor="White" 
                        onclick="lbhome_Click" style="font-weight: 700">Home</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="5" bgcolor="#666633" align="left" style="color: #FFFFFF">
                    <asp:Label ID="lblrpt_name" runat="server" 
                        style="font-weight: 700; font-size: large;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    From Month</td>
                <td>
                    <asp:DropDownList ID="drpmonth" runat="server" style="font-size: medium">
                        <asp:ListItem Value="0">Select Month</asp:ListItem>
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style1">
                    From Year</td>
                <td>
                    <asp:DropDownList ID="drpyear" runat="server" style="font-size: medium">
                        <asp:ListItem Value="0">Select Year</asp:ListItem>
                        <asp:ListItem>2015</asp:ListItem>
                        <asp:ListItem>2016</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnview" runat="server" Text="View" onclick="btnview_Click" 
                        style="font-size: medium" />
                </td>
                <td colspan="2">
                    <asp:Button ID="btnexp" runat="server" Text="Export To Excel" 
                        onclick="btnexp_Click" style="font-size: medium" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblmsg" runat="server" 
                        style="color: #CC3300; font-weight: 700; font-size: medium;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="5" bgcolor="#D5D5A6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="5" bgcolor="#666633" class="style2">
                    </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
