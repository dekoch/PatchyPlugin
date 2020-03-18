Imports System.IO.Ports

Class frmMeasureSource

    Dim frmCOM As New frmCOM
    Dim main As New main

    Dim ManualControl(0 To 9) As ucManualControl
    Dim intControls As Integer = 0

    Dim strRadio As String = "multical"
    Dim strRadioOld As String = ""
    Dim strPortTyp As String = ""

    Dim bolPortOpen As Boolean = False
    Dim bolPortsPresent As Boolean = False

    Dim arrPorts(0 To 50) As String
    Dim WithEvents SerialPort As New System.IO.Ports.SerialPort("COM1", 9600, Parity.None, 8, StopBits.One)

    Private Sub frmMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        frmCOM.Show()
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        SettingsSave()

        bolAbortAll = True

        COMClose()

        Application.Exit()

    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strCurrentPath = My.Application.Info.DirectoryPath
        If strCurrentPath.EndsWith("\") = False Then strCurrentPath = strCurrentPath & "\"

        Me.Text = "Measure/Source plugin.dll (" & strPluginName & ") v" & strPluginVersion & " - www.mariokoch.de"

        btnOpen.Enabled = False
        btnClose.Enabled = False
        'UserLevel("0")

        FindPorts()

        For i As Integer = 0 To 9
            AddControl(i)
        Next

        IO.CreateFile(My.Resources.pluginxml, "plugin.xml", strCurrentPath)

        If IO.FileExists("plugin.xml", False) = True Then

            SettingsLoad()

            RadioChanged()

        Else

            Application.Exit()

        End If

    End Sub

    Private Sub AddControl(ByVal int As Integer)
        ManualControl(intControls) = New ucManualControl
        With ManualControl(intControls)
            .Name = "ManualControl"
            .Location = New System.Drawing.Point(7, 10 + (intControls * ManualControl(0).Height))
            .Visible = True
        End With
        grbCommands.Controls.Add(ManualControl(intControls))

        intControls += 1
    End Sub

    'Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    AddControl()

    'End Sub

    Private Sub timLog_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timLog.Tick
        timLog.Stop()

        If bolLog = True Then
            'If strLog.Length > 0 Then
            txtLog.AppendText("(" & System.DateTime.Now & ") " & strLog & vbCrLf)
            bolLog = False
        End If

        timLog.Start()
    End Sub

    Private Sub rbtnHBS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnHBS.CheckedChanged
        If rbtnHBS.Checked = True Then
            strRadio = "hbs"
        End If
        RadioChanged()
    End Sub

    Private Sub rbtnFluke45_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnFluke45.CheckedChanged
        If rbtnFluke45.Checked = True Then
            strRadio = "fluke45"
        End If
        RadioChanged()
    End Sub

    Private Sub rbtnMultical_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnMultical.CheckedChanged
        If rbtnMultical.Checked = True Then
            strRadio = "multical"
        End If
        RadioChanged()
    End Sub

    Private Sub rbtnBUER2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnBUER2.CheckedChanged
        If rbtnBUER2.Checked = True Then
            strRadio = "buer2"
        End If
        RadioChanged()
    End Sub

    Private Sub rbtnAVRNETIO_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnAVRNETIO.CheckedChanged
        If rbtnAVRNETIO.Checked = True Then
            strRadio = "avr-net-io"
        End If
        RadioChanged()
    End Sub

    Private Sub rbtnAgilent34450_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnAgilent34450.CheckedChanged
        If rbtnAgilent34450.Checked = True Then
            strRadio = "agilent34450"
        End If
        RadioChanged()
    End Sub

    Private Sub RadioChanged()

        If strRadio <> strRadioOld Then
            strRadioOld = strRadio



            rbtnHBS.Checked = False
            rbtnFluke45.Checked = False
            rbtnMultical.Checked = False
            rbtnBUER2.Checked = False
            rbtnAVRNETIO.Checked = False
            rbtnAgilent34450.Checked = False

            cmbPort.Enabled = False
            txtBaud.Enabled = False
            txtIP.Enabled = False
            txtPort.Enabled = False
            btnOpen.Enabled = False
            lblIP.Text = "IP:"

            txtBaud.Text = ""

            Select Case strRadio

                Case "hbs"
                    rbtnHBS.Checked = True
                    txtBaud.Text = "19200"
                    strPortTyp = "COM"

                Case "fluke45"
                    rbtnFluke45.Checked = True
                    txtBaud.Text = "9600"
                    strPortTyp = "COM"

                Case "multical"
                    rbtnMultical.Checked = True
                    txtBaud.Text = "38400"
                    strPortTyp = "COM"

                Case "buer2"
                    rbtnBUER2.Checked = True
                    txtBaud.Text = "9600"
                    strPortTyp = "COM"

                Case "avr-net-io"
                    rbtnAVRNETIO.Checked = True
                    txtPort.Text = "50290"
                    strPortTyp = "TCP"

                Case "agilent34450"
                    rbtnAgilent34450.Checked = True
                    lblIP.Text = "VISA:"
                    lblIP.Left = txtIP.Left - lblIP.Width
                    strPortTyp = "VISA"

            End Select

            Select Case strPortTyp

                Case "COM"
                    If bolPortsPresent = True Then
                        cmbPort.Enabled = True
                        txtBaud.Enabled = True
                        btnOpen.Enabled = True
                    End If

                Case "TCP"
                    txtIP.Enabled = True
                    txtPort.Enabled = True
                    btnOpen.Enabled = True

                Case "VISA"
                    txtIP.Text = "USBInstrument0"
                    txtIP.Enabled = True
                    btnOpen.Enabled = True


            End Select


        End If
    End Sub

    Private Sub FindPorts()

        Dim intArrayLength As Integer

        Array.Clear(arrPorts, 0, arrPorts.Length)

        arrPorts = SerialPort.GetPortNames

        intArrayLength = arrPorts.Length

        If intArrayLength = 0 Then
            WriteLog("No Ports found...")
            cmbPort.Items.Clear()
            'UserLevel("0")
            bolPortsPresent = False
        Else
            cmbPort.Items.Clear()
            cmbPort.Items.AddRange(arrPorts)
            'lblState.Text = "Ports found..."
            'UserLevel("3")
            'btnOpen.Enabled = True
            bolPortsPresent = True
            cmbPort.SelectedIndex = 0
        End If

        'cmbPort.Items.Add("SIM")

        intArrayLength = Nothing

    End Sub


    Private Sub UserLevel(ByVal str As String)

        rbtnHBS.Enabled = False
        rbtnBUER2.Enabled = False
        rbtnFluke45.Enabled = False
        rbtnMultical.Enabled = False
        rbtnAVRNETIO.Enabled = False
        rbtnAgilent34450.Enabled = False
        cmbPort.Enabled = False
        txtBaud.Enabled = False
        txtIP.Enabled = False
        txtPort.Enabled = False

        Select Case str

            Case "3"
                rbtnHBS.Enabled = True
                rbtnBUER2.Enabled = True
                rbtnFluke45.Enabled = True
                rbtnMultical.Enabled = True
                rbtnAVRNETIO.Enabled = True
                rbtnAgilent34450.Enabled = True

                Select Case strPortTyp

                    Case "COM"
                        If bolPortsPresent = True Then
                            cmbPort.Enabled = True
                            txtBaud.Enabled = True
                            btnOpen.Enabled = True
                        End If

                    Case "TCP"
                        txtIP.Enabled = True
                        txtPort.Enabled = True
                        btnOpen.Enabled = True

                    Case "VISA"
                        txtIP.Enabled = True

                End Select

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

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click

        'If cmbPort.Text.Length > 0 Then

        bolManualControl = True

        btnOpen.Enabled = False
        btnClose.Enabled = True
        UserLevel("0")

        strCompany = "BDG GmbH"

        Select Case strRadio

            Case "hbs"
                main.SetValue("#SOURCE0_BIND_HBS", "")
                main.SetValue("#SOURCE0_COM-PORT", cmbPort.Text)
                main.SetValue("#SOURCE0_COM-BAUD", txtBaud.Text)
                main.SetValue("#SOURCE0_START", "")

            Case "buer2"
                main.SetValue("#SOURCE0_BIND_BUER2", "")
                main.SetValue("#SOURCE0_COM-PORT", cmbPort.Text)
                main.SetValue("#SOURCE0_COM-BAUD", txtBaud.Text)
                main.SetValue("#SOURCE0_START", "")

            Case "avr-net-io"
                main.SetValue("#IO0_BIND_AVR-NET-IO", "")
                main.SetValue("#IO0_IP", txtIP.Text)
                main.SetValue("#IO0_PORT", txtPort.Text)
                main.SetValue("#IO0_START", "")

            Case "fluke45"
                main.SetValue("#MEAS0_BIND_FLUKE45", "")
                main.SetValue("#MEAS0_COM-PORT", cmbPort.Text)
                main.SetValue("#MEAS0_COM-BAUD", txtBaud.Text)
                main.SetValue("#MEAS0_START", "")

            Case "multical"
                main.SetValue("#MEAS0_BIND_MULTICAL", "")
                main.SetValue("#MEAS0_COM-PORT", cmbPort.Text)
                main.SetValue("#MEAS0_COM-BAUD", txtBaud.Text)
                main.SetValue("#MEAS0_START", "")

            Case "agilent34450"
                main.SetValue("#MEAS0_BIND_AGILENT34450", "")
                main.SetValue("#MEAS0_VISA-ALIAS", txtIP.Text)
                main.SetValue("#MEAS0_START", "")

        End Select

        bolPortOpen = True

        'Else

        'WriteLog("please select a port")

        'End If


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        COMClose()
        btnOpen.Enabled = True
        btnClose.Enabled = False

    End Sub

    Private Sub COMClose()
        bolPortOpen = False

        Select Case strRadio

            Case "hbs"
                main.SetValue("#SOURCE0_STOP", "")

            Case "buer2"
                main.SetValue("#SOURCE0_STOP", "")

            Case "fluke45"
                main.SetValue("#MEAS0_STOP", "")

            Case "multical"
                main.SetValue("#MEAS0_STOP", "")

            Case "agilent34450"
                main.SetValue("#MEAS0_STOP", "")

        End Select

        UserLevel("3")
    End Sub

    Private Sub chbDebug_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbDebug.CheckedChanged
        If chbDebug.Checked = True Then
            bolDebug = True
        Else
            bolDebug = False
        End If
    End Sub

    Private Sub SettingsLoad()

        If IO.FileExists("plugin.xml", False) = True Then

            Dim strRoot As String = "program/settings/measuresource/device"

            strRadio = XML.readXML(strRoot, "default", "value", "string", strSettingsPath)

            strRoot = "program/settings/measuresource/cmb"

            For i As Integer = 0 To UBound(ManualControl)

                ManualControl(i).cmbCommand.Text = XML.readXML(strRoot, "cmb_" & i, "value", "string", strSettingsPath)
                ManualControl(i).chbRefresh.Checked = XML.readXML(strRoot, "cmb_" & i, "refresh", "boolean", strSettingsPath)

            Next

        End If

    End Sub


    Private Sub SettingsSave()

        If IO.FileExists("plugin.xml", False) = True Then

            Dim strRoot As String = "program/settings/measuresource/device"

            XML.writeXML(strRoot, "default", "value", strRadio, "string", strSettingsPath)

            strRoot = "program/settings/measuresource/cmb"

            For i As Integer = 0 To UBound(ManualControl)

                XML.writeXML(strRoot, "cmb_" & i, "value", ManualControl(i).cmbCommand.Text, "string", strSettingsPath)
                XML.writeXML(strRoot, "cmb_" & i, "refresh", ManualControl(i).chbRefresh.Checked, "boolean", strSettingsPath)

            Next

        End If

    End Sub


    Private Sub timVisu1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timVisu1.Tick
        timVisu1.Stop()

        'Dim StopWatchJob As New Stopwatch
        'StopWatchJob.Start()

        For i As Integer = 0 To 9

            If bolAbortAll = False And bolPortOpen = True Then

                If ManualControl(i).chbRefresh.Checked = True Then

                    ManualControl(i).Send()

                    'Delay(0.5)

                End If

            End If

        Next

        'StopWatchJob.Stop()

        'Me.Text = StopWatchJob.ElapsedMilliseconds

        timVisu1.Start()
    End Sub



End Class