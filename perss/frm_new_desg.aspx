<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="frm_new_desg.aspx.vb" Inherits="frm_new_desg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <table style="left: 178px; width: 545px; position: relative; top: 13px; height: 135px">
        <tr>
            <td style="width: 157px; height: 19px; text-align: center">
                <asp:Label ID="Label1" runat="server" Text="Designation"></asp:Label></td>
            <td style="width: 140px; height: 19px">
                <asp:TextBox ID="txtdesg" runat="server" Width="224px" AutoPostBack="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 19px; text-align: center">
            </td>
            <td style="width: 140px; height: 19px">
                <asp:GridView ID="GridView1" runat="server" Width="292px">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width: 157px; height: 19px; text-align: center">
                <asp:Label ID="Label8" runat="server" Text="Abbrivation"></asp:Label></td>
            <td style="width: 140px; height: 19px">
                <asp:TextBox ID="txtdabr" runat="server" Width="224px" AutoPostBack="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 19px; text-align: center">
            </td>
            <td style="width: 140px; height: 19px">
                <asp:GridView ID="GridView2" runat="server" Width="295px">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 23px; width: 157px; text-align: center;">
                &nbsp;<asp:Label ID="Label2" runat="server" Text="Service Type"></asp:Label></td>
            <td style="height: 23px; width: 140px;">
                <asp:DropDownList ID="drpserv" runat="server" Width="228px" AutoPostBack="True">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="10">Accounts</asp:ListItem>
                    <asp:ListItem Value="20">Technical</asp:ListItem>
                    <asp:ListItem Value="30">Engineering</asp:ListItem>
                    <asp:ListItem Value="40">General</asp:ListItem>
                    <asp:ListItem Value="99">Others</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 21px; width: 157px; text-align: center;">
                &nbsp;<asp:Label ID="Label3" runat="server" Text="Gaz/ Non-Gaz"></asp:Label></td>
            <td style="height: 21px; width: 140px;">
                <asp:DropDownList ID="drpgngz" runat="server" Width="227px" AutoPostBack="True">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="10">GAZ</asp:ListItem>
                    <asp:ListItem Value="20">Non Gaz</asp:ListItem>
                    <asp:ListItem Value="30">Workman</asp:ListItem>
                    <asp:ListItem Value="40">Workcharge</asp:ListItem>
                    <asp:ListItem Value="99">Others</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: center">
                <asp:Label ID="Label7" runat="server" Text="Hirarchy Code"></asp:Label></td>
            <td style="width: 140px; height: 21px">
                <asp:TextBox ID="txthecode" runat="server" Width="225px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: center">
                &nbsp;</td>
            <td style="width: 140px; height: 21px">
                <asp:GridView ID="GridView3" runat="server" Width="298px">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: center">
                <asp:Label ID="Label4" runat="server" Text="Class"></asp:Label></td>
            <td style="width: 140px; height: 21px">
                <asp:DropDownList ID="drpclass" runat="server" Width="227px">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>B</asp:ListItem>
                    <asp:ListItem>C</asp:ListItem>
                    <asp:ListItem>D</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: center">
                &nbsp;
                <asp:Label ID="Label5" runat="server" Text="Min Age"></asp:Label></td>
            <td style="width: 140px; height: 21px">
                <asp:TextBox ID="txtmage" runat="server" Width="224px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: center">
                &nbsp;
                <asp:Label ID="Label6" runat="server" Text="Retd. Age"></asp:Label></td>
            <td style="width: 140px; height: 21px">
                <asp:TextBox ID="txtrage" runat="server" Width="224px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: center">
                &nbsp;
                </td>
            <td style="width: 140px; height: 21px">
                </td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: center">
            </td>
            <td style="width: 140px; height: 21px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: center">
                &nbsp;<asp:Label ID="Label9" runat="server" Text="Cadre"></asp:Label></td>
            <td style="width: 140px; height: 21px">
                <asp:DropDownList ID="drpcadr" runat="server" Width="230px" AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: center">
                &nbsp;<asp:Label ID="Label10" runat="server" Text="Status"></asp:Label></td>
            <td style="width: 140px; height: 21px">
                <asp:DropDownList ID="drpstats" runat="server" Width="231px">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="1">Permanent</asp:ListItem>
                    <asp:ListItem Value="2">Temporary</asp:ListItem>
                    <asp:ListItem Value="3">Workcharge</asp:ListItem>
                    <asp:ListItem Value="4">WorkMan</asp:ListItem>
                    <asp:ListItem Value="5">Contigent</asp:ListItem>
                    <asp:ListItem Value="9">Others</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 21px; text-align: center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: right">
                </td>
            <td style="width: 140px; height: 21px">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="117px" /></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: right">
                &nbsp;</td>
            <td style="width: 140px; height: 21px">
                <asp:Button ID="btnUndo" runat="server" Text="Undo" Width="117px" 
                    Visible="False" /></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: right">
                &nbsp;</td>
            <td style="width: 140px; height: 21px">
                <asp:Button ID="btnPreview" runat="server" Text="Preview Report" 
                    Width="117px" Visible="False" /></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: right">
                &nbsp;</td>
            <td style="width: 140px; height: 21px">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate O/o" Width="117px" 
                    Visible="False" /></td>
        </tr>
        <tr>
            <td style="width: 157px; height: 21px; text-align: right">
            </td>
            <td style="width: 140px; height: 21px">
            </td>
        </tr>
    </table>
</asp:Content>

