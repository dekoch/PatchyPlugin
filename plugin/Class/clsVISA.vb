Imports System.IO.Ports
Imports System.Text

Class clsVISA

    ' intState:-9=dll error -8=error timeout -7=error checksum -6=error receiving -5=error sending -4=error closing -3=error opening -2=not found -1=error 0=ready
    Dim intState As Integer = 0

    Dim visaResManager As Ivi.Visa.Interop.ResourceManager
    Dim visaInstrument As Ivi.Visa.Interop.FormattedIO488
    Dim bolVISALoaded As Boolean = False

    Dim intTimeOut As Integer = 2000
    Dim bolTimedOut As Boolean = False
    Dim strTerminationOK As String = ""
    Dim bolCrLf As Boolean = False
    Dim arrPorts(0 To 50) As String
    Dim strSelectedPort As String = ""
    Dim bolDataReceivedEnabled As Boolean = False
    Dim strReceive As String = ""
    Dim strReceived As String = ""
    Dim arrReceived(0 To 250) As String
    Dim bolTextInBuffer As Boolean = False
    Dim bolPortOpen As Boolean = False
    Dim bolSim As Boolean = False
    Dim strOldSend As String = ""
    Private Delegate Sub DelegateSub()
    Private WithEvents timGetBytes As New System.Timers.Timer
    Private WithEvents timTimeOut As New System.Timers.Timer

    Dim bolRead As Boolean = False

    Public Event TextReceived(ByVal str As String)
    Public Event DebugLog(ByVal str As String)
    Public Event ErrorLog(ByVal str As String)

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        Select Case name

            Case "send"
                If bolSim = False Then
                    Send(str)
                End If

            Case "transmit"
                Transmit(str)

            Case "port"
                strSelectedPort = str
                If str = "SIM" Then
                    bolSim = True
                Else
                    bolSim = False
                End If

            Case "read"
                'MsgBox(str)
                If str = "True" Then
                    bolRead = True
                Else
                    bolRead = False
                End If

            Case "reset"
                intState = 0

            Case Else
                RaiseEvent ErrorLog("VISA " & strSelectedPort & " error@SetValue-command not found-" & name)

        End Select


    End Sub


    Public Function GetValue(ByVal name As String)

        Select Case name

            Case "state"
                Return intState


            Case "receive"

                Dim str As String = ""

                If bolSim = False Then
                    'timTimeOut.Stop()
                    'timTimeOut.Interval = intTimeOut
                    'bolTimedOut = False
                    'timTimeOut.Start()

                    Dim intAttempts As Integer = 0

                    If strReceived = "" Then

                        Do

                            Send(strOldSend)

                            intAttempts += 1

                            RaiseEvent DebugLog("VISA " & strSelectedPort & " attempt: " & intAttempts)

                        Loop Until intAttempts > 3 Or strReceived <> ""

                        If intAttempts > 3 Then
                            ' fehler
                            intState = -7
                            RaiseEvent ErrorLog("VISA " & strSelectedPort & " error@checksum")
                        End If

                    End If

                    'timTimeOut.Stop()


                    str = strReceived

                Else

                    str = "1 2 3V;4 5 6V;7 8 9V"

                End If

                Return str


            Case Else
                Return "VISA " & strSelectedPort & " error@GetValue-command not found-" & name

        End Select

    End Function

    Private Sub Send(ByVal str As String)

        Debug.WriteLine(intState)

        If intState = 0 Then

            Dim strReceived As String = ""
            Dim bolChecksumOK As Boolean = False
            Dim intAttempts As Integer = 0

            strOldSend = str

            'Do
            Transmit(str, , bolRead)

            'strReceived = GetValue("receive")

            'visaInstrument.WriteString(str)
            'strReceived = visaInstrument.ReadString()

            'If strReceived <> "" Then
            '    bolChecksumOK = True
            'End If

            'For i As Integer = 0 To UBound(arrReceived)

            '    'Debug.Write(arrReceived(i))

            '    If strTerminationOK.Length > 0 Then

            '        If arrReceived(i).StartsWith(strTerminationOK) = True Or arrReceived(i).EndsWith(strTerminationOK) Then

            '            bolChecksumOK = True

            '        End If

            '    Else

            '        If strTerminationOK = "vbCrLf" Then

            '            If bolCrLf = True Then

            '                bolChecksumOK = True

            '            End If

            '        Else

            '            bolChecksumOK = True

            '        End If

            '    End If
            'Next

            'intAttempts += 1
            '    Loop Until bolChecksumOK = True Or intAttempts = 3

            'If intAttempts = 3 Then
            '    ' fehler
            '    intState = -7
            '    RaiseEvent ErrorLog("VISA " & strSelectedPort & " error@checksum")
            'End If

            RaiseEvent TextReceived(strReceived)

            intAttempts = Nothing
            strReceived = Nothing

        Else

            RaiseEvent ErrorLog("VISA " & strSelectedPort & " error: " & intState)

        End If

    End Sub

    Private Sub Transmit(ByVal str As String, Optional ByVal clear As Boolean = True, Optional ByVal read As Boolean = True)

        'Debug.WriteLine("transmit " & str)

        Dim strRx As String = ""

        'sendet daten über den com-port
        RaiseEvent DebugLog("VISA " & strSelectedPort & " send """ & str & """")

        If clear = True Then
            strReceived.Remove(0, strReceived.Length)

            For i As Integer = 0 To UBound(arrReceived)
                arrReceived(i) = ""
            Next

            bolTextInBuffer = False
        End If

        bolCrLf = False

        str = str & vbCr.ToString

        Dim intLen As Integer = str.Length

        Dim byrSend(intLen - 1) As Byte

        For a As Integer = 0 To intLen - 1
            byrSend(a) = Asc(str.Chars(a))
        Next a

        intLen = byrSend.Length

        If str.Length > 1 Then
            If bolPortOpen = True Then
                'Try

                'Do Until strRx.StartsWith("1")
                '    Delay(0.05)
                '    Try
                '        visaInstrument.WriteString("*OPC?")
                '        strRx = visaInstrument.ReadString()
                '    Catch ex As Exception
                '        Debug.WriteLine("Transmit 1" & ex.ToString)
                '    End Try
                'Loop

                'strRx = ""

                Try
                    visaInstrument.WriteString(str)
                    If read = True Then
                        strReceived = visaInstrument.ReadString()
                    End If
                Catch ex As Exception
                    strReceived = ""
                End Try

            'Do Until strRx.StartsWith("1")
            '    Delay(0.05)
            '    Try
            '        visaInstrument.WriteString("*OPC?")
            '        strRx = visaInstrument.ReadString()
            '    Catch ex As Exception
            '        Debug.WriteLine("Transmit 2" & ex.ToString)
            '    End Try
            'Loop

            'strRx = ""

            'Debug.WriteLine(strReceived)
            ' bolTextInBuffer = True
            'Catch
            '    intState = -5
            '    RaiseEvent ErrorLog("VISA " & strSelectedPort & " error@sending")
            'End Try

        Else
            intState = -1
            RaiseEvent DebugLog("VISA " & strSelectedPort & " is not open")
        End If
        End If

    End Sub

    'Private Sub timTimeOut_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timTimeOut.Elapsed

    '    timTimeOut.Stop()

    'RaiseEvent ErrorLog("ComPort " & SerialPort.PortName & " error@timeout:" & timTimeOut.Interval)
    'If strTerminationOK.Length > 0 Then
    'intState = -8

    'bolTimedOut = True

    'strReceived.Remove(0, strReceived.Length)

    ''End If

    'bolTextInBuffer = True

    'End Sub



    'termination fuer vbCrLf = ""
    Public Sub OpenPort(ByVal termination As String, ByVal timeout As Integer)

        If bolSim = False Then

            strTerminationOK = termination
            intTimeOut = timeout

            If bolPortOpen = False Then

                RaiseEvent DebugLog("VISA " & strSelectedPort & " opening")
                Try

                    visaInstrument.IO = visaResManager.Open(strSelectedPort)
                    visaInstrument.IO.Timeout = timeout
                    visaInstrument.WriteString("*CLS") 'fehlerreset

                    bolPortOpen = True
                    RaiseEvent DebugLog("VISA " & strSelectedPort & " opened")
                Catch ex As Exception
                    intState = -3
                    bolPortOpen = False
                    RaiseEvent ErrorLog("VISA " & strSelectedPort & " error@opening")
                End Try

            End If

        End If

    End Sub

    Public Sub ClosePort()
        If bolPortOpen = True Then
            RaiseEvent DebugLog("VISA " & strSelectedPort & " closing")
            Try

                visaInstrument.WriteString("*CLS")
                visaInstrument.WriteString("*RST")

                'visaInstrument = Nothing
                bolPortOpen = False
                RaiseEvent DebugLog("VISA " & strSelectedPort & " closed")
            Catch ex As Exception
                intState = -4
                bolPortOpen = True
                RaiseEvent ErrorLog("VISA " & strSelectedPort & " error@closing " & ex.ToString)
            End Try

        End If
    End Sub

    Public Sub Init()

        intState = 0

        bolDataReceivedEnabled = True

        If IO.FileExists(strCurrentPath & "Ivi.Visa.Interop.dll", False) = True Then

            If bolVISALoaded = False Then
                Try
                    visaResManager = New Ivi.Visa.Interop.ResourceManager
                    visaInstrument = New Ivi.Visa.Interop.FormattedIO488
                    bolVISALoaded = True
                Catch
                    intState = -9
                    RaiseEvent ErrorLog("VISA " & strSelectedPort & " error@init")
                    RaiseEvent ErrorLog("--> please install VISA <--")
                    bolVISALoaded = False
                End Try
            End If

        Else
            intState = -9
            RaiseEvent ErrorLog("--> please install VISA <--")
        End If

    End Sub


End Class
