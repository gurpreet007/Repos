﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!doctype html />
<html>
<head id="Head1" runat="server">
<script>
    window.history.forward(1);
</script>
    <title></title>
       <style type="text/css">
           .lnkPadded
           {
               padding:10px;
           }
        th
        {
            font: italic bold 15px serif; 
            background-color:#D5D5A6;
            color:#666633;
            }
        .style2
        {
            height: 26px;
        }
        .style3
        {
            font-size: medium;
        }
        .style4
        {
            text-align: left;
        }
        .style5
        {
            font-size: medium;
            text-align: left;
        }
        .style6
        {
            height: 2px;
        }
        .style7
        {
            width: 19px;
        }
        .style8
        {
            height: 27px;
               text-align: center;
           }
        .style9
        {
            width: 19px;
            height: 27px;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table align="center" 
            style="border: thick groove #666633; width: 57%; height: 313px;">
            <tr>
                <td colspan="8" align="center" 
                    
                    
                    style="font-family: 'Times New Roman', Times, serif; font-size: large; font-weight: bold; font-style: oblique; font-variant: normal; text-transform: uppercase; color: #666633; background-color: #D5D5A6;">
                    Bank Payments Reconciliation</td>
            </tr>
            <tr>
                <td colspan="8" bgcolor="#666633" align="right">
                    <asp:Menu ID="Menu1" runat="server" BackColor="#666633" 
                        DynamicHorizontalOffset="4" Font-Bold="True" Font-Names="Verdana" 
                        Font-Size="0.8em" ForeColor="White" Orientation="Horizontal" 
                        StaticSubMenuIndent="20px" onmenuitemclick="Menu1_MenuItemClick" 
                        BorderStyle="Solid">
                        <DynamicHoverStyle BackColor="#284E98" ForeColor="White" />
                        <DynamicMenuItemStyle HorizontalPadding="15px" VerticalPadding="8px" 
                            BorderStyle="Solid" />
                        <DynamicMenuStyle BackColor="#666633" />
                        <DynamicSelectedStyle BackColor="#507CD1" />
                        <Items>
                            <asp:MenuItem Selected="false" Text="Logout" 
                                Value="Logout"></asp:MenuItem>
                            <asp:MenuItem Text="Reports" Value="Reports" Selectable="False">
                                <asp:MenuItem Text="SAP" Value="SAP" Selectable="False">
                                    <asp:MenuItem Text="Abstract Report" Value="SAP_ABS" 
                                        NavigateUrl=".\Reports\rptabstract.aspx?rpt=SAP_ABSTRACT_REPORT"></asp:MenuItem>
                                    <asp:MenuItem Text="Summary Report" Value="SAP_SUM" 
                                        NavigateUrl=".\Reports\rptsummary.aspx?rpt=SAP_SUMMARY_REPORT"></asp:MenuItem>
                                    <asp:MenuItem Text="Transaction Report" Value="NONSAP_TRAN" 
                                        NavigateUrl=".\Reports\rptsummary.aspx?rpt=SAP_TRANSACTION_REPORT"></asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="NON SAP" Value="NON SAP" Selectable="False">
                                    <asp:MenuItem Text="Abstract Report" Value="NONSAP_ABS" 
                                        NavigateUrl=".\Reports\rptabstract.aspx?rpt=NONSAP_ABSTRACT_REPORT"></asp:MenuItem>
                                    <asp:MenuItem Text="Summary Report" Value="NONSAP_SUM" 
                                        NavigateUrl=".\Reports\rptsummary.aspx?rpt=NONSAP_SUMMARY_REPORT"></asp:MenuItem>
                                    <asp:MenuItem Text="Transaction Report" Value="NONSAP_TRAN" 
                                        NavigateUrl=".\Reports\rptsummary.aspx?rpt=NONSAP_TRANSACTION_REPORT"></asp:MenuItem>
                                </asp:MenuItem>
                            </asp:MenuItem>
                        </Items>
                        <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
                        <StaticMenuItemStyle HorizontalPadding="15px" VerticalPadding="10px" />
                        <StaticSelectedStyle BackColor="#507CD1" />
                    </asp:Menu>
                </td>
            </tr>
            <tr>
                <td colspan="8" bgcolor="#666633" align="left" style="color: #FFFFFF">
                    Welcome:
                    <asp:Label ID="lblUser" runat="server" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td bgcolor="#D5D5A6" style="font-weight: 700" colspan="2">
                    Upload File</td>
                <td colspan="2">
                    &nbsp;</td>
                <td nowrap="nowrap" colspan="2">
                    &nbsp;</td>
                <td class="style7" colspan="2">
                    &nbsp; </td>
            </tr>
            <tr>
                <td nowrap="nowrap" colspan="2">
                    Select&nbsp; Gateway</td>
                <td colspan="2">
                    <asp:DropDownList ID="drpfile_ty" runat="server" Width="277px" 
                        style="font-weight: 700">
                    </asp:DropDownList>
                </td>
                <td nowrap="nowrap" colspan="2">
                    &nbsp;</td>
                <td class="style7" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td nowrap="nowrap" colspan="2">
                    Transaction Date</td>
                <td colspan="2">
                    <asp:TextBox ID="txtdt_txn" runat="server" type="date"></asp:TextBox>
                </td>
                <td nowrap="nowrap" colspan="2">
                    &nbsp;</td>
                <td class="style7" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3" colspan="2">
                    Select File</td>
                <td colspan="2">
                    <asp:FileUpload ID="Fupld1" runat="server" />
                </td>
                <td nowrap="nowrap" colspan="2">
                    &nbsp;</td>
                <td class="style7">
                    &nbsp;</td>
                <td class="style7">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" class="style8">
                    &nbsp;</td>
                <td colspan="2" class="style8">
                    &nbsp;</td>
                <td colspan="2" class="style8">
                    &nbsp;</td>
                <td class="style9" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" class="style8">
                    <asp:Button ID="btnVerify" runat="server" Text="VERIFY" height="29px" 
                        width="160px" onclick="btnVerify_Click" />
                </td>
                <td colspan="2" class="style8">
                    <asp:Button ID="btnAccept" runat="server" Text="ACCEPT" height="29px" 
                        width="160px" onclick="btnAccept_Click" />
                </td>
                <td colspan="2" class="style8">
                    <asp:Button ID="btnDelete" runat="server" Text="DELETE" height="29px" 
                        width="160px" onclick="btnDelete_Click" />
                </td>
                <td class="style9" colspan="2">
                    <asp:Button ID="btnFinalise" runat="server" Text="FINALISE" height="29px" 
                        width="160px" onclick="btnFinalise_Click" />
                    <asp:LinkButton ID="lnkException" runat="server" Font-Bold="True" 
                        ForeColor="Black" onclick="lnkException_Click" CssClass="lnkPadded">Exceptions</asp:LinkButton>
                    </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Label ID="lblmsg" runat="server" 
                        style="color: #CC3300; font-weight: 700; font-size: large;"></asp:Label>
                </td>
                <td class="style9" colspan="2">
                    </td>
            </tr>
            <tr>
                <td class="style6" colspan="8">
                    </td>
            </tr>
            <tr>
                <td bgcolor="#D5D5A6" colspan="2">
                    <strong>Reports</strong></td>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnShowSummary" runat="server" Text="SHOW SUMMARY" Width="160px" 
                        onclick="btnShowSummary_Click" />
                </td>
                <td nowrap="nowrap" colspan="2">
                    
                    <asp:Button ID="btnPaymentDetails" runat="server" Text="PAYMENT DETAILS" 
                        Width="160px" onclick="btnPaymentDetails_Click" />
                </td>
                <td class="style7" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
                <td colspan="2" style="text-align: right">
                    &nbsp;</td>
                <td nowrap="nowrap" colspan="2">
                    &nbsp;</td>
                <td class="style7" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td rowspan="3" colspan="2">
                    
                    <asp:RadioButtonList ID="rblist1" runat="server" style="font-weight: 700" 
                        Width="163px">
                        <asp:ListItem Value="Total" Selected="True">Total</asp:ListItem>
                        <asp:ListItem Value="Matched">Matched</asp:ListItem>
                        <asp:ListItem>UnMatched</asp:ListItem>
                    </asp:RadioButtonList>
                    
                </td>
                <td class="style4" colspan="2">
                    <asp:Label ID="lbltotal" runat="server" CssClass="style5" Width="270px"></asp:Label>
                </td>
                <td class="style4" nowrap="nowrap" colspan="2">
                    
                    <strong>Transaction Date GW</strong></td>
<td align="left" class="style7" colspan="2">                
                    <asp:Label ID="lbltxn" runat="server" Width="280px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4" colspan="2">
                    <asp:Label ID="lblupdtd" runat="server" CssClass="style5" Width="270px"></asp:Label>
                </td>
                <td class="style4" nowrap="nowrap" colspan="2" style="font-weight: bold">
                    
                    T. Count (GW / PSPCL)</td>
                    <td align="left" class="style7" colspan="2">                
                    <asp:Label ID="lbltcount" runat="server" Width="280px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4" colspan="2">
                    <asp:Label ID="lblpend" runat="server" CssClass="style5" Width="270px"></asp:Label>
                </td>
                <td class="style4" nowrap="nowrap" colspan="2" style="font-weight: bold">
                    
                    <strong>Amount (GW / PSPCL)</strong></td>
            <td align="left" class="style7" colspan="2">                
                    <asp:Label ID="lblamt" runat="server" Width="280px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    
                    &nbsp;</td>
                <td colspan="2" style="text-align: center">
                    
                    <asp:Button ID="btnDownloadXLS" runat="server" Text="DOWNLOAD XLS" Width="160px" 
                        onclick="btnDownloadXLS_Click" />
                </td>
                <td class="style4" nowrap="nowrap" colspan="4">
                    
                    &nbsp;</td>
            </tr>
            <tr>
                 <td bgcolor="#D5D5A6" colspan="2">
                     <strong>Update Records</strong></td>
                <td colspan="2" style="text-align: center">
                    &nbsp;</td>
                <td class="style4" nowrap="nowrap" colspan="4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    
                    &nbsp;</td>
                <td colspan="2" style="text-align: center">
                    
                    <asp:Button ID="btnShowUnmatched" runat="server" Text="SHOW UNMATCHED" 
                        Width="160px" onclick="btnShowUnmatched_Click" />
                </td>
                <td class="style4" nowrap="nowrap" colspan="2">
                    &nbsp;</td>
                <td class="style7" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="8" style="text-align: center">
                            <asp:GridView ID="gvUnmatched" runat="server" AutoGenerateColumns="False" 
                                style="text-align: left; font-size: small;" AllowPaging="True" 
                                Width="840px" onpageindexchanging="gvUnmatched_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="GATEWAY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvid" runat="server" Text='<%# Eval("GATEWAY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TXNID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblref_1" runat="server" Text='<%# Eval("txnid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NET AMOUNT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamt" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAY_AMOUNT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpay_amt" runat="server" Text='<%# Eval("PAY_AMT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="STATUS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("PAY_STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAY_GATEWAY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpay_vid" runat="server" Text='<%# Eval("PAY_GATEWAY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="V_VID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvvid" runat="server" Text='<%# Eval("V_VID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P_VID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpvid" runat="server" Text='<%# Eval("P_VID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UPDATE">
                                        <ItemTemplate>
                                        <asp:ImageButton ID="imgUpdate" runat="server" Height="12px" 
                                                ImageUrl="~/images/download.jpg" onclick="imgUpdate_Click" Width="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="8" bgcolor="#D5D5A6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="8" bgcolor="#666633" class="style2">
                    </td>
            </tr>
        </table>
    
    </div>
    </form>
    
</body>
</html>
