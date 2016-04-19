
Partial Class cc
    Inherits System.Web.UI.Page
    Private Function GetSNo() As Integer
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet

        oraCn.FillData("select nvl(max(sno),0) + 1 from cadre.cclist", ds)

        Return ds.Tables(0).Rows(0)(0)
    End Function
    Private Sub FillListAll(Optional ByVal filter As String = "")
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim row As System.Data.DataRow

        sql = "select loc,sno from cadre.cclist " & "where upper(loc) like upper('%" & filter & "%') order by sno"
        lstAll.Items.Clear()
        oracn.FillData(sql, ds)
        For Each row In ds.Tables(0).Rows
            lstAll.Items.Add(New ListItem(row(0), row(1)))
        Next
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            FillListAll()
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Not lstAll.SelectedItem Is Nothing Then
        If Not lstCC.Items.Contains(lstAll.SelectedItem) Then
            lstCC.Items.Add(lstAll.SelectedItem)
            'lstAll.Items.RemoveAt(lstAll.SelectedIndex)
            lstAll.SelectedIndex = 0
            End If
        End If
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        lstCC.Items.Remove(lstCC.SelectedItem)
    End Sub

    Protected Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim litem As New ListItem
        Dim index As Integer
        litem = lstCC.SelectedItem
        index = lstCC.SelectedIndex
        If index = 0 Then
            Return
        End If
        lstCC.Items.Remove(lstCC.SelectedItem)
        lstCC.Items.Insert(index - 1, litem)
    End Sub

    Protected Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim litem As New ListItem
        Dim index As Integer
        litem = lstCC.SelectedItem
        index = lstCC.SelectedIndex
        If index = lstCC.Items.Count - 1 Then
            Return
        End If
        lstCC.Items.Remove(lstCC.SelectedItem)
        lstCC.Items.Insert(index + 1, litem)
    End Sub

    Protected Sub btnDone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDone.Click
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim i As Integer

        oracn.ExecQry("Update cadre.cclist set ccnum=-1")
        For i = 0 To lstCC.Items.Count - 1
            sql = "update cadre.cclist set ccnum=" & i + 1 & " where sno=" & lstCC.Items.Item(i).Value
            oracn.ExecQry(sql)
        Next i
        Response.Write("<script>window.close()</script>")
        'Response.Write("<script language='javascript'>parentwin = window.open('closer.htm', '_self');parentwin.opener = window.self;parentwin.close(); </script>")
    End Sub

    Protected Sub btnAddLoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddLoc.Click
        Dim sql As String
        Dim oracn As New OraDBconnection

        If txtNewLoc.Text.Length > 0 Then
            sql = "insert into cadre.cclist values(" & GetSNo() & ",'" & txtNewLoc.Text & "',-1)"
            oracn.ExecQry(sql)
        End If
        txtNewLoc.Text = ""
        FillListAll()
    End Sub

    Protected Sub btnRemLoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemLoc.Click
        Dim sql As String
        Dim oracn As New OraDBconnection

        If lstAll.SelectedValue <> "" Then
            sql = "delete from cadre.cclist where sno=" & lstAll.SelectedValue
            oracn.ExecQry(sql)
            FillListAll()
        End If
    End Sub

    Protected Sub txtNewLoc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNewLoc.TextChanged
        FillListAll(txtNewLoc.Text)
    End Sub
End Class
