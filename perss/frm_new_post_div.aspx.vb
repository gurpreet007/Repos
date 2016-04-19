Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports System.Data.Odbc

Partial Class frm_new_post_div
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Utils.AuthenticateUser(Me)

        'Disable 'Save and Generate Office Order' button if role is 'Creator' i.e. 'C'
        If Session("role") = "C" Then
            btnFinalSave.Enabled = False
        End If
        If Not Page.IsPostBack Then
            Dim oraCn As New OraDBconnection
            Dim ds As New System.Data.DataSet
            oraCn.FillData("select * from cadre.condiv", ds)
            If (ds.Tables(0).Rows.Count > 0) Then
                lblPending.Text = "There is an unsaved session. Either Save or Delete it before continuing"
                PanSave.Visible = True
            Else
                lblPending.Text = ""
                PanSave.Visible = False
            End If

            filldesg()
            fillscale()
            get_ce()
        End If

        If drpdesg.SelectedValue <> "" Then
            Label8.Text = Utils.GetDesgCount(GetLocCode(), drpdesg.SelectedItem.Value.Split("-")(0))
        End If
        'If gvcreate.Rows.Count <= 0 Then
        '    btnProceed.Visible = False
        'Else
        '    btnProceed.Visible = True
        'End If
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
    Private Function GetLocText() As String
        Dim loctext As String

        loctext = RadioButtonList2.SelectedItem.Text
        If drporgtype.SelectedValue <> "" And drporgtype.SelectedValue <> "0" Then
            loctext &= "|" & drporgtype.SelectedItem.Text
        End If
        If drporg.SelectedValue <> "" And drporg.SelectedValue <> "0" Then
            loctext &= "|" & drporg.SelectedItem.Text
        End If
        If drpcir.SelectedValue <> "" And drpcir.SelectedValue <> "0" Then
            loctext &= "|" & drpcir.SelectedItem.Text
        End If
        If drpdiv.SelectedValue <> "" And drpdiv.SelectedValue <> "0" Then
            loctext &= "|" & drpdiv.SelectedItem.Text
        End If
        If drpsubdiv.SelectedValue <> "" And drpsubdiv.SelectedValue <> "0" Then
            loctext &= "|" & drpsubdiv.SelectedItem.Text
        End If
        If drpsuboff.SelectedValue <> "" And drpsuboff.SelectedValue <> "0" Then
            loctext &= "|" & drpsuboff.SelectedItem.Text
        End If

        Return loctext
    End Function
    Protected Sub txtsdesg_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsdesg.TextChanged
        filldesg()
    End Sub

    Private Sub filldesg()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim lcode As Double

        lcode = GetLocCode()

        'If Panel_Branch.Visible = True Then
        If lblStepNum.Text = "1" Then
            'to create new posts
            sql = "select desgcode, desgabb from pshr.mast_desg where desgtext is not null and (desgabb like '%" _
                   & UCase(txtsdesg.Text.Replace(" ", "%")) & "%' )order by desgabb "
        ElseIf lblStepNum.Text = "2" Then
            'to abolish existing posts
            'sql = " Select d.desgcode, d.desgabb from cadre.cadr c , pshr.mast_desg d where c.desgcode = d.desgcode and c.loccode = " & lcode
            sql = "select desgcode || '-' || indx, pshr.get_desg(desgcode) || '-' || indx || ' (' || cadre.get_branch(branch) || ')', cadre.get_hecode(desgcode) as hcode from" &
                    " cadre.cadr where loccode = " & lcode & " order by hcode"
        Else
            Exit Sub
        End If

        oraCn.FillData(sql, ds)
        drpdesg.Items.Clear()
        If ds.Tables(0).Rows.Count > 0 Then
            drpdesg.Items.Add(New ListItem("--Select--", 0))
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drpdesg.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
            Next
        End If
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
        drpscale.Items.Add(New ListItem("Pb. Govt. Scale ", 0))
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Me.drpscale.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(2).ToString()))
        Next
        'For i = 0 To ds.Tables(0).Rows.Count - 1
        '    Me.drpscale.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(2).ToString()))
        '    Me.drpbranch.SelectedIndex = Me.drpbranch.Items.IndexOf(Me.drpbranch.Items.FindByValue(ds.Tables(0).Rows(i)(3)))
        '    Me.drptperm.SelectedIndex = Me.drptperm.Items.IndexOf(Me.drptperm.Items.FindByValue(ds.Tables(0).Rows(i)(4)))
        'Next
        ds.Clear()
        ds.Dispose()
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
    Private Sub fillorg()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim rloc As Long

        rloc = get_rloc()

        'sql = "select loccode, locabb from pshr.mast_loc where locname is not null and loccode like '%000000' and locrep = '" & Me.drporgtype.SelectedValue & "' and loctype <> 11 and aloc = 1 order by loccode"
        sql = "select loccode, locabb from pshr.mast_loc where locname is not null and loccode like '%000000' and locrep = '" & rloc & "' and loctype <> 11 and aloc = 1 order by loccode"
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
        Dim rloc As Long

        rloc = get_rloc()
        'If drporg.SelectedValue = "0" Then
        '    Exit Sub
        'End If

        'sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drporg.SelectedItem.Value, 1, 3) & "%000' and loctype = 30 and loccode not like '%000000'  and aloc=1 order by loccode "
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
        Dim rloc As Long

        rloc = get_rloc()

        'If drpcir.SelectedValue = "0" Then
        '    If drporg.SelectedValue = "0" Then
        '        'do nothing if no circle and no org is filled
        '        Exit Sub
        '    Else
        '        'if circle is not filled but org is filled then get locrep from org
        '        sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drporg.SelectedItem.Value, 1, 3) & "%' and loctype = 40 and locrep = " & Me.drporg.SelectedValue & " and aloc=1 order by loccode "
        '    End If
        'Else
        '    'if circle is filled then get locrep from circle
        '    sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drpcir.SelectedItem.Value, 1, 5) & "%' and loctype = 40 and aloc=1 order by loccode "
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
        Dim rloc As Long

        rloc = get_rloc()

        'If drpdiv.SelectedValue = "" Then
        '    Exit Sub
        'End If

        'If drpdiv.SelectedValue = "0" Then
        '    If drpcir.SelectedValue = "0" Then
        '        If drporg.SelectedValue = "0" Then
        '            'if none of div,circle and org is filled then do nothing
        '            Exit Sub
        '        Else
        '            'if div and cir are not filled but org is filled then get locrep from org
        '            sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drporg.SelectedItem.Value, 1, 8) & "%' and loctype = 50 and aloc=1 order by loccode "
        '        End If
        '    Else
        '        'if div is not filled but circle is filled then get locrep from circle
        '        sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drpcir.SelectedItem.Value, 1, 8) & "%' and loctype = 50 and aloc=1 order by loccode "
        '    End If
        'Else
        '    'if div is filled then then locrep from div
        '    sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drpdiv.SelectedItem.Value, 1, 8) & "%' and loctype = 50 and aloc=1 order by loccode "
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
    Protected Sub RadioButtonList2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList2.SelectedIndexChanged
        drporg.Items.Clear()
        drpcir.Items.Clear()
        drpdiv.Items.Clear()
        drpsubdiv.Items.Clear()
        get_ce()
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
            filldesg()
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
        filldesg()
    End Sub
    Protected Sub drpcir_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcir.SelectedIndexChanged
        drpdiv.Items.Clear()
        drpsubdiv.Items.Clear()
        filldiv()
        fillsubdiv()
        fillsuboff()
        filldesg()
    End Sub
    Protected Sub drpdiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdiv.SelectedIndexChanged
        drpsubdiv.Items.Clear()
        fillsubdiv()
        fillsuboff()
        filldesg()
    End Sub
    Protected Sub drpsubdiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpsubdiv.SelectedIndexChanged
        fillsuboff()
        filldesg()
    End Sub

    Private Sub AddNewPosts()
        Dim loccode As Long
        Dim txtLoc As String
        Dim txtDesg As String
        Dim txtBranch As String
        Dim txtScale As String
        Dim txtPType As String
        Dim txtCode As String
        Dim strRemarks As String

        loccode = GetLocCode()

        'If loccode = 0 Or
        '    drpdesg.SelectedValue = "0" Or
        '    drpbranch.SelectedValue = "99" Or
        '    drpscale.SelectedValue = "0" Then
        '    Exit Sub
        'End If

        'generate text_location
        txtLoc = GetLocText()

        'get text_desgination
        txtDesg = drpdesg.SelectedItem.Text

        'get text branch
        txtBranch = drpbranch.SelectedItem.Text

        'get text scale
        txtScale = drpscale.SelectedItem.Text

        'get text ptype
        txtPType = drptperm.SelectedItem.Text

        'get text remarks
        strRemarks = txtRem0.Text.Replace("|", "!")

        'get text code
        txtCode = loccode.ToString() & "|" & drpdesg.SelectedValue & "|" &
                    drpbranch.SelectedValue & "|" & drpscale.SelectedValue & "|" &
                    drptperm.SelectedValue & "|" & strRemarks

        'put text_location, text_desgination,text_branch,text_scale, text_ptype in grid

        Dim dtable As New System.Data.DataTable()
        Dim dr As System.Data.DataRow

        dtable.Columns.Add("Checked", System.Type.GetType("System.Boolean"))
        dtable.Columns.Add("Location", System.Type.GetType("System.String"))
        dtable.Columns.Add("Designation", System.Type.GetType("System.String"))
        dtable.Columns.Add("Branch", System.Type.GetType("System.String"))
        dtable.Columns.Add("Scale", System.Type.GetType("System.String"))
        dtable.Columns.Add("PType", System.Type.GetType("System.String"))
        dtable.Columns.Add("Code", System.Type.GetType("System.String"))
        Dim i As Integer
        For i = 0 To gvcreate.Rows.Count - 1
            dr = dtable.NewRow
            dr("Checked") = DirectCast(gvcreate.Rows(i).Cells(0).Controls(1), CheckBox).Checked
            dr("Location") = DirectCast(gvcreate.Rows(i).Cells(1).Controls(1), System.Web.UI.WebControls.Label).Text
            dr("Designation") = DirectCast(gvcreate.Rows(i).Cells(2).Controls(1), System.Web.UI.WebControls.Label).Text
            dr("Branch") = DirectCast(gvcreate.Rows(i).Cells(3).Controls(1), System.Web.UI.WebControls.Label).Text
            dr("Scale") = DirectCast(gvcreate.Rows(i).Cells(4).Controls(1), System.Web.UI.WebControls.Label).Text
            dr("PType") = DirectCast(gvcreate.Rows(i).Cells(5).Controls(1), System.Web.UI.WebControls.Label).Text
            dr("Code") = DirectCast(gvcreate.Rows(i).Cells(6).Controls(1), System.Web.UI.WebControls.Label).Text
            dtable.Rows.Add(dr)
        Next i
        dr = dtable.NewRow
        dr("Checked") = True
        dr("Location") = txtLoc
        dr("Designation") = txtDesg & strRemarks
        dr("Branch") = txtBranch
        dr("Scale") = txtScale
        dr("PType") = txtPType
        dr("Code") = txtCode
        dtable.Rows.Add(dr)
        dtable.AcceptChanges()
        gvcreate.DataSource = dtable
        gvcreate.DataBind()
        'btnProceed.Visible = True
    End Sub
    Private Sub AddAbolishPosts()
        Dim loccode As Long
        Dim txtLoc As String
        Dim txtDesg As String
        Dim txtCode As String

        loccode = GetLocCode()

        If loccode = 0 Or
            drpdesg.SelectedValue = "0" Then
            Exit Sub
        End If

        'generate text_location
        txtLoc = GetLocText()

        'get text_desgination
        txtDesg = drpdesg.SelectedItem.Text

        'get text code
        txtCode = loccode.ToString() & "|" & drpdesg.SelectedValue

        'put text_location, text_desgination in grid

        Dim dtable As New System.Data.DataTable()
        Dim dr As System.Data.DataRow

        dtable.Columns.Add("Checked", System.Type.GetType("System.Boolean"))
        dtable.Columns.Add("Location", System.Type.GetType("System.String"))
        dtable.Columns.Add("Designation", System.Type.GetType("System.String"))
        dtable.Columns.Add("Code", System.Type.GetType("System.String"))
        Dim i As Integer
        For i = 0 To gvabolish.Rows.Count - 1
            dr = dtable.NewRow
            dr("Checked") = DirectCast(gvabolish.Rows(i).Cells(0).Controls(1), CheckBox).Checked
            dr("Location") = DirectCast(gvabolish.Rows(i).Cells(1).Controls(1), System.Web.UI.WebControls.Label).Text
            dr("Designation") = DirectCast(gvabolish.Rows(i).Cells(2).Controls(1), System.Web.UI.WebControls.Label).Text
            dr("Code") = DirectCast(gvabolish.Rows(i).Cells(3).Controls(1), System.Web.UI.WebControls.Label).Text
            dtable.Rows.Add(dr)
        Next i
        dr = dtable.NewRow
        dr("Checked") = True
        dr("Location") = txtLoc
        dr("Designation") = txtDesg
        dr("Code") = txtCode
        dtable.Rows.Add(dr)
        dtable.AcceptChanges()
        gvabolish.DataSource = dtable
        gvabolish.DataBind()
        'btnProceed.Visible = True
    End Sub

    Protected Sub btn_AddToGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AddToGrid.Click
        If drporgtype.SelectedValue = 0 Then
            Utils.ShowMessage(Me, "Please select a valid organisation head")
            Exit Sub
        End If
        If drpdesg.SelectedValue = "0" Or drpdesg.SelectedValue = "" Then
            Utils.ShowMessage(Me, "Please select a valid designation")
            Exit Sub
        End If

        If lblStepNum.Text = "1" Then
            'If drpbranch.SelectedValue = 99 Then
            '    Utils.ShowMessage(Me, "Please select a valid branch")
            '    Exit Sub
            'End If

            If drpscale.SelectedValue = 0 Then
                Utils.ShowMessage(Me, "Please select a valid scale")
                Exit Sub
            End If

            AddNewPosts()
            btnProceed.Visible = (gvcreate.Rows.Count > 0)
        ElseIf lblStepNum.Text = "2" Then
            AddAbolishPosts()
            btnProceed.Visible = (gvabolish.Rows.Count > 0)
        End If
    End Sub
    Private Sub CreatePosts(ByVal strCode As String)
        Dim oraCn As New OraDBconnection
        Dim sql As String
        Dim loccode As Double
        Dim desgcode As Integer
        Dim branchid As Integer
        Dim scaleid As Integer
        Dim strptype As String
        Dim index As Integer
        Dim rno As Integer

        loccode = strCode.Split("|")(0)
        desgcode = strCode.Split("|")(1)
        branchid = strCode.Split("|")(2)
        scaleid = strCode.Split("|")(3)
        strptype = strCode.Split("|")(4)

        index = Utils.GetIndex(loccode, desgcode)
        rno = Utils.GetRowNum()

        sql = "insert into cadre.cadr  values(" & loccode & "," & desgcode & "," & index & "," & scaleid & ", " & rno & "," & branchid & ",'" & strptype & "','A',NULL)"
        oraCn.ExecQry(sql)
    End Sub
    Private Function GetActionID() As Integer
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim actionid As Integer
        Dim sql As String

        actionid = -1
        sql = "select nvl(max(actionid),-1) + 1 from cadre.condiv"
        oraCn.FillData(sql, ds)
        If ds.Tables(0).Rows.Count > 0 Then
            actionid = ds.Tables(0).Rows(0)(0)
        End If

        Return actionid
    End Function

    Private Sub AddCreatePosts(ByVal strcode As String, ByVal actionid As Integer, ByVal rno As Integer)
        Dim oraCn As New OraDBconnection
        Dim sql As String
        Dim loccode As Double
        Dim desgcode As Integer
        Dim branchid As Integer
        Dim scaleid As Integer
        Dim strptype As String
        Dim remarks As String

        loccode = strcode.Split("|")(0)
        desgcode = strcode.Split("|")(1)
        branchid = strcode.Split("|")(2)
        scaleid = strcode.Split("|")(3)
        strptype = strcode.Split("|")(4)
        remarks = strcode.Split("|")(5)

        sql = "insert into cadre.condiv values(" & loccode & "," & desgcode & "," & scaleid & ", " & branchid & ",'" & strptype & "', 1, " & actionid & ",0," & rno & ",'" & remarks & "')"
        oraCn.ExecQry(sql)
    End Sub
    Private Sub AddAbolishPosts(ByVal strcode As String, ByVal actionid As Integer, ByVal rno As Integer)
        Dim oraCn As New OraDBconnection
        Dim sql As String
        Dim loccode As Double
        Dim strDesgIndx As String
        Dim desgcode As Integer
        Dim index As Integer

        loccode = strcode.Split("|")(0)
        strDesgIndx = strcode.Split("|")(1)
        desgcode = strDesgIndx.Split("-")(0)
        index = strDesgIndx.Split("-")(1)

        sql = "insert into cadre.condiv values(" & loccode & "," & desgcode & ", NULL, NULL, NULL, 2, " & actionid & "," & index & "," & rno & ",NULL)"
        oraCn.ExecQry(sql)
    End Sub
    Public Sub ShowConDiv()
        Dim oraCn As New OraDBconnection
        Dim dt As New System.Data.DataTable
        Dim ds As New System.Data.DataSet
        Dim ds1 As New System.Data.DataSet
        Dim ds2 As New System.Data.DataSet
        Dim ds3 As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim c As Integer
        Dim j As Integer
        Dim separator As String

        separator = "-----"
        sql = "select distinct actionid from cadre.condiv order by actionid"
        oraCn.FillData(sql, ds)

        dt.Columns.Add(New System.Data.DataColumn("LocationC"))
        dt.Columns.Add(New System.Data.DataColumn("DesignationC"))
        dt.Columns.Add(New System.Data.DataColumn(separator))
        dt.Columns.Add(New System.Data.DataColumn("LocationA"))
        dt.Columns.Add(New System.Data.DataColumn("DesignationA"))

        'dt.Rows.Clear()
        'dt.AcceptChanges()
        'gvmap.DataSource = dt
        'gvmap.DataBind()
        ''loop for actionids
        For i = 0 To ds.Tables(0).Rows.Count - 1
            ds1 = New System.Data.DataSet
            ds2 = New System.Data.DataSet
            'get creation entries
            sql = "select pshr.get_org(c.loccode),pshr.get_desg(c.desgcode) || c.remarks from cadre.condiv c where c.action = 1 and c.actionid = " & ds.Tables(0).Rows(i)(0)
            oraCn.FillData(sql, ds1)

            'get abolished entries
            sql = "select pshr.get_org(d.loccode),pshr.get_desg(d.desgcode) from cadre.condiv d where d.action = 2 and d.actionid = " & ds.Tables(0).Rows(i)(0)
            oraCn.FillData(sql, ds2)

            c = Math.Max(ds1.Tables(0).Rows.Count, ds2.Tables(0).Rows.Count)

            'loop for created and abolished entries for a particular actionid
            For j = 0 To c - 1
                Dim dr As System.Data.DataRow = dt.NewRow()
                dr("LocationC") = ""
                dr("DesignationC") = ""
                dr(separator) = ""
                dr("LocationA") = ""
                dr("DesignationA") = ""

                If ds1.Tables(0).Rows.Count > j Then
                    dr("LocationC") = ds1.Tables(0).Rows(j)(0)
                    dr("DesignationC") = ds1.Tables(0).Rows(j)(1)
                End If
                If ds2.Tables(0).Rows.Count > j Then
                    dr("LocationA") = ds2.Tables(0).Rows(j)(0)
                    dr("DesignationA") = ds2.Tables(0).Rows(j)(1)
                End If
                dt.Rows.Add(dr)
            Next j

            Dim dr1 As System.Data.DataRow = dt.NewRow()
            dr1("LocationC") = ""
            dr1("DesignationC") = ""
            dr1(separator) = ""
            dr1("LocationA") = ""
            dr1("DesignationA") = ""

            dt.Rows.Add(dr1)
            ds1.Clear()
            ds1.Dispose()
            ds2.Clear()
            ds2.Dispose()
        Next i
        dt.AcceptChanges()
        gvmap.DataSource = dt
        gvmap.DataBind()
        'gvmap.Columns(5).ItemStyle.BackColor = Drawing.Color.Aqua
    End Sub
    Private Sub MapPosts()
        Dim i As Integer
        Dim blchecked As Boolean
        Dim strcode As String
        Dim actionid, rno As Integer
        Dim dtableC As New System.Data.DataTable
        Dim dtableA As New System.Data.DataTable
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet

        dtableC.Columns.Add("Checked", System.Type.GetType("System.Boolean"))
        dtableC.Columns.Add("Location", System.Type.GetType("System.String"))
        dtableC.Columns.Add("Designation", System.Type.GetType("System.String"))
        dtableC.Columns.Add("Branch", System.Type.GetType("System.String"))
        dtableC.Columns.Add("Scale", System.Type.GetType("System.String"))
        dtableC.Columns.Add("PType", System.Type.GetType("System.String"))
        dtableC.Columns.Add("Code", System.Type.GetType("System.String"))

        dtableA.Columns.Add("Checked", System.Type.GetType("System.Boolean"))
        dtableA.Columns.Add("Location", System.Type.GetType("System.String"))
        dtableA.Columns.Add("Designation", System.Type.GetType("System.String"))
        dtableA.Columns.Add("Code", System.Type.GetType("System.String"))

        'get new actionID
        actionid = GetActionID()

        'add posts to create to condiv with action = 1
        For i = 0 To gvcreate.Rows.Count - 1
            blchecked = DirectCast(gvcreate.Rows(i).Cells(0).Controls(1), CheckBox).Checked
            ds = New System.Data.DataSet
            sql = "select max(rowno) from cadre.condiv where action  = 1 and actionid = " & actionid
            oracn.FillData(sql, ds)
            rno = 0
            If Not IsDBNull(ds.Tables(0).Rows(0)(0)) Then
                rno = ds.Tables(0).Rows(0)(0) + 1
            End If
            If blchecked = True Then
                strcode = DirectCast(gvcreate.Rows(i).Cells(6).Controls(1), System.Web.UI.WebControls.Label).Text
                AddCreatePosts(strcode, actionid, rno)
            Else
                Dim drC As System.Data.DataRow = dtableC.NewRow()
                drC("Checked") = DirectCast(gvcreate.Rows(i).Cells(0).Controls(1), CheckBox).Checked
                drC("Location") = DirectCast(gvcreate.Rows(i).Cells(1).Controls(1), System.Web.UI.WebControls.Label).Text
                drC("Designation") = DirectCast(gvcreate.Rows(i).Cells(2).Controls(1), System.Web.UI.WebControls.Label).Text
                drC("Branch") = DirectCast(gvcreate.Rows(i).Cells(3).Controls(1), System.Web.UI.WebControls.Label).Text
                drC("Scale") = DirectCast(gvcreate.Rows(i).Cells(4).Controls(1), System.Web.UI.WebControls.Label).Text
                drC("PType") = DirectCast(gvcreate.Rows(i).Cells(5).Controls(1), System.Web.UI.WebControls.Label).Text
                drC("Code") = DirectCast(gvcreate.Rows(i).Cells(6).Controls(1), System.Web.UI.WebControls.Label).Text
                dtableC.Rows.Add(drC)
            End If
        Next i

        'add posts to abolish to condiv with action = 2
        For i = 0 To gvabolish.Rows.Count - 1
            ds = New System.Data.DataSet
            blchecked = DirectCast(gvabolish.Rows(i).Cells(0).Controls(1), CheckBox).Checked
            sql = "select max(rowno) from cadre.condiv where action  = 2 and actionid = " & actionid
            oracn.FillData(sql, ds)
            rno = 0
            If Not IsDBNull(ds.Tables(0).Rows(0)(0)) Then
                rno = ds.Tables(0).Rows(0)(0) + 1
            End If
            If blchecked = True Then
                strcode = DirectCast(gvabolish.Rows(i).Cells(3).Controls(1), System.Web.UI.WebControls.Label).Text
                AddAbolishPosts(strcode, actionid, rno)
            Else
                Dim drA As System.Data.DataRow = dtableA.NewRow()
                drA("Checked") = DirectCast(gvabolish.Rows(i).Cells(0).Controls(1), CheckBox).Checked
                drA("Location") = DirectCast(gvabolish.Rows(i).Cells(1).Controls(1), System.Web.UI.WebControls.Label).Text
                drA("Designation") = DirectCast(gvabolish.Rows(i).Cells(2).Controls(1), System.Web.UI.WebControls.Label).Text
                drA("Code") = DirectCast(gvabolish.Rows(i).Cells(3).Controls(1), System.Web.UI.WebControls.Label).Text
                dtableA.Rows.Add(drA)
            End If
        Next i

        'assign other dts to grids
        dtableC.AcceptChanges()
        gvcreate.DataSource = dtableC
        gvcreate.DataBind()

        dtableA.AcceptChanges()
        gvabolish.DataSource = dtableA
        gvabolish.DataBind()

        'show condiv table in gvmap
        ShowConDiv()
    End Sub
    Protected Sub btnProceed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProceed.Click
        btnProceed.Visible = False
        If lblStepNum.Text = "1" Then
            'move to step 2 - select posts to abolish
            Panel_Branch.Visible = False
            Label4.Visible = False  'label 'Existing'
            Label8.Visible = False  'label existing count
            Pan_Create.Visible = False
            Pan_Abolish.Visible = True
            'btnProceed.Visible = False
            drporgtype.Items.Clear()
            drporg.Items.Clear()
            drpcir.Items.Clear()
            drpdiv.Items.Clear()
            drpsubdiv.Items.Clear()
            drpdesg.Items.Clear()
            drpsuboff.Items.Clear()
            get_ce()
            lblStepNum.Text = "2"
            lblStepDes.Text = " of 4 : Select posts to abolish"
            btnProceed.Text = "Proceed to Map Posts"
        ElseIf lblStepNum.Text = "2" Then
            'move to step 3 - map posts
            Pan_Create.Visible = True
            Pan_Map.Visible = True
            Panel1.Visible = False
            btn_AddToGrid.Visible = False
            lblStepNum.Text = "3"
            lblStepDes.Text = " of 4 : Map the posts to create (left grid) with posts to abolish (right grid)"
            btnProceed.Visible = True
            btnProceed.Text = "Map Selected Posts"
        ElseIf lblStepNum.Text = "3" Then
            '3rd step, map posts
            btnProceed.Visible = True
            'btnGenRep.Visible = True
            MapPosts()
            If gvcreate.Rows.Count = 0 And gvabolish.Rows.Count = 0 Then
                'final step (step 4) - Save changes
                btnProceed.Visible = False
                gvcreate.Visible = False
                gvabolish.Visible = False
                lblStepNum.Text = "4"
                PanSave.Visible = True
                'lblRem.Visible = True
                'txtRem.Visible = True
                lblStepDes.Text = " of 4 : Now enter Remarks and click save to finalize changes"
            End If
        End If
    End Sub
    Private Function get_rloc() As Long
        Dim rloc As Long
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

    'Protected Sub DoSave()
    '    Dim oraCn As New OraDBconnection
    '    Dim ds As New System.Data.DataSet
    '    Dim sql As String
    '    Dim i As Integer
    '    Dim loccode As Long
    '    Dim desgcode As Integer
    '    Dim scale As Integer
    '    Dim branch As Integer
    '    Dim ptype As String
    '    Dim index As String
    '    Dim rno As Integer
    '    Dim action As Integer
    '    Dim actionid As Integer
    '    Dim oonum As Long

    '    If txtRem.Text = "" Then
    '        Utils.ShowMessage(Me, "Please enter remarks")
    '        Exit Sub
    '    End If

    '    oonum = Utils.GetOfficeOrderNum()

    '    sql = "select loccode,desgcode,scale,branch,ptype,action,indx,actionid from cadre.condiv order by actionid,action"
    '    oraCn.FillData(sql, ds)

    '    For i = 0 To ds.Tables(0).Rows.Count - 1
    '        loccode = ds.Tables(0).Rows(i)(0)
    '        desgcode = ds.Tables(0).Rows(i)(1)
    '        action = ds.Tables(0).Rows(i)(5)
    '        actionid = ds.Tables(0).Rows(i)(7)

    '        If action = 1 Then
    '            'create entries with action = 1
    '            scale = ds.Tables(0).Rows(i)(2)
    '            branch = ds.Tables(0).Rows(i)(3)
    '            ptype = ds.Tables(0).Rows(i)(4)

    '            'get new index to fill in
    '            index = Utils.GetDesgCount(loccode, desgcode) + 1
    '            rno = Utils.GetRowNum()

    '            sql = "insert into cadre.cadr  values(" & loccode & "," & desgcode & "," & index & "," & scale & ", " & rno & "," & branch & ",'" & ptype & "')"
    '            oraCn.ExecQry(sql)

    '            'insert into trans_hist
    '            sql = "insert into cadre.trans_hist values(" &
    '                        "0, " &
    '                        "0, " &
    '                        "0, " &
    '                        "-1, " &
    '                        rno & ", " &
    '                        "(select sysdate from dual), " &
    '                        "'ConDiv', " &
    '                        "'CC', " &
    '                        "NULL, " &
    '                        "NULL, " &
    '                        "NULL, " &
    '                        "'" & Session.SessionID & "', " &
    '                        actionid & ", " &
    '                        oonum &
    '                        ")"
    '            oraCn.ExecQry(sql)
    '        ElseIf action = 2 Then
    '            'abolish entries with action = 2
    '            Dim ds2 As New System.Data.DataSet

    '            'use index of post to be abolished
    '            index = ds.Tables(0).Rows(i)(6)

    '            'get old info of row to be abolished
    '            sql = "select rowno,scaleid,branch,ptype from cadre.cadr where loccode = " & loccode & " and desgcode = " & desgcode & " and indx = " & index
    '            oraCn.FillData(sql, ds2)

    '            'one and only one row should match the select criteria
    '            If ds2.Tables(0).Rows.Count <> 1 Then
    '                Utils.ShowMessage(Me, "An error has occurred")
    '                Exit Sub
    '            End If

    '            rno = ds2.Tables(0).Rows(0)(0)
    '            scale = ds2.Tables(0).Rows(0)(1)
    '            branch = ds2.Tables(0).Rows(0)(2)
    '            ptype = ds2.Tables(0).Rows(0)(3)

    '            sql = "delete from cadre.cadr where rowno = " & rno
    '            oraCn.ExecQry(sql)

    '            'add abolished row in trans_hist
    '            sql = "insert into cadre.trans_hist values(" &
    '                    loccode & "," &
    '                    desgcode & ", " &
    '                    index & ", " &
    '                    rno & ", " &
    '                    "-1, " &
    '                    "(select sysdate from dual), " &
    '                    "'ConDiv', " &
    '                    "'CA', " &
    '                    scale & ", " &
    '                    branch & ", " &
    '                    "'" & ptype & "', " &
    '                    "'" & Session.SessionID & "', " &
    '                    actionid & ", " &
    '                    oonum &
    '                    ")"
    '            oraCn.ExecQry(sql)
    '        End If
    '    Next
    '    Utils.ShowMessage(Me, "Conversion Diversion Completed Successfully")
    '    Response.Redirect("Mainpage.aspx")
    'End Sub
    Protected Sub drpdesg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdesg.SelectedIndexChanged
        Label8.Text = Utils.GetDesgCount(GetLocCode(), drpdesg.SelectedItem.Value.Split("-")(0))
        txtRem0.Text = ""
    End Sub

    Private Sub GenerateReport()
        Dim sql As String
        Dim i, r, cc, ac, rno As Integer
        Dim oracn As New OraDBconnection
        Dim ds, dsc, dsr, dsa As New System.Data.DataSet

        sql = "delete from cadre.divrpt"
        oracn.ExecQry(sql)

        sql = "select max(actionid) from cadre.condiv"
        oracn.FillData(sql, ds)

        'check if condiv is empty, then just exit
        If IsDBNull(ds.Tables(0).Rows(0)(0)) Then
            Return
        End If

        'Session("aid") = ds.Tables(0).Rows(0)(0)
        For i = 0 To ds.Tables(0).Rows(0)(0)
            dsc = New System.Data.DataSet
            sql = " select count(*) from cadre.condiv where action = 1 and actionid =" & i
            oracn.FillData(sql, dsc)
            cc = dsc.Tables(0).Rows(0)(0)
            dsc.Clear()
            dsc.Dispose()
            dsa = New System.Data.DataSet
            sql = " select count(*) from cadre.condiv where action = 2 and actionid =" & i
            oracn.FillData(sql, dsa)
            ac = dsa.Tables(0).Rows(0)(0)
            dsa.Clear()
            dsa.Dispose()

            If cc >= ac Then
                dsc = New System.Data.DataSet
                sql = " SELECT LOCCODE, DESGCODE, COUNT(*) as ccnt, remarks FROM CADRE.condiv where action  = 1 and actionid = " & i & " group by loccode, desgcode,remarks"
                oracn.FillData(sql, dsc)
                r = 0

                For r = 0 To dsc.Tables(0).Rows.Count - 1
                    dsr = New System.Data.DataSet
                    sql = "select max(rowno) from cadre.divrpt"
                    oracn.FillData(sql, dsr)
                    rno = 0

                    If Not IsDBNull(dsr.Tables(0).Rows(0)(0)) Then
                        rno = dsr.Tables(0).Rows(0)(0) + 1
                    End If
                    dsr.Clear()
                    dsr.Dispose()

                    sql = "insert into cadre.divrpt (cloccode, cdesgcode, ccount,actionid,rowno,remarks) values ( " & dsc.Tables(0).Rows(r)(0) & " , " & dsc.Tables(0).Rows(r)(1) & "," & dsc.Tables(0).Rows(r)(2) & "," & i & "," & rno & ",'" & dsc.Tables(0).Rows(r)(3) & "' )"
                    oracn.ExecQry(sql)
                Next r

                dsa = New System.Data.DataSet
                dsr = New System.Data.DataSet
                sql = "SELECT LOCCODE, DESGCODE, COUNT(*) as acnt FROM CADRE.condiv where action  = 2 and actionid = " & i & " group by loccode, desgcode"
                oracn.FillData(sql, dsa)

                sql = "select min(rowno) from cadre.divrpt where actionid = " & i
                oracn.FillData(sql, dsr)
                rno = dsr.Tables(0).Rows(0)(0)

                For r = 0 To dsa.Tables(0).Rows.Count - 1
                    sql = "update cadre.divrpt set aloccode = " & dsa.Tables(0).Rows(r)(0) & ", adesgcode = " & dsa.Tables(0).Rows(r)(1) & ", acount = " & dsa.Tables(0).Rows(r)(2) & " where rowno = " & rno
                    oracn.ExecQry(sql)
                    rno = rno + 1
                Next r
            ElseIf ac > cc Then
                dsa = New System.Data.DataSet
                sql = " SELECT LOCCODE, DESGCODE, COUNT(*) as acnt FROM CADRE.condiv where action  = 2 and actionid = " & i & " group by loccode, desgcode"
                oracn.FillData(sql, dsa)
                r = 0

                For r = 0 To dsa.Tables(0).Rows.Count - 1
                    dsr = New System.Data.DataSet
                    sql = "select max(rowno) from cadre.divrpt"
                    oracn.FillData(sql, dsr)
                    rno = 0

                    If Not IsDBNull(dsr.Tables(0).Rows(0)(0)) Then
                        rno = dsr.Tables(0).Rows(0)(0) + 1
                    End If
                    dsr.Clear()
                    dsr.Dispose()

                    sql = "insert into cadre.divrpt (aloccode, adesgcode, acount,actionid,rowno) values ( " & dsa.Tables(0).Rows(r)(0) & " , " & dsa.Tables(0).Rows(r)(1) & "," & dsa.Tables(0).Rows(r)(2) & "," & i & "," & rno & " )"
                    oracn.ExecQry(sql)
                Next r
                dsc = New System.Data.DataSet

                sql = "SELECT LOCCODE, DESGCODE, COUNT(*) as acnt,remarks FROM CADRE.condiv where action  = 1 and actionid = " & i & " group by loccode, desgcode,remarks"
                oracn.FillData(sql, dsc)
                dsr = New System.Data.DataSet
                sql = "select min(rowno) from cadre.divrpt where actionid = " & i
                oracn.FillData(sql, dsr)
                rno = dsr.Tables(0).Rows(0)(0)

                For r = 0 To dsc.Tables(0).Rows.Count - 1
                    sql = "update cadre.divrpt set cloccode = " & dsc.Tables(0).Rows(r)(0) & ", cdesgcode = " & dsc.Tables(0).Rows(r)(1) & ", ccount = " & dsc.Tables(0).Rows(r)(2) & ", remarks='" & dsc.Tables(0).Rows(r)(3) & "' where rowno = " & rno
                    oracn.ExecQry(sql)
                    rno = rno + 1
                Next r
            End If
        Next i
        ds.Dispose()
    End Sub
    Protected Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        'If Session("condiv_cclist") <> "1" Then
        '    Utils.ShowMessage(Me, "Create CC list first")
        '    Return
        'End If
        GenerateReport()
        DoReport("condiv-0")
        'Response.Redirect("rpt_con_div.aspx")
        'Response.Write("<script type='text/javascript'>detailedresults=window.open('rpt_con_div.aspx','_new');</script>")
    End Sub
    Private Sub DoReport(ByVal oonum As String)
        Dim rptFile As String = String.Empty
        Dim sql As String
        Dim oraCn As New OraDBconnection
        Dim ds, ds2, ds3 As New Data.DataSet
        Dim CrystalReportSource1 As New CrystalDecisions.Web.CrystalReportSource()
        Dim sysdate As String

        rptFile = "Reports\\rptcondiv.rpt"
        sql = " select pshr.get_org(cloccode),pshr.get_desg(cdesgcode), ccount,pshr.get_org(aloccode), pshr.get_desg(adesgcode),acount, actionid,oonum,filenum,odate,remarks from cadre.divrpt order by rowno"
        oraCn.FillData(sql, ds)
        CrystalReportSource1.Report.FileName = Server.MapPath(rptFile)
        CrystalReportSource1.ReportDocument.SetDatabaseLogon("pshr", "pshr")
        CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
        CrystalReportSource1.DataBind()

        sql = "select * from cadre.cclist where ccnum > 0 order by ccnum"
        oraCn.FillData(sql, ds2)
        CrystalReportSource1.ReportDocument.Subreports(0).SetDataSource(ds2.Tables(0))
        CrystalReportSource1.DataBind()

        'get sysdate
        oraCn.FillData("select to_char(sysdate,'dd-mm-yyyy') from dual", ds3)
        sysdate = ds3.Tables(0).Rows(0)(0)

        Dim pdfPath As String
        pdfPath = Server.MapPath("office_orders\\" & oonum & "-" & sysdate & ".pdf")
        Session("pdfpath") = pdfPath

        'save office order at server
        CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath)

        'Download report
        DownloadFile(pdfPath)

        oraCn.ExecQry("delete from cadre.condiv")
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
    Protected Sub DoSave(ByVal oonum As Long)
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim loccode As Long
        Dim desgcode As Integer
        Dim scale As Integer
        Dim branch As Integer
        Dim ptype As String
        Dim index As String
        Dim rno As Integer
        Dim action As Integer
        Dim actionid As Integer
        Dim remarks As String

        If txtRem.Text = "" Then
            Utils.ShowMessage(Me, "Please enter remarks")
            Exit Sub
        End If

        sql = "select loccode,desgcode,scale,branch,ptype,action,indx,actionid,remarks from cadre.condiv order by actionid,action"
        oraCn.FillData(sql, ds)

        For i = 0 To ds.Tables(0).Rows.Count - 1
            loccode = ds.Tables(0).Rows(i)(0)
            desgcode = ds.Tables(0).Rows(i)(1)
            action = ds.Tables(0).Rows(i)(5)
            actionid = ds.Tables(0).Rows(i)(7)

            If action = 1 Then
                'create entries with action = 1
                scale = ds.Tables(0).Rows(i)(2)
                branch = ds.Tables(0).Rows(i)(3)
                ptype = ds.Tables(0).Rows(i)(4)
                'remarks = ds.Tables(0).Rows(i)(8)
                remarks = txtRem.Text
                'get new index to fill in
                index = Utils.GetDesgCount(loccode, desgcode) + 1
                rno = Utils.GetRowNum()

                sql = "insert into cadre.cadr values(" & loccode & "," & desgcode & "," & index & "," & scale & ", " & rno & "," & branch & ",'" & ptype & "','A',NULL,'" & remarks & "',0)"
                oraCn.ExecQry(sql)

                'insert into trans_hist
                sql = "insert into cadre.trans_hist values(" &
                            "0, " &
                            "0, " &
                            "0, " &
                            "-1, " &
                            rno & ", " &
                            "(select sysdate from dual), " &
                            "'ConDiv', " &
                            "'CC', " &
                            "NULL, " &
                            "NULL, " &
                            "NULL, " &
                            "'" & Session.SessionID & "', " &
                            actionid & ", " &
                            oonum &
                            ")"
                oraCn.ExecQry(sql)
            ElseIf action = 2 Then
                'abolish entries with action = 2
                Dim ds2 As New System.Data.DataSet

                'use index of post to be abolished
                index = ds.Tables(0).Rows(i)(6)

                'get old info of row to be abolished
                sql = "select rowno,scaleid,branch,ptype from cadre.cadr where loccode = " & loccode & " and desgcode = " & desgcode & " and indx = " & index
                oraCn.FillData(sql, ds2)

                'one and only one row should match the select criteria
                If ds2.Tables(0).Rows.Count <> 1 Then
                    Utils.ShowMessage(Me, "An error has occurred")
                    Exit Sub
                End If

                rno = ds2.Tables(0).Rows(0)(0)
                scale = ds2.Tables(0).Rows(0)(1)
                branch = ds2.Tables(0).Rows(0)(2)
                ptype = ds2.Tables(0).Rows(0)(3)

                sql = "delete from cadre.cadr where rowno = " & rno
                oraCn.ExecQry(sql)

                'add abolished row in trans_hist
                sql = "insert into cadre.trans_hist values(" &
                        loccode & "," &
                        desgcode & ", " &
                        index & ", " &
                        rno & ", " &
                        "-1, " &
                        "(select sysdate from dual), " &
                        "'ConDiv', " &
                        "'CA', " &
                        scale & ", " &
                        branch & ", " &
                        "'" & ptype & "', " &
                        "'" & Session.SessionID & "', " &
                        actionid & ", " &
                        oonum &
                        ")"
                oraCn.ExecQry(sql)
            End If
        Next
        'Utils.ShowMessage(Me, "Conversion Diversion Completed Successfully")
        'Response.Redirect("Mainpage.aspx")
    End Sub
    Protected Sub btnFinalSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalSave.Click
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim oonum As Long
        Dim sql As String

        If Session("condiv_cclist") <> "1" Then
            Utils.ShowMessage(Me, "Create CC list first")
            Return
        End If

        'generate O/o
        oonum = Utils.GetOfficeOrderNum()

        'generate report
        GenerateReport()

        'update oonum,filenum and odate in divrpt table
        sql = "update cadre.divrpt set oonum= " & oonum & ", filenum= 'Cadre', odate = (select sysdate from dual)"
        oraCn.FillData(sql, ds)

        'save in cadre and save in trans_hist with o/o
        DoSave(oonum)

        'postback report with o/o
        'Response.Redirect("rpt_con_div.aspx")
        'Response.Write("<script type='text/javascript'>detailedresults=window.open('rpt_con_div.aspx','_new');</script>")
        DoReport(oonum)
        'Utils.ShowMessage(Me, "Still running")
    End Sub
    Protected Sub btnCreateCC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateCC.Click
        Response.Write("<script type='text/javascript'>detailedresults=window.open('cc.aspx','_new');</script>")
        Session("condiv_cclist") = "1"
    End Sub

    Protected Sub btnDelSess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelSess.Click
        Dim oraCn As New OraDBconnection
        'delete any existing rows in condiv
        oraCn.ExecQry("delete from cadre.condiv")
        lblPending.Text = ""
        Utils.ShowMessage(Me, "Session Deleted")
    End Sub

    Protected Sub drpsuboff_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpsuboff.SelectedIndexChanged
        filldesg()
    End Sub
End Class

