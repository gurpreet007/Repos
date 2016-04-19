Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports System.Threading
Partial Class rptcont
    Inherits System.Web.UI.Page
    Dim newt As Thread

    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        get_dir()
        Me.drporg.Items.Clear()
        'btnshowAbstract.Enabled = False
    End Sub
    Private Sub get_dir()
        Me.drporgtype.Items.Clear()
        If Me.RadioButtonList1.SelectedValue = "P" Then
            Me.drporgtype.Items.Add(New ListItem("--Select--", 0))
            Me.drporgtype.Items.Add(New ListItem("CMD/PSPCL", "800000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Distribution", "803000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Generation", "802000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Finance", "804000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Commercial", "805000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/HR", "806000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Admin", "801000000"))
            Me.drporgtype.Items.Add(New ListItem("Deputation", "600000000"))
            Me.drporgtype.Items.Add(New ListItem("Ombudsman", "810000000"))
        ElseIf Me.RadioButtonList1.SelectedValue = "T" Then
            Me.drporgtype.Items.Add(New ListItem("--Select--", 0))
            Me.drporgtype.Items.Add(New ListItem("CMD/PSTCL", "503000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Technical", "503000001"))
            Me.drporgtype.Items.Add(New ListItem("Director/Finance & Commercial", "503000002"))
            Me.drporgtype.Items.Add(New ListItem("Director/Admin", "503000004"))

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Utils.AuthenticateUser(Me)
        If Not Page.IsPostBack Then
            get_dir()
        End If
        'if drporg empty or 0 then disable btnshow0
        If drporg.SelectedValue = "" Or drporg.SelectedValue = "0" Then
            'btnshowAbstract.Enabled = False
        Else
            btnshowAbstract.Enabled = True
        End If
    End Sub

    Protected Sub drporgtype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drporgtype.SelectedIndexChanged
        If drporgtype.SelectedValue <> "0" Then
            fillorg()
        Else
            drporg.Items.Clear()
        End If
    End Sub
    Private Sub fillorg()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer

        If drporgtype.SelectedValue <> "600000000" Then
            sql = "select loccode, locabb from pshr.mast_loc where locname is not null and " +
                "locrep = '" & Me.drporgtype.SelectedValue & "' and aloc = 1 and loctype = 20 order by loccode "
        Else
            sql = "select loccode, locabb from pshr.mast_loc where locname is not null and " +
                "loccode like '%000000' and (loccode like '6%' or loccode like '7%') and loctype <> 11 and " +
                "aloc = 1 order by loccode "
        End If
        oraCn.FillData(sql, ds)
        drporg.Items.Clear()

        If ds.Tables(0).Rows.Count > 0 Then
            drporg.Items.Add(New ListItem("--Select--", 0))

            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drporg.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
            Next
        End If
        ds.Clear()
        ds.Dispose()
        btnshowAbstract.Enabled = True
    End Sub

    Private Sub GetAttached(ByRef srno As Integer, ByVal loccode As Long)
        Dim sql As String
        Dim row As System.Data.DataRow
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim a As New Integer
        sql = "select rowno,srno from cadre.cadr where loccode=" & loccode & " order by scaleid desc,desgcode"
        oracn.FillData(sql, ds)

        'assign serial no. to posts under this officer
        For Each row In ds.Tables(0).Rows
            ' If IsDBNull(row("srno")) Then
            sql = "update cadre.cadr set srno=" & srno & " where rowno=" & row("rowno")
            oracn.ExecQry(sql)
            srno += 1
            'End If
        Next
    End Sub
    Private Sub GetUnderAEE(ByRef srno As Integer, ByVal loccode As Long)
        Dim sql As String
        Dim row3 As System.Data.DataRow
        Dim oracn As New OraDBconnection
        Dim ds3 As New System.Data.DataSet
        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & loccode & " and aloc=1 and loctype in (50) order by loctype desc"
        oracn.FillData(sql, ds3)
        For Each row3 In ds3.Tables(0).Rows
            GetAttached(srno, row3("loccode"))
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
        GetAttached(srno, loccode)

        'get SE/XEN/AEE/Suboffice(SO) under this chief
        ds1 = New System.Data.DataSet

        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & loccode & " and aloc=1 and loctype in (30,40,50,60,70) order by loctype desc"
        oracn.FillData(sql, ds1)

        For Each row1 In ds1.Tables(0).Rows
            If row1("loctype") = 30 Then    'SE under CE
                GetAttached(srno, row1("loccode"))    'A/W SE

                'get underlying XEN,AEE,SO to this SE
                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (40,50,60,70) order by loctype desc"
                ds2 = New System.Data.DataSet
                oracn.FillData(sql, ds2)

                For Each row2 In ds2.Tables(0).Rows
                    If row2("loctype") = 40 Then     'XEN Under SE Under CE
                        GetAttached(srno, row2("loccode"))    'A/W XEN

                        'get underlying AEE and S/O to this XEN
                        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (50,60,70) order by loctype desc"
                        ds3 = New System.Data.DataSet
                        oracn.FillData(sql, ds3)
                        For Each row3 In ds3.Tables(0).Rows
                            If row3("loctype") = 50 Then        'AEE Under XEN Under SE 
                                GetAttached(srno, row3("loccode"))  'A/W AEE

                                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row3("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                                ds4 = New System.Data.DataSet
                                oracn.FillData(sql, ds4)
                                For Each row4 In ds4.Tables(0).Rows
                                    GetAttached(srno, row4("loccode"))    'SO Under AEE Under XEN Under SE 
                                Next
                            ElseIf row3("loctype") = 60 Or row3("loctype") = 70 Then        'SO Under XEN
                                GetAttached(srno, row3("loccode"))    'SO Under AEE Under SE 
                            End If
                        Next
                    ElseIf row2("loctype") = 50 Then   'AEE under SE
                        GetAttached(srno, row2("loccode"))    'A/W AEE
                        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                        ds3 = New System.Data.DataSet
                        oracn.FillData(sql, ds3)
                        For Each row3 In ds3.Tables(0).Rows
                            GetAttached(srno, row3("loccode"))    'SO Under AEE Under SE Under CE
                        Next
                    ElseIf row2("loctype") = 60 Or row2("loctype") = 70 Then   'SO under SE
                        GetAttached(srno, row2("loccode"))
                    End If
                Next
                ds2.Clear()
                ds2.Dispose()

                'if officer is Sr. XEN
            ElseIf row1("loctype") = 40 Then            'XEN under CE
                ds2 = New System.Data.DataSet
                GetAttached(srno, row1("loccode"))      'A/W XEN
                'get underlying AEE to this XEN
                'GetUnderAEE(srno, row1("loccode"))
                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (50,60,70) order by loctype desc"
                oracn.FillData(sql, ds2)

                For Each row2 In ds2.Tables(0).Rows
                    If row2("loctype") = 50 Then   'AEE under XEN
                        GetAttached(srno, row2("loccode"))    'A/W AEE
                        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                        ds3 = New System.Data.DataSet
                        oracn.FillData(sql, ds3)
                        For Each row3 In ds3.Tables(0).Rows
                            GetAttached(srno, row3("loccode"))    'SO Under AEE Under XEN Under CE
                        Next
                    ElseIf row2("loctype") = 60 Or row2("loctype") = 70 Then   'SO under XEN
                        GetAttached(srno, row2("loccode"))
                    End If
                Next

                'if officer is AEE/AE
            ElseIf row1("loctype") = 50 Then   'AEE under SE
                GetAttached(srno, row1("loccode"))    'A/W AEE
                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                ds2 = New System.Data.DataSet
                oracn.FillData(sql, ds2)
                For Each row2 In ds2.Tables(0).Rows
                    GetAttached(srno, row2("loccode"))    'SO Under AEE Under CE
                Next
            ElseIf row1("loctype") = 60 Or row1("loctype") = 70 Then   'SO under CE
                GetAttached(srno, row1("loccode"))
            End If
        Next
    End Sub
    Protected Sub btnshowAbstract_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnshowAbstract.Click
        Dim lcode As String
        Dim opt As String

        If drporg.SelectedValue = "" Or drporg.SelectedValue = "0" Then
            lcode = Me.drporgtype.SelectedValue
            opt = "D"
        Else
            If Mid$(Me.drporg.SelectedValue, 1, 3) = "800" Then
                lcode = drporg.SelectedValue
            Else
                lcode = Mid$(Me.drporg.SelectedValue, 1, 3)
            End If
            opt = "ALL"
        End If

        btnshowAbstract_report(lcode, opt)
    End Sub

    Protected Sub btnshowContwDesg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnshowContwDesg.Click
        Dim lcode As String
        Dim opt As String
        Dim pdfpath As String

        If drporg.SelectedValue = "" Or drporg.SelectedValue = "0" Then
            lcode = drporgtype.SelectedValue
            opt = "D"
        Else
            lcode = drporg.SelectedValue
            opt = "ALL"
        End If
        pdfpath = Server.MapPath("office_orders\\" & "conteo" & "-" & lcode & ".pdf")

        If (System.IO.File.Exists(pdfpath) = False) Then
            UpdateSrno(lcode, opt)
            pdfpath = MakeContReport(lcode, opt)
        End If
        DownloadFile(pdfpath)
    End Sub
    Private Function MakeContReport(ByVal loc As Long, ByVal opt As String) As String
        Dim rptFile As String = String.Empty
        Dim fileName As String = String.Empty
        Dim sql As String = ""
        Dim ds As New System.Data.DataSet

        rptFile = "Reports\\CrystalReportwD.rpt"

        If opt = "D" Then
            sql = "SELECT c.loccode,  pshr.get_org(c.loccode)   AS zone,  pshr.get_org(c.loccode)   AS org,  " &
                "pshr.get_desg(c.desgcode) AS des, c.desgcode,  cadre.get_scale_txt(c.scaleid) AS scale,  " &
                "c.scaleid,  c.branch, " +
                "decode(c.branch, 99,' ',88,' ',cadre.get_branch_withbraces(c.branch)) as branchtext, " &
                "(SELECT COUNT(*)  FROM cadre.cadr e  WHERE e.ptype  = 'P'  AND e.loccode  = c.loccode  " &
                    "AND e.desgcode = c.desgcode  AND e.branch   =c.branch  ) AS perm, " &
                "(SELECT COUNT(*)  FROM cadre.cadr e  WHERE e.ptype  = 'T'  AND e.loccode  = c.loccode  " &
                    "AND e.desgcode = c.desgcode  AND e.branch   =c.branch  ) AS temp, " &
                "(SELECT COUNT(*)  FROM cadre.cadr e  WHERE e.ptype  = 'X'  AND e.loccode  = c.loccode  " &
                    "AND e.desgcode = c.desgcode  AND e.branch   =c.branch  ) AS ex, " &
                "(SELECT COUNT(*)  FROM cadre.cadr e  WHERE e.ptype  = 'M'  AND e.loccode  = c.loccode  " &
                    "AND e.desgcode = c.desgcode  AND e.branch   =c.branch  ) AS pm " &
                "FROM (select loccode,desgcode,scaleid,branch,min(srno) as srno from  cadre.cadr " &
                "group by loccode,desgcode,scaleid,branch) c where c.loccode in " &
                "(SELECT loccode  FROM mast_loc    START WITH loccode =" & loc &
                " CONNECT BY prior loccode=locrep AND aloc=1 AND (loccode not like '%000000' or " &
                "loccode = " & loc & ")) " &
                "and desgcode in (9047,9048,9365,9050,9366,9052,9056,9057,9060) " &
                "order by c.srno "
        ElseIf opt = "ALL" Then
            sql = "select c.srno,c.loccode,pshr.get_zone(c.loccode) as zone,pshr.get_org(c.loccode) as org," &
                "pshr.get_desg(c.desgcode) as des,c.desgcode,c.scaleid,cadre.get_scale_txt(c.scaleid) as scale, " &
                "c.branch," +
                "decode(c.branch, 99,' ',88,' ',cadre.get_branch_withbraces(c.branch)) as branchtext," +
                " c.remarks, " &
                "(select count(*) from cadre.cadr e where e.ptype = 'P' and e.rowno = c.rowno) as perm, " &
                "(select count(*) from cadre.cadr e where e.ptype = 'T' and e.rowno = c.rowno) as temp, " &
                "(select count(*) from cadre.cadr e where e.ptype = 'X' and e.rowno = c.rowno) as ex, " &
                "(select count(*) from cadre.cadr e where e.ptype = 'M' and e.rowno = c.rowno) as pm  " &
                "from cadre.cadr c, pshr.mast_loc m where c.loccode = m.loccode and c.srno is not null and m.aloc=1 and " &
                "m.loccode in (select loccode from mast_loc start with loccode=" & loc &
                " connect by prior loccode=locrep and aloc=1)  " &
                "and desgcode in (9047,9048,9365,9050,9366,9052,9056,9057,9060) " &
                "order by c.srno "
        End If

        Dim oraCn As New OraDBconnection
        oraCn.FillData(sql, ds)

        Dim ds1 As New System.Data.DataSet
        'Dim sysdate As String

        'get sysdate
        'oraCn.FillData("select to_char(sysdate,'dd-mm-yyyy') from dual", ds1)
        'sysdate = ds1.Tables(0).Rows(0)(0)
        'ds1.Clear()

        Dim pdfPath As String
        pdfPath = Server.MapPath("office_orders\\" & "conteo" & "-" & loc & ".pdf")

        'save office order at server
        Dim CrystalReportSource1 As New CrystalReportSource()
        CrystalReportSource1.Report.FileName = Server.MapPath("Reports\\CrystalReportwD.rpt")
        CrystalReportSource1.ReportDocument.SetDatabaseLogon("pshr", "pshr")
        CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
        CrystalReportSource1.DataBind()
        CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath)
        Return pdfPath
    End Function

    Private Sub btnshowAbstract_report(ByVal loc As Long, ByVal opt As String)
        Dim rptFile As String = String.Empty
        Dim fileName As String = String.Empty
        Dim sql As String
        Dim ds As New System.Data.DataSet
        Dim osd_where As String

        Dim director As Boolean
        If opt = "D" Then
            director = True
        Else
            director = False
        End If

        rptFile = "Reports\\rptcontabs.rpt"

        If loc.ToString().Length = 3 Then
            loc = loc & "000000"
        End If

        If Not director Then
            sql = " select pshr.get_desg(desgcode),pshr.get_org(" & loc & " ) as loc, cadre.get_scale_txt(scaleid) as scale ,cadre.get_branch(branch) as branchtext, sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4) from (" &
                                  "select desgcode, scaleid,branch, cnt as cnt1,0 as cnt2,0 as cnt3,0 as cnt4 from (select desgcode, scaleid,branch, count(*) as cnt,ptype from cadre.cadr where  loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by desgcode,ptype, scaleid,branch) a where ptype = 'P'" &
                       " union all select desgcode, scaleid,branch, 0 as cnt1,cnt as cnt2,0 as cnt3,0 as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where  loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by desgcode,ptype, scaleid, branch) a where ptype = 'T'" &
                       " union all select desgcode, scaleid,branch, 0 as cnt1,0 as cnt2,cnt as cnt3,0 as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where  loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by desgcode,ptype, scaleid,branch) a where ptype = 'X'" &
                       " union all select desgcode, scaleid,branch, 0 as cnt1,0 as cnt2,0 as cnt3,cnt as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by desgcode,ptype, scaleid,branch) a where ptype = 'M') b " &
                       " group by desgcode, scaleid,branch  order by scaleid desc"
        Else
            If loc = 800000000 Then
                osd_where = "800010000,800000100"
            ElseIf loc = 503000000 Then
                osd_where = "503010000"
            ElseIf loc = 801000000 Then
                osd_where = "801010000"
            ElseIf loc = 802000000 Then
                osd_where = "802020000, 802010000"
            ElseIf loc = 803000000 Then
                osd_where = "803010000"
            ElseIf loc = 804000000 Then
                osd_where = "804010000,804010100,804010101,804010102,804010103,507030500,501000102,507030501,507030502,507030503,507030504"
            ElseIf loc = 805000000 Then
                osd_where = "805010000"
            ElseIf loc = 806000000 Then
                osd_where = "806010000, 806020000, 806020001, 806020002, 806020100"
            Else
                osd_where = ""
            End If

            sql = " select pshr.get_desg(desgcode),pshr.get_org(" & loc & " ) as loc, cadre.get_scale_txt(scaleid) as scale ,cadre.get_branch(branch) as branchtext, sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4) from (" &
                          "select desgcode, scaleid,branch,  cnt as cnt1,0 as cnt2,0 as cnt3,0 as cnt4 from (select desgcode, scaleid,branch, count(*) as cnt,ptype from cadre.cadr where loccode = " & loc & " or loccode in (" & osd_where & ") group by desgcode,ptype, scaleid,branch) a where ptype = 'P' " &
               " union all select desgcode, scaleid,branch,  0 as cnt1,cnt as cnt2,0 as cnt3,0 as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where loccode = " & loc & " or loccode in (" & osd_where & ") group by desgcode,ptype, scaleid,branch) a where ptype = 'T' " &
               " union all select desgcode, scaleid,branch,  0 as cnt1,0 as cnt2,cnt as cnt3,0 as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where loccode = " & loc & " or loccode in (" & osd_where & ") group by desgcode,ptype, scaleid,branch) a where ptype = 'X' " &
               " union all select desgcode, scaleid, branch, 0 as cnt1,0 as cnt2,0 as cnt3,cnt as cnt4 from (select desgcode, scaleid, branch,count(*) as cnt,ptype from cadre.cadr where loccode = " & loc & " or loccode in (" & osd_where & ") group by desgcode,ptype, scaleid,branch) a where ptype = 'M')" &
               " b group by desgcode, scaleid,branch  order by scaleid desc"
        End If

        Dim oraCn As New OraDBconnection
        Dim ds1 As New System.Data.DataSet
        Dim sysdate As String

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
    Private Sub PreGenerateReport(ByVal lcode As String, ByVal opt As String)
        UpdateSrno(lcode, opt)
        MakeContReport(lcode, opt)
    End Sub
    Protected Sub btnPreGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreGenerate.Click
        Dim lcode As String
        Dim opt As String

        If drporg.SelectedValue = "" Or drporg.SelectedValue = "0" Then
            lcode = drporgtype.SelectedValue
            opt = "D"
        Else
            lcode = drporg.SelectedValue
            opt = "ALL"
        End If
        PreGenerateReport(lcode, opt)
        Utils.ShowMessage(Me, "Report Pre-Generation Complete")
    End Sub
    Private Sub ReportDoneTableEmpty()
        Dim oraCn As New OraDBconnection
        oraCn.ExecQry("DELETE FROM cadre.cadre_cont_reportstatus")
    End Sub
    Private Sub ReportDoneTableEntry(ByVal loccode As String)
        Dim oraCn As New OraDBconnection
        oraCn.ExecQry("insert into cadre.cadre_cont_reportstatus values(" & loccode & ",sysdate)")
    End Sub
    Private Sub MakeAllContReports()
        Dim orgtypeval As String
        Dim orgval As String
        Dim sql As String
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet

        ReportDoneTableEmpty()

        For Each orgtype As ListItem In drporgtype.Items
            orgtypeval = orgtype.Value
            If (orgtypeval = "0") Then
                Continue For
            End If

            PreGenerateReport(orgtypeval, "D")
            ReportDoneTableEntry(orgtypeval)

            If orgtypeval <> "600000000" Then
                sql = "select loccode, locabb from pshr.mast_loc where locname is not null and locrep = '" & orgtypeval & "' and aloc = 1 and loctype = 20 order by loccode "
            Else
                sql = "select loccode, locabb from pshr.mast_loc where locname is not null and loccode like '%000000' and (loccode like '6%' or loccode like '7%') and loctype <> 11 and aloc = 1 order by loccode "
            End If
            oraCn.FillData(sql, ds)

            For Each drow As System.Data.DataRow In ds.Tables(0).Rows
                orgval = drow("loccode").ToString()
                PreGenerateReport(orgval, "ALL")
                ReportDoneTableEntry(orgval)
            Next
            ds.Clear()
        Next
        Utils.ShowMessage(Me, "All Pre-Generations Complete")
        ds.Dispose()
    End Sub
    Protected Sub btnPreGenerateAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreGenerateAll.Click
        If btnPreGenerateAll.Text = "Regenerate All Detailed Reports" Then
            Timer1.Enabled = True
            newt = New Thread(AddressOf MakeAllContReports)
            newt.Start()
            Session("obj_newt") = newt
            btnPreGenerateAll.Text = "Stop Regeneration"
        Else
            Timer1.Enabled = False
            newt = Session("obj_newt")
            newt.Abort()
            Session("obj_newt") = ""
            btnPreGenerateAll.Text = "Regenerate All Detailed Reports"
        End If
    End Sub
    Protected Sub btnTimer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTimer.Click, Timer1.Tick
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String

        sql = "select nvl(wm_concat(replace(pshr.get_org(loccode),',','+++')),' ') from cadre.cadre_cont_reportstatus"
        oraCn.FillData(sql, ds)
        Label5.Text = ds.Tables(0).Rows(0)(0).ToString().Replace(",", "<br/>").Replace("+++", ",")
        ds.Clear()
        ds.Dispose()
    End Sub
End Class