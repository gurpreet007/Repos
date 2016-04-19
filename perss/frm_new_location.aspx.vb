'create table cadre.temp_mastloc (loccode number(9) not null, reploc number(9), action char(1) not null)
Partial Class frm_new_location
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
        End If
    End Sub

    Private Sub EnableAllButtons()
        btnorg.Enabled = True
        btncir.Enabled = True
        btndiv.Enabled = True
        btnsdiv.Enabled = True
        btnsoff.Enabled = True
    End Sub
    Private Sub DisableButtons(ByVal clickedBtnStr As String)
        btnorg.Enabled = False
        btncir.Enabled = False
        btndiv.Enabled = False
        btnsdiv.Enabled = False
        btnsoff.Enabled = False
        If (clickedBtnStr = "btnorg") Then
            btnorg.Enabled = True
        ElseIf ((clickedBtnStr = "btncir")) Then
            btncir.Enabled = True
        ElseIf ((clickedBtnStr = "btndiv")) Then
            btndiv.Enabled = True
        ElseIf ((clickedBtnStr = "btnsdiv")) Then
            btnsdiv.Enabled = True
        ElseIf ((clickedBtnStr = "btnsoff")) Then
            btnsoff.Enabled = True
        End If
    End Sub

    Protected Sub btnorg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnorg.Click
        If Me.drpcorp.SelectedValue = "0" Then
            Utils.ShowMessage(Me, "Please Select Organisation Type")
            Exit Sub
        End If
        If btnorg.Text <> "Save" Then
            DisableButtons("btnorg")
            Me.drporg.Visible = False
            Me.txtorg.Visible = True
            Me.txtabbr.Visible = True
            Me.lbloabr.Visible = True
            Me.btnorg.Text = "Save"
            If Me.drpcorp.SelectedValue = "803000000" Then
                Me.txtorg.Text = "DS ZONE"
                Me.txtabbr.Text = "CE"
            Else
                Me.txtorg.Text = "CHIEF ENGINEER"
                Me.txtabbr.Text = "CE"
            End If
            drpcir.Items.Clear()
            drpdiv.Items.Clear()
            drpsdiv.Items.Clear()
        Else
            Dim oraCn As New OraDBconnection
            Dim ds As New System.Data.DataSet
            Dim sql As String
            Dim i As Long
            Dim rloc As Double
            rloc = get_rloc()

            sql = "select NVL(max(loccode),0) from pshr.mast_loc where loccode like "

            'Me.drpcorp.Items.Add(New ListItem("CMD/PSPCL", "800000000"))
            'Me.drpcorp.Items.Add(New ListItem("Director/Distribution", "803000000"))
            'Me.drpcorp.Items.Add(New ListItem("Director/Generation", "802000000"))
            'Me.drpcorp.Items.Add(New ListItem("Director/Finance", "804000000"))
            'Me.drpcorp.Items.Add(New ListItem("Director/Commercial", "805000000"))
            'Me.drpcorp.Items.Add(New ListItem("Director/HR", "806000000"))
            'Me.drpcorp.Items.Add(New ListItem("Director/Admin", "801000000"))
            'Me.drpcorp.Items.Add(New ListItem("Deputation", "600000000"))
            'Me.drpcorp.Items.Add(New ListItem("Ombudsman", "810000000"))

            If Me.RadioButtonList2.SelectedValue = "P" Then
                Select Case (Me.drpcorp.SelectedValue)
                    Case 803000000 'Director/Distribution
                        sql &= "'3%'"
                    Case 802000000 'Director/Generation
                        sql &= "'1%'"
                    Case 805000000 'Director/Commercial
                        sql &= "'4%'"
                    Case 806000000 'Director/HR
                        sql &= "'8%'"
                    Case 801000000 'Director/Admin
                        sql &= "'4%'"
                    Case 600000000 'Deputation
                        sql &= "'6%'"
                    Case 804000000 'Director/Finance
                        sql &= "'5%'"
                    Case 800000000 'CMD/PSPCL
                        sql &= "'111%'"
                End Select
            ElseIf Me.RadioButtonList2.SelectedValue = "T" Then
                Select Case (Me.drpcorp.SelectedValue)
                    Case 503000000 'CMD/PSTCL
                        sql &= "'2%'"
                    Case 503000001 'Director/Technical
                        sql &= "'2%'"
                    Case 503000002 'Director/Finance & Commercial
                        sql &= "'2%'"
                    Case 503000004 'Director/Admin
                        sql &= "'2%'"
                End Select
            End If
            sql &= " and loccode like '___000000'"

            oraCn.FillData(sql, ds)

            If (drpcorp.SelectedValue = 800000000 And
                IsDBNull(ds.Tables(0).Rows(0)(0))) Then
                i = 111000000
            Else
                i = Double.Parse(ds.Tables(0).Rows(0)(0).ToString()) + 1000000
            End If

            ds.Clear()
            ds.Dispose()

            sql = "insert into pshr.mast_loc values ( " & i & " ,'" & Me.txtorg.Text & "',20," & rloc & ", '" & GetCorpCode(drpcorp.SelectedValue) & "',null,99,0,'" & Me.txtabbr.Text & "','" & Me.txtorg.Text & "','" & GetCorpCode(drpcorp.SelectedValue) & "',null,1)"
            oraCn.ExecQry(sql)

            'remember the new addition
            sql = "INSERT INTO cadre.temp_mastloc VALUES(" & i & " ,NULL, 'C')"
            oraCn.ExecQry(sql)

            Me.txtorg.Visible = False
            Me.txtabbr.Visible = False
            Me.lbloabr.Visible = False
            Me.drporg.Visible = True
            Me.btnorg.Text = "Add"
            'Utils.ShowMessage(Me, "Organization Added Successfully")
            fillorg()
            EnableAllButtons()
        End If
    End Sub
    Protected Sub btncir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncir.Click
        'If Me.drporg.SelectedValue = "0" Then
        '    Utils.ShowMessage(Me, "Please Select The Organisaion to Add New Circle")
        '    Exit Sub
        'End If
        If Me.drpcorp.SelectedValue = "0" Then
            Utils.ShowMessage(Me, "Please Select Organisation Type")
            Exit Sub
        End If
        If btncir.Text <> "Save" Then
            DisableButtons("btncir")
            Me.drpcir.Visible = False
            Me.txtcir.Visible = True
            Me.btncir.Text = "Save"
            Me.txtcabbr.Visible = True
            Me.lblcabr.Visible = True
            If Me.drpcorp.SelectedValue = "803000000" Then
                Me.txtcir.Text = "DS CIRCLE"
                Me.txtcabbr.Text = "SE"
            Else
                Me.txtcir.Text = "SUPERINTENDENT ENGINEER"
                Me.txtcabbr.Text = "SE"
            End If
            drpdiv.Items.Clear()
            drpsdiv.Items.Clear()
        Else
            Dim oraCn As New OraDBconnection
            Dim ds As New System.Data.DataSet
            Dim sql As String
            Dim i As Long
            Dim rloc As Long
            sql = ""
            rloc = get_rloc()
            sql = "select max(loccode) from pshr.mast_loc where loccode like '" & Mid$(rloc, 1, 3) & "%0000' "

            oraCn.FillData(sql, ds)
            i = ds.Tables(0).Rows(0)(0).ToString()
            i = ds.Tables(0).Rows(0)(0).ToString() + 10000

            ds.Clear()
            ds.Dispose()

            sql = "insert into pshr.mast_loc values ( " & i & ",'" & Me.txtcir.Text & "',30," & rloc & ", '" & GetCorpCode(drpcorp.SelectedValue) & "',null,99,0,'" & Me.txtcabbr.Text & "','" & Me.txtcir.Text & "','" & GetCorpCode(drpcorp.SelectedValue) & "',null,1)"
            oraCn.ExecQry(sql)
            'remember the new addition
            sql = "INSERT INTO cadre.temp_mastloc VALUES(" & i & " ,NULL, 'C')"
            oraCn.ExecQry(sql)


            Me.txtcir.Visible = False
            Me.txtcabbr.Visible = False
            Me.lblcabr.Visible = False
            Me.drpcir.Visible = True
            'Utils.ShowMessage(Me, "Circle Added Succesfully")
            Me.btncir.Text = "Add"
            fillcir()
            EnableAllButtons()
        End If
    End Sub
    Protected Sub btndiv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndiv.Click
        'If Me.drporg.SelectedValue = "0" Then
        '    Utils.ShowMessage(Me, "Please Select The Organisaion/Circle to Add New Division")
        '    Exit Sub
        'End If
        If Me.drpcorp.SelectedValue = "0" Then
            Utils.ShowMessage(Me, "Please Select Organisation Type")
            Exit Sub
        End If
        If btndiv.Text <> "Save" Then
            DisableButtons("btndiv")
            Me.drpdiv.Visible = False
            Me.txtdiv.Visible = True
            Me.btndiv.Text = "Save"
            Me.txtdabbr.Visible = True
            Me.lbldabbr.Visible = True

            If Me.drpcorp.SelectedValue = "803000000" Then
                Me.txtdiv.Text = "DS DIVISION"
                Me.txtdabbr.Text = "SR.XEN"
            Else
                Me.txtdiv.Text = "SR.XEN"
                Me.txtdabbr.Text = "SR.XEN"
            End If
            drpsdiv.Items.Clear()
        Else
            Dim oraCn As New OraDBconnection
            Dim ds As New System.Data.DataSet
            Dim sql As String
            Dim i As Long
            Dim rloc As Long
            sql = ""
            rloc = get_rloc()
            If Left(rloc, 3) = 502 Then
                sql = "select max(loccode) from pshr.mast_loc where loccode like '" & Mid$(rloc, 1, 5) & "%00'"
            Else
                sql = "select max(loccode) from pshr.mast_loc where loccode like '" & Mid$(rloc, 1, 5) & "%00' and loctype = 40"
            End If

            oraCn.FillData(sql, ds)
            If Not IsDBNull(ds.Tables(0).Rows(0)(0)) Then
                i = ds.Tables(0).Rows(0)(0) + 100
            Else
                i = rloc + 100
            End If

            ds.Clear()
            ds.Dispose()

            sql = "insert into pshr.mast_loc values ( " & i & " ,'" & Me.txtdiv.Text & "',40," & rloc & ",'" & GetCorpCode(drpcorp.SelectedValue) & "',null,99,0,'" & Me.txtdabbr.Text & "','" & Me.txtdiv.Text & "','" & GetCorpCode(drpcorp.SelectedValue) & "',null,1)"
            oraCn.ExecQry(sql)

            'remember the new addition
            sql = "INSERT INTO cadre.temp_mastloc VALUES(" & i & " ,NULL, 'C')"

            oraCn.ExecQry(sql)

            Me.txtdiv.Visible = False
            Me.txtdabbr.Visible = False
            Me.lbldabbr.Visible = False
            Me.drpdiv.Visible = True
            Me.btndiv.Text = "Add"
            'Utils.ShowMessage(Me, "Division Added Successfully")
            filldiv()
            EnableAllButtons()
        End If
    End Sub
    Protected Sub btnsdiv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsdiv.Click
        'If Me.drporg.SelectedValue = "0" Then
        '    Utils.ShowMessage(Me, "Please Select The Organisaion/Circle/Division to Add New SubDivision")
        '    Exit Sub
        'End If
        If Me.drpcorp.SelectedValue = "0" Then
            Utils.ShowMessage(Me, "Please Select Organisation Type")
            Exit Sub
        End If
        If btnsdiv.Text <> "Save" Then
            DisableButtons("btnsdiv")
            Me.drpsdiv.Visible = False
            Me.txtsdiv.Visible = True
            Me.btnsdiv.Text = "Save"
            Me.txtsdabr.Visible = True
            Me.lblsdabr.Visible = True

            If Me.drpcorp.SelectedValue = "803000000" Then
                Me.txtsdiv.Text = "DS SUB DIVISION"
                Me.txtsdabr.Text = "AEE"
            Else
                Me.txtsdiv.Text = "AEE"
                Me.txtsdabr.Text = "AEE"
            End If
        Else
            Dim oraCn As New OraDBconnection
            Dim ds As New System.Data.DataSet
            Dim sql As String
            Dim i As Long
            Dim rloc As Long
            sql = ""
            rloc = get_rloc()

            sql = "select max(loccode) from pshr.mast_loc where loccode like '" & Mid$(rloc, 1, 7) & "%'"
            oraCn.FillData(sql, ds)
            i = ds.Tables(0).Rows(0)(0).ToString() + 1
            ds.Clear()
            ds.Dispose()

            sql = "insert into pshr.mast_loc values ( " & i & " ,'" & Me.txtsdiv.Text & "',50," & rloc & " , '" & GetCorpCode(drpcorp.SelectedValue) & "',null,99,0,'" & Me.txtsdabr.Text & "','" & Me.txtsdiv.Text & "','" & GetCorpCode(drpcorp.SelectedValue) & "',null,1)"
            oraCn.ExecQry(sql)
            'remember the new addition
            sql = "INSERT INTO cadre.temp_mastloc VALUES(" & i & " ,NULL, 'C')"
            oraCn.ExecQry(sql)

            Me.txtsdiv.Visible = False
            Me.txtsdabr.Visible = False
            Me.lblsdabr.Visible = False
            Me.drpsdiv.Visible = True
            Me.btnsdiv.Text = "Add"
            'Utils.ShowMessage(Me, "Subdivision added successfully")
            fillsdiv()
            EnableAllButtons()
        End If
    End Sub
    Protected Sub btnsoff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsoff.Click
        If Me.drpcorp.SelectedValue = "0" Then
            Utils.ShowMessage(Me, "Please Select Organisation Type")
            Exit Sub
        End If
        If btnsoff.Text <> "Save" Then
            DisableButtons("btnsoff")
            Me.drpsoff.Visible = False
            Me.txtsoff.Visible = True
            Me.btnsoff.Text = "Save"
            Me.txtsoffabr.Visible = True
            Me.lblsoffabr.Visible = True

            If Me.drpcorp.SelectedValue = "803000000" Then
                Me.txtsoff.Text = "SUB OFFICE"
                Me.txtsoffabr.Text = "SUB OFF"
            Else
                Me.txtsoff.Text = "SUB OFFICE"
                Me.txtsoffabr.Text = "SUB OFF"
            End If
        Else
            Dim oraCn As New OraDBconnection
            Dim ds As New System.Data.DataSet
            Dim sql As String
            Dim i As Long
            Dim rloc As Long
            sql = ""
            rloc = get_rloc()

            sql = "select max(loccode) from pshr.mast_loc where loccode like '" & Mid$(rloc, 1, 7) & "%'"
            oraCn.FillData(sql, ds)
            i = ds.Tables(0).Rows(0)(0).ToString() + 1
            ds.Clear()
            ds.Dispose()

            sql = "insert into pshr.mast_loc values ( " & i & " ,'" & Me.txtsoff.Text & "',60," & rloc & " , '" & GetCorpCode(drpcorp.SelectedValue) & "',null,99,0,'" & Me.txtsoffabr.Text & "','" & Me.txtsoff.Text & "','" & GetCorpCode(drpcorp.SelectedValue) & "',null,1)"
            oraCn.ExecQry(sql)
            'remember the new addition
            sql = "INSERT INTO cadre.temp_mastloc VALUES(" & i & " ,NULL, 'C')"
            oraCn.ExecQry(sql)

            Me.txtsoff.Visible = False
            Me.txtsoffabr.Visible = False
            Me.lblsoffabr.Visible = False
            Me.drpsoff.Visible = True
            Me.btnsoff.Text = "Add"
            'Utils.ShowMessage(Me, "Subdivision added successfully")
            fillsoff()
            EnableAllButtons()
        End If
    End Sub

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

    Private Function GetCorpCode(ByVal loccode As Long) As String
        Dim gtdc As String
        gtdc = "U" 'unknown
        Select Case loccode
            Case 800000000
                gtdc = "P"
            Case 803000000
                gtdc = "D"
            Case 802000000
                gtdc = "G"
            Case 804000000
                gtdc = "F"
            Case 805000000
                gtdc = "C"
            Case 806000000
                gtdc = "H"
            Case 801000000
                gtdc = "A"
            Case 503000000
                gtdc = "T"
            Case 503000001
                gtdc = "T"
            Case 503000002
                gtdc = "T"
            Case 503000004
                gtdc = "T"
        End Select
        Return gtdc
    End Function
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

    Protected Sub drpcorp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcorp.SelectedIndexChanged
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
    End Sub
    Protected Sub drporg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drporg.SelectedIndexChanged
        drpcir.Items.Clear()
        drpdiv.Items.Clear()
        drpsdiv.Items.Clear()
        drpsoff.Items.Clear()
        fillcir()
        filldiv()
        fillsdiv()
        fillsoff()
        ' filldesg()
    End Sub
    Protected Sub drpcir_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcir.SelectedIndexChanged
        drpdiv.Items.Clear()
        drpsdiv.Items.Clear()
        drpsoff.Items.Clear()
        filldiv()
        fillsdiv()
        fillsoff()
        ' filldesg()
        'get_vals()
    End Sub
    Protected Sub drpdiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdiv.SelectedIndexChanged
        drpsdiv.Items.Clear()
        drpsoff.Items.Clear()
        fillsdiv()
        fillsoff()
        'get_vals()
    End Sub
    Protected Sub drpsdiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpsdiv.SelectedIndexChanged
        drpsoff.Items.Clear()
        fillsoff()
    End Sub

    Protected Sub RadioButtonList2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList2.SelectedIndexChanged
        get_ce()
        Me.drporg.Items.Clear()
        drpcir.Items.Clear()
        drpdiv.Items.Clear()
        drpsdiv.Items.Clear()
    End Sub

    Private Sub GenerateReport(ByVal oonum As Integer, ByVal filename As String)
        If oonum = 0 Then
        Else

        End If
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim oonum As New Integer

        oonum = Utils.GetOfficeOrderNum()

        GenerateReport(oonum, "Cadre")

        sql = "DELETE FROM cadre.temp_mastloc"
        oracn.ExecQry(sql)

    End Sub
    Protected Sub btnUndo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo.Click
        Dim sql As String
        Dim oracn As New OraDBconnection

        sql = "DELETE FROM pshr.mast_loc WHERE loccode IN (SELECT loccode FROM cadre.temp_mastloc WHERE action='C')"
        oracn.ExecQry(sql)

        sql = "DELETE FROM cadre.temp_mastloc"
        oracn.ExecQry(sql)

        drporg.ClearSelection()
        fillorg()
        fillcir()
        filldiv()
        fillsdiv()
    End Sub

    Protected Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        GenerateReport(0, "")
    End Sub

    
    
  
End Class