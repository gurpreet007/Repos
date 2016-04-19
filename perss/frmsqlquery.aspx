<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="frmsqlquery.aspx.vb" Inherits="frmsqlquery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <style type="text/css">
        .style2
        {
            width: 324px;
        }
        .style3
        {
            width: 389px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2"  ContentPlaceHolderID="MainContent"  Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:GridView ID="gvquery" runat="server" AutoGenerateSelectButton="True" 
        Visible="False">
    </asp:GridView>
    <asp:Panel ID="panSQLBuilder" runat="server">
        <table style="width:100%;">
            <tr>
                <td class="style3">
                    <asp:DropDownList ID="drpListOrCount" runat="server" Width="350px" 
                        AutoPostBack="True">
                        <asp:ListItem Value="L">Show List</asp:ListItem>
                        <asp:ListItem Value="C">Show Count</asp:ListItem>
                        <asp:ListItem Value="G">Show List Group by</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="drpDisplayCols" runat="server" Width="165px">
                        <asp:ListItem Value="0">Default Columns</asp:ListItem>
                        <asp:ListItem Value="pshr.get_org(loccode) as Loc">Location</asp:ListItem>
                        <asp:ListItem Value="pshr.get_desg(desgcode) as Desg">Designation</asp:ListItem>
                        <asp:ListItem Value="indx">Index</asp:ListItem>
                        <asp:ListItem Value="cadre.get_scale_txt(scaleid) as Scale">Scale</asp:ListItem>
                        <asp:ListItem Value="cadre.get_branch(branch) as Branch">Branch</asp:ListItem>
                        <asp:ListItem Value="ptype">Post Type</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnAddDispCol" runat="server" Text="+" 
                        UseSubmitBehavior="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:DropDownList ID="drpLoc" runat="server" Width="350px" Height="21px">
                    </asp:DropDownList>
                    <asp:Button ID="btnAddLoc" runat="server" Text="+" UseSubmitBehavior="False" />
                </td>
                <td>
                    <asp:DropDownList ID="drpDesg" runat="server" Width="165px">
                    </asp:DropDownList>
                    <asp:Button ID="btnAddDesg" runat="server" Text="+" UseSubmitBehavior="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="Label1" runat="server" Text="Filter"></asp:Label>
                    <asp:TextBox ID="txtFilterLoc" runat="server" Width="315px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:DropDownList ID="drpBranch" runat="server" Width="350px">
                        <asp:ListItem Value="0">All Branches</asp:ListItem>
                        <asp:ListItem Value="1">Electrical</asp:ListItem>
                        <asp:ListItem Value="2">Computer</asp:ListItem>
                        <asp:ListItem Value="3">Mechanical</asp:ListItem>
                        <asp:ListItem Value="4">Civil</asp:ListItem>
                        <asp:ListItem Value="5">ECE</asp:ListItem>
                        <asp:ListItem Value="6">Instrumentation</asp:ListItem>
                        <asp:ListItem Value="99">Not Known</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnAddBranch" runat="server" Text="+" 
                        UseSubmitBehavior="False" />
                </td>
                <td>
                    <asp:DropDownList ID="drpScale" runat="server" Width="165px">
                    </asp:DropDownList>
                    <asp:Button ID="btnAddScale" runat="server" Text="+" 
                        UseSubmitBehavior="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:DropDownList ID="drpPostType" runat="server" Width="350px">
                        <asp:ListItem Value="0">All Posts</asp:ListItem>
                        <asp:ListItem Value="P">Permanent</asp:ListItem>
                        <asp:ListItem Value="T">Temporary</asp:ListItem>
                        <asp:ListItem Value="X">Ex-Cadre</asp:ListItem>
                        <asp:ListItem Value="M">Personel Measure</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnAddPost" runat="server" Text="+" UseSubmitBehavior="False" />
                </td>
                <td>
                    <asp:Button ID="btnClear" runat="server" Text="Clear Query" Width="95px" 
                        UseSubmitBehavior="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <asp:TextBox ID="txtquery" runat="server" Height="64px" Width="920px" 
        TextMode="MultiLine"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblsqlbrief" runat="server" Text="Please Enter SQL Brief :- " 
        Visible="False"></asp:Label>
    <asp:TextBox ID="txtsqlbrief" runat="server" Height="25px" Visible="False" 
        Width="449px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblmsg" runat="server" Text="Label" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnshowdata" runat="server" Height="26px" Text="Show Data" 
        Width="129px" UseSubmitBehavior="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnshowbin" runat="server" Text="Show SQL Bin" Height="26px" 
        Width="129px" UseSubmitBehavior="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnsave" runat="server"   Enabled="False" Text="Save Query" 
        height="26px" width="129px" UseSubmitBehavior="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnprtexcl" runat="server" Text="Print in Excel" height="26px" 
        width="129px" UseSubmitBehavior="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnexit" runat="server" Height="26px" Text="Exit" 
        Width="129px" UseSubmitBehavior="False" />
    <br />
    <asp:GridView ID="gvdata" runat="server" Visible="False">
    </asp:GridView>
    <br />
    </asp:Content>