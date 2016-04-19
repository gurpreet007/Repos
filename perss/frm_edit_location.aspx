<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="frm_edit_location.aspx.vb" Inherits="frm_edit_location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style4
        {
            height: 120px;
        }
        .style9
        {
            height: 25px;
            width: 243px;
        }
        .style11
        {
            height: 26px;
        }
        .style12
        {
            width: 116px;
        }
        .style15
        {
            height: 26px;
        }
        .style16
        {
            width: 138px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
    <div align="center" >
   
    <br />
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
        </tr>
        <tr>
            <td style="width: 127px; height: 11px; text-align: left; background-color: paleturquoise;">
                &nbsp;<asp:Label ID="Label7" runat="server" Text="Organisation Type"></asp:Label></td>
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
        </tr>
        <tr>
            <td style="width: 127px; text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:Label ID="Label8" runat="server" Text="Organisation"></asp:Label></td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise; height: 26px;">
                <asp:DropDownList ID="drporg" runat="server" Height="22px" Width="253px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 127px; text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:Label ID="Label9" runat="server" Text="Circle" Width="35px" Height="12px"></asp:Label></td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:DropDownList ID="drpcir" runat="server" Height="22px" Width="253px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 127px; text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:Label ID="Label3" runat="server" Text="Division Office"></asp:Label></td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:DropDownList ID="drpdiv" runat="server" Height="22px" Width="253px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 127px; text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:Label ID="Label4" runat="server" Text="Sub Division"></asp:Label></td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:DropDownList ID="drpsdiv" runat="server" Height="22px" Width="253px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 127px; text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:Label ID="Label10" runat="server" Text="Sub Office"></asp:Label></td>
            <td style="text-align: center; vertical-align: middle; background-color: paleturquoise;">
                <asp:DropDownList ID="drpsoff" runat="server" Height="22px" Width="253px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        </table>
    <br />
    <table style="width: 459px; height: 1px">
        <tr>
            <td style="text-align: left; vertical-align: top;" class="style9" align="right">
                <asp:Button ID="btnEdit" runat="server" 
                    Text="Edit Location" Width="168px" />
            </td>
            <td style="width: 3px; height: 25px; text-align: left;">
                <asp:Button ID="btnAbolish" runat="server" 
                    Text="Abolish Location" Width="168px" />
            </td>
            <td style="width: 3px; height: 25px; text-align: left;">
                <asp:Button ID="btnChRep" runat="server" 
                    Text="Change Reporting Location" Width="180px" />
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
                                        <td class="style12">
                                            <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtorg" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style12">
                                            <asp:Label ID="lbloabr" runat="server" Text="Abbrivation"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtabbr" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style12">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnsave" runat="server" Text="Save" Width="168px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <div align="center">
                            <asp:Panel ID="PanAbolish" runat="server" Visible="False">
                                <table style="width:100%;" align="center">
                                    <tr>
                                        <td class="style15" colspan="2">
                                            Please select the new reporting location for the offices working under abolished 
                                            location</td>
                                    </tr>
                                    <tr>
                                        <td class="style15">
                                            &nbsp;</td>
                                        <td class="style11">
                                            <asp:CheckBox ID="chkAbolishUnderlying" runat="server" AutoPostBack="True" 
                                                Text=" Abolish underlying locations as well" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style15">
                                            Organisation Type</td>
                                        <td class="style11">
                                            <asp:DropDownList ID="drpcorp_rep" runat="server" AutoPostBack="True" 
                                                Height="25px" Width="300px">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="Zone">Zone</asp:ListItem>
                                                <asp:ListItem>Circle</asp:ListItem>
                                                <asp:ListItem>Division</asp:ListItem>
                                                <asp:ListItem Value="SDivision">S/Division</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style16">
                                            Organisation</td>
                                        <td>
                                            <asp:DropDownList ID="drporg_rep" runat="server" AutoPostBack="True" 
                                                Height="25px" Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style16">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Label ID="lbl_loccode" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style16">
                                            Filter</td>
                                        <td>
                                            <asp:TextBox ID="txtFilterRep" runat="server" AutoPostBack="True" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style16">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnDoAbolish" runat="server" Text="Abolish" Width="168px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style16">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnUndo" runat="server" Text="Undo Abolish" Width="168px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style16">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview Report" 
                                                Width="168px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style16">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnGenerate" runat="server" Text="Generate O/o" Width="168px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            </div>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Panel ID="panChRep" runat="server" Visible="False">
                                <table style="width:100%;">
                                    <tr>
                                        <td colspan="2">
                                            Please select the new reporting location</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Organisation Type</td>
                                        <td>
                                            <asp:DropDownList ID="drpCRcorp_rep" runat="server" AutoPostBack="True" 
                                                Height="25px" Width="300px">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem>Directors</asp:ListItem>
                                                <asp:ListItem Value="Zone">Zone</asp:ListItem>
                                                <asp:ListItem>Circle</asp:ListItem>
                                                <asp:ListItem>Division</asp:ListItem>
                                                <asp:ListItem Value="SDivision">S/Division</asp:ListItem>
                                                <asp:ListItem Value="SubOffice">Sub-Office</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Organisation</td>
                                        <td>
                                            <asp:DropDownList ID="drpCRorg_rep" runat="server" AutoPostBack="True" 
                                                Height="25px" Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:Label ID="lbl_loccode_chrep" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Filter</td>
                                        <td>
                                            <asp:TextBox ID="txtCRFilterRep" runat="server" AutoPostBack="True" 
                                                Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:CheckBox ID="chkOnlyMerge" runat="server" Text="Only Merge Posts" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnDoChRep" runat="server" Text="Change Reporting Location" 
                                                Width="183px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnUndoChRep" runat="server" Text="Undo" Width="183px" 
                                                height="26px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnCRPreview" runat="server" Text="Preview Report" 
                                                Width="183px" height="26px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnCRGenerate" runat="server" Text="Generate O/o" 
                                                Width="183px" height="26px" />
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

