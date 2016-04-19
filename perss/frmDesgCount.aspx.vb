Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports System.Data.Odbc

Partial Class frmDesgCount
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Utils.AuthenticateUser(Me)
        Dim sql As String
        Dim rtpFile As String
        Dim oraCn As New OraDBconnection
        Dim ds As New Data.DataSet
        Dim pdfPath As String

        sql = "select c.desgcode, " &
                "desgabb, " &
                "ms.newscale|| ' + ' || ms.gradepay as scale, " &
                "count(decode(ptype,'P','*',NULL)) as P, " &
                "count(decode(ptype,'T','*',NULL)) as T, " &
                "count(decode(ptype,'M','*',NULL)) as M, " &
                "count(decode(ptype,'X','*',NULL)) as X, " &
                "count(*) as Total " &
                "from " &
                "cadre.cadr c " &
                "full outer join pshr.mast_desg md on c.desgcode = md.desgcode " &
                "full outer join cadre.mast_scale ms on c.scaleid = ms.scaleid " &
                "where c.desgcode in (select desgcode from pshr.mast_desg where adesg='A') " &
                "and hia=0 " &
                "group by c.desgcode,desgabb,c.scaleid,ms.newscale,ms.gradepay,hecode " &
                "order by hecode,c.scaleid desc"
        oraCn.FillData(sql, ds)
        pdfPath = Server.MapPath("office_orders\\" & "ByDesgCount.pdf")

        'save office order at server
        Dim CrystalReportSource1 As New CrystalReportSource()
        CrystalReportSource1.Report.FileName = Server.MapPath("Reports\\DesgCount.rpt")
        CrystalReportSource1.ReportDocument.SetDatabaseLogon("pshr", "123")
        CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
        CrystalReportSource1.DataBind()
        CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath)
        DownloadFile(pdfPath)
    End Sub
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
End Class
