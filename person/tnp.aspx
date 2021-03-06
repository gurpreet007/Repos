﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="tnp.aspx.cs" Inherits="frmproposal" ClientIDMode="Static"%>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style>
        #wrapper {margin:auto; text-align:center}
        #header {background-color:Silver; width: 90%; margin: auto; color:White; text-align:center; padding:5px}
        #divchoose {display: inline-block;  width:30%; padding:5px; background-color:White; color:Black; text-align:left; padding-left:30px; border-right-style:dotted; border-right-width:1px}
        #divpropose {display: inline-block; width:50%; padding:5px; background-color:White; color:Black; vertical-align:top; height:100%}
        #grid {margin: auto; width:90%; clear: both; padding:5px; background-color:White; color:Black}
        #prop_controls {margin: auto; width:90%; clear: both; padding:5px; background-color:Silver; color:White; border-bottom-style:dotted}
        #prev_controls {margin: auto;width:90%; clear: both; padding:5px; background-color:Silver; color:White; border-bottom-style:dotted}
        #save_controls {margin: auto;width:90%; clear: both; padding:5px; background-color:Silver; color:White}
        #hidd_fields {margin: auto;width:90%; clear: both; padding:5px; background-color:White; color:Black}
        #imgEmpPhoto {width: 138px; height: 170px}
        #grid {overflow:auto}
        #drpLocs {width:80%;max-width:90%; overflow:hidden}
        #txtPropLine {width:80%; height:40px}
        input[type=text].divsave {width:80px;}
        input[type=text].divpropose {width:80%;}
        textarea {width:80%;}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="wrapper">
        <div id="header">
            <asp:Label ID="lblProposalName" runat="server" Text="Label"></asp:Label>
        </div>
        <div id="divchoose" runat="server">
            <h2>Choose</h2>
            <p>
                <asp:TextBox ID="txtEmpid" runat="server"></asp:TextBox>
                <asp:Button ID="btnSelNewID" runat="server" Text="Show Info" onclick="btnSelNewID_Click"/>
                <br />
                <asp:Label ID="lblMsgNew" runat="server" Text=""></asp:Label>
                <br />
                <asp:DropDownList ID="drpOfficer" runat="server"> </asp:DropDownList>
                <asp:Button ID="btnSelOutID" runat="server" Text="Show Info" onclick="btnSelOutID_Click"/>
                <br />
                <asp:Label ID="lblMsgOut" runat="server" Text=""></asp:Label>
            </p>
            <strong>Name: </strong>
            <asp:Label ID="lblInfoName" runat="server" Text=""></asp:Label>
            <br />
            <strong>Desg: </strong>
            <asp:Label ID="lblInfoDesg" runat="server" Text=""></asp:Label>
            <br />
            <strong>Loc: </strong>
            <asp:Label ID="lblInfoWLoc" runat="server" Text=""></asp:Label>
            <br />
            <strong>Mapping: </strong>
            <asp:Label ID="lblInfoPCLoc" runat="server" Text=""></asp:Label>
            <br />
            <asp:Image ID="imgEmpPhoto" runat="server" />
            <p>
                <asp:Button ID="btnTransfer" runat="server" Text="Transfer" onclick="btnTransfer_Click"/>
                <asp:Button ID="btnPromote" runat="server" Text="Promote" onclick="btnPromote_Click"/>
            </p>
        </div>
        <div id="divpropose"  runat="server">
            <h2>Propose</h2>
            <asp:Label ID="lblInfo" runat="server" Text="Transfer 'Name' to:"></asp:Label>
            <br />
            <asp:DropDownList ID="drpFilter" runat="server" AutoPostBack="true" onselectedindexchanged="drpFilter_SelectedIndexChanged">
                <asp:ListItem Value="A">Show All Posts</asp:ListItem>
                <asp:ListItem Value="E">Show Empty</asp:ListItem>
                <asp:ListItem Value="F">Show Filled</asp:ListItem>
                <asp:ListItem Value="S">Show Special</asp:ListItem>
                <asp:ListItem Value="H">Show Higher Posts</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:TextBox ID="txtLocFilter" runat="server" AutoPostBack="True" CssClass="divpropose unwatermarked" ontextchanged="txtLocFilter_TextChanged"></asp:TextBox>
            <br />
            <asp:DropDownList ID="drpLocs" runat="server" AutoPostBack="true" class="divpropose" onselectedindexchanged="drpLocs_SelectedIndexChanged"></asp:DropDownList>
            <br />
            <asp:TextBox ID="txtCLoc" runat="server" AutoPostBack="True" CssClass="divpropose unwatermarked"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtCDesg" runat="server" CssClass="divpropose unwatermarked"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="divpropose unwatermarked"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtPrvComment" runat="server" CssClass="divpropose unwatermarked"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtSno" runat="server" CssClass="divpropose unwatermarked"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtNewEmpid" runat="server" CssClass="divpropose unwatermarked" Visible=false></asp:TextBox>
            <br />
            <asp:TextBox ID="txtDispLeft" runat="server" TextMode="MultiLine" CssClass="divpropose unwatermarked"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtDispRight" runat="server" TextMode="MultiLine" CssClass="divpropose unwatermarked"></asp:TextBox>
            <br />
            <asp:Button ID="btnSelProposed" runat="server" Text="Select" onclick="btnSelProposed_Click"/>
        </div>
        <div id="grid">
            <asp:GridView ID="gvProposals" runat="server" HorizontalAlign="Left" 
                AutoGenerateEditButton="True" AutoGenerateDeleteButton="True" 
                Font-Size="Small" Width="100%" onrowdeleting="gvProposals_RowDeleting" 
                onrowediting="gvProposals_RowEditing">
                <HeaderStyle BackColor="Silver" ForeColor="White" />
            </asp:GridView>
        </div>
        <div id="prop_controls">
            <strong>Proposal Report: </strong>
            <asp:DropDownList ID="ddPropLineMode" runat="server" AutoPostBack="True"
                onselectedindexchanged="ddPropLineMode_SelectedIndexChanged">
                <asp:ListItem Value="A">Auto 1st Line</asp:ListItem>
                <asp:ListItem Value="M">Manual</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddApprover" runat="server" style="margin-left: 0px" Width="80px">
                <asp:ListItem Selected="True">CMD / PSPCL</asp:ListItem>
                <asp:ListItem>Dir. Admin. / PSPCL</asp:ListItem>
                <asp:ListItem>Dir. Dist. / PSPCL</asp:ListItem>
                <asp:ListItem>Dir. Comm. / PSPCL</asp:ListItem>
                <asp:ListItem>Dir. Finance / PSPCL</asp:ListItem>
                <asp:ListItem>Dir. Gen. / PSPCL</asp:ListItem>
             </asp:DropDownList>
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
            <asp:Button ID="btnPDFProp" runat="server" Text="PDF" onclick="btnPDFProp_Click"/>
            <asp:Button ID="btnXLSProp" runat="server" Text="XLS" onclick="btnXLSProp_Click"/>
            <br />
            <asp:TextBox ID="txtPropLine" runat="server" TextMode="MultiLine" Visible=false></asp:TextBox>
        </div>
        <div id="prev_controls">
            <strong>Preview Report: </strong>
            <a href="frmnotes.aspx" target="_blank">Insert Notes</a>
            <a href="CC.aspx" target="_blank">Insert CC List</a>
            <asp:DropDownList ID="ddFSizePrev" runat="server">
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
            <asp:Button ID="btnPreview" runat="server" Text="Preview" onclick="btnPreview_Click"/>
        </div>
        <div id="save_controls">
            <strong>Save: </strong>
            <asp:TextBox ID="txtOoNum" runat="server" CssClass="divsave unwatermarked"></asp:TextBox>
            <asp:TextBox ID="txtOoDate" runat="server" CssClass="divsave unwatermarked"></asp:TextBox>
            <asp:TextBox ID="txtEndorsNo" runat="server" CssClass="divsave unwatermarked"></asp:TextBox>
            <asp:Button ID="btnSave" runat="server" Text="Save and Generate O/o" onclick="btnSave_Click"/>
        </div>
        <div id="hidd_fields">
            <asp:HiddenField ID="hidEmpID" runat="server" />
            <asp:HiddenField ID="hidWDesgCode" runat="server" />
            <asp:HiddenField ID="hidWLoccode" runat="server" />
            <asp:HiddenField ID="hidPCRowNo" runat="server" />
            <asp:HiddenField ID="hidStatus" runat="server" />
            <asp:HiddenField ID="hidolddesgcode" runat="server" />
            <asp:HiddenField ID="hidsno" runat="server" />
            <asp:HiddenField ID="hidbranch" runat="server" />
            <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
            <cr:crystalreportsource ID="CrystalReportSource1" runat="server">
                <Report FileName="Reports\\rptposttrans.rpt"> </Report>
            </cr:crystalreportsource>
        </div>
    </div>

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

