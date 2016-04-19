Imports System.Data
Imports System.Data.Odbc
Imports Microsoft.VisualBasic
'Imports Oracle.DataAccess.Client
Imports System.Data.OracleClient

Public Class OraDBconnection
    Public Sub OraDBConOpen(ByRef conn As System.Data.OracleClient.OracleConnection)
        Dim connString As String
        Dim serviceName As String
		Dim password As String
        If Utils.dummy Then
            serviceName = "oracl"
			password="123"
        Else
            serviceName = "pshr"
			password="PinkWater2010"
        End If
        connString = "user id=pshr;password=" + password + ";" +
            "data source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521)))(CONNECT_DATA=(" +
            "SERVICE_NAME=" + serviceName +
            ")))"
        conn.ConnectionString = connString
        On Error GoTo er
        conn.Open()
er:
        If Err.Number > 0 Then
            '     MsgBox(Err.Description)
            Exit Sub
        End If

    End Sub
    Public Sub conclose(ByRef conn As System.Data.OracleClient.OracleConnection)
        If conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
    End Sub
    Public Sub FillData(ByVal sqlString As String, ByRef dataset As System.Data.DataSet)
        Dim cmd As New System.Data.OracleClient.OracleCommand
        Dim con As New System.Data.OracleClient.OracleConnection
        OraDBConOpen(con)
        Dim dbadapter As New System.Data.OracleClient.OracleDataAdapter(sqlString, con)
        dbadapter.Fill(dataset)
        cmd.Dispose()
        conclose(con)
    End Sub
    Public Sub ExecQry(ByVal sql As String)
        Dim cmd As New System.Data.OracleClient.OracleCommand
        Dim con As New System.Data.OracleClient.OracleConnection
        OraDBConOpen(con)
        cmd.CommandText = sql
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        cmd.Dispose()
        conclose(con)
    End Sub

    Public Sub fillgrid(ByRef gv As System.Web.UI.WebControls.GridView, ByRef ds As System.Data.DataSet)
        gv.DataSource = ds
        gv.DataBind()
    End Sub
End Class
