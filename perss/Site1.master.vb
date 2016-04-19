Partial Class Site1
    Inherits System.Web.UI.MasterPage
    Private Sub HideNav()
        If Session("role") Is Nothing Then
            NavigationMenu.Visible = False
        ElseIf (Len(Session("role")) > 0 And Session("role") = "V") Then
            NavigationMenu.Items.Remove(NavigationMenu.FindItem("Home"))
            NavigationMenu.Items.Remove(NavigationMenu.FindItem("Post"))
            NavigationMenu.Items.Remove(NavigationMenu.FindItem("Location"))
            NavigationMenu.Items.Remove(NavigationMenu.FindItem("Designation"))
            NavigationMenu.Items.Remove(NavigationMenu.FindItem("Change Password"))
            NavigationMenu.Items(0).ChildItems.Remove(NavigationMenu.FindItem("Reports/SQL Bin"))
            NavigationMenu.Items(0).ChildItems.Remove(NavigationMenu.FindItem("Reports/Abstracts"))
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        HideNav()
        If Utils.dummy Then
            lblDummy.Text = "DUMMY"
        End If
        'End If
    End Sub

    Sub NavigationMenu_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs)

        ' Display the text of the menu item selected by the user.

    End Sub
End Class

