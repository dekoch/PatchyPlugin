Class frmCOM


    Private Sub frmCOM_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        bolAbortAll = True
        Application.Exit()

    End Sub
End Class