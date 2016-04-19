<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="locrptcont.aspx.vb" Inherits="rptcont" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 465px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
        <p>
            <br />
        </p>
   <div align = "center">
        <table style="width: 62%; height: 113px;">
        <tr>
            <td align ="center">
                <asp:Label ID="Label3" runat="server" 
                    style="font-weight: 700; font-family: Arial; font-size: large" 
                    Text="Location Wise List of Posts"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align ="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td align ="center">
                <asp:TextBox ID="txtLocFilter" runat="server" AutoPostBack="True" Width="200px">Loc Search</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align ="center">
                <asp:DropDownList ID="drpLocs" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align ="center">
                <asp:TextBox ID="txtDesgFilter" runat="server" AutoPostBack="True" 
                    Width="200px">Desg Search</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align ="center">
                <asp:DropDownList ID="drpDesgs" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align ="center">
                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" />
            </td>
        </tr>
        </table>
    </div>

</asp:Content>

