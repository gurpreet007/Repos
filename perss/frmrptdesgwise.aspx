<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="frmrptdesgwise.aspx.vb" Inherits="frmrptdesgwise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    .style1
    {
        height: 20px;
    }
        .style2
        {
            height: 40px;
        }
        .style3
        {
            height: 47px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

        <p>
            <br />
        </p>
   <div align = "center">
        <table style="width: 55%; height: 168px;">
        <tr>
            <td colspan="2" align ="center" class="style3">
                <asp:Label ID="Label3" runat="server" 
                    style="font-weight: 700; font-family: Arial; font-size: large" 
                    Text="Designation Wise Report"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label4" runat="server" Text="Select Designation"></asp:Label>
            </td>
            <td style="text-align: left" class="style2">
                <asp:DropDownList ID="drpdesg" runat="server" height="22px" width="288px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label5" runat="server" Text="Filter Designation"></asp:Label>
            </td>
            <td style="text-align: left" class="style1">
                <asp:TextBox ID="txtsdesg" runat="server" Width="284px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label6" runat="server" Text="Select Branch"></asp:Label>
            </td>
            <td style="text-align: left" class="style1">
                <asp:DropDownList ID="drpbranch" runat="server" height="22px" width="288px" 
                    AutoPostBack="True">
                    <asp:ListItem Value="1">Electrical</asp:ListItem>
                    <asp:ListItem Value="4">Civil</asp:ListItem>
                    <asp:ListItem Value="99">Not Known</asp:ListItem>
                    <asp:ListItem Value="0">All</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left">
                <asp:Button ID="btnshow" runat="server" Text="Show" UseSubmitBehavior="False" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnclr" runat="server" Text="Clear" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:LinkButton runat="server" OnClick="downDesgParams_Click">Download Desg. Parameters</asp:LinkButton>
            </td>
        </tr>
    </table>
    </div>

</asp:Content>

