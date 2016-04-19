'create table cadre.temp_mastloc (loccode number(9) not null, reploc number(9), action char(1) not null)
Partial Class frm_edit_location
    Inherits System.Web.UI.Page
    Private Sub get_ce()
        Me.drpcorp.Items.Clear()
        If Me.RadioButtonList2.SelectedValue = "P" Then
            Me.drpcorp.Items.Add(New ListItem("--Select--", 0))
            Me.drpcorp.Items.Add(New ListItem("CMD/PSPCL", "800000000"))
            Me.drpcorp.Items.Add(New ListItem("Director/Distribution", "803000000"))
            Me.drpcorp.Items.Add(New ListItem("Director/Generation", "802000000"))
            Me.drpcorp.Items.Add(New ListItem("Director/Finance", "804000000"))
            Me.drpcorp.Items.Add(New ListItem("Director/Commercial", "805000000"))
            Me.drpcorp.Items.Add(New ListItem("Director/HR", "806000000"))
            Me.drpcorp.Items.Add(New ListItem("Director/Admin", "801000000"))
            Me.drpcorp.Items.Add(New ListItem("Deputation", "600000000"))
        ElseIf Me.RadioButtonList2.SelectedValue = "T" Then
            Me.drpcorp.Items.Add(New ListItem("--Select--", 0))
            Me.drpcorp.Items.Add(New ListItem("CMD/PSTCL", "503000000"))
            Me.drpcorp.Items.Add(New ListItem("Director/Technical", "503000001"))
            Me.drpcorp.Items.Add(New ListItem("Director/Finance & Commercial", "503000002"))
            Me.drpcorp.Items.Add(New ListItem("Director/Admin", "503000004"))
        End If
    End Sub
    'Private Sub fillorg()

    '    Dim oraCn As New OraDBconnection
    '    Dim ds As New System.Data.DataSet
    '    Dim sql As String
    '    Dim i As Integer


    '    sql = "select locname,loccode from pshr.mast_loc where  "
    '    If drpcorp.SelectedValue = "Zone" Then
    '        sql &= "loccode like '___000000' and loctype = 20"
    '    ElseIf drpcorp.SelectedValue = "Circle" Then
    '        sql &= "loccode like '______000' and loctype = 30"
    '    ElseIf drpcorp.SelectedValue = "Division" Then
    '        sql &= "loccode like '_______00' and loctype = 40"
    '    ElseIf drpcorp.SelectedValue = "SDivision" Then
    '        sql &= "loctype = 50"
    '    Else
    '        Return
    '    End If
    '    sql &= " and aloc=1 and locname like '%" & UCase(txtFilter.Text.Replace(" ", "%")) & "%' order by loccode"

    '    oraCn.FillData(sql, ds)
    '    drporg.Items.Clear()
    '    For i = 0 To ds.Tables(0).Rows.Count - 1
    '        drporg.Items.Add(New ListItem(ds.Tables(0).Rows(i)(0).ToString(), ds.Tables(0).Rows(i)(1).ToString()))
    '    Next

    '    'put the first org's name and abbr in edit textboxes
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        fillEditTexts(drporg.Items(0).Value)
    '        btnEdit.Enabled = True
    '        btnAbolish.Enabled = True
    '    Else
    '        btnEdit.Enabled = False
    '        btnAbolish.Enabled = False
    '    End If

    '    ds.Clear()
    '    ds.Dispose()
    'End Sub
    Private Sub fillorg_replacement(ByVal drp_corp As DropDownList, ByRef drp_org As DropDownList, ByVal txtFilter As String)
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer

        sql = "select locname,loccode from pshr.mast_loc where "
        If drp_corp.SelectedValue = "Zone" Then
            sql &= "loccode like '___000000' and loctype = 20"
        ElseIf drp_corp.SelectedValue = "Circle" Then
            sql &= "loccode like '______000' and loctype = 30"
        ElseIf drp_corp.SelectedValue = "Division" Then
            sql &= "loccode like '_______00' and loctype = 40"
        ElseIf drp_corp.SelectedValue = "SDivision" Then
            sql &= "loctype = 50"
        ElseIf drp_corp.SelectedValue = "SubOffice" Then
            sql &= "loctype = 60"
        ElseIf drp_corp.SelectedValue = "Directors" Then
            drp_org.Items.Add(New ListItem("CMD/PSPCL", "800000000"))
            drp_org.Items.Add(New ListItem("Director/Distribution", "803000000"))
            drp_org.Items.Add(New ListItem("Director/Generation", "802000000"))
            drp_org.Items.Add(New ListItem("Director/Finance", "804000000"))
            drp_org.Items.Add(New ListItem("Director/Commercial", "805000000"))
            drp_org.Items.Add(New ListItem("Director/HR", "806000000"))
            drp_org.Items.Add(New ListItem("Director/Admin", "801000000"))
            drp_org.Items.Add(New ListItem("Deputation", "600000000"))
            Return
        Else
            drp_org.Items.Clear()
            Return
        End If
        sql &= " and aloc=1 and locname like '%" & UCase(txtFilter.Replace(" ", "%")) & "%' order by loccode"

        oraCn.FillData(sql, ds)
        drp_org.Items.Clear()
        For i = 0 To ds.Tables(0).Rows.Count - 1
            drp_org.Items.Add(New ListItem(ds.Tables(0).Rows(i)(0).ToString(), ds.Tables(0).Rows(i)(1).ToString()))
        Next
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub fillEditTexts(ByVal rloc As Long)
        Dim oracn As New OraDBconnection
        Dim sql As String
        Dim ds As New System.Data.DataSet
        sql = "select locname,locabb from pshr.mast_loc where aloc=1 and loccode = " & rloc
        oracn.FillData(sql, ds)
        Me.txtorg.Text = ds.Tables(0).Rows(0)(0)
        Me.txtabbr.Text = ds.Tables(0).Rows(0)(1)
    End Sub
    Private Function get_rloc() As Double
        Dim rloc As Double
        If drpcorp.SelectedValue <> "" And drpcorp.SelectedValue <> "0" Then
            rloc = Me.drpcorp.SelectedValue
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
        If drpsdiv.SelectedValue <> "" And drpsdiv.SelectedValue <> "0" Then
            rloc = Me.drpsdiv.SelectedValue
        End If
        If drpsoff.SelectedValue <> "" And drpsoff.SelectedValue <> "0" Then
            rloc = Me.drpsoff.SelectedValue
        End If

        Return rloc

    End Function
    Private Function GetLocCode() As Long
        Dim loccode As Long
        loccode = -1
        If drpsoff.SelectedValue <> "" And drpsoff.SelectedValue <> "0" Then
            loccode = drpsoff.SelectedItem.Value
        ElseIf drpsdiv.SelectedValue <> "" And drpsdiv.SelectedValue <> "0" Then
            loccode = drpsdiv.SelectedItem.Value
        ElseIf drpdiv.SelectedValue <> "" And drpdiv.SelectedValue <> "0" Then
            loccode = drpdiv.SelectedItem.Value
        ElseIf drpcir.SelectedValue <> "" And drpcir.SelectedValue <> "0" Then
            loccode = drpcir.SelectedItem.Value
        ElseIf drporg.SelectedValue <> "" And drporg.SelectedValue <> "0" Then
            loccode = drporg.SelectedItem.Value
        ElseIf drpcorp.SelectedValue <> "" And drpcorp.SelectedValue <> "0" Then
            loccode = drpcorp.SelectedValue
        End If
            Return loccode
    End Function
    Private Sub fillorg()
        Me.drporg.Items.Clear()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim rloc As Double
        rloc = get_rloc()
        If rloc <> "600000000" Then
            sql = "select locabb,loccode from pshr.mast_loc where locrep=  " & rloc & " and loccode like '___000000' and aloc=1  order by loccode"
        Else
            sql = "select locabb,loccode from pshr.mast_loc where loccode like '___000000' and (loccode like '6%' or loccode like '7%') and aloc=1  order by loccode"
        End If
        oraCn.FillData(sql, ds)
        drporg.Items.Clear()

        If ds.Tables(0).Rows.Count > 0 Then
            drporg.Items.Add(New ListItem("--Select--", 0))
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drporg.Items.Add(New ListItem(ds.Tables(0).Rows(i)(0).ToString(), ds.Tables(0).Rows(i)(1).ToString()))
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
        rloc = get_rloc()
        sql = "select loccode, locabb from pshr.mast_loc where locrep = " & rloc & " and loctype = 30 and aloc=1 order by loccode "
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
    Private Sub fillsdiv()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim rloc As Double
        rloc = get_rloc()
        sql = "select loccode, locabb from pshr.mast_loc where locrep = " & rloc & " and loctype = 50 and aloc=1 order by loccode "
        oraCn.FillData(sql, ds)
        drpsdiv.Items.Clear()
        If ds.Tables(0).Rows.Count > 0 Then
            drpsdiv.Items.Add(New ListItem("--Select--", 0))
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drpsdiv.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
            Next
        End If
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub fillsoff()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim rloc As Double
        rloc = get_rloc()
        sql = "select loccode, locabb from pshr.mast_loc where locrep = " & rloc & " and loctype = 60 and aloc=1 order by loccode "
        oraCn.FillData(sql, ds)
        drpsoff.Items.Clear()
        If ds.Tables(0).Rows.Count > 0 Then
            drpsoff.Items.Add(New ListItem("--Select--", 0))
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drpsoff.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
            Next
        End If
        ds.Clear()
        ds.Dispose()
    End Sub
    Protected Sub drpcorp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcorp.SelectedIndexChanged
        PanAbolish.Visible = False
        panEdit.Visible = False
        drporg.ClearSelection()
        drporg.Items.Clear()
        drpcir.Items.Clear()
        drpdiv.Items.Clear()
        drpsdiv.Items.Clear()
        drpsoff.Items.Clear()
        fillorg()
        fillcir()
        filldiv()
        fillsdiv()
        fillsoff()
        'If drpcorp.SelectedValue = "0" Then
        '    btnEdit.Enabled = False
        '    btnAbolish.Enabled = False
        '    drporg.Items.Clear()
        'Else
        '    btnEdit.Enabled = True
        '    btnAbolish.Enabled = True
        'End If
        'PanAbolish.Visible = False
        'panEdit.Visible = False
        'fillorg()

    End Sub
    Protected Sub drporg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drporg.SelectedIndexChanged
        PanAbolish.Visible = False
        panEdit.Visible = False
        drpcir.Items.Clear()
        drpdiv.Items.Clear()
        drpsdiv.Items.Clear()
        drpsoff.Items.Clear()

        fillcir()
        filldiv()
        fillsdiv()
        fillsoff()
        'txtabbr.Enabled = False
        'txtorg.Enabled = False
        'fillEditTexts(Me.drporg.SelectedValue)
        'PanAbolish.Visible = False
        'panEdit.Visible = False
    End Sub
    Protected Sub drpcir_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcir.SelectedIndexChanged
        PanAbolish.Visible = False
        panEdit.Visible = False
        drpdiv.Items.Clear()
        drpsdiv.Items.Clear()
        drpsoff.Items.Clear()
        filldiv()
        fillsdiv()
        fillsoff()
    End Sub
    Protected Sub drpdiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdiv.SelectedIndexChanged
        drpsdiv.Items.Clear()
        drpsoff.Items.Clear()
        PanAbolish.Visible = False
        panEdit.Visible = False
        fillsdiv()
        fillsoff()
    End Sub
    Protected Sub drpsdiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpsdiv.SelectedIndexChanged
        drpsoff.Items.Clear()
        PanAbolish.Visible = False
        panEdit.Visible = False
        fillsoff()
    End Sub
    Protected Sub drpsoff_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpsoff.SelectedIndexChanged
        PanAbolish.Visible = False
        panEdit.Visible = False
    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim oracn As New OraDBconnection
        Dim sql As String
        Dim loccode As Long

        If Me.txtorg.Text = "" Then
            Utils.ShowMessage(Me, "Please enter the location name")
            Exit Sub
        End If
        If Me.txtabbr.Text = "" Then
            Utils.ShowMessage(Me, "Please enter the location abbreviation")
            Exit Sub
        End If

        loccode = GetLocCode()
        If (loccode = -1) Then
            Utils.ShowMessage(Me, "Please select a valid location")
            Exit Sub
        End If
        sql = "Update pshr.mast_loc set Locname = '" & Me.txtorg.Text & "' , locabb = '" & Me.txtabbr.Text & "' where loccode = " & loccode
        oracn.ExecQry(sql)

        Utils.ShowMessage(Me, "Location Modified Successfully")
        fillorg()
    End Sub
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim loccode As Long
        loccode = GetLocCode()
        If loccode = -1 Then
            Utils.ShowMessage(Me, "Please select a valid location")
            Return
        End If
        PanAbolish.Visible = False
        panChRep.Visible = False
        panEdit.Visible = True
        txtabbr.Enabled = True
        txtorg.Enabled = True
        fillEditTexts(loccode)
    End Sub
    Protected Sub btnAbolish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAbolish.Click
        Dim loccode As Long

        loccode = GetLocCode()
        If loccode = -1 Then
            Utils.ShowMessage(Me, "Please select a valid location")
            Return
        End If
        panEdit.Visible = False
        panChRep.Visible = False
        PanAbolish.Visible = True
    End Sub
    Protected Sub drpcorp_rep_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcorp_rep.SelectedIndexChanged
        fillorg_replacement(drpcorp_rep, drporg_rep, txtFilterRep.Text)
        'If drpcorp_rep.SelectedValue = "0" Then
        '    btnEdit.Enabled = False
        '    btnAbolish.Enabled = False
        '    drporg_rep.Items.Clear()
        '    PanAbolish.Visible = False
        '    panEdit.Visible = False
        'Else
        '    btnEdit.Enabled = True
        '    btnAbolish.Enabled = True
        'End If
    End Sub
    Protected Sub btnDoAbolish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDoAbolish.Click
        Dim oracn As New OraDBconnection
        Dim sql As String
        Dim loccode As Long

        loccode = GetLocCode()

        If loccode = -1 Then
            Utils.ShowMessage(Me, "Please select a valid location")
            Exit Sub
        End If

        If Not chkAbolishUnderlying.Checked Then
            If drporg_rep.SelectedValue = "" Then
                Utils.ShowMessage(Me, "Please select new reporting location of underlying locations")
                Return
            End If
        End If
        'abolish row with loccode in drporg.selectedvalue
        sql = "update pshr.mast_loc set aloc=0 where loccode = " & loccode
        oracn.ExecQry(sql)

        'save change in temp_mastloc
        sql = "INSERT INTO cadre.temp_mastloc VALUES(" & loccode & ",NULL,'A')"
        oracn.ExecQry(sql)

        If chkAbolishUnderlying.Checked Then
            'user want all the reporting locations to be abolished as well
            'abolish rows whose locrep = org.selectedvalue
            sql = "update pshr.mast_loc set aloc=0 where locrep = " & loccode
            oracn.ExecQry(sql)

            'sql= "INSERT INTO cadre.temp_mastloc(loccode,reploc,action) (SELECT loccode,NULL,  'A' FROM mast_loc WHERE locrep=" & drporg.SelectedValue & ")"
            sql = "INSERT INTO cadre.temp_mastloc(loccode,reploc,action) (SELECT loccode,locrep,'A' FROM mast_loc WHERE locrep=" & loccode & ")"
            oracn.ExecQry(sql)
        Else
            'action=U => Updated with new reporting location, so save old location///////
            'sql= "INSERT INTO cadre.temp_mastloc(loccode,reploc,action) (SELECT loccode," & drporg.SelectedValue & ",'U' FROM mast_loc WHERE locrep=" & drporg.SelectedValue & ")"
            sql = "INSERT INTO cadre.temp_mastloc(loccode,reploc,action) (SELECT loccode,locrep,'U' FROM mast_loc WHERE locrep=" & loccode & ")"
            oracn.ExecQry(sql)

            'user want to change reporting location of underlying locations
            'update rows whose locrep = org.selectedvalue to drporg_rep.selectedvalue
            sql = "update pshr.mast_loc set locrep=" & drporg_rep.SelectedValue & " where locrep=" & loccode
            oracn.ExecQry(sql)
        End If

        'Utils.ShowMessage(Me, "Location Abolished Successfully")
        fillorg()
    End Sub
    Protected Sub txtFilterRep_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterRep.TextChanged
        fillorg_replacement(drpcorp_rep, drporg_rep, txtFilterRep.Text)
    End Sub
    'Protected Sub txtFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilter.TextChanged
    '    fillorg()
    'End Sub
    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAbolishUnderlying.CheckedChanged
        If chkAbolishUnderlying.Checked Then
            drpcorp_rep.Enabled = False
            drporg_rep.Enabled = False
            txtFilterRep.Enabled = False
            drpcorp_rep.SelectedIndex = 0
            drporg_rep.Items.Clear()
        Else
            drpcorp_rep.Enabled = True
            drporg_rep.Enabled = True
            txtFilterRep.Enabled = True
        End If
    End Sub
    Private Sub DoUndo()
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim i As Integer
        Dim loccode, oldlocrep As Long

        'remove locations in cadr which exists in temp_postedit
        sql = "DELETE FROM cadre.cadr WHERE loccode IN (SELECT DISTINCT loccode FROM cadre.temp_postedit)"
        oracn.ExecQry(sql)

        'put locations from temp_postedit in cadr
        oracn.ExecQry("INSERT INTO cadre.cadr SELECT * FROM cadre.temp_postedit")

        'delete from temp_postedit
        oracn.ExecQry("DELETE FROM cadre.temp_postedit")

        'if action = A, just set aloc=1
        sql = "update mast_loc set aloc=1 where loccode in (select loccode from cadre.temp_mastloc where action='A')"
        oracn.ExecQry(sql)

        'if action = U, set aloc = 1 and set locrep to reploc
        sql = "select loccode,reploc from cadre.temp_mastloc where action='U'"
        oracn.FillData(sql, ds)
        For i = 0 To i = ds.Tables(0).Rows.Count
            loccode = ds.Tables(0).Rows(i)("loccode")
            oldlocrep = ds.Tables(0).Rows(i)("reploc")
            sql = "update mast_loc set aloc=1, locrep=" & oldlocrep & " where loccode=" & loccode
            oracn.ExecQry(sql)
        Next

        'if action = N, reset locrep of loccode
        sql = "select loccode,reploc from cadre.temp_mastloc where action='N'"
        oracn.FillData(sql, ds)
        For i = 0 To i = ds.Tables(0).Rows.Count
            loccode = ds.Tables(0).Rows(i)("loccode")
            oldlocrep = ds.Tables(0).Rows(i)("reploc")
            sql = "update mast_loc set locrep=" & oldlocrep & " where loccode=" & loccode
            oracn.ExecQry(sql)
        Next

        sql = "delete from cadre.temp_mastloc"
        oracn.ExecQry(sql)

        ds.Clear()
        ds.Dispose()
    End Sub
    Protected Sub btnUndo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo.Click
        DoUndo()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Utils.AuthenticateUser(Me)
        If Not Page.IsPostBack Then
            'Dim sql As String
            'Dim oracn As New OraDBconnection
            get_ce()
            'sql = "DELETE FROM pshr.mast_loc WHERE loccode IN (SELECT loccode FROM cadre.temp_mastloc WHERE action='C')"
            'oracn.ExecQry(sql)
            'sql = "DELETE FROM cadre.temp_mastloc"
            'oracn.ExecQry(sql)
            DoUndo()
        End If
    End Sub
    Private Sub GenerateReport(ByVal oonum As Integer, ByVal filename As String)
        If oonum = 0 Then
            Response.Write("<script type='text/javascript'>detailedresults=window.open('rpt_edit_loc.aspx','_new');</script>")
        End If
    End Sub
    Private Sub GenerateReport_CR(ByVal oonum As Integer, ByVal filename As String)
        If oonum = 0 Then
            Response.Write("<script type='text/javascript'>detailedresults=window.open('rpt_crloc.aspx','_new');</script>")
        End If
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim oonum As Integer
        Dim oracn As New OraDBconnection

        'remove undo information
        oracn.ExecQry("DELETE FROM cadre.temp_mastloc")

        oonum = Utils.GetOfficeOrderNum()
        GenerateReport(oonum, "Cadre")
    End Sub
    Protected Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        GenerateReport(0, "")
    End Sub
    Protected Sub RadioButtonList2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList2.SelectedIndexChanged
        get_ce()
        PanAbolish.Visible = False
        panEdit.Visible = False
    End Sub
    
    Protected Sub btnChRep_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChRep.Click
        Dim loccode As Long

        loccode = GetLocCode()
        If loccode = -1 Then
            Utils.ShowMessage(Me, "Please select a valid location")
            Return
        End If
        panEdit.Visible = False
        PanAbolish.Visible = False
        panChRep.Visible = True
    End Sub

    Protected Sub drpCRcorp_rep_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCRcorp_rep.SelectedIndexChanged
        fillorg_replacement(drpCRcorp_rep, drpCRorg_rep, txtCRFilterRep.Text)
        'If drpcorp_rep.SelectedValue = "0" Then
        '    btnEdit.Enabled = False
        '    btnAbolish.Enabled = False
        '    btnChRep.Enabled = False
        '    drpCRorg_rep.Items.Clear()
        '    PanAbolish.Visible = False
        '    panEdit.Visible = False
        '    panChRep.Visible = False
        'Else
        '    btnEdit.Enabled = True
        '    btnAbolish.Enabled = True
        '    btnChRep.Enabled = True
        'End If
    End Sub

    Protected Sub btnDoChRep_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDoChRep.Click
        Dim oracn As New OraDBconnection
        Dim sql As String
        Dim loccode As Long
        Dim ds As New System.Data.DataSet
        Dim parent_loctype As String
        loccode = GetLocCode()

        If loccode = -1 Then
            Utils.ShowMessage(Me, "Please select a valid location")
            Exit Sub
        End If

        If drpCRorg_rep.SelectedValue = "" Then
            Utils.ShowMessage(Me, "Please select the new reporting location")
            Return
        End If

        'if chkOnlyMerge is False then location (being edited) itself will be added under the selected (parent) location
        'if chkOnlyMerge is True then locrep of location (being edited) won't change, 
        'only the underlying individual posts will be moved/merged with the locations of parent location
        If (chkOnlyMerge.Checked = False) Then
            'action=N => Updated with New reporting location, so save old location
            sql = "INSERT INTO cadre.temp_mastloc(loccode,reploc,action) (SELECT loccode,locrep,'N' FROM mast_loc WHERE loccode=" & loccode & ")"
            oracn.ExecQry(sql)

            'if a location is being put under a Sub/Div (loctype 50) 
            'then change the loctype of child location to loctype of suboffice (i.e. 60)

            'check loctype of parent location
            sql = "SELECT loctype FROM pshr.mast_loc WHERE LOCCODE=" & drpCRorg_rep.SelectedValue
            oracn.FillData(sql, ds)
            parent_loctype = ds.Tables(0).Rows(0)(0)

            'if parent loctype is 50 (i.e. it is Sub/Div) then change the child location's loctype to 60
            If (parent_loctype = "50") Then
                'user want to change reporting location of underlying locations
                'update rows whose locrep = org.selectedvalue to drporg_rep.selectedvalue
                sql = "update pshr.mast_loc set locrep=" & drpCRorg_rep.SelectedValue & ", loctype=60 where loccode=" & loccode
            Else
                sql = "update pshr.mast_loc set locrep=" & drpCRorg_rep.SelectedValue & " where loccode=" & loccode
            End If
            oracn.ExecQry(sql)
        Else
            'save for undo
            oracn.ExecQry("DELETE FROM cadre.temp_postedit")
            sql = "INSERT INTO cadre.temp_postedit SELECT * FROM cadre.cadr WHERE LOCCODE IN ('" & loccode & "','" & drpCRorg_rep.SelectedValue & "')"
            oracn.ExecQry(sql)

            'delete posts from old loccode
            sql = "DELETE FROM cadre.cadr WHERE loccode = " & loccode
            oracn.ExecQry(sql)

            'update posts in cadr to new loccode
            sql = "INSERT INTO cadre.cadr SELECT '" & drpCRorg_rep.SelectedValue & "',c.desgcode," &
                "indx +  (SELECT nvl(MAX(indx),0) FROM cadre.cadr WHERE loccode=" & drpCRorg_rep.SelectedValue & " AND desgcode=c.desgcode  )," &
                "c.scaleid,c.rowno,c.branch, c.ptype,c.tmp,c.srno,c.remarks,0 " &
                "FROM cadre.temp_postedit c WHERE loccode =" & loccode
            oracn.ExecQry(sql)
        End If
        'fillorg()
    End Sub

    Protected Sub btnCRGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCRGenerate.Click
        'Dim oonum As Integer
        Dim oracn As New OraDBconnection

        'remove undo information
        oracn.ExecQry("DELETE FROM cadre.temp_mastloc")
        oracn.ExecQry("DELETE FROM cadre.temp_postedit")
        'oonum = Utils.GetOfficeOrderNum()
        'GenerateReport_CR(oonum, "Cadre")
    End Sub

    Protected Sub btnCRPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCRPreview.Click
        'GenerateReport_CR(0, "")
    End Sub

    Protected Sub btnUndoChRep_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndoChRep.Click
        DoUndo()
    End Sub

    Protected Sub txtCRFilterRep_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCRFilterRep.TextChanged
        fillorg_replacement(drpCRcorp_rep, drpCRorg_rep, txtCRFilterRep.Text)
    End Sub

  
    Protected Sub drporg_rep_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drporg_rep.SelectedIndexChanged
        lbl_loccode.Text = drporg_rep.SelectedValue
    End Sub

    Protected Sub drpCRorg_rep_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCRorg_rep.SelectedIndexChanged
        lbl_loccode_chrep.Text = drpCRorg_rep.SelectedValue
    End Sub
End Class