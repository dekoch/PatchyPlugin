Class clsBUER2

    Dim WithEvents COM As New clsCOM
    Dim strCOMState As String

    Dim strCommand As String = ""

    Public intNr As Integer = 0

    Dim strOldVoltageType As String = ""
    Dim dblWait As Double = 0.25


    ' "#HBS_Start",
    '"#HBS_Stop",
    '"#HBS_ID",
    '"#HBS_COM-Port",
    '"#HBS_SET_VOLT_AC",
    '"#HBS_SET_FREQ",
    '"#HBS_SET_VOLT_DC",
    '"#HBS_SET_CURRENT",
    '"#HBS_SET_OUTPUT",
    '"#HBS_MEAS_VOLT",
    '"#HBS_MEAS_CURRENT",
    '"#HBS_MEAS_VA",
    '"#HBS_MEAS_W"

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        str = str.Replace(",", ".")

        strCommand = name

        If bolManualControl = True Then
            COM.SetValue("reset", "")
        End If

        If name = "com-port" Then
            COM.Init()
        End If

        Select Case name

            Case "start"
                COM.OpenPort("vbCrLf", 2000)

            Case "stop"
                COM.ClosePort()

            Case "com-port"
                Send("port", str)

            Case "com-baud"
                Send("baud", str)

            Case "id"
                Send("send", "ID")
                GetValue(COM.GetValue("receive"))

            Case "set_v_ac"
                If strOldVoltageType <> "AC" Then
                    LGGrundstellung()

                    Send("send", "SB,R")
                    Delay(dblWait)

                    strOldVoltageType = "AC"
                End If

                Send("send", "UAC," & str & "V")

            Case "set_freq"
                Send("send", "F" & str & ",S")

            Case "set_v_dc"
                If strOldVoltageType <> "DC" Then
                    LGGrundstellung()

                    Send("send", "DC,S")
                    Send("send", "POS,S")
                    Send("send", "SB,R")
                    Delay(dblWait)

                    strOldVoltageType = "DC"
                End If

                Send("send", "UDC," & str & "V")

            Case "set_v_tdc"
                If strOldVoltageType <> "TDC" Then
                    LGGrundstellung()
                    Send("send", "PULS,S")
                    Send("send", "SB,R")
                    Delay(dblWait)

                    strOldVoltageType = "TDC"
                End If

                Send("send", "UAC," & str & "V")


                'Case "set_i"
                '    Send("send", "SOUR:CURR," & str)

            Case "set_output"
                If str = "1" Then
                    Send("send", "SB,S")
                Else
                    Send("send", "SB,R")
                End If

                'Case "set_i_option"
                '    Send("send", "SOUR:CURRRNG," & str)

                'Case "set_oscillator"
                '    Send("send", "OUTP:AUX," & str)

            Case "meas_v"
                Send("send", "MUA")
                GetValue(COM.GetValue("receive"))

            Case "meas_i"
                Send("send", "MIA1") '3A
                GetValue(COM.GetValue("receive"))

            Case "meas_i2"
                Send("send", "MIA2") '200mA
                GetValue(COM.GetValue("receive"))

                'Case "meas_i_peak"
                '    Send("send", "MEAS:CURRP?")
                '    GetValue(COM.GetValue("receive"))

                'Case "meas_crest"
                '    Send("send", "MEAS:CFACT?")
                '    GetValue(COM.GetValue("receive"))

                'Case "meas_power"
                '    Send("send", "MEAS:PFACT?")
                '    GetValue(COM.GetValue("receive"))

                'Case "meas_va"
                '    Send("send", "MEAS:VA?")
                '    GetValue(COM.GetValue("receive"))

            Case "meas_w"
                Send("send", "MPA")
                GetValue(COM.GetValue("receive"))

            Case "meas_freq"
                Send("send", "MFA")
                GetValue(COM.GetValue("receive"))

                'Case "options"
                '    Send("send", "*OPT?")
                '    GetValue(COM.GetValue("receive"))

            Case "terminal"
                Send("send", str)
                GetValue(COM.GetValue("receive"))

            Case Else
                WriteLog("not supported command " & name)
                intState = -11

        End Select

        If intState > 0 Then
            intState = 0
        End If

    End Sub

    Private Sub LGGrundstellung()
        Send("send", "SB,S")
        Delay(dblWait)
        Send("send", "PULS,R")
        Send("send", "DC,R")
        Send("send", "DCP,R")
        Send("send", "UDC,0V")
        Send("send", "UAC,0V")
        'Send("send", "SB,R")
        'Delay(dblWait)
    End Sub

    Private Sub Send(ByVal name As String, ByVal str As String)

        COM.SetValue(name, str)

    End Sub

    Private Sub COM_DebugLog(ByVal str As String) Handles COM.DebugLog
        If bolDebug = True Then
            WriteLog(str)
        End If
    End Sub

    Private Sub COM_ErrorLog(ByVal str As String) Handles COM.ErrorLog
        WriteLog(str)
        strCOMState = COM.GetValue("state")
        If strCOMState < 0 Then
            intState = strCOMState
        End If
    End Sub

    Private Sub GetValue(ByVal str As String)

        If intState > 0 Then

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

            'For i As Integer = 0 To UBound(arrRx)
            '    WriteLog(arrRx(i))
            'Next

            strSourceRx = arrRx(0)

            Select Case strCommand

                Case "id"
                    WriteLog("ID: " & arrRx(0))

                Case "meas_v"
                    arrSourceValue(intNr, intMeasurePoint) = arrRx(0)

                Case "meas_i"
                    arrSourceValue(intNr, intMeasurePoint) = arrRx(0)

                Case "meas_i2"
                    arrSourceValue(intNr, intMeasurePoint) = arrRx(0)

                Case "meas_va"
                    arrSourceValue(intNr, intMeasurePoint) = arrRx(0)

                Case "meas_w"
                    arrSourceValue(intNr, intMeasurePoint) = arrRx(0)

                Case Else
                    WriteLog("get: " & arrRx(0))

            End Select

        End If

    End Sub
End Class
