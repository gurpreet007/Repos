<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="frm_new_location.aspx.vb" Inherits="frm_new_location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<br />
    <br />
   <div align="center" style="height: 472px" >
    <table style="width: 459px; height: 1px; align: Center;" frame="box">
        <tr>
            <td style="width: 127px; height: 11px; text-align: left; background-color: paleturquoise;">
                &nbsp;</td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" 
                    Height="16px" RepeatDirection="Horizontal" Width="294px">
                    <asp:ListItem Selected="True" Value="P">PSPCL</asp:ListItem>
                    <asp:ListItem Value="T">PSTCL</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 127px; height: 11px; text-align: left; background-color: paleturquoise;">
                &nbsp;<asp:Label ID="Label6" runat="server" Text="Organisation Type"></asp:Label></td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:DropDownList ID="drpcorp" runat="server" Height="22px" Width="253px" 
                    AutoPostBack="True">
                <asp:ListItem Value="O">--Select--</asp:ListItem>
                <asp:ListItem Value="D">DISTRIBUTION</asp:ListItem>
                <asp:ListItem Value="G">GENERATION</asp:ListItem>
                <asp:ListItem Value="T">TRANSMISSION</asp:ListItem>
                <asp:ListItem Value="C">PSPCL HEAD OFFICE</asp:ListItem>
                <asp:ListItem Value="X">DEPUTATION</asp:ListItem>
            </asp:DropDownList></td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
            </td>
        </tr>
        <tr>
            <td style="width: 127px; text-align: center; vertical-align: middle; background-color: paleturquoise;" rowspan="3">
                <asp:Label ID="Label1" runat="server" Text="Organisation"></asp:Label></td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise; height: 26px;">
                <asp:DropDownList ID="drporg" runat="server" Height="22px" Width="253px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="vertical-align: middle; text-align: center; background-color: paleturquoise;" rowspan="3">
                <asp:Button ID="btnorg" runat="server" Text="Add" Width="40px" Height="23px" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; background-color: paleturquoise; text-align: center">
                <asp:TextBox ID="txtorg" runat="server" Visible="False" Width="253px" 
                    height="22px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:Label ID="lbloabr" runat="server" Text="Abb." Visible="False"></asp:Label>
                <asp:TextBox ID="txtabbr" runat="server" Visible="False" Width="221px" 
                    height="22px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 127px; text-align: center; vertical-align: middle; background-color: paleturquoise;" rowspan="3">
                <asp:Label ID="Label2" runat="server" Text="Circle" Width="35px" Height="12px"></asp:Label></td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:DropDownList ID="drpcir" runat="server" Height="22px" Width="253px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="vertical-align: middle; text-align: center; background-color: paleturquoise;" rowspan="3">
                <asp:Button ID="btncir" runat="server" Text="Add" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; background-color: paleturquoise; text-align: center">
                <asp:TextBox ID="txtcir" runat="server" Visible="False" Width="253px" 
                    height="22px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:Label ID="lblcabr" runat="server" Text="Abb." Visible="False"></asp:Label>
                <asp:TextBox ID="txtcabbr" runat="server" Visible="False" Width="221px" 
                    height="22px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 127px; text-align: center; vertical-align: middle; background-color: paleturquoise;" rowspan="3">
                <asp:Label ID="Label3" runat="server" Text="Division Office"></asp:Label></td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:DropDownList ID="drpdiv" runat="server" Height="22px" Width="253px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="vertical-align: middle; text-align: center; background-color: paleturquoise;" rowspan="3">
                <asp:Button ID="btndiv" runat="server" Text="Add" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; background-color: paleturquoise; text-align: center">
                <asp:TextBox ID="txtdiv" runat="server" Visible="False" Width="253px" 
                    Height="22px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:Label ID="lbldabbr" runat="server" Text="Abb." Visible="False"></asp:Label>
                <asp:TextBox ID="txtdabbr" runat="server" Visible="False" Width="217px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 127px; text-align: center; vertical-align: middle; background-color: paleturquoise;" rowspan="3">
                <asp:Label ID="Label4" runat="server" Text="Sub Division"></asp:Label></td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:DropDownList ID="drpsdiv" runat="server" Height="22px" Width="253px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="vertical-align: middle; text-align: center; background-color: paleturquoise;" rowspan="3">
                <asp:Button ID="btnsdiv" runat="server" Text="Add" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; background-color: paleturquoise; text-align: center">
                <asp:TextBox ID="txtsdiv" runat="server" Visible="False" Width="253px" 
                    Height="22px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:Label ID="lblsdabr" runat="server" Text="Abb." Visible="False"></asp:Label>
                <asp:TextBox
                    ID="txtsdabr" runat="server" Visible="False" Width="217px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: center; background-color: paleturquoise;" rowspan="3">
                <asp:Label ID="Label7" runat="server" Text="Sub Office"></asp:Label></td>
            <td style="height: 13px; text-align: center; background-color: paleturquoise;">
                <asp:DropDownList ID="drpsoff" runat="server" Height="22px" Width="253px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="text-align: center; background-color: paleturquoise;" rowspan="3">
                <asp:Button ID="btnsoff" runat="server" Text="Add" /></td>
        </tr>
        <tr>
            <td style="height: 13px; text-align: center; background-color: paleturquoise;">
                <asp:TextBox ID="txtsoff" runat="server" Visible="False" Width="253px" 
                    Height="22px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="height: 13px; text-align: center; background-color: paleturquoise;">
                <asp:Label ID="lblsoffabr" runat="server" Text="Abb." Visible="False"></asp:Label>
                <asp:TextBox
                    ID="txtsoffabr" runat="server" Visible="False" Width="217px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="3" style="height: 13px; text-align: center; background-color: paleturquoise;">
                <asp:Button ID="btnPreview" runat="server" Text="See Preview" Visible="False" />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 13px; text-align: center; background-color: paleturquoise;">
                <asp:Button ID="btnUndo" runat="server" Text="Undo Changes" Visible="False" />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 13px; text-align: center; background-color: paleturquoise;">
                <asp:Button ID="btnSave" runat="server" Text="Save and Generate O/O" 
                    Visible="False" />
            </td>
        </tr>
    </table>
</div>

</asp:Content>

