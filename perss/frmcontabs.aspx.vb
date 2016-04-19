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

Partial Class frmcontabs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Utils.AuthenticateUser(Me)
        Dim arylist As New ArrayList()
        Dim selformula As [String] = String.Empty
        Dim oStream As MemoryStream = Nothing
        Dim rptFile As String = String.Empty
        Dim fileName As String = String.Empty
        Dim sql As String
        Dim doc As New ReportDocument()
        Dim loc As Long
        Dim pcount, tcount, xcount, mcount, rnum As Integer

        If Not IsPostBack Then

            rptFile = "Reports\\rptorgabs.rpt"

            'sql = "select  pshr.get_zone(substr(loccode,1,3) * 1000000),substr(loccode,1,3),sum(Perm),sum(Temp),sum(Ex),sum(PM) from " &
            '    "((select loccode,desgcode,1 as perm,0 as Temp,0 as Ex, 0 as PM from cadre.cadr where ptype = 'P' ) union all " &
            '    "(select loccode,desgcode, 0 as Perm, 1 as Temp,0 as Ex, 0 as PM from cadre.cadr where ptype = 'T' ) Union all " &
            '    "(select loccode,desgcode, 0 as Perm,0 as Temp,1 as Ex, 0 as PM from cadre.cadr where ptype = 'X' ) union all " &
            '    "(select loccode,desgcode, 0 as Perm,0 as Temp,0 as Ex, 1 as PM from cadre.cadr where ptype = 'M' )) " &
            '    " group by substr(loccode,1,3) order by substr(loccode,1,3)"
            Dim oraCn As New OraDBconnection
            Dim ds, ds1 As New Data.DataSet

            sql = "delete from cadre.rptorgabs"
            oraCn.ExecQry(sql)

            'sql = "select distinct (substr(loccode,1,3)) from cadre.cadr order by substr(loccode,1,3)"
            sql = "select distinct (substr(l.loccode,1,3)),l.loctype from cadre.cadr c, pshr.mast_loc l where c.loccode=l.loccode and l.loctype not in (30,40,50,60,70)  order by l.loctype"

            oraCn.FillData(sql, ds)
            rnum = 1
            For i = 0 To ds.Tables(0).Rows.Count - 1
                pcount = 0
                tcount = 0
                xcount = 0
                mcount = 0

                'loc = ds.Tables(0).Rows(i)(0)
                loc = ds.Tables(0).Rows(i)(0) & "000000"
                ' If loc = 501000000 Then
                ' loc = 801000000
                ' End If

                If Mid$(loc, 1, 2) = "80" Then
                    sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
                             "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc = 1 and loctype in (11,30,40,50,60)) group by loccode,ptype) a where ptype = 'P' " &
                             " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc = 1 and loctype in (11,30,40,50,60)) group by loccode,ptype) a where ptype = 'T' " &
                             " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc = 1 and loctype in (11,30,40,50,60)) group by loccode,ptype) a where ptype = 'X' " &
                             " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc = 1 and loctype in (11,30,40,50,60)) group by loccode,ptype) a where ptype = 'M')" &
                             " b group by substr(loccode,1,3) order by substr(loccode,1,3) "


                    'sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
                    '                             "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and loctype in (11,30)) group by loccode) a where ptype = 'P' " &
                    '                             " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and loctype in (11,30)) group by loccode) a where ptype = 'T' " &
                    '                             " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and loctype in (11,30)) group by loccode) a where ptype = 'X' " &
                    '                             " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and loctype in (11,30)) group by loccode) a where ptype = 'M')" &
                    '                             " b group by substr(loccode,1,3) order by substr(loccode,1,3)"
                Else
                    sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
                             "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by loccode,ptype) a where ptype = 'P' " &
                             " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by loccode,ptype) a where ptype = 'T' " &
                             " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by loccode,ptype) a where ptype = 'X' " &
                             " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by loccode,ptype) a where ptype = 'M')" &
                             " b group by substr(loccode,1,3) order by substr(loccode,1,3)"

                End If


                ds1 = New System.Data.DataSet
                oraCn.FillData(sql, ds1)

                For j = 0 To ds1.Tables(0).Rows.Count - 1
                    pcount += ds1.Tables(0).Rows(j)(2)
                    tcount += ds1.Tables(0).Rows(j)(3)
                    xcount += ds1.Tables(0).Rows(j)(4)
                    mcount += ds1.Tables(0).Rows(j)(5)
                Next j
                If ds1.Tables(0).Rows.Count > 0 Then
                    sql = "Insert into cadre.rptorgabs values ('" & ds1.Tables(0).Rows(0)(0) & "'," & ds1.Tables(0).Rows(0)(1) & "," & pcount & "," & tcount & "," & xcount & "," & mcount & ",null,null," & rnum & ")"
                    oraCn.ExecQry(sql)
                    rnum = rnum + 1
                End If
                ds1.Clear()
                ds1.Dispose()
            Next i
            ds.Clear()
            ds.Dispose()

            ds = New System.Data.DataSet
            sql = "select * from cadre.rptorgabs order by rnum"
            oraCn.FillData(sql, ds)
            'for crystal reports view

            'Me.CrystalReportSource1.Report.FileName = Server.MapPath(rptFile)
            'Me.CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
            'Me.CrystalReportViewer1.ReportSource = CrystalReportSource1
            'Me.CrystalReportViewer1.DataBind()

            ' for pdf view
            Dim ds2 As New System.Data.DataSet
            Dim sysdate As String

            'get sysdate
            oraCn.FillData("select to_char(sysdate,'dd-mm-yyyy') from dual", ds2)
            sysdate = ds2.Tables(0).Rows(0)(0)
            ds2.Clear()

            Dim pdfPath As String
            pdfPath = Server.MapPath("office_orders\\" & "OrgAbs" & "-" & sysdate & ".pdf")
            Session("pdfpath") = pdfPath

            'save office order at server
            Dim CrystalReportSource1 As New CrystalReportSource()
            CrystalReportSource1.Report.FileName = Server.MapPath("Reports\\rptorgabs.rpt")
            CrystalReportSource1.ReportDocument.SetDatabaseLogon("pshr", "pshr")
            CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
            CrystalReportSource1.DataBind()
            CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath)
            DownloadFile(pdfPath)
        End If
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
