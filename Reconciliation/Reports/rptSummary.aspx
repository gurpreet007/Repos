<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptSummary.aspx.cs" Inherits="rptSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
        .style2
        {
            font-size: large;
            width: 469px;
        }
        .style3
        {
            font-size: large;
            width: 244px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table align="center" 
            style="border: thick groove #666633; width: 57%; height: 313px;">
            <tr>
                <td colspan="4" align="center" 
                    
                    
                    
                    
                    style="font-family: 'Times New Roman', Times, serif; font-size: large; font-weight: bold; font-style: oblique; font-variant: normal; text-transform: uppercase; color: #666633; background-color: #D5D5A6;">
                    Bank Payments Reconciliation</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#666633" align="right">
                    <asp:LinkButton ID="lbhome" runat="server" ForeColor="White" 
                         style="font-weight: 700" onclick="lbhome_Click">Home</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#666633" align="left" style="color: #FFFFFF">
                    <asp:Label ID="lblrpt_name" runat="server" 
                        style="font-weight: 700; font-size: large;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    From Date</td>
                <td>
                    <asp:TextBox ID="txtdt_frm" runat="server" type="date" Width="190px"></asp:TextBox>
                </td>
                <td class="style3">
                    To Date</td>
                <td>
                    <asp:TextBox ID="txtdt_to" runat="server" type="date" Width="190px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Category</td>
                <td>
                    <asp:DropDownList ID="drpcatg" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    <asp:Label ID="lblzone_cir" runat="server" Text="Circle"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpzone" runat="server" Width="200px" Visible="False">
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpcircle" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Division</td>
                <td>
                    <asp:DropDownList ID="drpdiv" runat="server" Width="200px">
                        <asp:ListItem Value="none">Select Division</asp:ListItem>
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    Sub Divsion</td>
                <td>
                    <asp:DropDownList ID="drpSubDiv" runat="server" Width="200px">
                        <asp:ListItem Value="none">Select Sub Division</asp:ListItem>
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnview" runat="server" Text="View"  
                        style="font-size: medium" onclick="btnview_Click" />
                </td>
                <td colspan="2">
                    <asp:Button ID="btnexp" runat="server" Text="Export To Excel" 
                         style="font-size: medium" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblmsg" runat="server" 
                        style="color: #CC3300; font-weight: 700; font-size: medium;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#D5D5A6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" bgcolor="#666633" class="style2">
                    </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
