
Partial Class Account_ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Utils.AuthenticateUser(Me)
    End Sub

    Protected Sub ChangePasswordPushButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '    Dim uid, upass, newpass, sql As String
        '    Dim oraCn As New OraDBconnection
        '    Dim ds1 As New System.Data.DataSet

        '    upass = ChangeUserPassword.CurrentPassword
        '    newpass = ChangeUserPassword.NewPassword

        '    uid = Session("uid")
        '    sql = "select * from cadre.users where uname='" & uid & "' and upass = '" & upass & "'"
        '    oraCn.FillData(sql, ds1)
        '    If ds1.Tables(0).Rows.Count = 1 Then
        '        sql = "update cadre.users set upass = '" & newpass & "' where uname = '" & uid & "'"
        '        'oraCn.ExecQry(sql)
        '        'Response.Redirect("..\Mainpage.aspx")
        '    End If

        '    ds1.Clear()
        '    ds1.Dispose()
    End Sub

    Protected Sub ChangePassword1_ChangingPassword(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LoginCancelEventArgs) Handles ChangePassword1.ChangingPassword
        Dim sql, uid As String
        Dim oraCn As New OraDBconnection
        Dim ds1 As New System.Data.DataSet

        uid = Session("uid")
        sql = "select * from cadre.users where uname='" & uid & "' and upass = '" & ChangePassword1.CurrentPassword & "'"
        oraCn.FillData(sql, ds1)
        If ds1.Tables(0).Rows.Count = 1 Then
            sql = "update cadre.users set upass = '" & ChangePassword1.NewPassword & "' where uname = '" & uid & "'"
            oraCn.ExecQry(sql)
            Session("pass_changed") = "1"
            Utils.ShowMessage(Me, "Password changed successfully")
            Response.Redirect("Login.aspx")
        End If
        ds1.Clear()
        ds1.Dispose()
    End Sub
End Class
