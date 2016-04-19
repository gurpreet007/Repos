Imports System.Data.DataSet
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loccode As Long
        Dim sql As String
        Dim row1, row2 As System.Data.DataRow
        Dim oracn As New OraDBconnection
        Dim ds1, ds2, ds3, ds4 As System.Data.DataSet
        Dim srno As Integer

        GetAttached(srno, loccode)

        'get SE/XEN/AEE/Suboffice(SO) under this chief
        ds1 = New System.Data.DataSet
        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & loccode & " and aloc=1 and loctype in (30,40,50,60,70) order by loctype desc"
        oracn.FillData(sql, ds1)

        For Each row1 In ds1.Tables(0).Rows
            If row1("loctype") = 30 Then    'SE under CE
                GetAttached(srno, row1("loccode"))    'A/W SE

                'get underlying XEN,AEE,SO to this SE
                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (40,50,60,70) order by loctype desc"
                ds2 = New System.Data.DataSet
                oracn.FillData(sql, ds2)

                For Each row2 In ds2.Tables(0).Rows
                    If row2("loctype") = 40 Then     'XEN Under SE Under CE
                        GetAttached(srno, row2("loccode"))    'A/W XEN

                        'get underlying AEE and S/O to this XEN
                        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (50,60,70) order by loctype desc"
                        ds3 = New System.Data.DataSet
                        oracn.FillData(sql, ds3)
                        For Each row3 In ds3.Tables(0).Rows
                            If row3("loctype") = 50 Then        'AEE Under XEN Under SE 
                                GetAttached(srno, row3("loccode"))  'A/W AEE

                                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row3("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                                ds4 = New System.Data.DataSet
                                oracn.FillData(sql, ds4)
                                For Each row4 In ds4.Tables(0).Rows
                                    GetAttached(srno, row4("loccode"))    'SO Under AEE Under XEN Under SE 
                                Next
                            ElseIf row3("loctype") = 60 Or row3("loctype") = 70 Then        'SO Under XEN
                                GetAttached(srno, row3("loccode"))    'SO Under AEE Under SE 
                            End If
                        Next
                    ElseIf row2("loctype") = 50 Then   'AEE under SE
                        GetAttached(srno, row2("loccode"))    'A/W AEE
                        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                        ds3 = New System.Data.DataSet
                        oracn.FillData(sql, ds3)
                        For Each row3 In ds3.Tables(0).Rows
                            GetAttached(srno, row3("loccode"))    'SO Under AEE Under SE Under CE
                        Next
                    ElseIf row2("loctype") = 60 Or row2("loctype") = 70 Then   'SO under SE
                        GetAttached(srno, row2("loccode"))
                    End If
                Next
                ds2.Clear()
                ds2.Dispose()

                'if officer is Sr. XEN
            ElseIf row1("loctype") = 40 Then            'XEN under CE
                ds2 = New System.Data.DataSet
                GetAttached(srno, row1("loccode"))      'A/W XEN
                'get underlying AEE to this XEN
                'GetUnderAEE(srno, row1("loccode"))
                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (50,60,70) order by loctype desc"
                oracn.FillData(sql, ds2)

                For Each row2 In ds2.Tables(0).Rows
                    If row2("loctype") = 50 Then   'AEE under XEN
                        GetAttached(srno, row2("loccode"))    'A/W AEE
                        sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row2("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                        ds3 = New System.Data.DataSet
                        oracn.FillData(sql, ds3)
                        For Each row3 In ds3.Tables(0).Rows
                            GetAttached(srno, row3("loccode"))    'SO Under AEE Under XEN Under CE
                        Next
                    ElseIf row2("loctype") = 60 Or row2("loctype") = 70 Then   'SO under XEN
                        GetAttached(srno, row2("loccode"))
                    End If
                Next

                'if officer is AEE/AE
            ElseIf row1("loctype") = 50 Then   'AEE under SE
                GetAttached(srno, row1("loccode"))    'A/W AEE
                sql = "select loccode,loctype from pshr.mast_loc where locrep=" & row1("loccode") & " and aloc=1 and loctype in (60,70) order by loctype desc"
                ds2 = New System.Data.DataSet
                oracn.FillData(sql, ds2)
                For Each row2 In ds2.Tables(0).Rows
                    GetAttached(srno, row2("loccode"))    'SO Under AEE Under CE
                Next
            ElseIf row1("loctype") = 60 Or row1("loctype") = 70 Then   'SO under CE
                GetAttached(srno, row1("loccode"))
            End If
        Next
    End Sub
    Private Sub GetAttached(ByRef srno As Integer, ByVal loccode As Long)
        Dim sql As String
        Dim row As System.Data.DataRow
        Dim oracn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim a As New Integer
        sql = "select rowno,srno from cadre.cadr where loccode=" & loccode & " order by scaleid desc,desgcode"
        oracn.FillData(sql, ds)

        'assign serial no. to posts under this officer
        For Each row In ds.Tables(0).Rows
            ' If IsDBNull(row("srno")) Then
            sql = "update cadre.cadr set srno=" & srno & " where rowno=" & row("rowno")
            oracn.ExecQry(sql)
            srno += 1
            'End If
        Next
    End Sub
End Class
