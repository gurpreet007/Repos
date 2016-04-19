Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports System.Data.Odbc

Partial Class frm_new_post
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Utils.AuthenticateUser(Me)

        'Disable 'Save and Generate Office Order' button if role is 'Creator' i.e. 'C'
        If Session("role") = "C" Then
            btnSave.Enabled = False
        End If

        If Not Page.IsPostBack Then
            Dim ds As New System.Data.DataSet
            Dim oracn As New OraDBconnection

            oracn.ExecQry("DELETE FROM cadre.rptcadr")
            filldesg()
            fillscale()
            get_ce()

            oracn.FillData("select * from cadre.cadr where tmp='T'", ds)
            If (ds.Tables(0).Rows.Count > 0) Then
                lblPending.Text = "There is an unsaved session. Either Save or Delete it before continuing"
            Else
                lblPending.Text = ""
            End If

        End If

        If drpdesg.SelectedValue <> "" Then
            Label8.Text = Utils.GetDesgCount(GetLocCode(), drpdesg.SelectedItem.Value)
        End If
    End Sub
    Private Sub fillorg()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim rloc As Double
        rloc = get_rloc()
        If rloc <> "600000000" Then
            sql = "select loccode, locabb from pshr.mast_loc where locname is not null and loccode like '%000000' and locrep = " & rloc & " and loctype <> 11 and aloc = 1 order by loccode "
        Else
            sql = "select loccode, locabb from pshr.mast_loc where locname is not null and loccode like '%000000' and (loccode like '6%' or loccode like '7%') and loctype <> 11 and aloc = 1 order by loccode "
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
    End Sub
    Private Sub fillcir()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim rloc As Double
        'If drporg.SelectedValue = "0" Then
        'Exit Sub
        'End If
        rloc = get_rloc()
        'sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(rloc, 1, 3) & "%000' and loctype = 30 and loccode not like '%000000' and aloc=1 order by loccode "
        sql = "select loccode, locabb from pshr.mast_loc where locrep = " & rloc & " and loctype = 30 and loccode not like '%000000' and aloc=1 order by loccode "
        oraCn.FillData(sql, ds)
        drpcir.Items.Clear()
        If ds.Tables(0).Rows.Count > 0 Then
            drpcir.Items.Add(New ListItem("--Select--", 0))
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drpcir.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
            Next
        End If
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub filldiv()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim rloc As Double
        rloc = get_rloc()

        'If drpcir.SelectedValue = "0" Or drpcir.SelectedValue = "" Then
        '    If drporg.SelectedValue = "0" Or drporg.SelectedValue = "" Then
        '        'do nothing if no circle and no org is filled
        '        Exit Sub
        '    Else
        '        'if circle is not filled but org is filled then get locrep from org
        '        sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(rloc, 1, 3) & "%' and loctype = 40 and locrep = " & Me.drporg.SelectedValue & " and aloc=1 order by loccode "
        '    End If
        'Else
        '    'if circle is filled then get locrep from circle
        '    sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(rloc, 1, 5) & "%' and loctype = 40 and aloc=1 order by loccode "
        'End If

        sql = "select loccode, locabb from pshr.mast_loc where locrep = " & rloc & " and loctype = 40 and aloc=1 order by loccode "
        oraCn.FillData(sql, ds)
        drpdiv.Items.Clear()
        If ds.Tables(0).Rows.Count > 0 Then
            drpdiv.Items.Add(New ListItem("--Select--", 0))
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drpdiv.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
            Next
        End If
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub fillsubdiv()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim rloc As Double
        rloc = get_rloc()
        'If drpdiv.SelectedValue = "" Then
        '    Exit Sub
        'End If

        'If drpdiv.SelectedValue = "0" Or drpdiv.SelectedValue = "" Then
        '    If drpcir.SelectedValue = "0" Or drpcir.SelectedValue = "" Then
        '        If drporg.SelectedValue = "0" Or drporg.SelectedValue = "" Then
        '            'if none of div,circle and org is filled then do nothin g
        '            Exit Sub
        '        Else
        '            'if div and cir are not filled but org is filled then get locrep from org
        '            sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(rloc, 1, 8) & "%' and loctype = 50  and aloc=1 order by loccode "
        '        End If
        '    Else
        '        'if div is not filled but circle is filled then get locrep from circle
        '        sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(rloc, 1, 8) & "%' and loctype = 50 and aloc=1 order by loccode "
        '    End If
        'Else
        '    'if div is filled then then locrep from div
        '    sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(rloc, 1, 8) & "%' and loctype = 50 and aloc=1 order by loccode "
        'End If
        sql = "select loccode, locabb from pshr.mast_loc where locrep = " & rloc & " and loctype = 50 and aloc=1 order by loccode "

        oraCn.FillData(sql, ds)
        drpsubdiv.Items.Clear()
        If ds.Tables(0).Rows.Count > 0 Then
            drpsubdiv.Items.Add(New ListItem("--Select--", 0))

            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drpsubdiv.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
            Next
        End If
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub fillsuboff()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim rloc As Double
        rloc = get_rloc()
        'If drpdiv.SelectedValue = "" Then
        '    Exit Sub
        'End If

        'If drpdiv.SelectedValue = "0" Or drpdiv.SelectedValue = "" Then
        '    If drpcir.SelectedValue = "0" Or drpcir.SelectedValue = "" Then
        '        If drporg.SelectedValue = "0" Or drporg.SelectedValue = "" Then
        '            'if none of div,circle and org is filled then do nothin g
        '            Exit Sub
        '        Else
        '            'if div and cir are not filled but org is filled then get locrep from org
        '            sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(rloc, 1, 8) & "%' and loctype = 50  and aloc=1 order by loccode "
        '        End If
        '    Else
        '        'if div is not filled but circle is filled then get locrep from circle
        '        sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(rloc, 1, 8) & "%' and loctype = 50 and aloc=1 order by loccode "
        '    End If
        'Else
        '    'if div is filled then then locrep from div
        '    sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(rloc, 1, 8) & "%' and loctype = 50 and aloc=1 order by loccode "
        'End If
        sql = "select loccode, locabb from pshr.mast_loc where locrep = " & rloc & " and loctype in (60,70) and aloc=1 order by loccode "

        oraCn.FillData(sql, ds)
        drpsuboff.Items.Clear()
        If ds.Tables(0).Rows.Count > 0 Then
            drpsuboff.Items.Add(New ListItem("--Select--", 0))
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drpsuboff.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
            Next
        End If
        ds.Clear()
        ds.Dispose()
    End Sub

    Private Sub filldesg()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        sql = "select desgcode, desgabb from pshr.mast_desg where desgtext is not null and (desgabb like '%" _
                & UCase(txtsdesg.Text.Replace(" ", "%")) & "%' ) and adesg = 'A' order by desgabb "
        oraCn.FillData(sql, ds)
        drpdesg.Items.Clear()
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Me.drpdesg.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
        Next
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub fillscale()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        sql = "select distinct tab1 ,'Scale-' || NewScale || '- Grade Pay - ' || gradepay,  scaleid  from cadre.mast_scale where scale is not null order by scaleid"
        oraCn.FillData(sql, ds)
        drpscale.Items.Clear()
        '  drpscale.Items.Add(New ListItem(" ", 98))
        drpscale.Items.Add(New ListItem("Pb. Govt. Scale ", 0))
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Me.drpscale.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(2).ToString()))
        Next
        ds.Clear()
        ds.Dispose()
    End Sub
    Protected Sub txtsdesg_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsdesg.TextChanged
        filldesg()
    End Sub
    Private Function GetLikeStr(ByVal lc As Double) As String
        Dim strlc As String
        Dim ce_code, se_code, xen_code, ae_code, likestr As String

        strlc = lc.ToString()
        ce_code = Left(strlc, 3)
        se_code = Mid(strlc, 4, 3)
        xen_code = Mid(strlc, 7, 2)
        ae_code = Right(strlc, 1)

        likestr = ce_code
        If se_code <> "000" Then
            likestr &= se_code
            If xen_code <> "00" Then
                likestr &= xen_code
                If ae_code <> "0" Then
                    likestr &= ae_code
                End If
            End If
        End If
        likestr &= "%"
        Return likestr
    End Function
    Private Sub fillgrid()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim like_str As String
        Dim loccode As String

        loccode = GetLocCode()
        sql = "select pshr.get_org(loccode) as Location, pshr.get_desg(desgcode) || " +
            "decode(branch,99,'-',88,'-','('|| CADRE.GET_branch(BRANCH) || ')-') || indx   as Designation ,cadre.get_scale(scaleid) as Scale," +
            "ptype, rowno, cadre.get_hecode(desgcode) as hcode,decode(hia,1,'YES','') as HIA, remarks from cadre.cadr"
        If RadioButtonList1.SelectedValue = 0 Then
            sql &= " where loccode = '" & loccode
        Else
            like_str = GetLikeStr(loccode)
            sql &= " where loccode like '" & like_str
        End If
        sql &= "' order by loccode,hcode,indx"

        lblloccode.Text = loccode
        oraCn.FillData(sql, ds)
        oraCn.fillgrid(gv, ds)
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Function GetLocCode() As Long
        Dim loccode As Long
        If drpsuboff.SelectedValue <> "" And drpsuboff.SelectedValue <> "0" Then
            loccode = drpsuboff.SelectedItem.Value
        ElseIf drpsubdiv.SelectedValue <> "" And drpsubdiv.SelectedValue <> "0" Then
            loccode = drpsubdiv.SelectedItem.Value
        ElseIf drpdiv.SelectedValue <> "" And drpdiv.SelectedValue <> "0" Then
            loccode = drpdiv.SelectedItem.Value
        ElseIf drpcir.SelectedValue <> "" And drpcir.SelectedValue <> "0" Then
            loccode = drpcir.SelectedItem.Value
        ElseIf drporg.SelectedValue <> "" And drporg.SelectedValue <> "0" Then
            loccode = drporg.SelectedItem.Value
        ElseIf drporgtype.SelectedValue <> "" And drporgtype.SelectedValue <> "0" Then
            loccode = drporgtype.SelectedValue
        End If
            Return loccode
    End Function
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Dim loccode As Double
        Dim oraCn As New OraDBconnection
        Dim sql As String
        Dim desgcode As Integer
        Dim index As Integer
        Dim rno As Integer
        Dim scaleid As Integer

        loccode = GetLocCode()
        rno = Utils.GetRowNum()
        desgcode = drpdesg.SelectedItem.Value
        scaleid = drpscale.SelectedItem.Value
        index = Label8.Text + 1

        'add a row and mark it as temporary
        sql = "insert into cadre.cadr  values(" & loccode & "," & desgcode & "," & index &
            "," & scaleid & ", " & rno & "," & Me.drpbranch.SelectedValue & ",'" &
            Me.drptperm.SelectedValue & "','T',NULL,'" & txtRem.Text & "',0)"
        oraCn.ExecQry(sql)
        fillgrid()
        Label8.Text = Utils.GetDesgCount(GetLocCode(), drpdesg.SelectedItem.Value)
        Utils.ShowMessage(Me, "Post created temporarily. Click 'Save and Generate O/O' to make it permanent")
    End Sub
    Protected Sub gv_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gv.PageIndexChanging
        gv.PageIndex = e.NewPageIndex
        fillgrid()
    End Sub
    Protected Sub gv_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gv.RowDeleting
        Dim s As String
        Dim oraCn As New OraDBconnection
        Dim sql As String
        s = gv.Rows(e.RowIndex).Cells(5).Text
        'only delete temporary rows
        'sql = "delete from cadre.cadr where tmp='T' and rowno = " & s
        sql = "delete from cadre.cadr where rowno = " & s
        oraCn.ExecQry(sql)
        'sql = "update cadre.cadr set rowno = rowno - 1 where rowno > " & s & ""
        'oraCn.ExecQry(sql)
        fillgrid()
    End Sub
    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        fillgrid()
    End Sub
    Protected Sub drporgtype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drporgtype.SelectedIndexChanged
        drporg.Items.Clear()
        drpcir.Items.Clear()
        drpdiv.Items.Clear()
        drpsubdiv.Items.Clear()
        If drporgtype.SelectedValue <> "0" Then
            fillorg()
            fillcir()
            filldiv()
            fillsubdiv()
            fillsuboff()
            fillgrid()
        End If
    End Sub
    Protected Sub drporg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drporg.SelectedIndexChanged
        drpcir.Items.Clear()
        drpdiv.Items.Clear()
        drpsubdiv.Items.Clear()
        fillcir()
        filldiv()
        fillsubdiv()
        fillsuboff()
        fillgrid()
    End Sub
    Protected Sub drpcir_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcir.SelectedIndexChanged
        drpdiv.Items.Clear()
        drpsubdiv.Items.Clear()
        filldiv()
        fillsubdiv()
        fillsuboff()
        fillgrid()
    End Sub
    Protected Sub drpdiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdiv.SelectedIndexChanged
        drpsubdiv.Items.Clear()
        fillsubdiv()
        fillsuboff()
        fillgrid()
    End Sub
    Protected Sub drpsubdiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpsubdiv.SelectedIndexChanged
        fillsuboff()
        fillgrid()
    End Sub
    Protected Sub drpdesg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdesg.SelectedIndexChanged
        fillgrid()
        txtRem.Text = ""
    End Sub
    Private Sub get_ce()
        Me.drporgtype.Items.Clear()
        If Me.RadioButtonList2.SelectedValue = "P" Then
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
        ElseIf Me.RadioButtonList2.SelectedValue = "T" Then
            Me.drporgtype.Items.Add(New ListItem("--Select--", 0))
            Me.drporgtype.Items.Add(New ListItem("CMD/PSTCL", "503000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Technical", "503000001"))
            Me.drporgtype.Items.Add(New ListItem("Director/Finance & Commercial", "503000002"))
            Me.drporgtype.Items.Add(New ListItem("Director/Admin", "503000004"))

        End If
    End Sub
    Protected Sub RadioButtonList2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList2.SelectedIndexChanged
        get_ce()
        Me.drporg.Items.Clear()
        Me.drpcir.Items.Clear()
        Me.drpdiv.Items.Clear()
        Me.drpsubdiv.Items.Clear()
    End Sub
    Private Function get_rloc() As Double
        Dim rloc As Double
        'rloc = Me.drporgtype.SelectedValue

        'If Me.drporgtype.SelectedValue <> "" And Me.drporgtype.SelectedValue <> "0" And drporg.Items.Count > 0 Then
        '    rloc = Me.drporg.SelectedValue
        'End If

        'If Me.drporg.SelectedValue <> "" And Me.drporg.SelectedValue <> "0" And drpcir.Items.Count > 0 Then
        '    rloc = Me.drpcir.SelectedValue
        'End If
        'If Me.drpcir.SelectedValue <> "" And Me.drpcir.SelectedValue <> "0" And drpdiv.Items.Count > 0 Then
        '    rloc = Me.drpdiv.SelectedValue
        'End If
        'If Me.drpdiv.SelectedValue <> "" And Me.drpdiv.SelectedValue <> "0" And drpsubdiv.Items.Count > 0 Then
        '    rloc = Me.drpsubdiv.SelectedValue
        'End If

        If drporgtype.SelectedValue <> "" And drporgtype.SelectedValue <> "0" Then
            rloc = Me.drporgtype.SelectedValue
        End If
        If drporg.SelectedValue <> "" And drporg.SelectedValue <> "0" Then
            rloc = Me.drporg.SelectedValue
        End If
        If drpcir.SelectedValue <> "" And drpcir.SelectedValue <> "0" Then
            rloc = Me.drpcir.SelectedValue
        End If
        If drpdiv.SelectedValue <> "" And drpdiv.SelectedValue <> "0" Then
            rloc = Me.drpdiv.SelectedValue
        End If
        If drpsubdiv.SelectedValue <> "" And drpsubdiv.SelectedValue <> "0" Then
            rloc = Me.drpsubdiv.SelectedValue
        End If

        Return rloc
    End Function
    Private Function GenerateReport(ByVal oonum As Integer) As String
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sysdate As String

        'get sysdate
        oracn.FillData("select to_char(sysdate,'dd-mm-yyyy') from dual", ds)
        sysdate = ds.Tables(0).Rows(0)(0)
        ds.Clear()

        Dim pdfPath As String
        pdfPath = Server.MapPath("office_orders\\" & oonum & "-" & sysdate & ".pdf")
        Session("pdfpath") = pdfPath

        oracn.ExecQry("delete from cadre.rptcadr")
        oracn.ExecQry("insert into cadre.rptcadr select c.*," & oonum & ",sysdate from cadre.cadr c where tmp='T'")
        sql = "select pshr.get_org(loccode), pshr.get_desg(desgcode),cadre.get_scale_txt(scaleid),cadre.get_branch(branch),ptype, " &
                "remarks, decode(oonum,0,NULL,oonum), decode(oonum,0,NULL,odate) ,count(*) from cadre.rptcadr" &
                " where tmp = 'T' group by loccode, desgcode,scaleid,branch,ptype, oonum,odate,remarks "
        oracn.FillData(sql, ds)

        'save office order at server
        Dim CrystalReportSource1 As New CrystalReportSource()
        CrystalReportSource1.Report.FileName = Server.MapPath("Reports\\rpt_new_post.rpt")
        CrystalReportSource1.ReportDocument.SetDatabaseLogon("pshr", "pshr")
        CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
        Dim ds2 As New System.Data.DataSet
        sql = "select * from cadre.cclist where ccnum > 0 order by ccnum"
        oracn.FillData(sql, ds2)
        CrystalReportSource1.ReportDocument.Subreports(0).SetDataSource(ds2.Tables(0))
        CrystalReportSource1.DataBind()
        CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath)

        Return pdfPath
    End Function
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
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim oracn As New OraDBconnection
        Dim oonum As Integer
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim rowno As Integer
        Dim pdfPath As String

        If Session("new_post_cclist") <> "1" Then
            Utils.ShowMessage(Me, "Create CC list first")
            Return
        End If

        'generate O/O number
        oonum = Utils.GetOfficeOrderNum()

        'generate report in separate thread

        pdfPath = GenerateReport(oonum)

        'save in trans_hist
        sql = "SELECT rowno FROM cadre.cadr WHERE tmp='T'"
        oracn.FillData(sql, ds)

        For i = 0 To ds.Tables(0).Rows.Count - 1
            rowno = ds.Tables(0).Rows(i)("rowno")
            sql = "INSERT into cadre.trans_hist values(0,0,0,0," & rowno & ",(select sysdate from dual),'Post Created','CP',0,99,NULL,'" & Session.SessionID & "',0," & oonum & ")"
            oracn.ExecQry(sql)
        Next

        'make changes permanent
        oracn.ExecQry("UPDATE cadre.cadr set tmp='A' where tmp='T'")
        DownloadFile(pdfPath)
    End Sub
    Protected Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If Session("new_post_cclist") <> "1" Then
            Utils.ShowMessage(Me, "Create CC list first")
            Return
        End If
        GenerateReport(0)
        DownloadFile(Session("pdfpath").ToString())
    End Sub
    Protected Sub btnUndo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo.Click
        Dim sql As String
        Dim oracn As New OraDBconnection

        sql = "delete from cadre.cadr where tmp='T'"
        oracn.ExecQry(sql)
        Utils.ShowMessage(Me, "All changes have been undone")
    End Sub
    Protected Sub btnCreateCC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateCC.Click
        Response.Write("<script type='text/javascript'>detailedresults=window.open('cc.aspx','_new');</script>")
        Session("new_post_cclist") = "1"
    End Sub

    Protected Sub btnDelSession_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelSession.Click
        Dim oracn As New OraDBconnection

        'delete temporary rows
        oracn.ExecQry("DELETE FROM cadre.cadr WHERE tmp='T'")
        lblPending.Text = ""
        Utils.ShowMessage(Me, "Session Deleted")
    End Sub
    Protected Sub drpsuboff_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpsuboff.SelectedIndexChanged
        fillgrid()
    End Sub
End Class