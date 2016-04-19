<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="cc.aspx.vb" Inherits="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 42px;
        }
        .style2
        {
            width: 79px;
        }
        .style3
        {
            width: 324px;
        }
        .style4
        {
            width: 63px;
        }
        .style6
        {
            width: 51px;
        }
        .style7
        {
            width: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style1" colspan="2">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style1" colspan="2">
                <asp:Label ID="Label1" runat="server" Text="Available Locations" Width="150px"></asp:Label>
            </td>
            <td class="style4">
                &nbsp;</td>
            <td class="style3">
                <asp:Label ID="Label2" runat="server" Text="Locations to be shown in CC list"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style1" rowspan="2" align="right" colspan="2">
                <asp:ListBox ID="lstAll" runat="server" Height="180px" Width="328px">
                </asp:ListBox>
            </td>
            <td class="style4">
                <asp:Button ID="btnAdd" runat="server" Height="26px" Text="----&gt;" 
                    Width="69px" />
            </td>
            <td class="style3" rowspan="2">
                <asp:ListBox ID="lstCC" runat="server" height="180px" width="328px">
                </asp:ListBox>
            </td>
            <td>
                <asp:Button ID="btnUp" runat="server" Height="30px" Text="^" Width="37px" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style4">
                <asp:Button ID="btnRemove" runat="server" height="26px" Text="&lt;----" 
                    width="69px" />
            </td>
            <td>
                <asp:Button ID="btnDown" runat="server" height="32px" Text="v" width="37px" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style6" align="right">
                <asp:Button ID="btnAddLoc" runat="server" height="30px" 
                    Text="Add Location" width="158px" />
            </td>
            <td class="style7">
                <asp:Button ID="btnRemLoc" runat="server" height="30px" Text="Remove Location" 
                    width="153px" />
            </td>
            <td class="style4">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style1" colspan="2">
                <asp:TextBox ID="txtNewLoc" runat="server" Width="332px" AutoPostBack="True" 
                    ToolTip="Search Location"></asp:TextBox>
            </td>
            <td class="style4">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style1" colspan="2">
                &nbsp;</td>
            <td class="style4">
                <asp:Button ID="btnDone" runat="server" Height="27px" Text="Done" 
                    Width="69px" />
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

