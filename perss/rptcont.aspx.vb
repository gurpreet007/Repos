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
    'Private Sub GetUnderAEE(ByRef srno As Integer, ByVal loccode As Long)
    '    Dim sql As String
    '    Dim row3 As System.Data.DataRow
    '    Dim oracn As New OraDBconnection
    '    Dim ds3 As New System.Data.DataSet
    '    sql = "select loccode,loctype from pshr.mast_loc where locrep=" & loccode & " and aloc=1 and loctype in (50) order by loctype desc"
    '    oracn.FillData(sql, ds3)
    '    For Each row3 In ds3.Tables(0).Rows
    '        GetAttached(srno, row3("loccode"))
    '    Next
    'End Sub
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
    'Private Sub UpdateSrno_bu(ByVal loccode As String, ByVal opt As String)
    '    Dim sql As String
    '    Dim row1, row2 As System.Data.DataRow
    '    Dim oracn As New OraDBconnection
    '    Dim ds1, ds2, ds3, ds4 As System.Data.DataSet
    '    Dim srno As Integer

    '    'reset srno's
    '    sql = "update cadre.cadr set srno=NULL"
    '    oracn.ExecQry(sql)

    '    'initialize serialno
    '    srno = 1

    '    'now we have loccode of chief, get posts under it
    '    GetAttached(srno, loccode)

    '    'get SE/XEN/AEE/Suboffice(SO) under this chief
    '    ds1 = New System.Data.DataSet

    '    sql = "select loccode,loctype from pshr.mast_loc where locrep=" & loccode & " and aloc=1 and loctype in (30,40,50,60,70) order by loctype desc"
    '    oracn.FillData(sql, ds1)

    '    For Each row1 In ds1.Tables(0).Rows
    '        If row1("loctype") = 30 Then    'SE under CE
    '            GetAttached(srno, row1("loccode"))    'A/W SE

    '            'get underlying XEN,AEE,SO to this SE
    '            sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (40,50,60,70) order by loctype desc"
    '            ds2 = New System.Data.DataSet
    '            oracn.FillData(sql, ds2)

    '            For Each row2 In ds2.Tables(0).Rows
    '                If row2("loctype") = 40 Then     'XEN Under SE Under CE
    '                    GetAttached(srno, row2("loccode"))    'A/W XEN

    '                    'get underlying AEE and S/O to this XEN
    '                    sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (50,60,70) order by loctype desc"
    '                    ds3 = New System.Data.DataSet
    '                    oracn.FillData(sql, ds3)
    '                    For Each row3 In ds3.Tables(0).Rows
    '                        If row3("loctype") = 50 Then        'AEE Under XEN Under SE 
    '                            GetAttached(srno, row3("loccode"))  'A/W AEE

    '                            sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row3("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
    '                            ds4 = New System.Data.DataSet
    '                            oracn.FillData(sql, ds4)
    '                            For Each row4 In ds4.Tables(0).Rows
    '                                GetAttached(srno, row4("loccode"))    'SO Under AEE Under XEN Under SE 
    '                            Next
    '                        ElseIf row3("loctype") = 60 Or row3("loctype") = 70 Then        'SO Under XEN
    '                            GetAttached(srno, row3("loccode"))    'SO Under AEE Under SE 
    '                        End If
    '                    Next
    '                ElseIf row2("loctype") = 50 Then   'AEE under SE
    '                    GetAttached(srno, row2("loccode"))    'A/W AEE
    '                    sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
    '                    ds3 = New System.Data.DataSet
    '                    oracn.FillData(sql, ds3)
    '                    For Each row3 In ds3.Tables(0).Rows
    '                        GetAttached(srno, row3("loccode"))    'SO Under AEE Under SE Under CE
    '                    Next
    '                ElseIf row2("loctype") = 60 Or row2("loctype") = 70 Then   'SO under SE
    '                    GetAttached(srno, row2("loccode"))
    '                End If
    '            Next
    '            ds2.Clear()
    '            ds2.Dispose()

    '            'if officer is Sr. XEN
    '        ElseIf row1("loctype") = 40 Then            'XEN under CE
    '            ds2 = New System.Data.DataSet
    '            GetAttached(srno, row1("loccode"))      'A/W XEN
    '            'get underlying AEE to this XEN
    '            'GetUnderAEE(srno, row1("loccode"))
    '            sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (50,60,70) order by loctype desc"
    '            oracn.FillData(sql, ds2)

    '            For Each row2 In ds2.Tables(0).Rows
    '                If row2("loctype") = 50 Then   'AEE under XEN
    '                    GetAttached(srno, row2("loccode"))    'A/W AEE
    '                    sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
    '                    ds3 = New System.Data.DataSet
    '                    oracn.FillData(sql, ds3)
    '                    For Each row3 In ds3.Tables(0).Rows
    '                        GetAttached(srno, row3("loccode"))    'SO Under AEE Under XEN Under CE
    '                    Next
    '                ElseIf row2("loctype") = 60 Or row2("loctype") = 70 Then   'SO under XEN
    '                    GetAttached(srno, row2("loccode"))
    '                End If
    '            Next

    '            'if officer is AEE/AE
    '        ElseIf row1("loctype") = 50 Then   'AEE under CE
    '            GetAttached(srno, row1("loccode"))    'A/W AEE
    '            sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
    '            ds2 = New System.Data.DataSet
    '            oracn.FillData(sql, ds2)
    '            For Each row2 In ds2.Tables(0).Rows
    '                GetAttached(srno, row2("loccode"))    'SO Under AEE Under CE
    '            Next
    '        ElseIf row1("loctype") = 60 Or row1("loctype") = 70 Then   'SO under CE
    '            GetAttached(srno, row1("loccode"))
    '        End If
    '    Next
    'End Sub
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
        pdfpath = Server.MapPath("office_orders\\" & "cont" & "-" & lcode & ".pdf")

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
                "c.scaleid,  c.branch, c.hia, c.remarks, " +
                "decode(c.branch, 99,' ',88,' ',cadre.get_branch_withbraces(c.branch)) as branchtext, " &
                "(SELECT COUNT(*)  FROM cadre.cadr e  WHERE e.ptype  = 'P'  AND e.loccode  = c.loccode  " &
                    "AND e.desgcode = c.desgcode  AND e.branch   =c.branch  ) AS perm, " &
                "(SELECT COUNT(*)  FROM cadre.cadr e  WHERE e.ptype  = 'T'  AND e.loccode  = c.loccode  " &
                    "AND e.desgcode = c.desgcode  AND e.branch   =c.branch  ) AS temp, " &
                "(SELECT COUNT(*)  FROM cadre.cadr e  WHERE e.ptype  = 'X'  AND e.loccode  = c.loccode  " &
                    "AND e.desgcode = c.desgcode  AND e.branch   =c.branch  ) AS ex, " &
                "(SELECT COUNT(*)  FROM cadre.cadr e  WHERE e.ptype  = 'M'  AND e.loccode  = c.loccode  " &
                    "AND e.desgcode = c.desgcode  AND e.branch   =c.branch  ) AS pm " &
                "FROM (select loccode,desgcode,scaleid,branch,hia,remarks,min(srno) as srno from  cadre.cadr " &
                "group by loccode,desgcode,scaleid,branch,remarks,hia) c where c.loccode in " &
                "(SELECT loccode  FROM pshr.mast_loc    START WITH loccode =" & loc &
                " CONNECT BY prior loccode=locrep AND aloc=1 AND (loccode not like '%000000' or " &
                "loccode = " & loc & ")) order by c.srno "
        ElseIf opt = "ALL" Then
            sql = "select c.srno,c.loccode,pshr.get_zone(c.loccode) as zone,pshr.get_org(c.loccode) as org," &
                "pshr.get_desg(c.desgcode) as des,c.desgcode,c.scaleid,cadre.get_scale_txt(c.scaleid) as scale, " &
                "c.branch,c.hia, " +
                "decode(c.branch, 99,' ',88,' ',cadre.get_branch_withbraces(c.branch)) as branchtext," +
                " c.remarks, " &
                "(select count(*) from cadre.cadr e where e.ptype = 'P' and e.rowno = c.rowno) as perm, " &
                "(select count(*) from cadre.cadr e where e.ptype = 'T' and e.rowno = c.rowno) as temp, " &
                "(select count(*) from cadre.cadr e where e.ptype = 'X' and e.rowno = c.rowno) as ex, " &
                "(select count(*) from cadre.cadr e where e.ptype = 'M' and e.rowno = c.rowno) as pm  " &
                "from cadre.cadr c, pshr.mast_loc m where c.loccode = m.loccode and c.srno is not null and m.aloc=1 and " &
                "m.loccode in (select loccode from pshr.mast_loc start with loccode=" & loc &
                " connect by prior loccode=locrep and aloc=1)  order by c.srno "
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
        pdfPath = Server.MapPath("office_orders\\" & "cont" & "-" & loc & ".pdf")

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
                osd_where = "410010000,410020000,410020100,410020101,410020102," &
                            "410020103,410020104,410020105,410020106,410020107," &
                            "410020109,410020200,410020201,410020300,410020301," &
                            "410020302,410020400,410020401,410020402,410020403," &
                            "410020404,410020500,410020501,410020502,410020503," &
                            "410020504,410020505,410030000,410030001,410030100," &
                            "410030200,410030300,410030600,411010000,410000004," &
                            "410000400,410000500,410000600,410000601,801010000"
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
        oraCn.ExecQry("insert into cadre.cadre_cont_reportstatus values(" & loccode & ")")
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
        If btnPreGenerateAll.Text = "Pre-Generate All Detailed Reports" Then
            Timer1.Enabled = True
            newt = New Thread(AddressOf MakeAllContReports)
            newt.Start()
            Session("obj_newt") = newt
            btnPreGenerateAll.Text = "Stop Pre-Generation"
        Else
            Timer1.Enabled = False
            newt = Session("obj_newt")
            newt.Abort()
            Session("obj_newt") = ""
            btnPreGenerateAll.Text = "Pre-Generate All Detailed Reports"
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

    Protected Sub btnMatrix_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMatrix.Click
        Dim desgSQL As String
        Dim orgSQL As String
        Dim desgDS As New System.Data.DataSet
        Dim oraCn As New OraDBconnection
        Dim desgabb As String
        Dim desgCode As String
        Dim scale As String
        Dim scaleID As String
        Dim serialNo As Integer = 0

        'desgSQL = "select desgcode,desgabb from pshr.mast_desg where desgcode in " &
        '    "(select distinct desgcode from cadre.cadr) order by hecode asc"

        desgSQL = "select c.desgcode, md.desgabb as desgabb,ms.newscale||'--'||ms.gradepay as scale, c.scaleid as sid from " &
                    "cadre.cadr c inner join pshr.mast_desg md on c.desgcode = md.desgcode " &
                    "inner join cadre.mast_scale ms on c.scaleid = ms.scaleid " &
                    "group by c.scaleid, c.desgcode, md.desgabb, ms.newscale, ms.gradepay, md.hecode order by md.hecode, c.scaleid desc"

        oraCn.FillData(desgSQL, desgDS)

        'delete any existing rows in cadre.cadre_matrix
        oraCn.ExecQry("delete from cadre.cadre_matrix")

        serialNo = 0

        For Each dRow As System.Data.DataRow In desgDS.Tables(0).Rows
            'desgAbb = dRow("desgabb")
            desgAbb = dRow("desgabb")
            desgCode = dRow("desgcode")
            scale = dRow("scale")
            scaleID = dRow("sid")
            serialNo += 1
            orgSQL = String.Format("insert into CADRE.cadre_matrix select '{2}' as sno, '{0}' as desg, '{4}' as scale, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep  and loccode not like '%000000' start with loccode = 800000000)) as CMD, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep  and loccode not like '%000000' start with loccode = 803000000)) as DIR_DIST, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep  and loccode not like '%000000' start with loccode = 802000000)) as DIR_GEN, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep  and loccode not like '%000000' start with loccode = 805000000)) as DIR_COMM, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep  and loccode not like '%000000' start with loccode = 804000000)) as DIR_FIN, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep  and loccode not like '%000000' start with loccode = 801000000)) as DIR_ADMN, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep  and loccode not like '%000000' start with loccode = 806000000)) as DIR_HR, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 101000000)) as CE_HYD, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 106000000)) as THERMAL, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 105000000)) as GNDTP, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 104000000)) as GHTP_LM, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 103000000)) as GGSSTP_RPR, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 305000000)) as BORDER, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 301000000)) as SOUTH, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 302000000)) as WEST, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 304000000)) as NORTH, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 303000000)) as CENTRAL, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 401000000)) as REAPDRP, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 112000000)) as FUEL, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 507000000)) as HRD, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 506000000)) as PLANNING, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 505000000)) as COMMERCIAL," &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 533000000)) as CAO_HQ, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 514000000)) as CAO_REV, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 540000000)) as CFO, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 538000000)) as CFINOFF," &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 535000000)) as COST_CONT, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 536000000)) as FA, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 513000000)) as CA, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 407000000)) as TAI, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 508000000)) as CDNC, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 509000000)) as ARR_TR, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 306000000)) as PPR, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 113000000)) as CARCHITECT, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 308000000)) as EANF, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 108000000)) as TS, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 309000000)) as IT, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 310000000)) as STOREWS, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 404000000)) as METERING, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 409000000)) as GRIEV, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 411000000)) as GM_ADMIN, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 405000000)) as MM, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 504000000)) as DGPVS, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 807000000)) as SEC_PSPCL," &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 810000000)) as OMBUDSMAN, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 601000000)) as BBMB, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 608000000)) as PSTCL, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 111000000)) as OSD, " &
                                    "(select count(*) from cadre.cadr where desgcode='{1}' and scaleid='{3}' and loccode in (select loccode from pshr.mast_loc connect by prior loccode=locrep start with loccode = 114000000)) as PLANNING_IRRI_CHD " &
                                    "from dual", desgabb, desgCode, serialNo, scaleID, scale)

            oraCn.ExecQry(orgSQL)
        Next
        ExportMatrix()
    End Sub
    Private Sub ExportMatrix()
        Dim sql As String
        Dim ds As New System.Data.DataSet
        Dim oraCn As New OraDBconnection
        Dim dg As New DataGrid
        Dim stringWriter As New System.IO.StringWriter
        Dim htmlWriter As New System.Web.UI.HtmlTextWriter(stringWriter)

        sql = "select * from cadre.cadre_matrix order by sno"
        
        oraCn.FillData(sql, ds)
        dg.DataSource = ds
        dg.DataBind()

        Response.AddHeader("content-disposition", "attachment;filename=cadre_matrix.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.xls"

        dg.RenderControl(htmlWriter)
        Response.Write(stringWriter.ToString())
        Response.End()
        dg.Dispose()
    End Sub
End Class