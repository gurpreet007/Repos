<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="frmlocdetailed.aspx.vb" Inherits="frmlocdetailed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 32px;
        }
        .style2
        {
            height: 34px;
        }
        .style3
        {
            height: 43px;
        }
        .style4
        {
            height: 34px;
            width: 149px;
        }
        .style5
        {
            height: 32px;
            width: 149px;
        }
        .style6
        {
            width: 149px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

   <div align = "center">
        <table style="width: 93%; height: 268px;">
        <tr>
            <td colspan="2" align ="center" class="style3">
                <asp:Label ID="Label3" runat="server" 
                    style="font-weight: 700; font-family: Arial; font-size: large" 
                    Text="Individual Location Wise Report"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style4" align="right">
                <asp:Label ID="Label4" runat="server" Text="Select Location"></asp:Label>
            </td>
            <td style="text-align: left" class="style2">
                <asp:DropDownList ID="drploc" runat="server" Width="430px" AutoPostBack="True" 
                    Height="29px">
                </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chkreploc" runat="server" 
                    Text="Include Reporting Offices" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td class="style5" align="right">
                <asp:Label ID="Label5" runat="server" Text="Filter Location"></asp:Label>
            </td>
            <td style="text-align: left" class="style1">
                <asp:TextBox ID="txtfloc" runat="server" AutoPostBack="True" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" align="right">
                <asp:Label ID="Label6" runat="server" Text="Select Designation"></asp:Label>
            </td>
            <td style="text-align: left" class="style1">
                <asp:DropDownList ID="drpdesg" runat="server" Width="300px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style5" align="right">
                <asp:Label ID="Label7" runat="server" Text="Filter Designation"></asp:Label>
            </td>
            <td style="text-align: left" class="style1">
                <asp:TextBox ID="txtfdesg" runat="server" AutoPostBack="True" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" align="right">
                <asp:Label ID="Label8" runat="server" Text="Select Cadre"></asp:Label>
            </td>
            <td style="text-align: left" class="style1">
                <asp:DropDownList ID="drpbranch" runat="server" width="303px">
                    <asp:ListItem Value="0">All</asp:ListItem>
                    <asp:ListItem Value="1">Electrical</asp:ListItem>
                    <asp:ListItem Value="4">Civil</asp:ListItem>
                    <asp:ListItem Value="99">Not Known</asp:ListItem>
                </asp:DropDownList>
            </td>
                <td>
                                              &nbsp;</td>
        </tr>
        <tr>
            <td class="style6">
                &nbsp;</td>
            <td style="text-align: left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td class="style6">
                &nbsp;</td>
            <td style="text-align: left">
                <asp:Button ID="btnShow" runat="server" Text="Show Data" Width="170px" />
            </td>
        </tr>
    </table>
    </div>


</asp:Content>

