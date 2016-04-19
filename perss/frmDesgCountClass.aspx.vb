Imports CrystalDecisions.Web
Imports CrystalDecisions.Shared

Partial Class frmDesgCountClass
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Utils.AuthenticateUser(Me)
        Dim sql As String
        Dim rtpFile As String
        Dim oraCn As New OraDBconnection
        Dim ds As New Data.DataSet
        Dim pdfPath As String

        'sql = "select c.desgcode, " &
        '        "desgabb, " &
        '        "ms.newscale|| ' + ' || ms.gradepay as scale, " &
        '        "count(decode(ptype,'P','*',NULL)) as P, " &
        '        "count(decode(ptype,'T','*',NULL)) as T, " &
        '        "count(decode(ptype,'M','*',NULL)) as M, " &
        '        "count(decode(ptype,'X','*',NULL)) as X, " &
        '        "count(*) as Total " &
        '        "from " &
        '        "cadre.cadr c " &
        '        "full outer join pshr.mast_desg md on c.desgcode = md.desgcode " &
        '        "full outer join cadre.mast_scale ms on c.scaleid = ms.scaleid " &
        '        "where c.desgcode in (select desgcode from pshr.mast_desg where adesg='A') " &
        '        "and hia=0 " &
        '        "group by c.desgcode,desgabb,c.scaleid,ms.newscale,ms.gradepay,hecode " &
        '        "order by hecode,c.scaleid desc"
        sql = "select c.desgcode, pshr.get_desg(c.desgcode) as desgtext,pshr.get_scale_txt(c.scaleid) scale, " &
                "count(decode(cls,'A',1,null)) A, " &
                "count(decode(cls,'B',1,null)) B, " &
                "count(decode(cls,'C',1,null)) C, " &
                "count(decode(cls,'D',1,null)) D, " &
                "count(*) tot from " &
                "cadre.cadr c inner join pshr.mast_desg md on c.desgcode = md.desgcode " &
                "where adesg = 'A' and c.loccode not like '601%' " &
                "group by c.desgcode, cls, hecode,c.scaleid " &
                "order by hecode,c.scaleid desc"
        oraCn.FillData(sql, ds)
        pdfPath = Server.MapPath("office_orders\\" & "ByDesgCountClass.pdf")

        'save office order at server
        Dim CrystalReportSource1 As New CrystalReportSource()
        CrystalReportSource1.Report.FileName = Server.MapPath("Reports\\DesgCountClass.rpt")
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
