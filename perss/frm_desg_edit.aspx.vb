Partial Class frm_desg_edit
    Inherits System.Web.UI.Page
    Private Sub filldesg()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        sql = "select desgcode, desgabb from pshr.mast_desg where adesg='A' and desgtext is not null and desgabb like '%" & UCase(txtFilter.Text.Replace(" ", "%")) & "%' order by desgabb "
        oraCn.FillData(sql, ds)
        drpdesg.Items.Clear()
        btnAbolish.Enabled = False
        btnEdit.Enabled = False

        If ds.Tables(0).Rows.Count > 0 Then
            btnAbolish.Enabled = True
            btnEdit.Enabled = True
        End If

        For i = 0 To ds.Tables(0).Rows.Count - 1
            Me.drpdesg.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
        Next

        ds.Clear()
        ds.Dispose()
    End Sub
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
        If Not IsPostBack Then
            DoUndo()
            filldesg()
        End If
    End Sub
    Private Sub fillEditTexts(ByVal desg As String)
        Dim oracn As New OraDBconnection
        Dim sql As String
        Dim ds As New System.Data.DataSet
        sql = "select desgtext,desgabb,hecode,cls,minage,retdage,servcode,gazcode,cadrecode,empstatus from pshr.mast_desg where desgcode = " & desg
        oracn.FillData(sql, ds)
        txtDesg.Text = ds.Tables(0).Rows(0)("desgtext")
        txtabbr.Text = ds.Tables(0).Rows(0)("desgabb")
        txthecode.Text = ds.Tables(0).Rows(0)("hecode")
        txtMinAge.Text = ds.Tables(0).Rows(0)("minage")
        txtRetdAge.Text = ds.Tables(0).Rows(0)("retdage")
        drpclass.SelectedIndex = drpclass.Items.IndexOf(drpclass.Items.FindByValue(ds.Tables(0).Rows(0)("cls")))
        drpserv.SelectedIndex = drpserv.Items.IndexOf(drpserv.Items.FindByValue(ds.Tables(0).Rows(0)("servcode")))
        drpgngz.SelectedIndex = drpgngz.Items.IndexOf(drpgngz.Items.FindByValue(ds.Tables(0).Rows(0)("gazcode")))
        drpcadr.SelectedIndex = drpcadr.Items.IndexOf(drpcadr.Items.FindByValue(ds.Tables(0).Rows(0)("cadrecode")))
        drpstats.SelectedIndex = drpstats.Items.IndexOf(drpstats.Items.FindByValue(ds.Tables(0).Rows(0)("empstatus")))
        ds.Clear()
        ds.Dispose()
    End Sub
    Protected Sub drpdesg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdesg.SelectedIndexChanged
        PanAbolish.Visible = False
        panEdit.Visible = False
    End Sub
    Protected Sub txtFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilter.TextChanged
        filldesg()
        PanAbolish.Visible = False
        panEdit.Visible = False
    End Sub
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        fillcadr()
        fillEditTexts(Me.drpdesg.SelectedValue)
        PanAbolish.Visible = False
        panEdit.Visible = True
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
            oraCn.fillgrid(GridView1, ds)
        Else
            oraCn.fillgrid(GridView1, ds)
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
    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim oracn As New OraDBconnection
        Dim sql As String

        If Me.txtDesg.Text = "" Then
            Utils.ShowMessage(Me, "Please enter the designation text")
            Exit Sub
        End If
        If Me.txtabbr.Text = "" Then
            Utils.ShowMessage(Me, "Please enter the designation abbreviation")
            Exit Sub
        End If
        If Me.txthecode.Text = "" Then
            Utils.ShowMessage(Me, "Please enter the designation hierarchy code")
            Exit Sub
        End If
        If Me.drpclass.SelectedValue = "0" Then
            Utils.ShowMessage(Me, "Please enter the designation class")
            Exit Sub
        End If
        If Not IsNumeric(txtMinAge.Text()) Then
            Utils.ShowMessage(Me, "Please enter the correct minimum age")
            Exit Sub
        End If

        If Not IsNumeric(txtRetdAge.Text()) Then
            Utils.ShowMessage(Me, "Please enter the correct retirement age")
            Exit Sub
        End If
        sql = String.Format("Update pshr.mast_desg set desgtext = '{0}', desgabb='{1}', hecode='{2}', " +
                            "cls='{3}', minage='{4}', retdage='{5}', servcode='{6}', gazcode='{7}', " +
                            "cadrecode='{8}',empstatus='{9}' where desgcode='{10}'",
                            txtDesg.Text, txtabbr.Text, txthecode.Text, drpclass.SelectedValue,
                            txtMinAge.Text, txtRetdAge.Text, drpserv.SelectedValue, drpgngz.SelectedValue,
                            drpcadr.SelectedValue, drpstats.SelectedValue, drpdesg.SelectedValue)
        'sql = "Update pshr.mast_desg set desgtext = '" & Me.txtDesg.Text & "' , desgabb = '" & txtabbr.Text &
        '    "', hecode=" & txthecode.Text & ", cls='" & drpclass.SelectedValue &
        '    "', minage='" & txtMinAge.Text() & "' " & "where desgcode = " & Me.drpdesg.SelectedValue
        oracn.ExecQry(sql)

        Utils.ShowMessage(Me, "Designation Modified Successfully")
        PanAbolish.Visible = False
        panEdit.Visible = False
        filldesg()
    End Sub
    Protected Sub btnDoAbolish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDoAbolish.Click
        Dim oracn As New OraDBconnection
        Dim sql As String

        sql = "Update pshr.mast_desg set adesg = 'D' where desgcode = " & Me.drpdesg.SelectedValue
        oracn.ExecQry(sql)

        sql = "INSERT INTO cadre.temp_mastdesg values(" & Me.drpdesg.SelectedValue & ",'" & Me.txtRemarks.Text & "','A')"
        oracn.ExecQry(sql)

        'Utils.ShowMessage(Me, "Designation Abolished Successfully")
        PanAbolish.Visible = False
        panEdit.Visible = False
        filldesg()
    End Sub

    Protected Sub btnAbolish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAbolish.Click
        PanAbolish.Visible = True
        panEdit.Visible = False
    End Sub
    Private Sub DoUndo()
        Dim oracn As New OraDBconnection
        Dim sql As String

        sql = "UPDATE mast_desg SET adesg = 'A' WHERE desgcode IN (SELECT desgcode FROM cadre.temp_mastdesg WHERE action='A')"
        oracn.ExecQry(sql)

    End Sub
    Protected Sub btnUndo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo.Click
        DoUndo()
        Utils.ShowMessage(Me, "Undo Complete")
    End Sub
    Private Sub GenerateReport(ByVal oonum As Integer, ByVal filename As String)
        If oonum = 0 Then
        Else
        End If
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim oracn As New OraDBconnection
        Dim sql As String
        Dim oonum As Integer

        oonum = Utils.GetOfficeOrderNum()
        GenerateReport(oonum, "Cadre")
        sql = "DELETE FROM cadre.temp_mastdesg WHERE action='A'"
        oracn.ExecQry(sql)
    End Sub

    Protected Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        GenerateReport(0, "")
    End Sub
    'Private Sub ShowHecodes()
    '    Dim oraCn As New OraDBconnection
    '    Dim ds As New System.Data.DataSet
    '    Dim sqldesg As String

    '    sqldesg = "select unique desgtext as Designation,hecode as Hierarchy_Codes from mast_desg where" _
    '                        & " hecode = " & txthecode.Text & "order by desgtext"

    '    Try
    '        oraCn.FillData(sqldesg, ds)
    '        oraCn.fillgrid(GridView1, ds)
    '    Catch ex As Exception
    '    End Try

    '    ds.Clear()
    '    ds.Dispose()
    'End Sub
End Class
