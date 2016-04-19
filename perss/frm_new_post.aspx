<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="frm_new_post.aspx.vb" Inherits="frm_new_post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 19px;
            width: 314px;
        }
        .style2
        {
            height: 24px;
            width: 314px;
        }
        .style3
        {
            height: 13px;
            width: 314px;
        }
        .style4
        {
            height: 20px;
            width: 314px;
        }
        .style5
        {
            height: 18px;
            width: 314px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <br />
   <div align="center" >
    <table style="width: 937px; height: 70px" align="right">
        <tr>
            <td align="left" class="style1">
                &nbsp;</td>
            <td align="left">
                <asp:Label ID="lblPending" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" class="style1">
                &nbsp;</td>
            <td align="left">
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" 
                    Height="16px" RepeatDirection="Horizontal" Width="294px">
                    <asp:ListItem Selected="True" Value="P">PSPCL</asp:ListItem>
                    <asp:ListItem Value="T">PSTCL</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="right" class="style1">
                <asp:Label ID="Label10" runat="server" Text="Select Organisation Head"></asp:Label></td>
            <td align="left">
                <asp:DropDownList ID="drporgtype" runat="server" Height="22px" Width="299px" 
                    AutoPostBack="True">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" class="style1">
                <asp:Label ID="Label1" runat="server" Text="Organisation"></asp:Label></td>
            <td align="left">
                <asp:DropDownList ID="drporg" runat="server" Height="22px" Width="299px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" class="style2">
                <asp:Label ID="Label2" runat="server" Text="Circle" Width="47px"></asp:Label></td>
            <td align="left">
                <asp:DropDownList ID="drpcir" runat="server" Height="22px" Width="299px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" class="style3">
                <asp:Label ID="Label7" runat="server" Text="Division Office"></asp:Label></td>
            <td align="left">
                <asp:DropDownList ID="drpdiv" runat="server" Height="22px" Width="299px" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" class="style3">
                <asp:Label ID="Label3" runat="server" Text="Sub-Division"></asp:Label></td>
            <td align="left"><asp:DropDownList ID="drpsubdiv" 
                    runat="server" Height="22px" Width="299px" AutoPostBack="True">
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="style3">
                <asp:Label ID="Label13" runat="server" Text="Sub-Office"></asp:Label></td>
            <td align="left">
                <asp:DropDownList ID="drpsuboff" 
                    runat="server" Height="22px" Width="299px" AutoPostBack="True">
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="style3">
                <asp:Label ID="Label5" runat="server" Text="Designation"></asp:Label></td>
            <td align="left">
                <asp:DropDownList ID="drpdesg" runat="server" Height="22px" Width="299px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;<asp:TextBox ID="txtRem" runat="server" Width="167px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label4" runat="server" Text="Existing: "></asp:Label><asp:Label ID="Label8"
                    runat="server" Text="0"></asp:Label></td>
        </tr>
        <tr>
            <td align="right" class="style3">
                <asp:Label ID="Label9" runat="server" Text="Filter Designation"></asp:Label></td>
            <td align="left">
                <asp:TextBox ID="txtsdesg" runat="server" height="22px" width="293px" 
                    AutoPostBack="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="style3">
                <asp:Label ID="Label11" runat="server" Text="Branch"></asp:Label></td>
            <td align="left">
                <asp:DropDownList ID="drpbranch" runat="server" Height="22px" Width="299px" 
                    AutoPostBack="True">
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
            <td align="right" class="style3">
                <asp:Label ID="Label6" runat="server" Text="Scale"></asp:Label></td>
            <td align="left">
                <asp:DropDownList ID="drpscale" runat="server" Height="22px" Width="299px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" class="style3">
                <asp:Label ID="Label12" runat="server" Text="Post type"></asp:Label></td>
            <td align="left">
                <asp:DropDownList ID="drptperm" runat="server" Height="20px" Width="191px">
                    <asp:ListItem Value="P">Permanent</asp:ListItem>
                    <asp:ListItem Value="T">Temporary</asp:ListItem>
                    <asp:ListItem Value="X">Ex- Cadre</asp:ListItem>
                    <asp:ListItem Value="M">Personnel Measure</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="left" class="style5">
            </td>
            <td align="left">
                <asp:Button ID="btnCreate" runat="server" Text="Create" Width="183px" 
                    UseSubmitBehavior="False" /></td>
        </tr>
        <tr>
            <td align="left" class="style5">
                &nbsp;</td>
            <td align="left">
                <asp:Button ID="btnCreateCC" runat="server" height="26px" Text="Create CC List" 
                    width="183px" UseSubmitBehavior="False" />
            </td>
        </tr>
        <tr>
            <td align="left" class="style5">
                &nbsp;</td>
            <td align="left">
                <asp:Button ID="btnPreview" runat="server" Text="See Preview" Width="183px" 
                    UseSubmitBehavior="False" /></td>
        </tr>
        <tr>
            <td align="left" class="style5">
                &nbsp;</td>
            <td align="left">
                <asp:Button ID="btnUndo" runat="server" Text="Undo Changes" Width="183px" 
                    UseSubmitBehavior="False" /></td>
        </tr>
        <tr>
            <td align="left" class="style5">
                &nbsp;</td>
            <td align="left">
                <asp:Button ID="btnSave" runat="server" Text="Save and Generate O/O" 
                    Width="183px" style="height: 26px" UseSubmitBehavior="False" /></td>
        </tr>
        <tr>
            <td align="left" class="style5">
                &nbsp;</td>
            <td align="left">
                <asp:Button ID="btnDelSession" runat="server" Text="Delete Session" 
                    Width="182px" />
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="lblloccode" runat="server"></asp:Label>
            </td>
            <td align="left">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    RepeatDirection="Horizontal" AutoPostBack="True" style="margin-left: 0px">
                    <asp:ListItem Selected="True" Value="0">Show Directly Reporting</asp:ListItem>
                    <asp:ListItem Value="1">Show All</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px; text-align: center;">
                <asp:Panel ID="Panel1" runat="server" Height="465px" ScrollBars="Auto" 
                    Width="884px">
            <asp:GridView ID="gv" runat="server" AutoGenerateDeleteButton="True"
                         BackColor="LightSkyBlue" BorderColor="#0000C0"
                        BorderStyle="None" BorderWidth="1px" CellPadding="1" CellSpacing="2" Height="251px"
                        ShowFooter="True" Width="836px" AllowPaging="True"  >
                        <RowStyle BackColor="MintCream" ForeColor="DarkSlateGray" />
                        <FooterStyle ForeColor="#8C4510" />
                        <PagerSettings Position="TopAndBottom" />
                        <PagerStyle BackColor="LightBlue" BorderColor="Transparent" 
                            ForeColor="DarkSlateGray" Font-Bold="True" Font-Size="Larger" 
                            HorizontalAlign="Center" VerticalAlign="Middle" />
                        <SelectedRowStyle BackColor="#738A9C" ForeColor="White" />
                        <HeaderStyle BackColor="PowderBlue" ForeColor="Teal" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
                &nbsp;</td>
        </tr>
    </table>
</div>

</asp:Content>

