<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="frm_desg_edit.aspx.vb" Inherits="frm_desg_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style4
        {
            height: 120px;
        }
        .style5
        {
            width: 112px;
        }
        .style6
        {
        }
        .style8
        {
            height: 11px;
            width: 243px;
        }
        .style9
        {
            height: 25px;
            width: 243px;
        }
        .style10
        {
            width: 109px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
    <div align="center" >
   
    <br />
    <br />
    <table style="width: 459px; height: 1px">
        <tr>
            <td style="text-align: left" class="style8">
                Select Designation</td>
            <td style="width: 3px; height: 11px; text-align: left" colspan="2">
                <asp:DropDownList ID="drpdesg" runat="server" Height="25px" Width="300px" 
                    AutoPostBack="True">
                <asp:ListItem Value="0">--Select--</asp:ListItem>
                <asp:ListItem Value="Zone">Zone</asp:ListItem>
                <asp:ListItem>Circle</asp:ListItem>
                <asp:ListItem>Division</asp:ListItem>
                <asp:ListItem Value="SDivision">S/Division</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top;" class="style9">
                Filter Designation</td>
            <td style="width: 3px; height: 25px; text-align: left;" colspan="2">
                <asp:TextBox ID="txtFilter" runat="server" Width="295px" AutoPostBack="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top;" class="style9">
                &nbsp;</td>
            <td style="width: 3px; height: 25px; text-align: left;">
                <asp:Button ID="btnEdit" runat="server" Enabled="False" 
                    Text="Edit Designation" Width="168px" />
            </td>
            <td style="width: 3px; height: 25px; text-align: left;">
                <asp:Button ID="btnAbolish" runat="server" Enabled="False" 
                    Text="Abolish Designation" Width="168px" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top;" class="style4" colspan="3">
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:Panel ID="panEdit" runat="server" Visible="False">
                                <table style="width:100%;">
                                    <tr>
                                        <td class="style5">
                                            Designation</td>
                                        <td>
                                            <asp:TextBox ID="txtDesg" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            <asp:Label ID="lbloabr" runat="server" Text="Abbrivation"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtabbr" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            Service Type</td>
                                        <td>
                                            <asp:DropDownList ID="drpserv" runat="server" Width="228px" AutoPostBack="True">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="10">Accounts</asp:ListItem>
                                                <asp:ListItem Value="20">Technical</asp:ListItem>
                                                <asp:ListItem Value="30">Engineering</asp:ListItem>
                                                <asp:ListItem Value="40">General</asp:ListItem>
                                                <asp:ListItem Value="99">Others</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            Gaz/ Non-Gaz</td>
                                        <td>
                                            <asp:DropDownList ID="drpgngz" runat="server" Width="227px" AutoPostBack="True">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="10">GAZ</asp:ListItem>
                                                <asp:ListItem Value="20">Non Gaz</asp:ListItem>
                                                <asp:ListItem Value="30">Workman</asp:ListItem>
                                                <asp:ListItem Value="40">Workcharge</asp:ListItem>
                                                <asp:ListItem Value="99">Others</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            Hierarchy Code</td>
                                        <td>
                                            <asp:TextBox ID="txthecode" runat="server" Width="50px" MaxLength="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" Width="292px">
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            Class</td>
                                        <td>
                                            <asp:DropDownList ID="drpclass" runat="server" Width="227px">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem>A</asp:ListItem>
                                                <asp:ListItem>B</asp:ListItem>
                                                <asp:ListItem>C</asp:ListItem>
                                                <asp:ListItem>D</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            Min Age</td>
                                        <td>
                                            <asp:TextBox ID="txtMinAge" runat="server" Width="50px" MaxLength="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            Retd. Age</td>
                                        <td>
                                            <asp:TextBox ID="txtRetdAge" runat="server" Width="50px" MaxLength="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            Cadre
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpcadr" runat="server" Width="230px" AutoPostBack="True">
                                                </asp:DropDownList></td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            Status
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpstats" runat="server" Width="231px">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Permanent</asp:ListItem>
                                                <asp:ListItem Value="2">Temporary</asp:ListItem>
                                                <asp:ListItem Value="3">Workcharge</asp:ListItem>
                                                <asp:ListItem Value="4">WorkMan</asp:ListItem>
                                                <asp:ListItem Value="5">Contigent</asp:ListItem>
                                                <asp:ListItem Value="9">Others</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnsave" runat="server" Text="Save" Width="168px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanAbolish" runat="server" Visible="False">
                                <table style="width:100%;">
                                    <tr>
                                        <td class="style6" colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            Remarks</td>
                                        <td>
                                            <asp:TextBox ID="txtRemarks" runat="server" style="margin-left: 1px" 
                                                TextMode="MultiLine" Width="302px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnDoAbolish" runat="server" Text="Abolish" Width="168px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnUndo" runat="server" Text="Undo" Width="168px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview Report" 
                                                Width="168px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnGenerate" runat="server" Text="Generate O/o" Width="168px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
</div>


</asp:Content>

