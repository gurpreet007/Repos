<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Copy of tnp.aspx.cs" Inherits="frmproposal" ClientIDMode="Static"%>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
    <style type="text/css">
        .style2
        {
            width: 253px;
        }
        .style4
        {
            width: 321px;
        }
        .style5
        {
            width: 209px;
        }
        .style6
        {
        }
        .style7
        {
            width: 99px;
        }
        .style8
        {
            width: 57px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="panMain" runat="server" Width="100%">
        <table style="width:100%;">
            <tr>
                <td>
                    </td>
                <td colspan="4" align="center">
                    <asp:Label ID="lblProposalName" runat="server" Font-Size="Large"></asp:Label>
                    <br />
                </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td class="style7">
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td class="style7" colspan="2">
                    <asp:Panel ID="panPresent" runat="server" GroupingText="Choose" 
                        height="310px" width="427px" style="margin-top: 0px">
                        <table style="width:100%; height: 182px;">
                            <tr>
                                <td class="style8">
                                    &nbsp;</td>
                                <td class="style6">
                                    <asp:TextBox ID="txtEmpid" runat="server" AutoPostBack="False" width="100%"></asp:TextBox>
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnSelNewID" runat="server" Text="Show Info" 
                                        onclick="btnSelNewID_Click" style="margin-left: 10px" />
                                    &nbsp;<asp:Label ID="lblMsgNew" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style8">
                                    &nbsp;</td>
                                <td class="style6">
                                    <asp:DropDownList ID="drpOfficer" runat="server" AutoPostBack="True" 
                                        height="25px" width="100%">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnSelOutID" runat="server" Text="Show Info" 
                                        onclick="btnSelOutID_Click" style="margin-left: 10px"/>
                                    &nbsp;<asp:Label ID="lblMsgOut" runat="server"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="right" class="style8">
                                    <asp:Label ID="lblSName" runat="server">Name</asp:Label>
                                </td>
                                <td align="left" class="style6" colspan="2">
                                    <asp:Label ID="lblInfoName" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style8">
                                    <asp:Label ID="lblSWDesg" runat="server">Desg.</asp:Label>
                                </td>
                                <td align="left" class="style6" colspan="2">
                                    <asp:Label ID="lblInfoDesg" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style8">
                                    <asp:Label ID="lblSWLoc" runat="server">Loc.</asp:Label>
                                </td>
                                <td align="left" class="style6" colspan="2">
                                    <asp:Label ID="lblInfoWLoc" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style8">
                                    <asp:Label ID="lblSPCLoc" runat="server">Mapping</asp:Label>
                                </td>
                                <td align="left" class="style6" colspan="2">
                                    <asp:Label ID="lblInfoPCLoc" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="style8">
                                    &nbsp;</td>
                                <td align="left" class="style6" colspan="2">
                                    <asp:Image ID="imgEmpPhoto" runat="server" Height="170px" Width="138px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="style8">
                                    &nbsp;</td>
                                <td align="center" class="style6">
                                    <asp:Button ID="btnTransfer" runat="server" onclick="btnTransfer_Click" 
                                        Text="Transfer" />
                                </td>
                                <td align="center" class="style5">
                                    <asp:Button ID="btnPromote" runat="server" onclick="btnPromote_Click" 
                                        Text="Promote" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td class="style7" colspan="2">
                    <asp:Panel ID="panProposed" runat="server" Enabled="False" 
                        GroupingText="Proposed Post" Height="310px" Width="427px">
                        <table style="width:100%; height: 3px;">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblInfo" runat="server" Text="Transfer 'Name' to:" 
                                        Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="style2">
                                    <asp:DropDownList ID="drpFilter" runat="server" AutoPostBack="True" 
                                        Height="25px" onselectedindexchanged="drpFilter_SelectedIndexChanged">
                                        <asp:ListItem Value="A">Show All Posts</asp:ListItem>
                                        <asp:ListItem Value="E">Show Empty</asp:ListItem>
                                        <asp:ListItem Value="F">Show Filled</asp:ListItem>
                                        <asp:ListItem Value="S">Show Special</asp:ListItem>
                                        <asp:ListItem Value="H">Show Higher Posts</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="style2">
                                    <asp:TextBox ID="txtLocFilter" runat="server" AutoPostBack="True" 
                                        CssClass="unwatermarked" Height="25px" ontextchanged="txtLocFilter_TextChanged" 
                                        width="274px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="style2">
                                    <asp:DropDownList ID="drpLocs" runat="server" AutoPostBack="True" 
                                        BackColor="AliceBlue" height="25px" 
                                        onselectedindexchanged="drpLocs_SelectedIndexChanged" width="274px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="style2">
                                    <asp:TextBox ID="txtCLoc" runat="server" AutoPostBack="True" 
                                        CssClass="unwatermarked" height="25px" width="274px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="style2">
                                    <asp:TextBox ID="txtCDesg" runat="server" CssClass="unwatermarked" 
                                        height="25px" width="274px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="unwatermarked" 
                                        height="45px" width="274px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtPrvComment" runat="server" 
                                        CssClass="unwatermarked" height="25px" width="274px" Visible="True"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtSno" runat="server" 
                                        CssClass="unwatermarked" height="25px" width="274px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtNewEmpid" runat="server" 
                                        CssClass="unwatermarked" height="25px" width="274px" Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtDispLeft" runat="server" CssClass="unwatermarked" 
                                        height="25px" Visible="True" width="274px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox ID="txtDispRight" runat="server" CssClass="unwatermarked" 
                                        height="25px" Visible="True" width="274px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSelProposed" runat="server" height="26px" 
                                        onclick="btnSelProposed_Click" Text="Select" width="136px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td class="style7">
                </td>
            </tr>
            <tr><td>&nbsp;<br/>
                <br/>
                <br/>
                <br/>
                <br/>
                </td></tr>
                <tr>
                    <td>
                        <br/>
                        <br/>
                        <br/>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <br/>
                        <br/>
                        <br/>
                    </td>
                </tr>--%>
            <tr>
                <td colspan="6" align="center">
                    <asp:Panel ID="panProposals" runat="server" style="margin-top: 17px">
                        <table style="width:100%;">
                            <tr align="center">
                                <td align="left">
                                <div style="width:100%;overflow:auto;"> 
                                    <asp:GridView ID="gvProposals" runat="server" HorizontalAlign="Left" 
                                        AutoGenerateEditButton="True" onrowediting="gvProposals_RowEditing" 
                                        AutoGenerateDeleteButton="True" onrowdeleting="gvProposals_RowDeleting" 
                                        Font-Size="Small" Width="100%" 
                                          >
                                    </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    &nbsp;<asp:Panel ID="panControls" runat="server">
                        <table style="width:100%;">
                            <tr align="center">
                                <td align="center">
                                    <%--<asp:Button ID="btncreatenotes" runat="server" height="23px" 
                                        onclick="btncreatenotes_Click" Text="Insert Notes" width="150px" />--%>
                                    <a href="frmnotes.aspx" target="_blank">Insert Notes</a>
                                </td>
                                <td align="center">
                                    <%--<asp:Button ID="btnCreateCC" runat="server" EnableTheming="True" height="23px" 
                                        onclick="btnCreateCC_Click" Text="Create CC List" width="150px" />--%>
                                    <a href="CC.aspx" target="_blank">Insert CC List</a>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddPropLineMode" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddPropLineMode_SelectedIndexChanged">
                                        <asp:ListItem Value="A">Auto 1st Line</asp:ListItem>
                                        <asp:ListItem Value="M">Manual</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddApprover" runat="server" style="margin-left: 0px" 
                                        Width="80px">
                                        <asp:ListItem Selected="True">CMD / PSPCL</asp:ListItem>
                                        <asp:ListItem>Dir. Admin. / PSPCL</asp:ListItem>
                                        <asp:ListItem>Dir. Dist. / PSPCL</asp:ListItem>
                                        <asp:ListItem>Dir. Comm. / PSPCL</asp:ListItem>
                                        <asp:ListItem>Dir. Finance / PSPCL</asp:ListItem>
                                        <asp:ListItem>Dir. Gen. / PSPCL</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Button ID="btnPrintProposal" runat="server" height="23px" 
                                        Text="Print Proposal" UseSubmitBehavior="False" width="150px" 
                                        onclick="btnPrintProposal_Click" Visible="False" />
                                    <asp:DropDownList ID="ddFSize" runat="server">
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem Selected="True">11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Button ID="btnXLSProp" runat="server" onclick="btnXLSProp_Click" 
                                        Text="XLS" />
                                    <asp:Button ID="btnPDFProp" runat="server" onclick="btnPDFProp_Click" 
                                        Text="PDF" />
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnPreview" runat="server" height="23px" 
                                        onclick="btnPreview_Click" Text="Preview Report" UseSubmitBehavior="False" 
                                        width="150px" />
                                </td>
                            </tr>
                            <tr align="center">
                                <td align="center" colspan="4">
                                    <asp:TextBox ID="txtPropLine" runat="server" TextMode="MultiLine" 
                                        Visible="False" Width="80%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    </td>
            </tr>
            <tr align="center">
                <td colspan="2">
                    &nbsp;</td>
                <td align="center" colspan="2">
                    <asp:Panel ID="panReports" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td align="center" class="style4">
                                    &nbsp;
                                    <asp:TextBox ID="txtOoNum" runat="server" CssClass="unwatermarked" 
                                        Height="25px" Width="78px"></asp:TextBox>
                                    &nbsp;
                                    <asp:TextBox ID="txtOoDate" runat="server" CssClass="unwatermarked" 
                                        Height="25px" width="78px"></asp:TextBox>
                                    &nbsp;
                                    <asp:TextBox ID="txtEndorsNo" runat="server" CssClass="unwatermarked" 
                                        Height="25px" width="78px"></asp:TextBox>
                                    <asp:Button ID="btnSave" runat="server" height="26px" onclick="btnSave_Click" 
                                        Text="Save and Generate O/o" UseSubmitBehavior="False" width="203px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:HiddenField ID="hidEmpID" runat="server" />
                    <asp:HiddenField ID="hidWDesgCode" runat="server" />
                    <asp:HiddenField ID="hidWLoccode" runat="server" />
                    <asp:HiddenField ID="hidPCRowNo" runat="server" />
                </td>
                <td align="center" colspan="2">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                        <Report FileName="Reports\\rptposttrans.rpt">
                        </Report>
                    </CR:CrystalReportSource>
                </td>
                <td colspan="2">
                    <asp:HiddenField ID="hidStatus" runat="server" />
                    <asp:HiddenField ID="hidolddesgcode" runat="server" />
                    <asp:HiddenField ID="hidsno" runat="server" />
                    <asp:HiddenField ID="hidbranch" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtEmpid').watermark('EMPID');
            $('#txtName').watermark('Name');
            $('#txtLoc').watermark('Posting Location');
            $('#txtLocFilter').watermark('Filter');
            $('#txtCLoc').watermark('Working Location');
            $('#txtCDesg').watermark('Working Designation');
            $('#txtRemarks').watermark('Remarks');
            $('#txtPrvComment').watermark('Prv. Comments');
            $('#txtDispLeft').watermark('Left Text (Optional)');
            $('#txtDispRight').watermark('Right Text (Optional)');
            $('#txtSno').watermark('Serial No. (Optional)').inputmask({ "mask": "9", "repeat": "3", "greedy": false });
            $('#txtOoNum').watermark('O/o Number');
            $('#txtOoDate').watermark('O/o Date');
            $('#txtEndorsNo').watermark('Endor. No.');
            $('#txtEventDate').watermark('Event Date');
            $('#txtNewEmpid').watermark('New EmpID (For JE to AE)');
            doAutoComp('txtName', 'tnp.aspx/GetNames2');
            doAutoComp('txtLoc', 'tnp.aspx/GetLocs2');
            doAutoComp('txtCLoc', 'tnp.aspx/GetLocs2');
            doAutoComp('txtCDesg', 'tnp.aspx/GetDesgs2');
            //gurpreet
            $('#txtOoDate').datepicker(DT_PCKR_OPTS);
            $('#txtEventDate').datepicker(DT_PCKR_OPTS);

            $('#txtName').addClass('hide');
            $('#txtLoc').addClass('hide');

            $('#drpSearchby').on('change', function () {
                var cond = $('#drpSearchby').val();
                if (cond == "empid") {
                    $('#txtEmpid').removeClass('hide');
                    $('#txtName').addClass('hide');
                    $('#txtLoc').addClass('hide');
                }
                else if (cond == "name") {
                    $('#txtEmpid').addClass('hide');
                    $('#txtName').removeClass('hide');
                    $('#txtLoc').addClass('hide');
                }
                else if (cond == "posting") {
                    $('#txtEmpid').addClass('hide');
                    $('#txtName').addClass('hide');
                    $('#txtLoc').removeClass('hide');
                }
            });
        });
    </script>
</asp:Content>

