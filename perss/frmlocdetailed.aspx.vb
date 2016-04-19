Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.IO
Imports System.Drawing
Imports System.Text.RegularExpressions
Imports CrystalDecisions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Partial Class frmlocdetailed
    Inherits System.Web.UI.Page
    Protected Sub DownloadFile(ByVal pdfPath As String)
        Dim objFi As New System.IO.FileInfo(pdfPath)
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + objFi.Name)
        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.AddHeader("Content-Length", objFi.Length.ToString())
        HttpContext.Current.Response.ContentType = "application/pdf"
        HttpContext.Current.Response.WriteFile(objFi.FullName)
        HttpContext.Current.Response.End()
    End Sub

    Private Sub FillLocation(Optional ByVal filter As String = "")
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet

        sql = "select loccode,substr(locabb,1,80) as lname from pshr.mast_loc where aloc=1 and " &
                "locabb like '%" & filter.Replace(" ", "%").ToUpper() & "%' and " &
                "loccode in (select distinct loccode from cadre.cadr) order by locabb"
        oracn.FillData(sql, ds)

        drploc.DataSource = ds.Tables(0)
        drploc.DataValueField = "loccode"
        drploc.DataTextField = "lname"
        drploc.DataBind()
        drploc.Items.Insert(0, New ListItem("--Select--", 0))
    End Sub

    Private Sub Filldesg(Optional ByVal filter As String = "")
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim loccode As String

        loccode = drploc.SelectedValue
        If Me.chkreploc.Checked = True Then
            loccode = Regex.Replace(loccode, "0+$", "%")
            sql = "select desgcode,desgtext from pshr.mast_desg where  " &
                    "desgtext like '%" & filter.Replace(" ", "%").ToUpper() & "%' and " &
                    "desgcode in (select desgcode from cadre.cadr where loccode like ('" & loccode & "')) order by hecode"
        Else
            sql = "select desgcode,desgtext from pshr.mast_desg where  " &
                    "desgtext like '%" & filter.Replace(" ", "%").ToUpper() & "%' and " &
                    "desgcode in (select desgcode from cadre.cadr where loccode = " & loccode & ") order by hecode"
        End If

        oracn.FillData(sql, ds)

        drpdesg.DataSource = ds.Tables(0)
        drpdesg.DataValueField = "desgcode"
        drpdesg.DataTextField = "desgtext"
        drpdesg.DataBind()
        drpdesg.Items.Insert(0, New ListItem("--All--", 0))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            FillLocation()
        End If
    End Sub

    Protected Sub txtfloc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfloc.TextChanged
        FillLocation(txtfloc.Text)
    End Sub

    Protected Sub txtfdesg_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfdesg.TextChanged
        Filldesg(txtfdesg.Text)
    End Sub

    Protected Sub drploc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drploc.SelectedIndexChanged
        Filldesg()
    End Sub

    Protected Sub chkreploc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkreploc.CheckedChanged
        Filldesg()
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Dim rptFile As String = "Reports\\rptcontabs.rpt"
        Dim fileName As String = String.Empty
        Dim sql As String
        Dim ds As New System.Data.DataSet
        Dim oraCn As New OraDBconnection
        Dim ds1 As New System.Data.DataSet
        Dim sysdate As String
        Dim loc As String
        Dim branchcode As String
        Dim branchclause As String
        Dim desgcode As String
        Dim desgclause As String

        loc = drploc.SelectedValue
        branchcode = drpbranch.SelectedValue
        desgcode = drpdesg.SelectedValue

        branchclause = ""
        If (branchcode = "1") Then
            branchclause = " and branch in (1,2,3,5) "
        ElseIf (branchcode = "4") Then
            branchclause = " and branch = 4 "
        ElseIf (branchcode = "99") Then
            branchclause = " and branch = 99 "
        End If

        desgclause = ""
        If (desgcode <> "0") Then
            desgclause = " and desgcode = " & desgcode & " "
        End If

        If chkreploc.Checked Then
            sql = " select pshr.get_desg(desgcode),pshr.get_org(" & loc & " ) as loc, cadre.get_scale_txt(scaleid) as scale ,cadre.get_branch(branch) as branchtext, sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4) from (" &
                                  "select desgcode, scaleid,branch, cnt as cnt1,0 as cnt2,0 as cnt3,0 as cnt4 from (select desgcode, scaleid,branch, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by desgcode,ptype, scaleid, branch) a where ptype = 'P'" & branchclause & desgclause &
                       " union all select desgcode, scaleid,branch, 0 as cnt1,cnt as cnt2,0 as cnt3,0 as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by desgcode,ptype, scaleid, branch) a where ptype = 'T'" & branchclause & desgclause &
                       " union all select desgcode, scaleid,branch, 0 as cnt1,0 as cnt2,cnt as cnt3,0 as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by desgcode,ptype, scaleid, branch) a where ptype = 'X'" & branchclause & desgclause &
                       " union all select desgcode, scaleid,branch, 0 as cnt1,0 as cnt2,0 as cnt3,cnt as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by desgcode,ptype, scaleid, branch) a where ptype = 'M'" & branchclause & desgclause & ") b " &
                       " group by desgcode, scaleid,branch  order by scaleid desc"
        Else
            sql = " select pshr.get_desg(desgcode),pshr.get_org(" & loc & " ) as loc, cadre.get_scale_txt(scaleid) as scale ,cadre.get_branch(branch) as branchtext, sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4) from (" &
                                  "select desgcode, scaleid,branch, cnt as cnt1,0 as cnt2,0 as cnt3,0 as cnt4 from (select desgcode, scaleid,branch, count(*) as cnt,ptype from cadre.cadr where loccode =" & loc & " group by desgcode,ptype, scaleid, branch) a where ptype = 'P'" & branchclause & desgclause &
                       " union all select desgcode, scaleid,branch, 0 as cnt1,cnt as cnt2,0 as cnt3,0 as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where loccode =" & loc & " group by desgcode,ptype, scaleid, branch) a where ptype = 'T'" & branchclause & desgclause &
                       " union all select desgcode, scaleid,branch, 0 as cnt1,0 as cnt2,cnt as cnt3,0 as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where loccode =" & loc & " group by desgcode,ptype, scaleid, branch) a where ptype = 'X'" & branchclause & desgclause &
                       " union all select desgcode, scaleid,branch, 0 as cnt1,0 as cnt2,0 as cnt3,cnt as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where loccode =" & loc & " group by desgcode,ptype, scaleid, branch) a where ptype = 'M'" & branchclause & desgclause & ") b " &
                       " group by desgcode, scaleid,branch  order by scaleid desc"
        End If


        oraCn.FillData(sql, ds)

        'get sysdate
        oraCn.FillData("select to_char(sysdate,'dd-mm-yyyy') from dual", ds1)
        sysdate = ds1.Tables(0).Rows(0)(0)
        ds1.Clear()

        Dim pdfPath As String
        pdfPath = Server.MapPath("office_orders\\" & "contabs" & "-" & sysdate & ".pdf")

        'save office order at server
        Dim CrystalReportSource1 As New CrystalReportSource()
        CrystalReportSource1.Report.FileName = Server.MapPath("Reports\\rptcontabs.rpt")
        CrystalReportSource1.ReportDocument.SetDatabaseLogon("pshr", "pshr")
        CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
        CrystalReportSource1.DataBind()
        CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath)
        DownloadFile(pdfPath)
    End Sub
End Class
