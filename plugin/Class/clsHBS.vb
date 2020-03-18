Class clsHBS

    Dim WithEvents COM As New clsCOM
    Dim strCOMState As String

    Dim strCommand As String = ""

    Public intNr As Integer = 0
   

    '"#HBS_Start",
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
                COM.OpenPort("", 500)

            Case "stop"
                COM.ClosePort()

            Case "com-port"
                Send("port", str)

            Case "com-baud"
                Send("baud", str)

            Case "id"
                Send("send", "*IDN?")
                GetValue(COM.GetValue("receive"))

            Case "set_v_ac"
                Send("send", "SOUR:VOLTAC," & str)

            Case "set_freq"
                Send("send", "SOUR:FREQ," & str)

            Case "set_v_dc"
                Send("send", "SOUR:VOLTDC," & str)

                'Case "set_v_tdc"
                '    Send("send", "OUTP:OT1," & str)

            Case ("set_tdc")
                Send("send", "OUTP:OT1," & str)

            Case "set_i"
                Send("send", "SOUR:CURR," & str)

            Case "set_output"
                Send("send", "OUTP," & str)

            Case "set_i_option"
                Send("send", "SOUR:CURRRNG," & str)

            Case "set_oscillator"
                Send("send", "OUTP:AUX," & str)

            Case "meas_v"
                Send("send", "MEAS:VOLT?")
                GetValue(COM.GetValue("receive"))

            Case "meas_i"
                Send("send", "MEAS:CURR?")
                GetValue(COM.GetValue("receive"))

            Case "meas_i_peak"
                Send("send", "MEAS:CURRP?")
                GetValue(COM.GetValue("receive"))

            Case "meas_crest"
                Send("send", "MEAS:CFACT?")
                GetValue(COM.GetValue("receive"))

            Case "meas_power"
                Send("send", "MEAS:PFACT?")
                GetValue(COM.GetValue("receive"))

            Case "meas_va"
                Send("send", "MEAS:VA?")
                GetValue(COM.GetValue("receive"))

            Case "meas_w"
                Send("send", "MEAS:POW?")
                GetValue(COM.GetValue("receive"))

            Case "get_options"
                Send("send", "*OPT?")
                GetValue(COM.GetValue("receive"))

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

        If arrRx(0) = "" Then

            arrRx(0) = "NaN"

        End If

        strSourceRx = arrRx(0)

        Dim strUnit As New System.Text.StringBuilder
        Dim dbl As New System.Text.StringBuilder

        For Each c As Char In arrRx(0).Replace(".", ",")
            If Char.IsNumber(c) Or InStr(c, ",") Then
                dbl.Append(c)
            Else
                strUnit.Append(c)
            End If
        Next

        Select Case strCommand

            Case "id"
                arrSourceID(intNr) = arrRx(0)
                WriteLog("ID: " & arrRx(0))

            Case "meas_v"
                arrSourceValue(intNr, intMeasurePoint) = dbl.ToString & vbTab & strUnit.ToString

            Case "meas_i"
                arrSourceValue(intNr, intMeasurePoint) = dbl.ToString & vbTab & strUnit.ToString

            Case "meas_va"
                arrSourceValue(intNr, intMeasurePoint) = dbl.ToString & vbTab & strUnit.ToString

            Case "meas_w"
                arrSourceValue(intNr, intMeasurePoint) = dbl.ToString & vbTab & strUnit.ToString

            Case Else
                WriteLog("get: " & arrRx(0))

        End Select

    End Sub
End Class
