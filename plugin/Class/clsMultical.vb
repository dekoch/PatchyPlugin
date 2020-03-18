Class clsMultical

    Public WithEvents COM As New clsCOM
    Dim strCOMState As String

    Dim strCommand As String = "meas_value"

    Public intNr As Integer = 0

    Dim WithEvents timRead1 As New Timer
    Dim arrRx1(0 To 100) As String

    Dim intSpeed As Integer = 70

    Dim int As Integer = 0

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        strCommand = name

        If bolManualControl = True Then
            COM.SetValue("reset", "")
        End If

        If name = "com-port" Then
            COM.Init()
        End If

        Select Case name

            Case "start"
                COM.OpenPort("", 2000)
                strCommand = "meas_value"
                timRead1.Interval = intSpeed
                timRead1.Start()

            Case "stop"
                timRead1.Stop()
                COM.ClosePort()

            Case "com-port"
                Send("port", str)

            Case "com-baud"
                Send("baud", str)

            Case "speed"
                intSpeed = Convert.ToInt32(str)
                timRead1.Interval = intSpeed

            Case "meas_value"
                'GetValue(COM.GetValue("receive"))
                If arrRx1(2) <> "" Then

                    strMeasureRx = arrRx1(2)
                    arrMeasureValue(intNr, intMeasurePoint) = arrRx1(2) & vbTab

                Else

                    strMeasureRx = "NaN(" & int & ")"
                    arrMeasureValue(intNr, intMeasurePoint) = "NaN(" & int & ")" & vbTab

                    int += 1

                End If


                'Case "terminal"
                '    Send("send", str)
                '    GetValue(COM.GetValue("receive"))

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

    Private Sub timRead1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timRead1.Tick
        timRead1.Stop()

        Send("send", "")
        Dim str As String = COM.GetValue("receive")

        'string to array (;)
        Dim arrRx(0 To 5) As String
        Dim strArr As New System.Text.StringBuilder
        Dim strSub As String
        Dim intSub As Integer = 0

        For i As Integer = 0 To Str.Length - 1

            strSub = Str.Substring(i, 1)
            If strSub <> ";" Then
                strArr.Append(strSub)
            Else
                arrRx(intSub) = strArr.ToString
                intSub += 1
                strArr.Remove(0, strArr.Length)
            End If

        Next

        Select Case strCommand

            Case "meas_value"
                'arrMeasureValue(0, intMeasurePoint) = arrRx(0)
                'WriteLog("get: " & arrRx(0))

                str = str.Replace(".", ",")

                'string to array (;)
                'Dim arrRx1(0 To 100) As String
                Dim strArr1 As New System.Text.StringBuilder
                Dim strSub1 As String
                Dim intSub1 As Integer = 0

                'antwort von multical: zeit messbereich wert/n
                'hier werden alle 3 strings in ein array geschrieben
                For i As Integer = 0 To str.Length - 1

                    strSub1 = str.Substring(i, 1)
                    If strSub1 <> " " Then
                        strArr1.Append(strSub1)
                    Else
                        arrRx1(intSub1) = strArr1.ToString
                        intSub1 += 1
                        strArr1.Remove(0, strArr1.Length)
                    End If

                Next

                'If arrRx1(2) <> "" Then

                '    strMeasureRx = arrRx1(2)
                '    arrMeasureValue(intNr, intMeasurePoint) = arrRx1(2) & vbTab

                'Else

                '    strMeasureRx = "NaN"
                '    arrMeasureValue(intNr, intMeasurePoint) = "NaN" & vbTab

                'End If

            Case Else
                WriteLog("get: " & arrRx(0))

        End Select

        timRead1.Start()
    End Sub

    'Private Sub GetValue(ByVal str As String)

    '    'string to array (;)
    '    Dim arrRx(0 To 5) As String
    '    Dim strArr As New System.Text.StringBuilder
    '    Dim strSub As String
    '    Dim intSub As Integer = 0

    '    For i As Integer = 0 To str.Length - 1

    '        strSub = str.Substring(i, 1)
    '        If strSub <> ";" Then
    '            strArr.Append(strSub)
    '        Else
    '            arrRx(intSub) = strArr.ToString
    '            intSub += 1
    '            strArr.Remove(0, strArr.Length)
    '        End If

    '    Next

    '    Select Case strCommand

    '        Case "id"
    '            WriteLog("ID: " & arrRx(0))

    '        Case "meas_value"
    '            'arrMeasureValue(0, intMeasurePoint) = arrRx(0)
    '            'WriteLog("get: " & arrRx(0))

    '            'string to array (;)
    '            Dim arrRx1(0 To 100) As String
    '            Dim strArr1 As New System.Text.StringBuilder
    '            Dim strSub1 As String
    '            Dim intSub1 As Integer = 0

    '            'antwort von multical: zeit messbereich wert/n
    '            'hier werden alle 3 strings in ein array geschrieben
    '            For i As Integer = 0 To str.Length - 1

    '                strSub1 = str.Substring(i, 1)
    '                If strSub1 <> " " Then
    '                    strArr1.Append(strSub1)
    '                Else
    '                    arrRx1(intSub1) = strArr1.ToString
    '                    intSub1 += 1
    '                    strArr1.Remove(0, strArr1.Length)
    '                End If

    '            Next

    '            If arrRx1(2) <> "" Then

    '                strMeasureRx = arrRx1(2)
    '                arrMeasureValue(intNr, intMeasurePoint) = arrRx1(2) & vbTab

    '            Else

    '                strMeasureRx = "NaN"
    '                arrMeasureValue(intNr, intMeasurePoint) = "NaN" & vbTab

    '            End If

    '        Case Else
    '            WriteLog("get: " & arrRx(0))

    '    End Select


    'End Sub

  
End Class
