
Partial Class frm_new_desg
    Inherits System.Web.UI.Page

    Private Sub fillcadr()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        sql = "select cadrecode, cadrename from pshr.mast_cadre order by cadrecode "
        oraCn.FillData(sql, ds)
        drpcadr.Items.Clear()
        drpcadr.Items.Add(New ListItem("Select", 0))
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Me.drpcadr.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
        Next
        ds.Clear()
        ds.Dispose()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Utils.AuthenticateUser(Me)
        If Not Page.IsPostBack Then
            fillcadr()
            'DoUndo()
        End If
    End Sub
    Private Function GetMaxHeCode() As Integer
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sqldesg As String
        Dim maxhecode As Integer
        sqldesg = "select max(hecode) from mast_desg where" _
                            & " servcode = " & drpserv.SelectedValue.ToString() _
                            & " And gazcode = " & drpgngz.SelectedValue.ToString()
        oraCn.FillData(sqldesg, ds)
        If IsDBNull(ds.Tables(0).Rows(0)(0)) Then
            Return 0
        End If

        maxhecode = ds.Tables(0).Rows(0)(0)
        ds.Clear()
        ds.Dispose()
        Return maxhecode
    End Function
    Private Function isValidatedDesig() As Boolean
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim count As Integer

        If txtdesg.Text = "" Then
            Utils.ShowMessage(Me, "Please enter designation to add")
            Return False
        End If

        'check if designation already exists
        sql = "select count(*) from pshr.mast_desg where desgtext  = '" & Me.txtdesg.Text & "'"
        oraCn.FillData(sql, ds)
        count = ds.Tables(0).Rows(0)(0)
        ds.Clear()
        ds.Dispose()
        If count <> 0 Then
            Utils.ShowMessage(Me, "This designation already exists")
            Return False
        End If
        Return True
    End Function
    Private Function isValidatedMinAge() As Boolean
        If txtmage.Text Is "" Then
            Utils.ShowMessage(Me, "Enter min. age")
            Return False
        End If

        If Not IsNumeric(txtmage.Text) Then
            Utils.ShowMessage(Me, "Min. age must be a number")
            Return False
        End If
        If txtmage.Text < 18 Or txtmage.Text > 58 Then
            Utils.ShowMessage(Me, "Min. age is not right")
            Return False
        End If
        Return True
    End Function
    Private Function isValidatedRetdAge() As Boolean
        If txtrage.Text Is "" Then
            Utils.ShowMessage(Me, "Enter Retd. Age")
            Return False
        End If
        If Not IsNumeric(txtrage.Text) Then
            Utils.ShowMessage(Me, "Retd. age must be a number")
            Return False
        End If
        If txtrage.Text < 50 Or txtrage.Text > 70 Then
            Utils.ShowMessage(Me, "Retd. age is not right")
            Return False
        End If
        Return True
    End Function
    Private Function isValidatedHeCode() As Integer
        Dim maxhecode As Integer
        If txthecode.Text Is "" Then
            Utils.ShowMessage(Me, "Please enter Hierarchy Code")
            Return False
        End If

        If Not IsNumeric(txthecode.Text) Then
            Utils.ShowMessage(Me, "Hierarchy Code must be a number")
            Return False
        End If

        If txthecode.Text < 1 Then
            Utils.ShowMessage(Me, "Hierarchy Code must be a greater than 1")
            Return False
        End If

        maxhecode = GetMaxHeCode()

        'if no he code then 99 is our default an automatic value
        If maxhecode = 0 Then
            Return True
        End If

        If txthecode.Text > maxhecode + 1 Then
            Utils.ShowMessage(Me, "Hierarchy Code is too large")
            Return False
        End If
        Return True
    End Function
    Private Function isValidatedAbbr() As Boolean
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim count As Integer

        If txtdabr Is "" Then
            Utils.ShowMessage(Me, "Please enter abbreviation")
            Return False
        End If

        'check if abbreviation already exists
        sql = "select count(*) from pshr.mast_desg where desgabb  = '" & Me.txtdabr.Text & "'"
        ds = New System.Data.DataSet
        oraCn.FillData(sql, ds)
        count = ds.Tables(0).Rows(0)(0)
        ds.Clear()
        ds.Dispose()
        If count <> 0 Then
            Utils.ShowMessage(Me, "This abbreviation already exists")
            Return False
        End If
        Return True
    End Function
    Private Function isValidated() As Boolean
        'check Designation
        'If Not isValidatedDesig() Then
        '    Return False
        'End If

        ''check abbreviation
        'If Not isValidatedAbbr() Then
        '    Return False
        'End If

        'check service type
        If drpserv.SelectedValue = "0" Then
            Utils.ShowMessage(Me, "Please select the service type")
            Return False
        End If

        'check gaz/non gaz
        If drpgngz.SelectedValue = "0" Then
            Utils.ShowMessage(Me, "Please select a value for Gaz/Non Gaz")
            Return False
        End If

        'check he code
        If Not isValidatedHeCode() Then
            Return False
        End If

        'check class
        If drpclass.SelectedValue = "0" Then
            Utils.ShowMessage(Me, "Please select class")
            Return False
        End If

        'check minimum age
        If Not isValidatedMinAge() Then
            Return False
        End If

        'check retirement age
        If Not isValidatedRetdAge() Then
            Return False
        End If

        'check cadre
        If drpcadr.SelectedValue = "0" Then
            Utils.ShowMessage(Me, "Please select Cadre")
            Return False
        End If

        'check status
        If drpstats.SelectedValue = "0" Then
            Utils.ShowMessage(Me, "Please select Status")
            Return False
        End If
        Return True
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim maxdesgcode As Integer

        If Not isValidated() Then
            Exit Sub
        End If

        'find what would be our new desgcode for our shiny new designation
        sql = "select max(desgcode)+1 from pshr.mast_desg  where desgcode not in(9999,7777,8888)"
        oraCn.FillData(sql, ds)
        maxdesgcode = ds.Tables(0).Rows(0)(0)
        ds.Clear()
        ds.Dispose()

        'create a new designation
        sql = "insert into mast_desg values (" & maxdesgcode & ",'" & Me.txtdesg.Text.ToUpper() & "'," & Me.drpserv.SelectedValue & "," & Me.drpgngz.SelectedValue & ",'" & Me.drpclass.SelectedValue & "'," & Me.txtmage.Text & "," & Me.txtrage.Text & "," & Me.txthecode.Text & ",'" & Me.txtdabr.Text.ToUpper() & "'," & Me.drpcadr.SelectedValue & "," & Me.drpstats.SelectedValue & ",'A')"
        oraCn.ExecQry(sql)

        'save for undo
        sql = "insert into cadre.temp_mastdesg values(" & maxdesgcode & ",NULL,'C')"
        oraCn.ExecQry(sql)

        'Utils.ShowMessage(Me, "Designation added successfully")
    End Sub
    Protected Sub txtdesg_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdesg.TextChanged
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sqldesg As String
        If txtdesg.Text <> "" Then
            sqldesg = "select desgtext as Matching_Designations from mast_desg where desgtext like upper('%" & txtdesg.Text & "%')and rownum<=10 order by desgtext"
            oraCn.FillData(sqldesg, ds)
            oraCn.fillgrid(GridView1, ds)
        End If
        ds.Clear()
        ds.Dispose()
    End Sub

    Protected Sub txtdabr_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdabr.TextChanged
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sqldesg As String
        If txtdabr.Text <> "" Then
            sqldesg = "select desgabb as Matching_Abbreviations from mast_desg where desgabb like upper('%" & txtdabr.Text & "%')and rownum<=10 order by desgtext"
            oraCn.FillData(sqldesg, ds)
            oraCn.fillgrid(GridView2, ds)
        End If
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub ShowHecodes()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sqldesg As String

        'If drpserv.SelectedValue = "0" Then
        '    ds.Clear()
        '    ds.Dispose()
        '    oraCn.fillgrid(GridView3, ds)
        '    Exit Sub
        'ElseIf drpgngz.SelectedValue = "0" Then
        '    ds.Clear()
        '    ds.Dispose()
        '    oraCn.fillgrid(GridView3, ds)
        '    Exit Sub
        'End If

        sqldesg = "select unique desgtext as Designation,hecode as Hierarchy_Codes from mast_desg where" _
                            & " servcode = " & drpserv.SelectedValue.ToString() _
                            & " And gazcode = " & drpgngz.SelectedValue.ToString() _
                            & " order by hecode"
        oraCn.FillData(sqldesg, ds)
        If ds.Tables(0).Rows.Count = 0 Then
            If drpserv.SelectedValue = "0" Then
                txthecode.Text = ""
            ElseIf drpgngz.SelectedValue = "0" Then
                txthecode.Text = ""
            Else
                txthecode.Text = 99
            End If
            ds.Clear()
            oraCn.fillgrid(GridView3, ds)
        Else
            txthecode.Text = ""
            oraCn.fillgrid(GridView3, ds)
        End If
        ds.Clear()
        ds.Dispose()
    End Sub
    Protected Sub drpgngz_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpgngz.SelectedIndexChanged
        ShowHecodes()
    End Sub
    Protected Sub drpserv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpserv.SelectedIndexChanged
        ShowHecodes()
    End Sub
    Private Sub DoUndo()
        Dim sql As String
        Dim oracn As New OraDBconnection

        sql = "DELETE FROM mast_desg WHERE desgcode IN (SELECT desgcode FROM cadre.temp_mastdesg WHERE action='C')"
        oracn.ExecQry(sql)

    End Sub
    Protected Sub btnUndo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo.Click
        DoUndo()
        Utils.ShowMessage(Me, "Undo Complete")
    End Sub

    Protected Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        GenerateReport(0, "")
    End Sub
    Private Sub GenerateReport(ByVal oonum As Integer, ByVal filename As String)
        If oonum = 0 Then
        Else

        End If
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim oonum As Integer
        Dim sql As String
        Dim oracn As New OraDBconnection

        oonum = Utils.GetOfficeOrderNum()
        GenerateReport(oonum, "Cadre")

        sql = "DELETE FROM cadre.temp_mastdesg WHERE action='C'"
        oracn.ExecQry(sql)
    End Sub
End Class