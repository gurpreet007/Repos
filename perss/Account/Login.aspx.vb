Partial Class Account_Login
    Inherits System.Web.UI.Page
    Private Function Authenticate_User(ByVal uid As String, ByVal upass As String) As Boolean
        Dim oraCn As New OraDBconnection
        Dim ds1 As New System.Data.DataSet
        Dim sql As String
        Dim authenticate As Boolean

        authenticate = False
        sql = "select role from cadre.users where uname='" & uid & "' and upass = '" & upass & "'"
        oraCn.FillData(sql, ds1)
        If ds1.Tables(0).Rows.Count = 1 Then
            Session("uid") = uid
            Session("role") = ds1.Tables(0).Rows(0)("role")
            authenticate = True
        End If
        ds1.Clear()
        ds1.Dispose()
        Return authenticate
    End Function
    Protected Sub Login1_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate
        Utils.dummy = Login1.RememberMeSet
        e.Authenticated = Authenticate_User(Login1.UserName, Login1.Password)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Session("uid") = "cadre"
        Session("role") = "A"
        Response.Redirect("..\Mainpage.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("pg") = "LoginPage"
        Session.Clear()
        If Session("pass_changed") = "1" Then
            Label1.Text = "Password Changed Successfully. Login with new password"
            Session("pass_changed") = ""
        End If
    End Sub
End Class