Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

Partial Class frmsqlquery
    Inherits System.Web.UI.Page
    Protected Sub btnshowbin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnshowbin.Click
        If btnshowbin.Text = "Show SQL Bin" Then
            Me.gvquery.Visible = True
            Me.txtquery.Visible = False
            Me.btnshowbin.Text = "Show Entry Screen"
            'Me.btnshowdata.Enabled = False
            loadquerygrid()
        ElseIf btnshowbin.Text = "Show Entry Screen" Then
            Me.gvquery.Visible = False
            Me.txtquery.Visible = True
            Me.btnshowbin.Text = "Show SQL Bin"
            'Me.btnshowdata.Enabled = True
        End If
    End Sub
    Private Sub loadquerygrid()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String

        sql = "select ptxt,psql,login,ldate,rowid  from cadre.sqlbin order by ldate"

        oraCn.FillData(sql, ds)
        oraCn.fillgrid(gvquery, ds)
        ds.Clear()
        ds.Dispose()
        'Me.gvdata.Visible = True
    End Sub
    Private Function isQueryOK(ByVal str As String) As Boolean
        If (str = "") Then
            Me.lblmsg.Visible = True
            Me.lblmsg.Text = "Please Enter SQL Command to view data"
            Return False

        End If

        If (str.ToUpper().Contains("DROP") Or
            str.ToUpper().Contains("TRUNCATE") Or
            str.ToUpper().Contains("DELETE")) Then
            Return False
        End If

        If (Not str.ToUpper().Contains("SELECT")) Then
            Return False
        End If

        If (str.ToUpper().Contains(";")) Then
            Return False
        End If
        Me.lblmsg.Visible = False
        Return True

    End Function
    Protected Sub btnshowdata_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnshowdata.Click
        Me.btnsave.Enabled = True
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        If (Not isQueryOK(Me.txtquery.Text)) Then
            lblmsg.Text = "Error: Prohibited Query"
            lblmsg.Visible = True
            Me.gvdata.Visible = False
            Return
        End If

        Try
            oraCn.FillData(Me.txtquery.Text, ds)
            If (ds.Tables(0).Rows.Count > 4096) Then
                lblmsg.Text = "Dataset too big. Consider using WHERE clause."
                lblmsg.Visible = True
                Me.gvdata.Visible = False
                Return
            End If
            oraCn.fillgrid(gvdata, ds)
        Catch ex As Exception
            lblmsg.Text = "Exception: " & ex.Message
            lblmsg.Visible = True
            Me.gvdata.Visible = False
            Return
        Finally
            ds.Clear()
            ds.Dispose()
        End Try
        Me.gvdata.Visible = True
    End Sub
    Private Function EscapeSQLQuery(ByVal str As String) As String
        str = str.Replace("'", "''")
        Return str
    End Function
    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim query As String
        Dim brief As String

        If Me.txtquery.Text <> "" Then
            If Me.txtsqlbrief.Text <> "" Then
                Dim oraCn As New OraDBconnection
                Dim sql1 As String

                query = EscapeSQLQuery(Me.txtquery.Text)
                brief = EscapeSQLQuery(Me.txtsqlbrief.Text)

                sql1 = "Insert into cadre.sqlbin values ('" & brief & "','" & query & "','cadre', SYSDATE)"
                oraCn.ExecQry(sql1)
                Me.lblmsg.Text = "Query successfully Added to bin"
                Me.lblsqlbrief.Visible = False
                Me.txtsqlbrief.Visible = False
            Else
                Me.lblsqlbrief.Visible = True
                Me.txtsqlbrief.Visible = True
                Me.lblmsg.Visible = True
                Me.lblmsg.Text = "Please Enter Sql Query Brief to proceed"
                Exit Sub
            End If
        Else
            Exit Sub
        End If
    End Sub
    Protected Sub btnprtexcl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprtexcl.Click
        Dim stringwrite As StringWriter
        Dim htmlwrite As HtmlTextWriter
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=FileName.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.xls"
        Me.EnableViewState = False
        stringwrite = New System.IO.StringWriter()
        htmlwrite = New HtmlTextWriter(stringwrite)
        gvdata.RenderControl(htmlwrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal Control As Control)
        'Confirms that an HtmlForm control is rendered for the 
        'specified ASP.NET server control at run time. 
        'No code required here. 
    End Sub

    Protected Sub gvquery_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvquery.SelectedIndexChanging
        Dim sql As String
        Dim ds2 As New System.Data.DataSet()
        Dim oracn As New OraDBconnection()
        sql = "select psql from cadre.sqlbin where rowid =  '" & gvquery.Rows(e.NewSelectedIndex).Cells(5).Text & "'"
        oracn.FillData(sql, ds2)
        Me.txtquery.Text = ds2.Tables(0).Rows(0)(0)
        Me.txtquery.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FillLocs()
            FillDesgs()
            FillScales()
        End If
    End Sub
    Private Sub FillLocs()
        Dim sql As String
        Dim ds As New System.Data.DataSet()
        Dim oracn As New OraDBconnection()

        sql = "select loccode,locname from pshr.mast_loc where aloc=1 and loccode in (select loccode from cadre.cadr) "
        sql &= "and locname like '%" & txtFilterLoc.Text.ToUpper() & "%'"
        sql &= " order by locname"
        oracn.FillData(sql, ds)
        drpLoc.DataSource = ds.Tables(0)
        drpLoc.DataTextField = "locname"
        drpLoc.DataValueField = "loccode"
        drpLoc.DataBind()
        drpLoc.Items.Insert(0, "---Select Location---")
    End Sub
    Private Sub FillDesgs()
        Dim sql As String
        Dim ds As New System.Data.DataSet()
        Dim oracn As New OraDBconnection()

        sql = "select desgcode,desgabb from pshr.mast_desg where adesg='A' order by desgabb"
        oracn.FillData(sql, ds)
        drpDesg.DataSource = ds.Tables(0)
        drpDesg.DataTextField = "desgabb"
        drpDesg.DataValueField = "desgcode"
        drpDesg.DataBind()
        drpDesg.Items.Insert(0, New ListItem("All Designations", 0))
    End Sub
    Private Sub FillScales()
        Dim sql As String
        Dim ds As New System.Data.DataSet()
        Dim oracn As New OraDBconnection()

        sql = "select scaleid,newscale || '-' || gradepay as scale from cadre.mast_scale order by scaleid"
        oracn.FillData(sql, ds)
        drpScale.DataSource = ds.Tables(0)
        drpScale.DataTextField = "scale"
        drpScale.DataValueField = "scaleid"
        drpScale.DataBind()
        drpScale.Items.Insert(0, New ListItem("All Scales", -1))
    End Sub
    Private Function isShowCount() As Boolean
        If (drpListOrCount.SelectedValue = "C") Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub drpListOrCount_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpListOrCount.SelectedIndexChanged
        CreateQuery()
    End Sub

    Private Sub CreateQuery()
        Dim sql As String

        If (Session("showcol") Is Nothing) Then
            Return
        End If

        sql = "SELECT "
        If (isShowCount()) Then
            sql &= " count(*) "
        Else
            sql &= " " & Session("showcol") & " "
        End If

        sql &= " FROM cadre.cadr WHERE "

        If (Len(Session("whereloc")) > 0) Then
            If drpListOrCount.SelectedValue = "G" Then
                Dim wloc As String
                wloc = Session("whereloc").ToString().Split(",")(0)
                wloc = Regex.Replace(wloc, "0+$", "%")
                sql &= " LOCCODE LIKE '" & wloc & "'"
            Else
                sql &= " LOCCODE IN (" & Session("whereloc") & ") "
                'sql &= " order by loccode, desgcode"
            End If
        End If

        If (Len(Session("wheredesg")) > 0 And Session("wheredesg") <> "0") Then
            sql &= " AND DESGCODE IN (" & Session("wheredesg") & ") "
        End If
        If (Len(Session("wherebranch")) > 0 And Session("wherebranch") <> "0") Then
            sql &= " AND branch IN (" & Session("wherebranch") & ") "
        End If
        If (Len(Session("wherescale")) > 0 And Session("wherescale") <> "-1") Then
            sql &= " AND scaleid IN (" & Session("wherescale") & ") "
        End If
        If (Len(Session("wherepost")) > 0 And Session("wherepost") <> "0") Then
            sql &= " AND ptype IN (" & Session("wherepost") & ") "
        End If
        If drpListOrCount.SelectedValue = "G" Then
            sql &= " group by loccode,desgcode"
            sql &= " order by loccode, desgcode"
        Else
            sql &= " order by loccode, desgcode"
        End If
        txtquery.Text = sql
    End Sub

    Protected Sub btnAddDispCol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDispCol.Click
        If (drpDisplayCols.SelectedValue = "0") Then
            If drpListOrCount.SelectedValue <> "G" Then
                Session("showcol") = " pshr.get_org(loccode) as Loc,pshr.get_desg(desgcode) as Desg,indx,cadre.get_scale_txt(scaleid) as Scale, cadre.get_branch(branch) as Branch,ptype "
            Else
                Session("showcol") = " pshr.get_org(loccode) as Loc,pshr.get_desg(desgcode) as Desg,count(*) "
            End If

        Else
            If (Len(Session("showcol")) > 0) Then
                Session("showcol") &= ","
            End If
            Session("showcol") &= drpDisplayCols.SelectedValue
        End If
            CreateQuery()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Session("showcol") = ""
        Session("whereloc") = ""
        Session("wheredesg") = ""
        Session("wherebranch") = ""
        Session("wherepost") = ""
        Session("wherescale") = ""
        txtquery.Text = ""
    End Sub

    Protected Sub btnAddLoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddLoc.Click
        If (drpLoc.SelectedValue <> 0) Then
            If (Len(Session("whereloc")) > 0) Then
                Session("whereloc") &= ","
            End If
            Session("whereloc") &= drpLoc.SelectedValue
        End If
        CreateQuery()
    End Sub

    Protected Sub btnAddDesg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDesg.Click
        If (drpDesg.SelectedValue = 0) Then
            Session("wheredesg") = "0"
        Else
            If (Len(Session("wheredesg")) > 0) Then
                Session("wheredesg") &= ","
            End If
            Session("wheredesg") &= drpDesg.SelectedValue
        End If
        CreateQuery()
    End Sub

    Protected Sub btnAddBranch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddBranch.Click
        If (drpBranch.SelectedValue = 0) Then
            Session("wherebranch") = "0"
        Else
            If (Len(Session("wherebranch")) > 0) Then
                Session("wherebranch") &= ","
            End If
            Session("wherebranch") &= drpBranch.SelectedValue
        End If
        CreateQuery()
    End Sub

    Protected Sub btnAddPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddPost.Click
        If (drpPostType.SelectedValue = "0") Then
            Session("wherepost") = "0"
        Else
            If (Len(Session("wherepost")) > 0) Then
                Session("wherepost") &= ","
            Else
                Session("wherepost") = ""
            End If
            Session("wherepost") &= "'" & drpPostType.SelectedValue & "'"
        End If
        CreateQuery()
    End Sub

    Protected Sub btnAddScale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddScale.Click
        If (drpScale.SelectedValue = -1) Then
            Session("wherescale") = "-1"
        Else
            If (Len(Session("wherescale")) > 0) Then
                Session("wherescale") &= ","
            End If
            Session("wherescale") &= drpScale.SelectedValue
        End If
        CreateQuery()
    End Sub

    Protected Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterLoc.TextChanged
        FillLocs()
    End Sub
End Class