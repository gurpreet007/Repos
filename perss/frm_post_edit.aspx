<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="frm_post_edit.aspx.vb" Inherits="frm_post_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

        .style9
        {
            height: 24px;
            }
        .style4
        {
            height: 120px;
        }
        .style11
        {
            height: 19px;
            width: 378px;
        }
        .style12
        {
            height: 24px;
            width: 378px;
        }
        .style13
        {
            height: 13px;
            width: 378px;
        }
        .style16
        {
            height: 25px;
        }
        .style24
        {
            width: 180px;
        }
        .style25
        {
            height: 75px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div align="center" >
        <br />
        <br />
        <table style="width: 459px; height: 1px">
            <tr>
            <td style="text-align: right;" class="style11">
                &nbsp;</td>
            <td style="width: 1px; height: 19px; text-align: left;">
                <asp:Label ID="lblPending" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
            </td>
            </tr>
            <tr>
            <td style="text-align: right;" class="style11">
                &nbsp;</td>
            <td style="width: 1px; height: 19px; text-align: left;">
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" 
                    Height="16px" RepeatDirection="Horizontal" Width="294px">
                    <asp:ListItem Selected="True" Value="P">PSPCL</asp:ListItem>
                    <asp:ListItem Value="T">PSTCL</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            </tr>
            <tr>
            <td style="text-align: right;" class="style11">
                <asp:Label ID="Label10" runat="server" Text="Select Organisation Head"></asp:Label></td>
            <td style="width: 1px; height: 19px; text-align: left;">
                <asp:DropDownList ID="drporgtype" runat="server" Height="22px" Width="299px" 
                    AutoPostBack="True">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList></td>
            </tr>
            <tr>
            <td style="text-align: right;" class="style11">
                <asp:Label ID="Label1" runat="server" Text="Organisation"></asp:Label></td>
            <td style="width: 1px; height: 19px; text-align: left;">
                <asp:DropDownList ID="drporg" runat="server" Height="22px" Width="299px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
            </tr>
            <tr>
            <td style="text-align: right; " class="style12">
                <asp:Label ID="Label2" runat="server" Text="Circle" Width="184px" 
                    style="margin-right: 0px"></asp:Label></td>
            <td style="text-align: left; height: 24px;">
                <asp:DropDownList ID="drpcir" runat="server" Height="22px" Width="299px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
            </tr>
            <tr>
            <td style="text-align: right;" class="style13">
                <asp:Label ID="Label7" runat="server" Text="Division Office"></asp:Label></td>
            <td style="height: 13px; text-align: left;">
                <asp:DropDownList ID="drpdiv" runat="server" Height="22px" Width="299px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
            </tr>
            <tr>
            <td style="text-align: right" class="style13">
                <asp:Label ID="Label3" runat="server" Text="Sub-Division"></asp:Label></td>
            <td style="height: 13px; text-align: left"><asp:DropDownList ID="drpsubdiv" 
                    runat="server" Height="22px" Width="299px" AutoPostBack="True">
            </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td style="text-align: right" class="style13">
                <asp:Label ID="Label25" runat="server" Text="Sub Office"></asp:Label>&nbsp; </td>
            <td style="height: 13px; text-align: left">
                <asp:DropDownList ID="drpsuboff" 
                    runat="server" Height="22px" Width="299px" AutoPostBack="True">
            </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td style="text-align: right;" class="style13">
                <asp:Label ID="Label5" runat="server" Text="Designation"></asp:Label></td>
            <td style="height: 20px; text-align: left;">
                <asp:DropDownList ID="drpdesg" runat="server" Height="22px" Width="299px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;" class="style9" colspan="3">
                    </td>
            </tr>
            <tr>
                <td style="text-align: right; " class="style16" colspan="2">
                    <asp:Panel ID="PanControls" runat="server" Enabled="False">
                        <table style="width:100%;">
                            <tr>
                                <td class="style24">
                                    <asp:Label ID="Label20" runat="server" Text="Select Action"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbActions" runat="server" AutoPostBack="True" 
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="U">Upgrade</asp:ListItem>
                                        <asp:ListItem Value="D">Downgrade</asp:ListItem>
                                        <asp:ListItem Value="E">Edit</asp:ListItem>
                                        <asp:ListItem Value="A">Abolish</asp:ListItem>
                                        <asp:ListItem Value="H">Abeyance</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td style="width: 3px; height: 25px; text-align: left;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;" class="style4" colspan="3">
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:Panel ID="panUpgrade" runat="server" Visible="False">
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label21" runat="server" Text="Remarks"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUpRemarks" runat="server" TextMode="MultiLine" 
                                                    Width="305px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                Designation</td>
                                            <td>
                                                <asp:DropDownList ID="drpupdesg" runat="server" AutoPostBack="True" 
                                                    Height="22px" Width="299px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="lbloabr" runat="server" Text="Filter Designation"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtupFilter" runat="server" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label11" runat="server" Text="Branch"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpupbranch" runat="server" AutoPostBack="True" 
                                                    Height="22px" Width="299px">
                                                    <asp:ListItem Value="99">NK</asp:ListItem>
                                                    <asp:ListItem Value="1">Electrical</asp:ListItem>
                                                    <asp:ListItem Value="2">Computer</asp:ListItem>
                                                    <asp:ListItem Value="3">Mechanical</asp:ListItem>
                                                    <asp:ListItem Value="4">Civil</asp:ListItem>
                                                    <asp:ListItem Value="5">ECE</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label12" runat="server" Text="Scale"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpupscale" runat="server" Height="22px" Width="299px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label13" runat="server" Text="Post type"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpuptperm" runat="server" Height="23px" Width="100px">
                                                    <asp:ListItem Value="P">Permanent</asp:ListItem>
                                                    <asp:ListItem Value="T">Temporary</asp:ListItem>
                                                    <asp:ListItem Value="X">Ex Cadre</asp:ListItem>
                                                    <asp:ListItem Value="M">Personnel Measure</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24">
                                            &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnUpgrade" runat="server" Text="Upgrade" Width="168px" 
                                                    UseSubmitBehavior="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="panDowngrade" runat="server" Visible="False">
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label22" runat="server" Text="Remarks"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDownRemarks" runat="server" TextMode="MultiLine" 
                                                    Width="305px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                Designation</td>
                                            <td>
                                                <asp:DropDownList ID="drpdowndesg" runat="server" AutoPostBack="True" 
                                                    Height="22px" Width="299px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="lbloabr0" runat="server" Text="Filter Designation"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdownFilter" runat="server" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label14" runat="server" Text="Branch"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpdownbranch" runat="server" AutoPostBack="True" 
                                                    Height="22px" Width="299px">
                                                    <asp:ListItem Value="99">NK</asp:ListItem>
                                                    <asp:ListItem Value="1">Electrical</asp:ListItem>
                                                    <asp:ListItem Value="2">Computer</asp:ListItem>
                                                    <asp:ListItem Value="3">Mechanical</asp:ListItem>
                                                    <asp:ListItem Value="4">Civil</asp:ListItem>
                                                    <asp:ListItem Value="5">ECE</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label15" runat="server" Text="Scale"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpdownscale" runat="server" Height="22px" Width="299px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label16" runat="server" Text="Post type"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpdowntperm" runat="server" Height="23px" Width="100px">
                                                    <asp:ListItem Value="P">Permanent</asp:ListItem>
                                                    <asp:ListItem Value="T">Temporary</asp:ListItem>
                                                    <asp:ListItem Value="X">Ex Cadre</asp:ListItem>
                                                    <asp:ListItem Value="M">Personnel Measure</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24">
                                            &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnDowngrade" runat="server" Text="Downgrade" Width="168px" 
                                                    UseSubmitBehavior="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="panEdit" runat="server" Visible="False">
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label24" runat="server" Text="Remarks"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditRemarks" runat="server" TextMode="MultiLine" 
                                                    Width="305px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                Designation</td>
                                            <td>
                                                <asp:DropDownList ID="drpEditDesg" runat="server" AutoPostBack="True" 
                                                    Height="22px" Width="299px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="lbloabr1" runat="server" Text="Filter Designation"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditFilter" runat="server" AutoPostBack="True" 
                                                    Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label17" runat="server" Text="Branch"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpeditbranch" runat="server" AutoPostBack="True" 
                                                    Height="22px" Width="299px">
                                                    <asp:ListItem Value="99">NK</asp:ListItem>
                                                    <asp:ListItem Value="1">Electrical</asp:ListItem>
                                                    <asp:ListItem Value="2">Computer</asp:ListItem>
                                                    <asp:ListItem Value="3">Mechanical</asp:ListItem>
                                                    <asp:ListItem Value="4">Civil</asp:ListItem>
                                                    <asp:ListItem Value="5">ECE</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label18" runat="server" Text="Scale"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpeditscale" runat="server" Height="22px" Width="299px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                                <asp:Label ID="Label19" runat="server" Text="Post type"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpedittperm" runat="server" Height="23px" Width="100px">
                                                    <asp:ListItem Value="P">Permanent</asp:ListItem>
                                                    <asp:ListItem Value="T">Temporary</asp:ListItem>
                                                    <asp:ListItem Value="X">Ex Cadre</asp:ListItem>
                                                    <asp:ListItem Value="M">Personnel Measure</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24" style="text-align: right;">
                                            &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="168px" 
                                                    UseSubmitBehavior="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style25">
                                <asp:Panel ID="PanAbolish" runat="server" Visible="False">
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="style24" align="right">
                                                <asp:Label ID="Label23" runat="server" Text="Remarks"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAbolishRemarks" runat="server" TextMode="MultiLine" 
                                                    Width="305px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24">
                                                &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnAbolish" runat="server" Text="Abolish" Width="168px" 
                                                    UseSubmitBehavior="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <asp:Panel ID="panAbeyance" runat="server" Visible="False">
                            <table style="width:100%;">
                            <tr>
                                <td class="style24" align="right">
                                    <asp:Label ID="Label26" runat="server" Text="Remarks"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAbeyanceRemarks" runat="server" TextMode="MultiLine" 
                                        Width="305px"></asp:TextBox>
                                </td>
                            </tr>
                                <tr>
                                    <td class="style24">
                                        &nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnAbeyance" runat="server" Text="Held in Abeyance" 
                                            UseSubmitBehavior="False" Width="168px" />
                                        <asp:Button ID="btnRemAbeyance" runat="server" Text="Remove from Abeyance" 
                                            UseSubmitBehavior="False" Width="168px" />
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>
                        </td>
                        </tr>
                        <tr>
                            <td class="style25">
                                <asp:Panel ID="PanSave" runat="server">
                                    <table style="width:100%;">
                                        <tr>
                                            <td align="right" class="style24">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style24">
                                                &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnCreateCC" runat="server" height="26px" Text="Create CC List" 
                                                    width="168px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style24">
                                                &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnGenPreview" runat="server" Text="Generate Preview" 
                                                    Width="168px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24">
                                                &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Save and Generate O/O" 
                                                    Width="168px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style24">
                                                &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnDelSession" runat="server" Text="Delete Session" 
                                                    Width="167px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style25">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style25">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

