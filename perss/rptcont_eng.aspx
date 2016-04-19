<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="rptcont_eng.aspx.vb" Inherits="rptcont" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 465px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
        <p>
            <br />
        </p>
   <div align = "center">
        <table style="width: 62%; height: 113px;">
        <tr>
            <td colspan="2" align ="center">
                <asp:Label ID="Label3" runat="server" 
                    style="font-weight: 700; font-family: Arial; font-size: large" 
                    Text="Organisation Wise List of Posts (Eng Officers)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align ="center">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
                    Height="16px" RepeatDirection="Horizontal" Width="177px">
                    <asp:ListItem Selected="True" Value="P">PSPCL</asp:ListItem>
                    <asp:ListItem Value="T">PSTCL</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Select Head of Organisation"></asp:Label>
            </td>
            <td style="text-align: left" class="style1">
                <asp:DropDownList ID="drporgtype" runat="server" height="22px" width="288px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Select Organisation"></asp:Label>
            </td>
            <td style="text-align: left" class="style1">
                <asp:DropDownList ID="drporg" runat="server" Height="22px" Width="288px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left" class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left" class="style1">
                <asp:Button ID="btnshowContwDesg" runat="server" 
                    Text="Show Detailed" Width="146px" />
            &nbsp;&nbsp;
                <asp:Button ID="btnshowAbstract" runat="server" 
                    Text="Show Abstract" Width="146px" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left" class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left" class="style1">
                <asp:Button ID="btnPreGenerate" runat="server" 
                    Text="Regenerate Detailed Report" Width="215px" height="26px" />
            &nbsp;&nbsp;
                <asp:Label ID="Label6" runat="server" Font-Bold="False" ForeColor="Red" 
                    Text="(This Action Will Take Some Time)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left" class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left" class="style1">
                <asp:Button ID="btnPreGenerateAll" runat="server" 
                    Text="Regenerate All Detailed Reports" Width="215px" Height="26px" />
            &nbsp;&nbsp;
                <asp:Label ID="Label4" runat="server" Font-Bold="False" ForeColor="Red" 
                    Text="(This Action Will Take More Time)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left" class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left" class="style1">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="1000">
                        </asp:Timer>
                        <asp:Button ID="btnTimer" runat="server" Text="Timer Button" Visible="False" />
                        <asp:Label ID="Label5" runat="server" Font-Size="Medium"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: left" class="style1">
                &nbsp;</td>
        </tr>
    </table>
    </div>

</asp:Content>

