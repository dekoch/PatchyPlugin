Class ucManualControl

    Dim main As New main

    Private Sub ucManualControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lblRx.Text = ""

        'cmbCommand.Items.AddRange(arrCommands)

        'cmbCommand.Items.Add("#SOURCE0_Terminal")
        'cmbCommand.Items.Add("#MEAS0_Terminal")

        For i As Integer = 0 To UBound(arrCommandsBDG)

            If arrCommandsBDG(i).StartsWith("#MEAS") Or arrCommandsBDG(i).StartsWith("#SOURCE") Or arrCommandsBDG(i).StartsWith("#IO") Then

                cmbCommand.Items.Add(arrCommandsBDG(i))

            End If


        Next

        cmbCommand.SelectedText = "#MEAS0_MEAS_VALUE"
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Send()
    End Sub

    Public Sub Send()

        main.SetValue(cmbCommand.Text, txtTx.Text)

        Select Case True

            Case cmbCommand.Text.StartsWith("#MEAS")
                lblRx.Text = strMeasureRx

            Case cmbCommand.Text.StartsWith("#SOURCE")
                lblRx.Text = strSourceRx

            Case cmbCommand.Text.StartsWith("#IO")
                lblRx.Text = strIORx

        End Select

        strMeasureRx = ""
        strSourceRx = ""

    End Sub

    Private Sub txtTx_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTx.KeyPress
        If Asc(e.KeyChar) = 13 Then

            Send()
            txtTx.Text = ""

        End If
    End Sub
End Class
