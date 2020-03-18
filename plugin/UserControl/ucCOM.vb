Imports System.IO.Ports

Class ucCOM

    Dim WithEvents COM As New clsCOM

    Dim arrPorts(0 To 50) As String
    Dim WithEvents SerialPort As New System.IO.Ports.SerialPort("COM1", 9600, Parity.None, 8, StopBits.One)

    Private Sub ucCOM_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        COM.ClosePort()
    End Sub

    Private Sub ucCOM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblLog.Text = ""
        btnOpen.Enabled = False
        btnClose.Enabled = False
        btnSend.Enabled = False
        txtTx.Enabled = False

        FindPorts()

    End Sub

    Private Sub btnOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpen.Click

        UserLevel("0")

        COM.Init()
        COM.SetValue("port", cmbPort.Text)
        COM.SetValue("baud", txtBaud.Text)
        COM.OpenPort("", 2000)

        btnOpen.Enabled = False
        btnClose.Enabled = True
        btnSend.Enabled = True
        txtTx.Enabled = True

    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click

        COM.ClosePort()

        btnOpen.Enabled = True
        btnClose.Enabled = False
        btnSend.Enabled = False
        txtTx.Enabled = False

        UserLevel("3")

    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click

        Send()

    End Sub

    Private Sub Send()

        txtRx.AppendText(txtTx.Text & vbCrLf)
        COM.SetValue("send", txtTx.Text)
        txtTx.Text = ""
        GetValue(COM.GetValue("receive"))

    End Sub

    'Private Sub COM_TextReceived(ByVal str As String) Handles COM.TextReceived

    '    txtRx.AppendText(str & vbCrLf)

    'End Sub

    Private Sub COM_DebugLog(ByVal str As String) Handles COM.DebugLog

        lblLog.Text = str

    End Sub

    Private Sub COM_ErrorLog(ByVal str As String) Handles COM.ErrorLog

        lblLog.Text = str

    End Sub


    Private Sub GetValue(ByVal str As String)

        'string to array (;)
        Dim arrRx(0 To 5) As String
        Dim strArr As New System.Text.StringBuilder
        Dim strSub As String
        Dim intSub As Integer = 0

        For i As Integer = 0 To str.Length - 1

            strSub = str.Substring(i, 1)
            If strSub <> ";" Then
                strArr.Append(strSub)
            Else
                arrRx(intSub) = strArr.ToString
                intSub += 1
                strArr.Remove(0, strArr.Length)
            End If

        Next

        For i As Integer = 0 To UBound(arrRx)

            If arrRx(i) <> "" Then

                txtRx.AppendText(arrRx(i) & vbCrLf)

            End If

        Next

    End Sub


    Private Sub FindPorts()

        Dim intArrayLength As Integer

        Array.Clear(arrPorts, 0, arrPorts.Length)

        arrPorts = SerialPort.GetPortNames

        intArrayLength = arrPorts.Length

        If intArrayLength = 0 Then
            WriteLog("No Ports found...")
            cmbPort.Items.Clear()
            UserLevel("0")
        Else
            cmbPort.Items.Clear()
            cmbPort.Items.AddRange(arrPorts)
            'lblState.Text = "Ports found..."
            btnOpen.Enabled = True

            cmbPort.SelectedIndex = 0
        End If

        intArrayLength = Nothing

    End Sub

    Private Sub UserLevel(ByVal str As String)

        cmbPort.Enabled = False
        txtBaud.Enabled = False

        Select Case str

            Case "3"
                cmbPort.Enabled = True
                txtBaud.Enabled = True

        End Select

    End Sub


    Private Sub txtBaud_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBaud.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8
                ' Zahlen und Backspace zulassen

            Case Else
                ' alle anderen Eingaben unterdrücken
                e.Handled = True
        End Select
    End Sub

    Private Sub txtTx_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTx.KeyPress

        If Asc(e.KeyChar) = 13 Then

            Send()

        End If

    End Sub
End Class
