
Partial Class Mainpage
    Inherits System.Web.UI.Page

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Utils.AuthenticateUser(Me)
        FillCounts()
    End Sub

    Private Sub FillCounts()
        Dim oraCn As New OraDBconnection
        Dim sql As String
        Dim ds As New System.Data.DataSet
        Dim ds1 As New System.Data.DataSet
        Dim ds2 As New System.Data.DataSet
        Dim loc As Long
        Dim pcount, tcount, xcount, mcount, total As Integer

        'sql = "select " +
        '      "(select count(*) from cadre.cadr where ptype = 'P') as Permanent, " +
        '      "(select count(*) from cadre.cadr where ptype = 'T') as Temporary, " +
        '      "(select count(*) from cadre.cadr where ptype = 'X') as ""X-Cadre"", " +
        '      "(select count(*) from cadre.cadr where ptype = 'M') as ""Pers Meas"", " +
        '      "(select count(*) from cadre.cadr where ptype in ('P','T','X','M')) as Total " +
        '      "from dual"

        'oraCn.FillData(sql, ds)
        'oraCn.fillgrid(gvCount, ds)

        pcount = 0
        tcount = 0
        xcount = 0
        mcount = 0

        sql = "select distinct (substr(l.loccode,1,3)),l.loctype from cadre.cadr c, pshr.mast_loc l where c.loccode=l.loccode and l.loctype not in (30,40,50,60,70)  order by l.loctype"
        oraCn.FillData(sql, ds)
        For i = 0 To ds.Tables(0).Rows.Count - 1
           
            loc = ds.Tables(0).Rows(i)(0) & "000000"
            If Mid$(loc, 1, 2) = "80" Then
                sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
                             "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc = 1 and loctype in (11,30,40,50,60)) group by loccode,ptype) a where ptype = 'P' " &
                             " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc = 1 and loctype in (11,30,40,50,60)) group by loccode,ptype) a where ptype = 'T' " &
                             " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc = 1 and loctype in (11,30,40,50,60)) group by loccode,ptype) a where ptype = 'X' " &
                             " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc = 1 and loctype in (11,30,40,50,60)) group by loccode,ptype) a where ptype = 'M')" &
                             " b group by substr(loccode,1,3) order by substr(loccode,1,3) "
            Else
                sql = " select pshr.get_org(" & loc & " ) as loc ,substr(loccode,1,3),  sum(cnt1),sum(cnt2),sum(cnt3),sum(cnt4)  from (" &
                         "           select loccode, cnt as cnt1, 0   as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by loccode,ptype) a where ptype = 'P' " &
                         " union all select loccode, 0   as cnt1, cnt as cnt2, 0   as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by loccode,ptype) a where ptype = 'T' " &
                         " union all select loccode, 0   as cnt1, 0   as cnt2, cnt as cnt3, 0   as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by loccode,ptype) a where ptype = 'X' " &
                         " union all select loccode, 0   as cnt1, 0   as cnt2, 0   as cnt3, cnt as cnt4 from (select loccode, count(*) as cnt,ptype from cadre.cadr where loccode in (select loccode from pshr.mast_loc start with loccode=" & loc & " connect by prior loccode=locrep and aloc=1) group by loccode,ptype) a where ptype = 'M')" &
                         " b group by substr(loccode,1,3) order by substr(loccode,1,3)"
            End If

            ds1 = New System.Data.DataSet
            oraCn.FillData(sql, ds1)

            For j = 0 To ds1.Tables(0).Rows.Count - 1
                pcount += ds1.Tables(0).Rows(j)(2)
                tcount += ds1.Tables(0).Rows(j)(3)
                xcount += ds1.Tables(0).Rows(j)(4)
                mcount += ds1.Tables(0).Rows(j)(5)
            Next j
        Next i
        total = pcount + tcount + xcount + mcount

        sql = "select " & pcount & " as Perm, " &
                          tcount & " as Temp, " &
                          xcount & " as ExCadre, " &
                          mcount & " as PersMeas, " &
                          total & " as Total " &
                          "from dual"

        oraCn.FillData(sql, ds2)
        oraCn.fillgrid(gvCount, ds2)
    End Sub
End Class
