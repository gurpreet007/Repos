Imports Microsoft.VisualBasic
Imports System.Web.UI.Page

Public Class Utils
    Public Shared dummy As Boolean
    Public Shared Sub ShowMessage(ByVal page As System.Web.UI.Page, ByVal message As String)
        page.RegisterClientScriptBlock("message", "<script type='text/javascript'> alert('" & message & "');</script>")
    End Sub

    Public Shared Function GetRowNum() As Long
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim oracn As New OraDBconnection
        sql = "select max(rowno) from cadre.cadr"

        oracn.FillData(sql, ds)
        If ds.Tables(0).Rows.Count > 0 Then
            Return (ds.Tables(0).Rows(0)(0)) + 1
        Else
            Return 1
        End If
    End Function
    Public Shared Function GetDesgCount(ByVal loccode As Integer, ByVal desgcode As Integer) As Integer
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim oracn As New OraDBconnection

        sql = "select count(*) from cadre.cadr where loccode= " & loccode & " and desgcode=" & desgcode
        oracn.FillData(sql, ds)
        If ds.Tables(0).Rows.Count > 0 Then
            Return (ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If
    End Function

    Public Shared Function GetIndex(ByVal loccode As Long, ByVal desgcode As Integer) As Integer
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim indx As Integer

        sql = "select max(INDX) from cadre.cadr where loccode =  " & loccode & " and desgcode = " & desgcode
        oracn.FillData(sql, ds)
        indx = "1"
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)(0)) Then
                indx = Val(ds.Tables(0).Rows(0)(0)) + 1
            End If
        End If
        ds.Clear()
        ds.Dispose()
        Return indx
    End Function

    Public Shared Function GetOfficeOrderNum() As Integer
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim oonum As Integer
        Dim sql As String

        oonum = -1
        sql = "select nvl(max(oonum),0) + 1 from cadre.trans_hist"
        oraCn.FillData(sql, ds)

        If ds.Tables(0).Rows.Count > 0 Then
            oonum = ds.Tables(0).Rows(0)(0)
        End If

        Return oonum
    End Function
    Public Shared Sub AuthenticateUser(ByVal page As System.Web.UI.Page)
        If Not isAllowed(page) Then
            page.Response.Redirect("Account/Login.aspx")
        End If
    End Sub
    Private Shared Function isAllowed(ByVal page As System.Web.UI.Page) As Boolean
        Dim allowed As Boolean
        allowed = False
        If page.Session("uid") = "" Then
            allowed = False
        Else
            allowed = True
        End If
        Return allowed
    End Function
End Class
