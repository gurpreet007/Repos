Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports System.Threading
Partial Class rptcont
    Inherits System.Web.UI.Page
    Dim newt As Thread
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
    Private Sub GetAttached(ByRef srno As Integer, ByVal loccode As Long, ByVal level As Integer)
        Dim sql As String
        Dim row As System.Data.DataRow
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim a As New Integer
        sql = "select rowno,srno from cadre.cadr where loccode=" & loccode & " order by scaleid desc,desgcode, indx"
        oracn.FillData(sql, ds)

        'assign serial no. to posts under this officer
        For Each row In ds.Tables(0).Rows
            ' If IsDBNull(row("srno")) Then
            'sql = "update cadre.cadr set srno=" & srno & ", rlevel = " & level & " where rowno=" & row("rowno")
            sql = "update cadre.cadr set srno=" & srno & " where rowno=" & row("rowno")
            oracn.ExecQry(sql)
            srno += 1
            'End If
        Next
    End Sub
    Private Sub UpdateSrno(ByVal loccode As String, ByVal opt As String)
        Dim sql As String
        Dim row1, row2 As System.Data.DataRow
        Dim oracn As New OraDBconnection
        Dim ds1, ds2, ds3, ds4 As System.Data.DataSet
        Dim srno As Integer

        'reset srno's
        sql = "update cadre.cadr set srno=NULL"
        oracn.ExecQry(sql)

        'initialize serialno
        srno = 1

        'now we have loccode of chief, get posts under it
        GetAttached(srno, loccode, 1)

        'get SE/XEN/AEE/Suboffice(SO) under this chief
        ds1 = New System.Data.DataSet

        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & loccode & " and aloc=1 and loctype in (30,40,50,60,70) order by loctype desc"
        oracn.FillData(sql, ds1)

        For Each row1 In ds1.Tables(0).Rows
            If row1("loctype") = 30 Then    'SE under CE
                GetAttached(srno, row1("loccode"), 2)    'A/W SE

                'get underlying XEN,AEE,SO to this SE
                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (40,50,60,70) order by loctype desc"
                ds2 = New System.Data.DataSet
                oracn.FillData(sql, ds2)

                For Each row2 In ds2.Tables(0).Rows
                    If row2("loctype") = 40 Then     'XEN Under SE Under CE
                        GetAttached(srno, row2("loccode"), 3)    'A/W XEN

                        'get underlying AEE and S/O to this XEN
                        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (50,60,70) order by loctype desc"
                        ds3 = New System.Data.DataSet
                        oracn.FillData(sql, ds3)
                        For Each row3 In ds3.Tables(0).Rows
                            If row3("loctype") = 50 Then        'AEE Under XEN Under SE Under CE
                                GetAttached(srno, row3("loccode"), 4)  'A/W AEE

                                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row3("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                                ds4 = New System.Data.DataSet
                                oracn.FillData(sql, ds4)
                                For Each row4 In ds4.Tables(0).Rows
                                    GetAttached(srno, row4("loccode"), 5)    'SO Under AEE Under XEN Under SE under CE
                                Next
                            ElseIf row3("loctype") = 60 Or row3("loctype") = 70 Then        'SO Under XEN under SE under CE
                                GetAttached(srno, row3("loccode"), 4)
                            End If
                        Next
                    ElseIf row2("loctype") = 50 Then   'AEE under SE
                        GetAttached(srno, row2("loccode"), 3)    'A/W AEE
                        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                        ds3 = New System.Data.DataSet
                        oracn.FillData(sql, ds3)
                        For Each row3 In ds3.Tables(0).Rows
                            GetAttached(srno, row3("loccode"), 4)    'SO Under AEE Under SE Under CE
                        Next
                    ElseIf row2("loctype") = 60 Or row2("loctype") = 70 Then   'SO under SE
                        GetAttached(srno, row2("loccode"), 3)
                    End If
                Next
                ds2.Clear()
                ds2.Dispose()

                'if officer is Sr. XEN
            ElseIf row1("loctype") = 40 Then            'XEN under CE
                ds2 = New System.Data.DataSet
                GetAttached(srno, row1("loccode"), 2)      'A/W XEN
                'get underlying AEE to this XEN
                'GetUnderAEE(srno, row1("loccode"))
                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (50,60,70) order by loctype desc"
                oracn.FillData(sql, ds2)

                For Each row2 In ds2.Tables(0).Rows
                    If row2("loctype") = 50 Then   'AEE under XEN
                        GetAttached(srno, row2("loccode"), 3)    'A/W AEE
                        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                        ds3 = New System.Data.DataSet
                        oracn.FillData(sql, ds3)
                        For Each row3 In ds3.Tables(0).Rows
                            GetAttached(srno, row3("loccode"), 4)    'SO Under AEE Under XEN Under CE
                        Next
                    ElseIf row2("loctype") = 60 Or row2("loctype") = 70 Then   'SO under XEN
                        GetAttached(srno, row2("loccode"), 3)
                    End If
                Next

                'if officer is AEE/AE
            ElseIf row1("loctype") = 50 Then   'AEE under CE
                GetAttached(srno, row1("loccode"), 2)    'A/W AEE
                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                ds2 = New System.Data.DataSet
                oracn.FillData(sql, ds2)
                For Each row2 In ds2.Tables(0).Rows
                    GetAttached(srno, row2("loccode"), 3)    'SO Under AEE Under CE
                Next
            ElseIf row1("loctype") = 60 Or row1("loctype") = 70 Then   'SO under CE
                GetAttached(srno, row1("loccode"), 2)
            End If
        Next
    End Sub
    Protected Sub txtLocFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLocFilter.TextChanged
        Dim sql As String = String.Empty
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim filter As String = txtLocFilter.Text.Replace(" ", "%").ToUpper()

        sql = String.Format("select locabb as text, loccode as val from pshr.mast_loc where aloc = 1 and (locname like '%{0}%'" &
                            "or locabb like '%{0}%') order by locabb", filter)

        oracn.FillData(sql, ds)
        drpLocs.DataSource = ds
        drpLocs.DataTextField = "text"
        drpLocs.DataValueField = "val"
        drpLocs.DataBind()

    End Sub
    Protected Sub txtDesgFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDesgFilter.TextChanged
        Dim sql As String = String.Empty
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim filter As String = txtDesgFilter.Text.Replace(" ", "%").ToUpper()
        sql = String.Format("select desgabb as text, desgcode as val from pshr.mast_desg where adesg = 'A' and (desgtext like '%{0}%'" &
                            "or desgabb like '%{0}%') order by desgabb", filter)

        oracn.FillData(sql, ds)
        drpDesgs.DataSource = ds
        drpDesgs.DataTextField = "text"
        drpDesgs.DataValueField = "val"
        drpDesgs.DataBind()
    End Sub
    Private Function MakeLocContReport(ByVal loccode As String, ByVal desgcode As String) As String
        Dim rptFile As String = String.Empty
        Dim fileName As String = String.Empty
        Dim sql As String = ""
        Dim ds As New System.Data.DataSet
        Dim desgclause As String = IIf(String.IsNullOrEmpty(desgcode), "", " and desgcode = " + desgcode + " ")
        rptFile = "Reports\\CrystalReportwD.rpt"

        sql = "select c.srno,c.loccode,pshr.get_zone(c.loccode) as zone,pshr.get_org(c.loccode) as org," &
            "pshr.get_desg(c.desgcode) as des,c.desgcode,c.scaleid,cadre.get_scale_txt(c.scaleid) as scale, " &
            "c.branch,c.hia, " +
            "decode(c.branch, 99,' ',88,' ',cadre.get_branch_withbraces(c.branch)) as branchtext," +
            " c.remarks, " &
            "(select count(*) from cadre.cadr e where e.ptype = 'P' and e.rowno = c.rowno) as perm, " &
            "(select count(*) from cadre.cadr e where e.ptype = 'T' and e.rowno = c.rowno) as temp, " &
            "(select count(*) from cadre.cadr e where e.ptype = 'X' and e.rowno = c.rowno) as ex, " &
            "(select count(*) from cadre.cadr e where e.ptype = 'M' and e.rowno = c.rowno) as pm  " &
            "from cadre.cadr c, pshr.mast_loc m where c.loccode = m.loccode and c.srno is not null and m.aloc=1" & desgclause & " and " &
            "m.loccode in (select loccode from pshr.mast_loc start with loccode=" & loccode &
            " connect by prior loccode=locrep and aloc=1)  order by c.srno "

        Dim oraCn As New OraDBconnection
        oraCn.FillData(sql, ds)

        Dim ds1 As New System.Data.DataSet

        Dim pdfPath As String
        pdfPath = Server.MapPath("office_orders\\" & "cont" & "-" & loccode & ".pdf")

        'save office order at server
        Dim CrystalReportSource1 As New CrystalReportSource()
        CrystalReportSource1.Report.FileName = Server.MapPath("Reports\\CrystalReportwD.rpt")
        CrystalReportSource1.ReportDocument.SetDatabaseLogon("pshr", "pshr")
        CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
        CrystalReportSource1.DataBind()
        CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath)
        Return pdfPath
    End Function
    Protected Sub btnShowReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowReport.Click
        Dim loccode As String = drpLocs.SelectedValue
        Dim desgcode As String = drpDesgs.SelectedValue

        UpdateSrno(loccode, "ALL")
        DownloadFile(MakeLocContReport(loccode, desgcode))
    End Sub
End Class