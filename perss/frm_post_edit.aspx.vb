Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports CrystalDecisions.Web
Imports CrystalDecisions.Shared

Partial Class frm_post_edit
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Utils.AuthenticateUser(Me)

        'Disable 'Save and Generate Office Order' button if role is 'Creator' i.e. 'C'
        If Session("role") = "C" Then
            btnSave.Enabled = False
        End If

        If Not Page.IsPostBack Then
            Dim oracn As New OraDBconnection
            Dim ds As New System.Data.DataSet
            get_ce()
            'oracn.ExecQry("delete from cadre.temp_edit")
            oracn.ExecQry("delete from cadre.rptedit")

            oracn.FillData("select * from cadre.temp_edit", ds)
            If (ds.Tables(0).Rows.Count > 0) Then
                lblPending.Text = "There is an unsaved session. Either Save or Delete it before continuing"
            Else
                lblPending.Text = ""
            End If


            filldesgExisting(Me.drpdesg)
        End If
        GetLocCode()
        GridShow()
        'filldesgExisting(Me.drpdesg)
    End Sub
    Private Function GetLocCode() As Long
        Dim loccode As Long
        loccode = -1
        If drpsuboff.SelectedValue <> "" And drpsuboff.SelectedValue <> "0" Then
            loccode = drpsuboff.SelectedItem.Value
        ElseIf drpsubdiv.SelectedValue <> "" And drpsubdiv.SelectedValue <> "0" Then
            loccode = drpsubdiv.SelectedItem.Value
        ElseIf drpdiv.SelectedValue <> "" And drpdiv.SelectedValue <> "0" Then
            loccode = drpdiv.SelectedItem.Value
        ElseIf drpcir.SelectedValue <> "" And drpcir.SelectedValue <> "0" Then
            loccode = drpcir.SelectedItem.Value
        ElseIf drporg.SelectedValue <> "" And drporg.SelectedValue <> "0" Then
            loccode = drporg.SelectedItem.Value
        ElseIf drporgtype.SelectedValue <> "" And drporgtype.SelectedValue <> "0" Then
            loccode = drporgtype.SelectedItem.Value
        End If
            Return loccode
    End Function
    Private Sub get_ce()
        Me.drporgtype.Items.Clear()
        If Me.RadioButtonList2.SelectedValue = "P" Then
            Me.drporgtype.Items.Add(New ListItem("--Select--", 0))
            Me.drporgtype.Items.Add(New ListItem("CMD/PSPCL", "800000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Distribution", "803000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Generation", "802000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Finance", "804000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Commercial", "805000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/HR", "806000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Admin", "801000000"))
            Me.drporgtype.Items.Add(New ListItem("Deputation", "600000000"))
            Me.drporgtype.Items.Add(New ListItem("Ombudsman", "810000000"))
        ElseIf Me.RadioButtonList2.SelectedValue = "T" Then
            Me.drporgtype.Items.Add(New ListItem("--Select--", 0))
            Me.drporgtype.Items.Add(New ListItem("CMD/PSTCL", "503000000"))
            Me.drporgtype.Items.Add(New ListItem("Director/Technical", "503000001"))
            Me.drporgtype.Items.Add(New ListItem("Director/Finance & Commercial", "503000002"))
            Me.drporgtype.Items.Add(New ListItem("Director/Admin", "503000004"))
        End If
    End Sub
    Private Sub fillorg()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        'sql = "select distinct(c.loccode), pshr.get_org(c.loccode) from cadre.cadr c, pshr.mast_loc l where c.loccode = l.loccode and l.locrep = '" & Me.drporgtype.SelectedValue & "' l.aloc = 1 order by loccode "
        sql = "select loccode, locabb from pshr.mast_loc where locname is not null and loccode like '%000000' and locrep = " & Me.drporgtype.SelectedValue & " and loctype <> 11 and aloc = 1 order by loccode "

        'sql = "select loccode, locabb from pshr.mast_loc where locname is not null and loccode like '%000000' and rep = '" & Me.drporgtype.SelectedValue & "' and aloc = 1 order by loccode "
        oraCn.FillData(sql, ds)
        drporg.Items.Clear()

        If ds.Tables(0).Rows.Count > 0 Then
            drporg.Items.Add(New ListItem("--Select--", 0))

            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drporg.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
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

        ' If drporg.SelectedValue = "0" Then
        'drpcir.Items.Clear()
        'drpdiv.Items.Clear()
        'drpsubdiv.Items.Clear()
        'Exit Sub
        'End If

        If (drporg.SelectedValue = "0" Or drporg.SelectedValue = "") Then
            sql = "select loccode, locabb from pshr.mast_loc where locrep = " & drporgtype.SelectedItem.Value & "and loctype = 30 and aloc=1 order by loccode "
        Else
            sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drporg.SelectedItem.Value, 1, 3) & "%000' and loctype = 30 and loccode not like '%000000' and aloc=1 order by loccode "
        End If

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

        If drpcir.SelectedValue = "0" Or drpcir.SelectedValue = "" Then
            If drporg.SelectedValue = "0" Or drporg.SelectedValue = "" Then
                'do nothing if no circle and no org is filled

                'drpdiv.Items.Clear()
                sql = "select loccode, locabb from pshr.mast_loc where locrep = " & drporgtype.SelectedItem.Value & "and loctype = 40 and aloc=1 order by loccode "
                'drpsubdiv.Items.Clear()
                'drpsuboff.Items.Clear()
                'Exit Sub
            Else
                'if circle is not filled but org is filled then get locrep from org
                sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drporg.SelectedItem.Value, 1, 3) & "%' and loctype = 40 and locrep = " & Me.drporg.SelectedValue & " and aloc=1 order by loccode "
            End If
        Else
            'if circle is filled then get locrep from circle
            sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drpcir.SelectedItem.Value, 1, 5) & "%' and loctype = 40 and aloc=1 order by loccode "
        End If

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
    Private Sub fillsubdiv()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer

        'If drpdiv.SelectedValue = "" Then
        '    Exit Sub
        'End If

        If drpdiv.SelectedValue = "0" Or drpdiv.SelectedValue = "" Then
            If drpcir.SelectedValue = "0" Or drpcir.SelectedValue = "" Then
                If drporg.SelectedValue = "0" Or drporg.SelectedValue = "" Then
                    'if none of div,circle and org is filled then do nothing
                    'drpsubdiv.Items.Clear()
                    'Exit Sub
                    sql = "select loccode, locabb from pshr.mast_loc where locrep = " & drporgtype.SelectedItem.Value & "and loctype = 50 and aloc=1 order by loccode "
                Else
                    'if div and cir are not filled but org is filled then get locrep from org
                    sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drporg.SelectedItem.Value, 1, 8) & "%' and loctype = 50 and aloc=1 order by loccode "
                End If
            Else
                'if div is not filled but circle is filled then get locrep from circle
                sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drpcir.SelectedItem.Value, 1, 8) & "%' and loctype = 50 and aloc=1 order by loccode "
            End If
        Else
            'if div is filled then then locrep from div
            sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drpdiv.SelectedItem.Value, 1, 8) & "%' and loctype = 50 and aloc=1 order by loccode "
        End If

        oraCn.FillData(sql, ds)
        drpsubdiv.Items.Clear()
        If ds.Tables(0).Rows.Count > 0 Then
            drpsubdiv.Items.Add(New ListItem("--Select--", 0))

            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drpsubdiv.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
            Next
        End If
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub fillsuboff()
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer

        'If drpsubdiv.SelectedValue = "" Then
        '    Exit Sub
        'End If

        If drpsubdiv.SelectedValue = "0" Or drpsubdiv.SelectedValue = "" Then
            If drpdiv.SelectedValue = "0" Or drpdiv.SelectedValue = "" Then
                If drpcir.SelectedValue = "0" Or drpcir.SelectedValue = "" Then
                    If drporg.SelectedValue = "0" Or drporg.SelectedValue = "" Then
                        'if none of div,circle and org is filled then do nothing
                        'Exit Sub
                        sql = "select loccode, locabb from pshr.mast_loc where locrep = " & drporgtype.SelectedItem.Value & "and loctype = 60 and aloc=1 order by loccode "
                    Else
                        'if div and cir are not filled but org is filled then get locrep from org
                        sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drporg.SelectedItem.Value, 1, 8) & "%' and loctype = 60 and aloc=1 order by loccode "
                    End If
                Else
                    'if div is not filled but circle is filled then get locrep from circle
                    sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drpcir.SelectedItem.Value, 1, 8) & "%' and loctype = 60 and aloc=1 order by loccode "
                End If
            Else
                'if div is filled then then locrep from div
                sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drpdiv.SelectedItem.Value, 1, 8) & "%' and loctype = 60 and aloc=1 order by loccode "
            End If
        Else
            'if sub div is filled then then locrep from sub div
            sql = "select loccode, locabb from pshr.mast_loc where locrep like '" & Mid$(drpsubdiv.SelectedItem.Value, 1, 8) & "%' and loctype = 60 and aloc=1 order by loccode "

        End If

        oraCn.FillData(sql, ds)
        drpsuboff.Items.Clear()
        If ds.Tables(0).Rows.Count > 0 Then
            drpsuboff.Items.Add(New ListItem("--Select--", 0))

            For i = 0 To ds.Tables(0).Rows.Count - 1
                Me.drpsuboff.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(0).ToString()))
            Next
        End If
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub filldesgAll(ByVal drpd As System.Web.UI.WebControls.DropDownList, ByVal filter As String)
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim selectedDesg As Integer

        selectedDesg = drpdesg.SelectedValue.Split("-")(0)
        If rbActions.SelectedValue = "U" Then
            sql = "select desgabb, desgcode from pshr.mast_desg where desgtext is not null and (desgabb like '%" _
                    & UCase(filter.Replace(" ", "%")) & "%' ) and cadre.get_hecode(desgcode) < cadre.get_hecode(" & selectedDesg & ") order by desgabb "
        ElseIf rbActions.SelectedValue = "D" Then
            sql = "select desgabb, desgcode from pshr.mast_desg where desgtext is not null and (desgabb like '%" _
                    & UCase(filter.Replace(" ", "%")) & "%' ) and cadre.get_hecode(desgcode) > cadre.get_hecode(" & selectedDesg & ") order by desgabb "
        Else
            sql = "select desgabb, desgcode from pshr.mast_desg where desgtext is not null and (desgabb like '%" _
                    & UCase(filter.Replace(" ", "%")) & "%' )  order by desgabb "
        End If
        oraCn.FillData(sql, ds)
        drpd.Items.Clear()
        drpd.Items.Add(New ListItem("-Select-", 0))
        For i = 0 To ds.Tables(0).Rows.Count - 1
            drpd.Items.Add(New ListItem(ds.Tables(0).Rows(i)(0).ToString(), ds.Tables(0).Rows(i)(1).ToString()))
        Next
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub filldesgExisting(ByVal drpd As System.Web.UI.WebControls.DropDownList)
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        Dim loccode As Long

        loccode = GetLocCode()
        'sql = "select c.desgcode, d.desgabb from pshr.mast_desg d,cadre.cadr c where " &
        '"d.desgtext is not null and " &
        '"c.desgcode = d.desgcode and " &
        '"c.loccode = " & loccode &
        '" order by d.desgabb "
        sql = "select pshr.get_desg(desgcode) || '-' || indx || '   (' || cadre.get_branch(branch)||')'  || decode(hia,1,'(HIA)',''), " &
            "desgcode || '-' || indx,cadre.get_hecode(desgcode) as hcode,branch from cadre.cadr " &
            "where loccode = " & loccode & " order by hcode, indx"

        oraCn.FillData(sql, ds)
        drpd.Items.Clear()
        For i = 0 To ds.Tables(0).Rows.Count - 1
            drpd.Items.Add(New ListItem(ds.Tables(0).Rows(i)(0).ToString(), ds.Tables(0).Rows(i)(1).ToString()))
        Next
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub fillscale(ByVal drpscale As DropDownList)
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim i As Integer
        sql = "select distinct tab1 ,'Scale-' || NewScale || '- Grade Pay - ' || gradepay,  scaleid  from cadre.mast_scale where scale is not null order by scaleid"
        oraCn.FillData(sql, ds)
        drpscale.Items.Clear()
        '  drpscale.Items.Add(New ListItem(" ", 98))
        drpscale.Items.Add(New ListItem("Pb. Govt. Scale ", 0))
        For i = 0 To ds.Tables(0).Rows.Count - 1
            drpscale.Items.Add(New ListItem(ds.Tables(0).Rows(i)(1).ToString(), ds.Tables(0).Rows(i)(2).ToString()))
        Next
        ds.Clear()
        ds.Dispose()
    End Sub
    Private Function GetOldInfo(ByVal loccode As Long, ByRef olddesgcode As Integer, ByRef oldindx As Integer, ByRef oldscaleid As Integer, ByRef oldbranch As Integer, ByRef oldptype As String) As Long
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim oldrowno As Long

        'desgcode from 1st part of drpdesg.selectedvalue
        olddesgcode = drpdesg.SelectedValue.Split("-")(0)
        'indx from 2nd part of drpdesg.selectedvalue
        oldindx = drpdesg.SelectedValue.Split("-")(1)

        'get rowno,scaleid,branch,ptype using (loccode + desgcode + indx) to be used as oldrownum in trans_hist
        sql = "select rowno, scaleid, branch, ptype from cadre.cadr where " &
            "loccode = " & loccode &
            "and desgcode = " & olddesgcode &
            "and indx = " & oldindx
        oracn.FillData(sql, ds)

        If ds.Tables(0).Rows.Count <> 1 Then
            Return -1
        End If

        oldrowno = ds.Tables(0).Rows(0)(0)
        oldscaleid = ds.Tables(0).Rows(0)(1)
        oldbranch = ds.Tables(0).Rows(0)(2)
        oldptype = ds.Tables(0).Rows(0)(3)

        ds.Clear()
        ds.Dispose()
        Return oldrowno
    End Function
    Private Sub GetOldInfoByRowno(ByVal rowno As Integer, ByRef loccode As Long, ByRef olddesgcode As Integer, ByRef oldindx As Integer, ByRef oldscaleid As Integer, ByRef oldbranch As Integer, ByRef oldptype As String)
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String

        'get scaleid,branch,ptype using rowno
        sql = "select loccode, desgcode, indx, scaleid, branch, ptype from cadre.cadr where rowno = " & rowno
        oracn.FillData(sql, ds)

        If ds.Tables(0).Rows.Count <> 1 Then
            Return
        End If

        loccode = ds.Tables(0).Rows(0)("loccode")
        olddesgcode = ds.Tables(0).Rows(0)("desgcode")
        oldindx = ds.Tables(0).Rows(0)("indx")
        oldscaleid = ds.Tables(0).Rows(0)("scaleid")
        oldbranch = ds.Tables(0).Rows(0)("branch")
        oldptype = ds.Tables(0).Rows(0)("ptype")

        ds.Clear()
        ds.Dispose()
    End Sub
    Private Sub EnableControls()
        PanControls.Enabled = False
        If drpdesg.Items.Count > 0 Then
            PanControls.Enabled = True
        End If
    End Sub
    Protected Sub RadioButtonList2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList2.SelectedIndexChanged
        get_ce()
        Me.drporg.Items.Clear()
    End Sub
    Protected Sub drporgtype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drporgtype.SelectedIndexChanged
        drpcir.Items.Clear()
        drpdiv.Items.Clear()
        drpsubdiv.Items.Clear()
        drpsuboff.Items.Clear()
        If drporgtype.SelectedValue <> "0" Then
            fillorg()
            fillcir()
            filldiv()
            fillsubdiv()
            fillsuboff()
            filldesgExisting(Me.drpdesg)
        Else
            drporg.Items.Clear()
            drpcir.Items.Clear()
            drpdiv.Items.Clear()
            drpsubdiv.Items.Clear()
            drpsuboff.Items.Clear()
            drpdesg.Items.Clear()
        End If
        EnableControls()
    End Sub
    Protected Sub drporg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drporg.SelectedIndexChanged
        drpdiv.Items.Clear()
        drpsubdiv.Items.Clear()
        fillcir()
        filldiv()
        fillsubdiv()
        fillsuboff()
        filldesgExisting(Me.drpdesg)
        EnableControls()
    End Sub
    Protected Sub drpcir_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpcir.SelectedIndexChanged
        filldiv()
        fillsubdiv()
        fillsuboff()
        filldesgExisting(Me.drpdesg)
        EnableControls()
    End Sub
    Protected Sub drpdiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdiv.SelectedIndexChanged
        fillsubdiv()
        fillsuboff()
        filldesgExisting(Me.drpdesg)
        EnableControls()
    End Sub
    Protected Sub drpsubdiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpsubdiv.SelectedIndexChanged
        fillsuboff()
        filldesgExisting(Me.drpdesg)
        EnableControls()
    End Sub
    Private Sub StartUpgrade(ByVal branch As Integer, ByVal scaleid As Integer, ByVal ptype As String)
        panUpgrade.Visible = True
        panDowngrade.Visible = False
        panEdit.Visible = False
        PanAbolish.Visible = False
        panAbeyance.Visible = False
        filldesgAll(Me.drpupdesg, "")
        fillscale(drpupscale)
        drpupbranch.SelectedValue = branch
        drpupscale.SelectedValue = scaleid
        drpuptperm.SelectedValue = ptype
    End Sub
    Private Function DoUp_NewRow(ByVal loccode As Long) As Long
        Dim oracn As New OraDBconnection
        Dim newdesgcode As Integer
        Dim newindx As Integer
        Dim newscaleid As Integer
        Dim newrowno As Long
        Dim newbranch As String
        Dim newptype As String
        Dim sql As String

        'updated desgcode is from drpupdesg
        newdesgcode = drpupdesg.SelectedValue

        'new index is from Utils.GetIndex()
        newindx = Utils.GetIndex(loccode, newdesgcode)

        'scaleid is from drpupscale
        newscaleid = drpupscale.SelectedValue

        'rno from Utils.GetRowNum()
        newrowno = Utils.GetRowNum()

        'branch from drpupbranch
        newbranch = drpupbranch.SelectedValue

        'ptype from drpuptperm
        newptype = drpuptperm.SelectedValue

        sql = "insert into cadre.cadr  values(" & loccode & "," & newdesgcode & "," & newindx & "," & newscaleid & ", " & newrowno & "," & newbranch & ",'" & newptype & "','A',NULL)"
        oracn.ExecQry(sql)

        Return newrowno
    End Function
    Private Sub DoUpgrade()
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim oldrowno As Long
        Dim olddesgcode As Integer
        Dim oldindx As Integer
        Dim oldscaleid As Integer
        Dim oldbranch As Integer
        Dim oldptype As String = ""
        Dim newrowno As Long
        Dim loccode As Long

        If txtUpRemarks.Text = "" Then
            Utils.ShowMessage(Me, "Please enter remarks")
            Exit Sub
        End If
        loccode = GetLocCode()
        'get necessary info about old row from cadr
        oldrowno = GetOldInfo(loccode, olddesgcode, oldindx, oldscaleid, oldbranch, oldptype)
        If oldrowno = -1 Then
            Exit Sub
        End If

        'delete old row using rowno
        sql = "delete from cadre.cadr where rowno = " & oldrowno
        oracn.ExecQry(sql)

        'add new row in cadr
        newrowno = DoUp_NewRow(loccode)


        sql = "insert into cadre.trans_hist values(" &
             loccode & "," &
             olddesgcode & ", " &
             oldindx & ", " &
             oldrowno & ", " &
             newrowno & ", " &
             "(select sysdate from dual), '" &
             txtUpRemarks.Text & "', " &
             "'UG', " &
             oldscaleid & ", " &
             oldbranch & ", " &
             "'" & oldptype & "', " &
             "'" & Session.SessionID & "', " &
             "NULL, " &
             Utils.GetOfficeOrderNum() &
             ")"

        oracn.ExecQry(sql)

        Utils.ShowMessage(Me, "Post Upgraded")
    End Sub
    Private Sub DoUpgrade2(ByVal oldrowno As Integer, ByVal ndesg As Integer, ByVal nbranch As Integer, ByVal nscaleid As Integer, ByVal nptype As String, ByVal remarks As String, ByVal oonum As Integer)
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim olddesgcode As Integer
        Dim oldindx As Integer
        Dim oldscaleid As Integer
        Dim oldbranch As Integer
        Dim oldptype As String
        Dim newrowno As Long
        Dim loccode As Long
        Dim newindx As Integer

        oldptype = ""
        'get necessary info about old row from cadr
        GetOldInfoByRowno(oldrowno, loccode, olddesgcode, oldindx, oldscaleid, oldbranch, oldptype)

        'delete old row using rowno
        sql = "delete from cadre.cadr where rowno = " & oldrowno
        oracn.ExecQry(sql)

        'add new row in cadr

        'new index is from Utils.GetIndex()
        newindx = Utils.GetIndex(loccode, ndesg)

        'rno from Utils.GetRowNum()
        newrowno = Utils.GetRowNum()

        sql = "insert into cadre.cadr  values(" & loccode & "," & ndesg & "," & newindx & "," & nscaleid & ", " & newrowno & "," & nbranch & ",'" & nptype & "','A',NULL,NULL)"
        oracn.ExecQry(sql)

        'newrowno = DoUp_NewRow(loccode)

        'save in trans_hist
        sql = "insert into cadre.trans_hist values(" &
             loccode & "," &
             olddesgcode & ", " &
             oldindx & ", " &
             oldrowno & ", " &
             newrowno & ", " &
             "(select sysdate from dual), '" &
             remarks & "', " &
             "'UG', " &
             oldscaleid & ", " &
             oldbranch & ", " &
             "'" & oldptype & "', " &
             "'" & Session.SessionID & "', " &
             "NULL, " &
             oonum &
             ")"

        oracn.ExecQry(sql)
    End Sub
    Private Function GetBSPInfo(ByRef branch As Integer, ByRef scaleid As Integer, ByRef ptype As String) As Boolean
        Dim loccode As Long
        Dim desgcode As Integer
        Dim indx As Integer
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet

        loccode = GetLocCode()
        'desgcode from 1st part of drpdesg.selectedvalue
        desgcode = drpdesg.SelectedValue.Split("-")(0)
        'indx from 2nd part of drpdesg.selectedvalue
        indx = drpdesg.SelectedValue.Split("-")(1)

        'get rowno,scaleid,branch,ptype using (loccode + desgcode + indx) to be used as oldrownum in trans_hist
        sql = "select scaleid, branch, ptype from cadre.cadr where " &
            "loccode = " & loccode &
            "and desgcode = " & desgcode &
            "and indx = " & indx
        oracn.FillData(sql, ds)

        If ds.Tables(0).Rows.Count <> 1 Then
            Return False
        End If

        scaleid = ds.Tables(0).Rows(0)("scaleid")
        branch = ds.Tables(0).Rows(0)("branch")
        ptype = ds.Tables(0).Rows(0)("ptype")

        ds.Clear()
        ds.Dispose()
        Return True

    End Function
    Protected Sub RadioButtonList3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbActions.SelectedIndexChanged
        Dim branch As Integer
        Dim scaleid As Integer
        Dim ptype As String
        Dim desg As Integer
        Dim seldesg As String

        ptype = ""
        If GetBSPInfo(branch, scaleid, ptype) = False Then
            Utils.ShowMessage(Me, "An error occured in getting info about selected post")
        End If

        seldesg = drpdesg.SelectedValue
        desg = seldesg.Substring(0, seldesg.IndexOf("-"))

        If rbActions.SelectedValue = "U" Then
            StartUpgrade(branch, scaleid, ptype)
        ElseIf rbActions.SelectedValue = "D" Then
            StartDowngrade(branch, scaleid, ptype)
        ElseIf rbActions.SelectedValue = "E" Then
            StartEdit(desg, branch, scaleid, ptype)
        ElseIf rbActions.SelectedValue = "A" Then
            StartAbolish()
        ElseIf rbActions.SelectedValue = "H" Then
            StartAbeyance()
        End If
    End Sub
    Private Function GetActionID() As Integer
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim actionid As Integer

        sql = "select nvl(max(actionid)+1,1) from cadre.temp_edit"
        oracn.FillData(sql, ds)
        actionid = ds.Tables(0).Rows(0)(0)
        ds.Clear()
        ds.Dispose()

        Return actionid
    End Function
    Private Function GetOldRowNum() As Integer
        Dim oldrowno As Integer
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim loccode, olddesgcode, oldindex As Integer

        loccode = GetLocCode()
        'desgcode from 1st part of drpdesg.selectedvalue
        olddesgcode = drpdesg.SelectedValue.Split("-")(0)
        '2nd part is index
        oldindex = drpdesg.SelectedValue.Split("-")(1)

        'get rowno,scaleid,branch,ptype using (loccode + desgcode + indx) to be used as oldrownum in trans_hist
        sql = "select rowno from cadre.cadr where " &
            "loccode = " & loccode &
            "and desgcode = " & olddesgcode &
            "and indx = " & oldindex
        oracn.FillData(sql, ds)

        If ds.Tables(0).Rows.Count <> 1 Then
            Return -1
        End If

        oldrowno = ds.Tables(0).Rows(0)(0)
        ds.Clear()
        ds.Dispose()
        Return oldrowno
    End Function
    Private Sub DoUpgradeTemp()
        Dim oracn As New OraDBconnection
        Dim sql As String
        Dim oldrowno As Integer

        If txtUpRemarks.Text = "" Then
            Utils.ShowMessage(Me, "Please enter remarks")
            Exit Sub
        End If
        If drpupdesg.SelectedValue = 0 Then
            Utils.ShowMessage(Me, "Please select a valid designation")
            Exit Sub
        End If
        oldrowno = GetOldRowNum()
        If oldrowno = -1 Then
            Utils.ShowMessage(Me, "Some error occured in upgrading")
            Exit Sub
        End If

        'insert info into temp_edit;
        sql = "insert into cadre.temp_edit(actionid, action,oldrowno,ndesg,branch, scaleid,ptype,remarks) values(" &
            GetActionID() & "," &
            "'U'," &
            oldrowno & "," &
            drpupdesg.SelectedValue & "," &
            drpupbranch.SelectedValue & "," &
            drpupscale.SelectedValue & "," &
             "'" & drpuptperm.SelectedValue & "'," &
            "'" & txtUpRemarks.Text & "'" &
            ")"

        oracn.ExecQry(sql)
    End Sub
    Private Sub DoDowngradeTemp()
        Dim oracn As New OraDBconnection
        Dim sql As String
        Dim oldrowno As Integer

        If txtDownRemarks.Text = "" Then
            Utils.ShowMessage(Me, "Please enter remarks")
            Exit Sub
        End If
        If drpdowndesg.SelectedValue = 0 Then
            Utils.ShowMessage(Me, "Please select a valid designation")
            Exit Sub
        End If
        oldrowno = GetOldRowNum()
        If oldrowno = -1 Then
            Utils.ShowMessage(Me, "Some error occured in downgrading")
        End If

        'insert info into temp_edit;
        sql = "insert into cadre.temp_edit (actionid, action,oldrowno,ndesg,branch, scaleid,ptype,remarks) values(" &
            GetActionID() & "," &
            "'D'," &
            oldrowno & "," &
            drpdowndesg.SelectedValue & "," &
            drpdownbranch.SelectedValue & "," &
            drpdownscale.SelectedValue & "," &
             "'" & drpdowntperm.SelectedValue & "'," &
            "'" & txtDownRemarks.Text & "'" &
            ")"

        oracn.ExecQry(sql)
    End Sub
    Private Sub DoEditTemp()
        Dim oracn As New OraDBconnection
        Dim sql As String
        Dim oldrowno As Integer
        Dim desg As Integer
        If txtEditRemarks.Text = "" Then
            Utils.ShowMessage(Me, "Please enter remarks")
            Exit Sub
        End If

        oldrowno = GetOldRowNum()
        If oldrowno = -1 Then
            Utils.ShowMessage(Me, "Some error occured in editing")
        End If

        'desg = drpdesg.SelectedValue.ToString().Split("-")(0)
        desg = drpEditDesg.SelectedValue
        'insert info into temp_edit;
        sql = "insert into cadre.temp_edit (actionid, action,oldrowno,ndesg,branch, scaleid,ptype,remarks) values(" &
            GetActionID() & "," &
            "'E'," &
            oldrowno & "," &
            desg & "," &
            drpeditbranch.SelectedValue & "," &
            drpeditscale.SelectedValue & "," &
             "'" & drpedittperm.SelectedValue & "'," &
            "'" & txtEditRemarks.Text & "'" &
            ")"

        oracn.ExecQry(sql)
    End Sub
    Private Sub DoAbolishTemp()
        Dim oracn As New OraDBconnection
        Dim sql As String
        Dim oldrowno As Integer

        If txtAbolishRemarks.Text = "" Then
            Utils.ShowMessage(Me, "Please enter remarks")
            Exit Sub
        End If

        oldrowno = GetOldRowNum()
        If oldrowno = -1 Then
            Utils.ShowMessage(Me, "Some error occured in abolishing")
        End If

        'insert info into temp_edit;
        sql = "insert into cadre.temp_edit (actionid, action,oldrowno,remarks) values(" &
            GetActionID() & "," &
            "'A'," &
            oldrowno & "," &
            "'" & txtAbolishRemarks.Text & "'" &
            ")"

        oracn.ExecQry(sql)
    End Sub
    Private Sub DoAbeyanceTemp()
        Dim oracn As New OraDBconnection
        Dim sql As String
        Dim oldrowno As Integer

        If txtAbeyanceRemarks.Text = "" Then
            Utils.ShowMessage(Me, "Please enter remarks")
            Exit Sub
        End If

        oldrowno = GetOldRowNum()
        If oldrowno = -1 Then
            Utils.ShowMessage(Me, "Some error occured in setting Held in Abeyance")
        End If

        'insert info into temp_edit;
        sql = "insert into cadre.temp_edit (actionid, action, oldrowno, remarks) values(" &
            GetActionID() & "," &
            "'H'," &
            oldrowno & "," &
            "'" & txtAbeyanceRemarks.Text & "'" &
            ")"

        oracn.ExecQry(sql)
    End Sub
    Protected Sub btnUpgrade_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpgrade.Click
        'DoUpgrade()
        DoUpgradeTemp()
        GridShow()
        PanSave.Visible = True
    End Sub
    Protected Sub txtupFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtupFilter.TextChanged
        filldesgAll(Me.drpupdesg, Me.txtupFilter.Text)
    End Sub
    Protected Sub txtdownFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdownFilter.TextChanged
        filldesgAll(Me.drpdowndesg, Me.txtdownFilter.Text)
    End Sub
    Private Sub StartDowngrade(ByVal branch As Integer, ByVal scaleid As Integer, ByVal ptype As String)
        panUpgrade.Visible = False
        panDowngrade.Visible = True
        panEdit.Visible = False
        PanAbolish.Visible = False
        panAbeyance.Visible = False
        filldesgAll(Me.drpdowndesg, "")
        fillscale(drpdownscale)
        drpdownbranch.SelectedValue = branch
        drpdownscale.SelectedValue = scaleid
        drpdowntperm.SelectedValue = ptype
    End Sub
    Private Function DoDown_NewRow(ByVal loccode As Long) As Long
        Dim oracn As New OraDBconnection
        Dim newdesgcode As Integer
        Dim newindx As Integer
        Dim newscaleid As Integer
        Dim newrowno As Long
        Dim newbranch As String
        Dim newptype As String
        Dim sql As String

        'updated desgcode is from drpupdesg
        newdesgcode = drpdowndesg.SelectedValue

        'new index is from Utils.GetIndex()
        newindx = Utils.GetIndex(loccode, newdesgcode)

        'scaleid is from drpupscale
        newscaleid = drpdownscale.SelectedValue

        'rno from Utils.GetRowNum()
        newrowno = Utils.GetRowNum()

        'branch from drpupbranch
        newbranch = drpdownbranch.SelectedValue

        'ptype from drpuptperm
        newptype = drpdowntperm.SelectedValue

        sql = "insert into cadre.cadr  values(" & loccode & "," & newdesgcode & "," & newindx & "," & newscaleid & ", " & newrowno & "," & newbranch & ",'" & newptype & "','A',NULL)"
        oracn.ExecQry(sql)

        Return newrowno
    End Function
    Private Sub DoDowngrade()
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim oldrowno As Long
        Dim olddesgcode As Integer
        Dim oldindx As Integer
        Dim newrowno As Long
        Dim loccode As Long
        Dim oldscaleid As Integer
        Dim oldbranch As Integer
        Dim oldptype As String = ""

        If txtDownRemarks.Text = "" Then
            Utils.ShowMessage(Me, "Please enter remarks")
            Exit Sub
        End If

        loccode = GetLocCode()
        'get necessary info about old row from cadr
        oldrowno = GetOldInfo(loccode, olddesgcode, oldindx, oldscaleid, oldbranch, oldptype)
        If oldrowno = -1 Then
            Exit Sub
        End If

        'delete old row using rowno
        sql = "delete from cadre.cadr where rowno = " & oldrowno
        oracn.ExecQry(sql)

        'add new row in cadr
        newrowno = DoDown_NewRow(loccode)

        'save history in trans_hist
        sql = "insert into cadre.trans_hist values(" &
            loccode & "," &
            olddesgcode & ", " &
            oldindx & ", " &
            oldrowno & ", " &
            newrowno & ", " &
            "(select sysdate from dual), '" &
            txtDownRemarks.Text & "', " &
            "'DG', " &
            oldscaleid & ", " &
            oldbranch & ", " &
            "'" & oldptype & "', " &
            "'" & Session.SessionID & "', " &
             "NULL, " &
             Utils.GetOfficeOrderNum() &
             ")"

        oracn.ExecQry(sql)

        Utils.ShowMessage(Me, "Post Downgraded")
    End Sub
    Protected Sub btnDowngrade_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDowngrade.Click
        'DoDowngrade()
        DoDowngradeTemp()
        GridShow()
        PanSave.Visible = True
    End Sub
    Private Sub DoDowngrade2(ByVal oldrowno As Integer, ByVal ndesg As Integer, ByVal nbranch As Integer,
                             ByVal nscaleid As Integer, ByVal nptype As String, ByVal remarks As String, ByVal oonum As Integer)
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim olddesgcode As Integer
        Dim oldindx As Integer
        Dim oldscaleid As Integer
        Dim oldbranch As Integer
        Dim oldptype As String
        Dim newrowno As Long
        Dim loccode As Long
        Dim newindx As Integer

        oldptype = ""
        'get necessary info about old row from cadr
        GetOldInfoByRowno(oldrowno, loccode, olddesgcode, oldindx, oldscaleid, oldbranch, oldptype)

        'delete old row using rowno
        sql = "delete from cadre.cadr where rowno = " & oldrowno
        oracn.ExecQry(sql)

        'add new row in cadr

        'new index is from Utils.GetIndex()
        newindx = Utils.GetIndex(loccode, ndesg)

        'rno from Utils.GetRowNum()
        newrowno = Utils.GetRowNum()

        sql = "insert into cadre.cadr  values(" & loccode & "," & ndesg & "," & newindx & "," & nscaleid & ", " & newrowno & "," & nbranch & ",'" & nptype & "','A',NULL,NULL)"
        oracn.ExecQry(sql)

        'newrowno = DoUp_NewRow(loccode)

        'save in trans_hist
        sql = "insert into cadre.trans_hist values(" &
             loccode & "," &
             olddesgcode & ", " &
             oldindx & ", " &
             oldrowno & ", " &
             newrowno & ", " &
             "(select sysdate from dual), '" &
             remarks & "', " &
             "'DG', " &
             oldscaleid & ", " &
             oldbranch & ", " &
             "'" & oldptype & "', " &
             "'" & Session.SessionID & "', " &
             "NULL, " &
             oonum &
             ")"

        oracn.ExecQry(sql)
    End Sub
    Private Sub StartEdit(ByVal desg As Integer, ByVal branch As Integer, ByVal scaleid As Integer, ByVal ptype As String)
        panUpgrade.Visible = False
        panDowngrade.Visible = False
        panEdit.Visible = True
        PanAbolish.Visible = False
        panAbeyance.Visible = False
        filldesgAll(Me.drpEditDesg, "")
        fillscale(drpeditscale)
        drpeditbranch.SelectedValue = branch
        drpeditscale.SelectedValue = scaleid
        drpedittperm.SelectedValue = ptype
        drpEditDesg.SelectedValue = desg
    End Sub
    Private Sub DoEdit()
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim rowno As Long
        Dim desgcode As Integer
        Dim indx As Integer
        Dim loccode As Long
        Dim newscaleid As Integer
        Dim newbranch As String
        Dim newptype As String
        Dim oldscaleid As Integer
        Dim oldbranch As Integer
        Dim oldptype As String = ""

        loccode = GetLocCode()
        'get necessary info about row from cadr
        rowno = GetOldInfo(loccode, desgcode, indx, oldscaleid, oldbranch, oldptype)
        If rowno = -1 Then
            Exit Sub
        End If

        'scaleid is from drpupscale
        newscaleid = drpeditscale.SelectedValue

        'branch from drpupbranch
        newbranch = drpeditbranch.SelectedValue

        'ptype from drpuptperm
        newptype = drpedittperm.SelectedValue

        sql = "update cadre.cadr set scaleid = " & newscaleid & ", branch = " & newbranch & ", ptype = '" & newptype & "' where rowno = " & rowno
        oracn.ExecQry(sql)

        Utils.ShowMessage(Me, "Post Edited")
    End Sub
    Private Sub DoEdit2(ByVal oldrowno As Integer, ByVal ndesg As Integer, ByVal nbranch As Integer,
                        ByVal nscaleid As Integer, ByVal nptype As String, ByVal remarks As String, ByVal oonum As Integer)
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim loccode As Long
        Dim oldscaleid As Integer
        Dim oldbranch As Integer
        Dim oldptype As String
        Dim olddesgcode As Integer
        Dim oldindx As Integer
        Dim nindx As Integer

        oldptype = ""
        'get necessary info about old row from cadr
        GetOldInfoByRowno(oldrowno, loccode, olddesgcode, oldindx, oldscaleid, oldbranch, oldptype)

        nindx = Utils.GetIndex(loccode, ndesg)
        sql = "update cadre.cadr set desgcode= " & ndesg & ", scaleid = " & nscaleid & ", indx = " & nindx & ", branch = " & nbranch & ", ptype = '" & nptype & "' where rowno = " & oldrowno
        oracn.ExecQry(sql)

        'save in trans_hist
        sql = "insert into cadre.trans_hist values(" &
             loccode & "," &
             olddesgcode & ", " &
             oldindx & ", " &
             oldrowno & ", " &
              "0, " &
             "(select sysdate from dual), '" &
             remarks & "', " &
             "'ED', " &
             oldscaleid & ", " &
             oldbranch & ", " &
             "'" & oldptype & "', " &
             "'" & Session.SessionID & "', " &
             "NULL, " &
             oonum &
             ")"
        oracn.ExecQry(sql)
    End Sub
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        'DoEdit()
        DoEditTemp()
        GridShow()
        PanSave.Visible = True
    End Sub
    Private Sub StartAbolish()
        panUpgrade.Visible = False
        panDowngrade.Visible = False
        panEdit.Visible = False
        PanAbolish.Visible = True
        panAbeyance.Visible = False
    End Sub
    Private Sub StartAbeyance()
        panUpgrade.Visible = False
        panDowngrade.Visible = False
        panEdit.Visible = False
        PanAbolish.Visible = False
        panAbeyance.Visible = True
    End Sub
    Private Sub DoAbolish()
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim oldrowno As Long
        Dim olddesgcode As Integer
        Dim oldindx As Integer
        Dim loccode As Long
        Dim oldscaleid As Integer
        Dim oldbranch As Integer
        Dim oldptype As String = ""
        Dim sessionid As String

        If txtAbolishRemarks.Text = "" Then
            Utils.ShowMessage(Me, "Please enter remarks")
            Exit Sub
        End If

        loccode = GetLocCode()

        sessionid = Session.SessionID
        'get necessary info about old row from cadr
        oldrowno = GetOldInfo(loccode, olddesgcode, oldindx, oldscaleid, oldbranch, oldptype)
        If oldrowno = -1 Then
            Exit Sub
        End If

        'delete old row using rowno
        sql = "delete from cadre.cadr where rowno = " & oldrowno
        oracn.ExecQry(sql)

        'save history in trans_hist
        sql = "insert into cadre.trans_hist values(" &
            loccode & "," &
            olddesgcode & ", " &
            oldindx & ", " &
            oldrowno & ", " &
            "-1, " &
            "(select sysdate from dual), '" &
            txtAbolishRemarks.Text & "', " &
            "'AB', " &
            oldscaleid & ", " &
            oldbranch & ", " &
            "'" & oldptype & "', " &
           "'" & Session.SessionID & "', " &
             "NULL, " &
             Utils.GetOfficeOrderNum() &
             ")"
        oracn.ExecQry(sql)
        Utils.ShowMessage(Me, "Post Abolished")
    End Sub
    Private Sub DoAbolish2(ByVal oldrowno As Integer, ByVal ndesg As Integer, ByVal nbranch As Integer,
                           ByVal nscaleid As Integer, ByVal nptype As String, ByVal remarks As String, ByVal oonum As Integer)
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim loccode As Long
        Dim oldscaleid As Integer
        Dim oldbranch As Integer
        Dim oldptype As String
        Dim olddesgcode As Integer
        Dim oldindx As Integer

        oldptype = ""
        'get necessary info about old row from cadr
        GetOldInfoByRowno(oldrowno, loccode, olddesgcode, oldindx, oldscaleid, oldbranch, oldptype)

        sql = "delete from cadre.cadr where rowno = " & oldrowno
        oracn.ExecQry(sql)

        'save in trans_hist
        sql = "insert into cadre.trans_hist values(" &
            loccode & "," &
            olddesgcode & ", " &
            oldindx & ", " &
            oldrowno & ", " &
            "-1, " &
            "(select sysdate from dual), '" &
            remarks & "', " &
            "'AB', " &
            oldscaleid & ", " &
            oldbranch & ", " &
            "'" & oldptype & "', " &
           "'" & Session.SessionID & "', " &
             "NULL, " &
            oonum &
             ")"
        oracn.ExecQry(sql)
    End Sub
    Private Sub DoAbeyance2(ByVal oldrowno As Integer, ByVal ndesg As Integer, ByVal nbranch As Integer,
                            ByVal nscaleid As Integer, ByVal nptype As String, ByVal remarks As String, ByVal oonum As Integer)
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim loccode As Long
        Dim oldscaleid As Integer
        Dim oldbranch As Integer
        Dim oldptype As String
        Dim olddesgcode As Integer
        Dim oldindx As Integer

        oldptype = ""
        'get necessary info about old row from cadr
        GetOldInfoByRowno(oldrowno, loccode, olddesgcode, oldindx, oldscaleid, oldbranch, oldptype)

        sql = "update cadre.cadr set hia = 1 where rowno = " & oldrowno
        oracn.ExecQry(sql)

        'save in trans_hist
        sql = "insert into cadre.trans_hist values(" &
            loccode & "," &
            olddesgcode & ", " &
            oldindx & ", " &
            oldrowno & ", " &
            "-1, " &
            "(select sysdate from dual), '" &
            remarks & "', " &
            "'HA', " &
            oldscaleid & ", " &
            oldbranch & ", " &
            "'" & oldptype & "', " &
           "'" & Session.SessionID & "', " &
             "NULL, " &
            oonum &
             ")"
        oracn.ExecQry(sql)
    End Sub

    Protected Sub btnAbolish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAbolish.Click
        'DoAbolish()
        DoAbolishTemp()
        GridShow()
        PanSave.Visible = True
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim i As Integer
        Dim actionid, oldrowno, ndesg, branch, scaleid As Integer
        Dim action, ptype, remarks As String
        Dim oonum As Integer

        If Session("post_edit_cclist") <> "1" Then
            Utils.ShowMessage(Me, "Create CC list first")
            Return
        End If

        oonum = Utils.GetOfficeOrderNum()

        sql = "update cadre.temp_edit set oonum = " & oonum
        oracn.ExecQry(sql)

        GenerateReport(oonum)

        sql = "select actionid,action,oldrowno,ndesg,branch,scaleid,ptype,remarks from cadre.temp_edit order by actionid"
        oracn.FillData(sql, ds)

        'exit if nothing to save
        If ds.Tables(0).Rows.Count = 0 Then
            Return
        End If

        ptype = ""
        For i = 0 To ds.Tables(0).Rows.Count - 1
            actionid = ds.Tables(0).Rows(i)("actionid")
            action = ds.Tables(0).Rows(i)("action")
            oldrowno = ds.Tables(0).Rows(i)("oldrowno")
            If action <> "A" And action <> "H" Then
                ndesg = ds.Tables(0).Rows(i)("ndesg")
                branch = ds.Tables(0).Rows(i)("branch")
                scaleid = ds.Tables(0).Rows(i)("scaleid")
                ptype = ds.Tables(0).Rows(i)("ptype")
            End If

            remarks = ds.Tables(0).Rows(i)("remarks")
            If action = "U" Then
                DoUpgrade2(oldrowno, ndesg, branch, scaleid, ptype, remarks, oonum)
            ElseIf action = "D" Then
                DoDowngrade2(oldrowno, ndesg, branch, scaleid, ptype, remarks, oonum)
            ElseIf action = "E" Then
                DoEdit2(oldrowno, ndesg, branch, scaleid, ptype, remarks, oonum)
            ElseIf action = "A" Then
                DoAbolish2(oldrowno, ndesg, branch, scaleid, ptype, remarks, oonum)
            ElseIf action = "H" Then
                DoAbeyance2(oldrowno, ndesg, branch, scaleid, ptype, remarks, oonum)
            End If
        Next

        'delete rows from temp_edit
        oracn.ExecQry("delete from cadre.temp_edit")

        ds.Clear()
        ds.Dispose()
        Dim pdfpath As String
        pdfpath = GeneratePDF(oonum)
        DownloadFile(pdfpath)
        'Response.Write("<script type='text/javascript'>detailedresults=window.open('rpt_edit_post.aspx','_new');</script>")
    End Sub
    Protected Sub DownloadFile(ByVal pdfPath As String)
        Dim objFi As New System.IO.FileInfo(pdfPath)
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + objFi.Name)
        HttpContext.Current.Response.Charset = ""
        HttpContext.Current.Response.AddHeader("Content-Length", objFi.Length.ToString())
        HttpContext.Current.Response.ContentType = "application/pdf"
        HttpContext.Current.Response.WriteFile(objFi.FullName)
        HttpContext.Current.Response.End()
    End Sub
    Protected Sub btnGenPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenPreview.Click
        Dim pdfpath As String
        If Session("post_edit_cclist") <> "1" Then
            Utils.ShowMessage(Me, "Create CC list first")
            Return
        End If
        GenerateReport(0)
        pdfpath = GeneratePDF(0)
        DownloadFile(pdfpath)
    End Sub
    Private Function GeneratePDF(ByVal oonum As Integer) As String
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sysdate As String
        Dim pdfPath As String

        'get sysdate
        oracn.FillData("select to_char(sysdate,'dd-mm-yyyy') from dual", ds)
        sysdate = ds.Tables(0).Rows(0)(0)
        ds.Clear()

        pdfPath = Server.MapPath("office_orders\\" & oonum & "-" & sysdate & ".pdf")

        sql = "select pshr.get_org(loccode),pshr.get_desg(odesg) as odesg,pshr.get_desg(ndesg) as ndesg," &
                "cadre.get_branch(obranch) as obranch,cadre.get_branch(nbranch) as nbranch,optype, nptype," &
                "cadre.get_scale_txt(oscaleid) as oscale,cadre.get_scale_txt(nscaleid) as nscale,oonum," &
                "odate,action,remarks from cadre.rptedit "

        oracn.FillData(sql, ds)

        'save office order at server
        Dim CrystalReportSource1 As New CrystalReportSource()
        CrystalReportSource1.Report.FileName = Server.MapPath("Reports\\rpt_edit_post.rpt")
        CrystalReportSource1.ReportDocument.SetDatabaseLogon("pshr", "pshr")
        CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
        CrystalReportSource1.DataBind()


        Dim ds2 As New System.Data.DataSet
        sql = "select * from cadre.cclist where ccnum > 0 order by ccnum"
        oracn.FillData(sql, ds2)
        CrystalReportSource1.ReportDocument.Subreports(0).SetDataSource(ds2.Tables(0))
        CrystalReportSource1.DataBind()

        CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath)
        Return pdfPath
    End Function
    Private Sub GenerateReport(ByVal oonum As Long)
        Dim sql As String
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim ds2 As New System.Data.DataSet
        Dim i As Integer
        Dim actionid As Integer
        Dim action As String
        Dim loccode As Long
        Dim oldrowno As Integer
        Dim odesg, ndesg As Integer
        Dim oindx, nindx As Integer
        Dim oscale, nscale As Integer
        Dim obranch, nbranch As Integer
        Dim optype, nptype As Char
        Dim remarks As String
        ds = New System.Data.DataSet
        sql = "select actionid,action,oldrowno,ndesg,branch,scaleid,ptype,remarks from cadre.temp_edit order by actionid"
        oracn.FillData(sql, ds)

        'exit if nothing to save
        If ds.Tables(0).Rows.Count = 0 Then
            Return
        End If

        oracn.ExecQry("DELETE FROM cadre.rptedit")

        For i = 0 To ds.Tables(0).Rows.Count - 1
            actionid = ds.Tables(0).Rows(i)("actionid")
            action = ds.Tables(0).Rows(i)("action")
            oldrowno = ds.Tables(0).Rows(i)("oldrowno")
            If action <> "A" And action <> "H" Then
                ndesg = ds.Tables(0).Rows(i)("ndesg")
                nbranch = ds.Tables(0).Rows(i)("branch")
                nscale = ds.Tables(0).Rows(i)("scaleid")
                nptype = ds.Tables(0).Rows(i)("ptype")
                nindx = Utils.GetIndex(loccode, ndesg)
            End If
            remarks = ds.Tables(0).Rows(i)("remarks")
            ds2 = New System.Data.DataSet
            sql = "select loccode, desgcode, indx, scaleid,branch,ptype from cadre.cadr where rowno = " & oldrowno
            oracn.FillData(sql, ds2)

            loccode = ds2.Tables(0).Rows(0)("loccode")
            odesg = ds2.Tables(0).Rows(0)("desgcode")
            oindx = ds2.Tables(0).Rows(0)("indx")
            oscale = ds2.Tables(0).Rows(0)("scaleid")
            obranch = ds2.Tables(0).Rows(0)("branch")
            optype = ds2.Tables(0).Rows(0)("ptype")

            If action = "A" Then
                sql = "INSERT INTO cadre.rptedit VALUES(" &
                loccode & "," &
                odesg & "," &
                "NULL," &
                oindx & "," &
                "NULL," &
                oscale & "," &
                "NULL," &
                obranch & "," &
                "NULL," &
                "'" & optype & "'," &
                "NULL," &
                "'" & action & "'," &
                actionid & "," &
                oonum & "," &
                "sysdate," &
                "'" & remarks & "')"
            ElseIf action = "H" Then
                sql = "INSERT INTO cadre.rptedit VALUES(" &
                loccode & "," &
                odesg & "," &
                "NULL," &
                oindx & "," &
                "NULL," &
                oscale & "," &
                "NULL," &
                obranch & "," &
                "NULL," &
                "'" & optype & "'," &
                "NULL," &
                "'" & action & "'," &
                actionid & "," &
                oonum & "," &
                "sysdate," &
                "'" & remarks & "')"
            Else
                sql = "INSERT INTO cadre.rptedit VALUES(" &
                loccode & "," &
                odesg & "," &
                ndesg & "," &
                oindx & "," &
                nindx & "," &
                oscale & "," &
                nscale & "," &
                obranch & "," &
                nbranch & "," &
                "'" & optype & "'," &
                "'" & nptype & "'," &
                "'" & action & "'," &
                actionid & "," &
                oonum & "," &
                "sysdate," &
                "'" & remarks & "')"
            End If

            oracn.ExecQry(sql)
        Next
        ds.Clear()
        ds.Dispose()
        ds2.Clear()
        ds2.Dispose()
    End Sub
    Private Sub FillBSPInfo()
        Dim branch As Integer
        Dim scaleid As Integer
        Dim ptype As String
        Dim desg As String
        Dim seldesg As String

        ptype = ""
        If GetBSPInfo(branch, scaleid, ptype) = False Then
            Utils.ShowMessage(Me, "An error occured in getting info about selected post")
        End If

        seldesg = drpdesg.SelectedValue
        desg = seldesg.Substring(0, seldesg.IndexOf("-"))
        If rbActions.SelectedValue = "U" Then
            drpupbranch.SelectedValue = branch
            drpupscale.SelectedValue = scaleid
            drpuptperm.SelectedValue = ptype
            drpEditDesg.SelectedValue = drpdesg.SelectedValue
        ElseIf rbActions.SelectedValue = "D" Then
            drpdownbranch.SelectedValue = branch
            drpdownscale.SelectedValue = scaleid
            drpdowntperm.SelectedValue = ptype
            drpEditDesg.SelectedValue = drpdesg.SelectedValue
        ElseIf rbActions.SelectedValue = "E" Then
            drpeditbranch.SelectedValue = branch
            drpeditscale.SelectedValue = scaleid
            drpedittperm.SelectedValue = ptype
            drpEditDesg.SelectedValue = desg
        End If
    End Sub
    Protected Sub drpdesg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdesg.SelectedIndexChanged
        FillBSPInfo()
    End Sub
    Protected Sub drpdesg_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdesg.TextChanged
        FillBSPInfo()
    End Sub

    Protected Sub btnCreateCC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateCC.Click
        Response.Write("<script type='text/javascript'>detailedresults=window.open('cc.aspx','_new');</script>")
        Session("post_edit_cclist") = "1"
    End Sub

    Protected Sub btnDelSession_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelSession.Click
        Dim oracn As New OraDBconnection

        'delete temporary rows
        oracn.ExecQry("DELETE FROM cadre.temp_edit")
        lblPending.Text = ""
        Utils.ShowMessage(Me, "Session Deleted")
    End Sub

    Protected Sub txtEditFilter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEditFilter.TextChanged
        filldesgAll(Me.drpEditDesg, Me.txtEditFilter.Text)
    End Sub

    Protected Sub drpsuboff_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpsuboff.SelectedIndexChanged
        filldesgExisting(Me.drpdesg)
        EnableControls()
    End Sub
    Private Sub GridShow()
        'Dim oracn As New OraDBconnection
        'Dim sql As String
        'Dim ds As New System.Data.DataSet
        'sql = "select * from cadre.temp_edit"
        'oracn.FillData(sql, ds)
        'oracn.fillgrid(gvChanges, ds)
    End Sub
    Protected Sub btnAbeyance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAbeyance.Click
        DoAbeyanceTemp()
        GridShow()
        PanSave.Visible = True
    End Sub

    Protected Sub btnRemAbeyance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemAbeyance.Click
        Dim oracn As New OraDBconnection
        Dim oldrowno As Integer
        Dim sql As String

        oldrowno = GetOldRowNum()
        sql = "update cadre.cadr set hia=0 where rowno = " & oldrowno
        oracn.ExecQry(sql)
        Utils.ShowMessage(Me, "Held in Abeyance Removed")
        filldesgExisting(Me.drpdesg)
    End Sub
End Class
