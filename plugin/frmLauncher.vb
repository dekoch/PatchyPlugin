Class frmLauncher

    Dim frmMeasureSource As New frmMeasureSource
    Dim main As New main
    Dim frmCOM As New frmCOM


    Private Sub frmLauncher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = "Launcher plugin.dll"
        lblAbout.Text = "plugin.dll (" & strPluginName & ") v" & strPluginVersion & vbCrLf & "www.mariokoch.de"
        lblAbout.Top = Me.ClientSize.Height - lblAbout.Height

        btnMeasureSource.Enabled = True
        btnCOM.Enabled = False
        btnDebug.Enabled = False

    End Sub


    Private Sub btnMeasureSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeasureSource.Click

        frmMeasureSource.Show()
        Me.Hide()

    End Sub

    Private Sub btnDebug_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDebug.Click

        main.Show()
        Me.Hide()

    End Sub

    Private Sub btnCOM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCOM.Click

        frmCOM.Show()
        Me.Hide()

    End Sub
End Class