
Partial Class frm_chng_pwd
    Inherits System.Web.UI.Page

   
    Protected Sub ChangeUserPassword_ChangingPassword(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LoginCancelEventArgs) Handles ChangeUserPassword.ChangingPassword
        'Dim sql, opass, npass As String
        'If Session("uid") = "" Then
        '    Response.Redirect("Loginpage.aspx")
        'End If
        'opass = ChangeUserPassword.CurrentPassword
        'npass = ChangeUserPassword.NewPassword

        'Dim oraCn As New OraDBconnection
        'Dim ds As New System.Data.DataSet
        'sql = "select upass from cadre.users where empid = " & Session("uid")
        'oraCn.FillData(sql, ds)

        'If ds.Tables(0).Rows.Count > 0 Then
        '    If opass = ds.Tables(0).Rows(0)(0) Then
        '        sql = "update cadre.users set upass = " & npass & "  where empid = " & Session("uid")
        'oraCn.ExecQry(sql)
        'ChangeUserPassword.Visible = False
        ' lblSuccess.Visible = True

        'Response.Redirect("frm_edit_location.aspx")

        '    End If

        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Utils.AuthenticateUser(Me)
    End Sub
End Class
