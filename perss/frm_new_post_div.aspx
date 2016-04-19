<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="frm_new_post_div.aspx.vb" Inherits="frm_new_post_div" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style2
    {
        width: 386px;
        text-align: right;
    }
    .style5
    {
        height: 13px;
    }
    .style13
    {
            height: 13px;
            width: 269px;
        }
    .style14
    {
            height: 18px;
            }
    .style15
    {
        }
        .style16
        {
            width: 524px;
        }
        .style18
        {
            width: 415px;
        }
        .style19
        {
            height: 18px;
            width: 269px;
        }
        .style20
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblStep" runat="server" Text="Step " Font-Bold="True"></asp:Label>
    <asp:Label ID="lblStepNum" runat="server" Text="1" Font-Bold="True"></asp:Label>
    <asp:Label ID="lblStepDes" runat="server" Text=" of 4 : Create New Posts" 
        Font-Bold="True"></asp:Label>
    <br />
<div align="center" >
    <table style="width: 937px; height: 70px">
        <tr>
            <td style="text-align: right;" class="style5" colspan="2">
                <asp:Panel ID="Panel1" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lblPending" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
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
                            <td class="style2">
                                <asp:Label ID="Label10" runat="server" Text="Select Organisation Head"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drporgtype" runat="server" AutoPostBack="True" 
                                    Height="22px" Width="299px">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label1" runat="server" Text="Organisation"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drporg" runat="server" Height="22px" Width="299px" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label2" runat="server" Text="Circle" Width="47px"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpcir" runat="server" AutoPostBack="True" Height="22px" 
                                    Width="299px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label7" runat="server" Text="Division Office"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpdiv" runat="server" AutoPostBack="True" Height="22px" 
                                    Width="299px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label3" runat="server" Text="Sub-Division"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpsubdiv" runat="server" AutoPostBack="True" 
                                    Height="22px" Width="299px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label25" runat="server" Text="Sub Office    "></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpsuboff" runat="server" AutoPostBack="True" 
                                    Height="22px" Width="299px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label5" runat="server" Text="Designation"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpdesg" runat="server" AutoPostBack="True" Height="22px" 
                                    Width="299px">
                                </asp:DropDownList>
                                &nbsp;&nbsp;<asp:TextBox ID="txtRem0" runat="server" Width="72px"></asp:TextBox>
                                &nbsp;
                                <asp:Label ID="Label4" runat="server" Text="Existing: "></asp:Label>
                                <asp:Label ID="Label8" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label9" runat="server" Text="Filter Designation"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtsdesg" runat="server" Width="296px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="height: 13px; text-align: right;" colspan="2">
                <asp:Panel ID="Panel_Branch" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label11" runat="server" Text="Branch"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpbranch" runat="server" AutoPostBack="True" 
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
                            <td class="style2">
                                <asp:Label ID="Label6" runat="server" Text="Scale"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpscale" runat="server" Height="22px" Width="299px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label12" runat="server" Text="Post type"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drptperm" runat="server" Height="20px" Width="191px">
                                    <asp:ListItem Value="P">Permanent</asp:ListItem>
                                    <asp:ListItem Value="T">Temporary</asp:ListItem>
                                    <asp:ListItem Value="X">Ex Cadre</asp:ListItem>
                                    <asp:ListItem Value="M">Personnel Measure</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;" class="style13">
                &nbsp;</td>
            <td style="height: 20px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center;" class="style20" colspan="2">
                <asp:Button ID="btn_AddToGrid" runat="server" Text="Add To Grid" 
                    Width="121px" UseSubmitBehavior="False" />
                </td>
        </tr>
        <tr>
            <td style="text-align: right;" class="style13">
                &nbsp;</td>
            <td style="height: 20px; text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 13px; text-align: right;" colspan="2">
                <table style="width: 100%;">
                    <tr>
                        <td class="style16" align="right">
                            <asp:Panel ID="Pan_Create" runat="server" Width="100%">
                                <asp:GridView ID="gvcreate" runat="server" AutoGenerateColumns="False" 
                                    Height="80px" Width="261px" CellPadding="4" ForeColor="#333333" 
                                    GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# eval("checked") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# eval("Location") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="Label14" runat="server" Text='<%# eval("Designation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch">
                                            <ItemTemplate>
                                                <asp:Label ID="Label15" runat="server" Text='<%# eval("Branch") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Scale">
                                            <ItemTemplate>
                                                <asp:Label ID="Label16" runat="server" Text='<%# eval("Scale") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PType">
                                            <ItemTemplate>
                                                <asp:Label ID="Label17" runat="server" Text='<%# eval("PType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="Label18" runat="server" Text='<%# eval("Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <EmptyDataTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </EmptyDataTemplate>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                        <td align="left">
                            <asp:Panel ID="Pan_Abolish" runat="server" Visible="False" Width="100%">
                                <asp:GridView ID="gvabolish" runat="server" AutoGenerateColumns="False" 
                                    Height="80px" Width="261px" CellPadding="4" ForeColor="#333333" 
                                    GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox3" runat="server" Checked='<%# eval("checked") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="Label19" runat="server" Text='<%# eval("Location") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="Label20" runat="server" 
                                                    Text='<%# eval("Designation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="Label24" runat="server" 
                                                    Text='<%# eval("Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <EmptyDataTemplate>
                                        <asp:CheckBox ID="CheckBox4" runat="server" />
                                    </EmptyDataTemplate>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style15" align="Center" colspan="2">
                            <asp:Button ID="btnProceed" runat="server" 
                                Text="Proceed to Select Posts to Abolish" Width="243px" Visible="False" 
                                UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 13px; text-align: right;" colspan="2" align="center">
                <asp:Panel ID="Pan_Map" runat="server" Visible="False">
                    <asp:GridView ID="gvmap" runat="server" CellPadding="4" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style19">
                &nbsp;</td>
            <td style="height: 18px; text-align: left;">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style14" colspan="2">
                <asp:Panel ID="PanSave" runat="server" Visible="False">
                    <table style="width:100%;">
                        <tr>
                            <td align="right" class="style18">
                                <asp:Label ID="lblRem" runat="server" Font-Bold="True" Text="Enter Remarks"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRem" runat="server" BorderStyle="Solid" Width="306px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style18">
                                &nbsp;</td>
                            <td align="left">
                                <asp:Button ID="btnCreateCC" runat="server" height="26px" Text="Create CC List" 
                                    width="219px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style18">
                                &nbsp;</td>
                            <td align="left">
                                <asp:Button ID="btnPreview" runat="server" Text="Preview Report" 
                                    Width="219px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style18">
                                &nbsp;</td>
                            <td align="left">
                                <asp:Button ID="btnFinalSave" runat="server" 
                                    Text="Save and Generate Office Order" Width="219px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style18">
                                &nbsp;</td>
                            <td align="left">
                                <asp:Button ID="btnDelSess" runat="server" Text="Delete Session" 
                                    Width="219px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="height: 18px" colspan="2">
                &nbsp;</td>
        </tr>
    </table>
</div>
</asp:Content>

