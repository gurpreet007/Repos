Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web

Partial Class frmrptdesgwise
    Inherits System.Web.UI.Page
    Protected Sub btnshow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnshow.Click
        If drpdesg.SelectedValue = "" Then
            Exit Sub
        End If

        Session("dcode") = Me.drpdesg.SelectedValue
        Session("bcode") = Me.drpbranch.SelectedValue

        btnshowDesg_report()
        'Response.Write("<script type='text/javascript'>window.open('rptdesgwise.aspx','_new');</script>")
    End Sub
    Private Sub btnshowDesg_report()
        Dim arylist As New ArrayList()
        Dim selformula As [String] = String.Empty
        Dim rptFile As String = String.Empty
        Dim fileName As String = String.Empty
        Dim sql As String
        Dim doc As New ReportDocument()
        Dim des, bran As Double
        Dim ds As New System.Data.DataSet
        Dim oraCn As New OraDBconnection

        des = Session("dcode")
        bran = Session("bcode")


        '  rptFile = "CrystalReport3.rpt"
        rptFile = "Reports\\rptdesgorgabs.rpt"
        '        sql = " select pshr.get_desg(desgcode),pshr.get_org(" & loc & " ) as loc, cadre.get_scale_txt(scaleid) as scale , sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4) from (" &
        '            " select desgcode, scaleid,  cnt as cnt1,0 as cnt2,0 as cnt3,0 as cnt4 from (select desgcode, scaleid, count(*) as cnt,ptype from cadre.cadr where   loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep) group by desgcode,ptype, scaleid) a where ptype = 'P'" &
        '" union all select desgcode, scaleid, 0 as cnt1,cnt as cnt2,0 as cnt3,0 as cnt4 from (select desgcode, scaleid, count(*) as cnt,ptype from cadre.cadr where  loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep ) group by desgcode,ptype, scaleid) a where ptype = 'T'" &
        '" union all select desgcode, scaleid, 0 as cnt1,0 as cnt2,cnt as cnt3,0 as cnt4 from (select desgcode, scaleid, count(*) as cnt,ptype from cadre.cadr where  loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep) group by desgcode,ptype, scaleid) a where ptype = 'X'" &
        '" union all select desgcode, scaleid, 0 as cnt1,0 as cnt2,0 as cnt3,cnt as cnt4 from (select desgcode, scaleid, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from mast_loc start with loccode=" & loc & " connect by prior loccode=locrep) group by desgcode,ptype, scaleid) a where ptype = 'M') b group by desgcode, scaleid  order by scaleid desc"



        'sql = "select pshr.get_desg(p.desgcode), pshr.get_zone(p.zonecd * 1000000),p.cnt as perm,t.cnt as temp  from " &
        '" (select substr(loccode,1,3) as zonecd,desgcode,count(*) as cnt from cadre.cadr where ptype = 'P' and desgcode = " & des & " group by substr(loccode,1,3),desgcode,ptype) p " &
        '" full outer join  (select substr(loccode,1,3) as zonecd,desgcode,count(*) as cnt from cadre.cadr where ptype = 'T' and desgcode  = " & des & "  group by substr(loccode,1,3),desgcode,ptype ) t " &
        '"  on p.Zonecd = t.zonecd order by p.zonecd "
        'sql = "select  pshr.get_desg(desgcode), pshr.get_zone(substr(loccode,1,3) * 1000000),substr(loccode,1,3), " &
        '    "sum(Perm),sum(Temp),sum(Ex),sum(PM) from " &
        '    "((select loccode,desgcode,1 as perm,0 as Temp,0 as Ex, 0 as PM from cadre.cadr where ptype = 'P' ) " &
        '    "union all " &
        '    "(select loccode,desgcode, 0 as Perm, 1 as Temp,0 as Ex, 0 as PM from cadre.cadr where ptype = 'T' ) " &
        '    "Union all " &
        '    "(select loccode,desgcode, 0 as Perm,0 as Temp,1 as Ex, 0 as PM from cadre.cadr where ptype = 'X' ) " &
        '    "union all " &
        '    "(select loccode,desgcode, 0 as Perm,0 as Temp,0 as Ex, 1 as PM from cadre.cadr where ptype = 'M' )) " &
        '    "where desgcode = " & des &
        '    " group by substr(loccode,1,3),desgcode order by substr(loccode,1,3) "

        PrepareTable(des)
        ds = New System.Data.DataSet
        sql = "select * from cadre.rptorgabs"
        oraCn.FillData(sql, ds)

        Dim ds1 As New System.Data.DataSet
        Dim sysdate As String

        'get sysdate
        oraCn.FillData("select to_char(sysdate,'dd-mm-yyyy') from dual", ds1)
        sysdate = ds1.Tables(0).Rows(0)(0)
        ds1.Clear()

        Dim pdfPath As String
        pdfPath = Server.MapPath("office_orders\\" & "Desgabs" & "-" & sysdate & ".pdf")
        Session("pdfpath") = pdfPath

        'save office order at server
        Dim CrystalReportSource1 As New CrystalReportSource()
        CrystalReportSource1.Report.FileName = Server.MapPath("Reports\\rptdesgorgabs.rpt")
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

    Private Sub PrepareTable(ByVal desgcode As Integer)
        Dim oraCn As New OraDBconnection
        Dim ds, ds1, ds2, ds3 As New Data.DataSet
        Dim pcount, tcount, xcount, mcount As Integer
        Dim sql, desgtxt, branch As String
        Dim loc As Long
        Dim where_branch As String
        'delete any old values
        sql = "delete from cadre.rptorgabs"
        oraCn.ExecQry(sql)

        'get desgtext from desgcode
        sql = " select pshr.get_desg(" & desgcode & ") from dual"
        oraCn.FillData(sql, ds2)
        desgtxt = ds2.Tables(0).Rows(0)(0)
        ds2.Clear()
        ds2.Dispose()

        'make where clause for branch
        where_branch = ""
        If Session("bcode") = 1 Then
            branch = "Electrical"
            where_branch = " branch not in (4,99) "
        ElseIf Session("bcode") = 4 Then
            branch = "Civil"
            where_branch = " branch = 4 "
        ElseIf Session("bcode") = 99 Then
            branch = "Not Known"
            where_branch = " branch = 99 "
        Else
            branch = "All"
            where_branch = " 1 = 1 "
        End If


        'sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
        '         "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'P'" &
        '         " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'T'" &
        '         " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'X'" &
        '         " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'M')" &
        '         " b group by substr(loccode,1,3) order by substr(loccode,1,3)"
        ''End If

        'sql = "select loccode,(select locrep from pshr.mast_loc where loccode = s.loccode) as locrep, ptype from cadre.cadr s where desgcode = " & desgcode & ""




        'sql = "select pshr.get_org(left(locrep,1,3) || '000000'),ptype,count(*) from (" & sql & ") group by left(locrep,1,3),ptype"


        'get all orgs
        sql = "select distinct (substr(loccode,1,3)) as orgs from cadre.cadr order by orgs"
        oraCn.FillData(sql, ds)

        For i = 0 To ds.Tables(0).Rows.Count - 1
            pcount = 0
            tcount = 0
            xcount = 0
            mcount = 0

            loc = ds.Tables(0).Rows(i)(0) & "000000"

            'If loc = 800000000 Or loc = 503000000 Then
            '    sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
            '             "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and loctype in (11,30)) and desgcode=" & desgcode & " group by loccode) a where ptype = 'P'" &
            '             " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and loctype in (11,30)) and desgcode=" & desgcode & " group by loccode) a where ptype = 'T'" &
            '             " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and loctype in (11,30)) and desgcode=" & desgcode & " group by loccode) a where ptype = 'X'" &
            '             " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and loctype in (11,30)) and desgcode=" & desgcode & " group by loccode) a where ptype = 'M')" &
            '             " b group by substr(loccode,1,3) order by substr(loccode,1,3)"
            'Else
            '    sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
            '             "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep) and desgcode=" & desgcode & "  group by loccode) a where ptype = 'P'" &
            '             " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep) and desgcode=" & desgcode & "  group by loccode) a where ptype = 'T'" &
            '             " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep) and desgcode=" & desgcode & "  group by loccode) a where ptype = 'X'" &
            '             " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep) and desgcode=" & desgcode & "  group by loccode) a where ptype = 'M')" &
            '             " b group by substr(loccode,1,3) order by substr(loccode,1,3)"
            'End If

            'If Mid$(loc, 1, 2) = "80" Then
            '    'sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
            '    '         "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep ) and desgcode=" & desgcode & "and " & where_branch & " group by loccode) a where ptype = 'P'" &
            '    '         " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep ) and desgcode=" & desgcode & "and " & where_branch & " group by loccode) a where ptype = 'T'" &
            '    '         " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep ) and desgcode=" & desgcode & "and " & where_branch & " group by loccode) a where ptype = 'X'" &
            '    '         " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep ) and desgcode=" & desgcode & "and " & where_branch & " group by loccode) a where ptype = 'M')" &
            '    '         " b group by substr(loccode,1,3) order by substr(loccode,1,3)"

            '    sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
            '             "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode = " & loc & " and desgcode=" & desgcode & " and " & where_branch & " group by loccode,ptype) a where ptype = 'P'" &
            '             " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode = " & loc & " and desgcode=" & desgcode & " and " & where_branch & " group by loccode,ptype) a where ptype = 'T'" &
            '             " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode = " & loc & " and desgcode=" & desgcode & " and " & where_branch & " group by loccode,ptype) a where ptype = 'X'" &
            '             " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode = " & loc & " and desgcode=" & desgcode & " and " & where_branch & " group by loccode,ptype) a where ptype = 'M')" &
            '             " b group by substr(loccode,1,3) order by substr(loccode,1,3)"

            'Else

            If loc <> "1020000000" And Not loc.ToString().StartsWith("8") Then


                sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
                         "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc where aloc=1 start with loccode=" & loc & " connect by prior loccode=locrep) and desgcode=" & desgcode & " and " & where_branch & " group by loccode,ptype) a where ptype = 'P'" &
                         " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc where aloc=1 start with loccode=" & loc & " connect by prior loccode=locrep) and desgcode=" & desgcode & " and " & where_branch & " group by loccode,ptype) a where ptype = 'T'" &
                         " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc where aloc=1 start with loccode=" & loc & " connect by prior loccode=locrep) and desgcode=" & desgcode & " and " & where_branch & " group by loccode,ptype) a where ptype = 'X'" &
                         " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc where aloc=1 start with loccode=" & loc & " connect by prior loccode=locrep) and desgcode=" & desgcode & " and " & where_branch & " group by loccode,ptype) a where ptype = 'M')" &
                         " b group by substr(loccode,1,3) order by substr(loccode,1,3)"
            ElseIf loc.ToString().StartsWith("8") Then

                sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
                         "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'P'" &
                         " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'T'" &
                         " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'X'" &
                         " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'M')" &
                         " b group by substr(loccode,1,3) order by substr(loccode,1,3)"
                'End If


            End If

            'sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
            '         "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'P'" &
            '         " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'T'" &
            '         " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'X'" &
            '         " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode like '" & Left(loc, 3) & "%' and desgcode=" & desgcode & " and " & where_branch & " and loccode in (select loccode from pshr.mast_loc where aloc=1) group by loccode,ptype) a where ptype = 'M')" &
            '         " b group by substr(loccode,1,3) order by substr(loccode,1,3)"
            ''End If

            ds1 = New System.Data.DataSet
            oraCn.FillData(sql, ds1)

            For j = 0 To ds1.Tables(0).Rows.Count - 1
                pcount += ds1.Tables(0).Rows(j)(2)
                tcount += ds1.Tables(0).Rows(j)(3)
                xcount += ds1.Tables(0).Rows(j)(4)
                mcount += ds1.Tables(0).Rows(j)(5)
            Next j
            If ds1.Tables(0).Rows.Count > 0 Then
                sql = "Insert into cadre.rptorgabs values ('" & ds1.Tables(0).Rows(0)(0) & "'," & ds1.Tables(0).Rows(0)(1) & "," & pcount & "," & tcount & "," & xcount & "," & mcount & ",'" & desgtxt & "','" & branch & "',0)"
                oraCn.ExecQry(sql)
            End If
            ds1.Clear()
            ds1.Dispose()
        Next i
        ds.Clear()
        ds.Dispose()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Utils.AuthenticateUser(Me)
        If Not Page.IsPostBack Then
            filldesg()
        End If
    End Sub
    Protected Sub txtsdesg_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsdesg.TextChanged
        filldesg()
    End Sub
    Private Sub filldesg()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        sql = "select desgcode, desgabb from pshr.mast_desg where desgtext is not null and (desgabb like '%" _
                & UCase(txtsdesg.Text.Replace(" ", "%")) & "%' )order by desgabb "
        oraCn.FillData(sql, ds)
        drpdesg.Items.Clear()
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Me.drpdesg.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
        Next
        ds.Clear()
        ds.Dispose()
    End Sub
   
    Protected Sub downDesgParams_Click(sender As Object, e As EventArgs)
        Dim sql As String
        Dim ds As New System.Data.DataSet
        Dim oraCn As New OraDBconnection
        Dim dg As New DataGrid
        Dim stringWriter As New System.IO.StringWriter
        Dim htmlWriter As New System.Web.UI.HtmlTextWriter(stringWriter)

        sql = "select desgtext, desgabb,desgcode, hecode, minage, retdage, servicedetail,gaztext,cls,cadrename, empstattext " &
        "from pshr.mast_desg " &
        "left outer join pshr.mast_service on pshr.mast_desg.servcode = pshr.mast_service.servicecode " &
        "left outer join pshr.mast_gaz on pshr.mast_desg.gazcode =pshr.mast_gaz.gazcode " &
        "left outer join pshr.mast_cadre on pshr.mast_cadre.cadrecode=pshr.mast_desg.cadrecode " &
        "left outer join pshr.mast_empstatus on pshr.mast_desg.empstatus = pshr.mast_empstatus.empstatcode " &
        "where adesg='A' order by hecode, desgtext"

        oraCn.FillData(sql, ds)
        dg.DataSource = ds
        dg.DataBind()

        Response.AddHeader("content-disposition", "attachment;filename=desg_params.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.xls"

        dg.RenderControl(htmlWriter)
        Response.Write(stringWriter.ToString())
        Response.End()
        dg.Dispose()
    End Sub
End Class