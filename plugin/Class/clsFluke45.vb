Class clsFluke45

    Public WithEvents COM As New clsCOM
    Dim strCOMState As String

    Dim strCommand As String = ""

    Public intNr As Integer = 0

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        strCommand = name

        If bolManualControl = True Then
            COM.SetValue("reset", "")
        End If

        '"#MEAS1_Start",
        '   "#MEAS1_Stop",
        '   "#MEAS1_COM-Port",
        '   "#MEAS1_COM-Baud",
        '   "#MEAS1_SET_V_AC",
        '   "#MEAS1_SET_V_DC",
        '   "#MEAS1_SET_A_AC",
        '   "#MEAS1_SET_A_DC",
        '   "#MEAS1_SET_FREQ",
        '   "#MEAS1_GET_VALUE"

        If name = "com-port" Then
            COM.Init()
        End If

        Select Case name

            Case "start"
                COM.OpenPort("=>", 2000)

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
                Send("send", "VAC")

            Case "set_v_dc"
                Send("send", "VDC")

            Case "set_v_tdc"
                Send("send", "VACDC")

            Case "set_a_ac"
                Send("send", "AAC")

            Case "set_a_dc"
                Send("send", "ADC")

            Case "set_a_tdc"
                Send("send", "AACDC")

            Case "set_freq"
                Send("send", "FREQ")

            Case "meas_value"
                Send("send", "VAL1?")
                GetValue(COM.GetValue("receive"))
                'arrMeasureValue(0, intMeasurePoint) = COM.GetValue("receive")
                'arrMeasureValue(0, intMeasurePoint) = "1"

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

        If arrRx(1) = "" Then

            arrRx(1) = "NaN"

        End If

        strMeasureRx = arrRx(1)

        Select Case strCommand

            Case "id"
                arrMeasureID(intNr) = arrRx(1)
                WriteLog("ID: " & arrRx(1))

            Case "meas_value"
                arrMeasureValue(intNr, intMeasurePoint) = strMeasureRx.Replace(".", ",") & vbTab

            Case Else
                WriteLog("get: " & arrRx(1))

        End Select


    End Sub
End Class
