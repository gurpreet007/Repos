﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Site1.master" AutoEventWireup="false" CodeFile="frmcontabs.aspx.vb" Inherits="frmcontabs" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
    AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
    ReportSourceID="CrystalReportSource1" ToolPanelView="None" />
<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
</CR:CrystalReportSource>
</asp:Content>

