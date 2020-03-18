Imports System.IO.Ports
Imports System.Text

Class clsCOM

    ' intState:-8=error timeout -7=error checksum -6=error receiving -5=error sending -4=error closing -3=error opening -2=not found -1=error 0=ready
    Dim intState As Integer = 0
    Dim WithEvents SerialPort As New System.IO.Ports.SerialPort("COM1", 38400, Parity.None, 8, StopBits.One)
    Dim intBaud As Integer = 38400
    Dim intTimeOut As Integer = 2000
    Dim bolTimedOut As Boolean = False
    Dim strTerminationOK As String = ""
    Dim bolCrLf As Boolean = False
    Dim arrPorts(0 To 50) As String
    Dim strSelectedPort As String = ""
    Dim bolDataReceivedEnabled As Boolean = False
    Dim strReceive As String = ""
    Dim strReceived As New System.Text.StringBuilder
    Dim arrReceived(0 To 250) As String
    Dim bolTextInBuffer As Boolean = False
    Dim bolPortOpen As Boolean = False
    Dim bolSim As Boolean = False
    Private Delegate Sub DelegateSub()
    Private TextboxAktualisieren As New DelegateSub(AddressOf Wertuebergabe)
    Private WithEvents timGetBytes As New System.Timers.Timer
    Private WithEvents timTimeOut As New System.Timers.Timer

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

            Case "baud"
                intBaud = str

            Case "reset"
                intState = 0

            Case Else
                RaiseEvent ErrorLog("error@SetValue-command not found-" & name)

        End Select


    End Sub


    Public Function GetValue(ByVal name As String)

        Select Case name

            Case "state"
                Return intState


            Case "receive"

                Dim str As String = ""

                If bolSim = False Then
                    timTimeOut.Stop()
                    timTimeOut.Interval = intTimeOut
                    bolTimedOut = False
                    timTimeOut.Start()

                    Do Until bolTextInBuffer = True
                        'Delay(0.1)
                        Threading.Thread.Sleep(50)
                    Loop

                    timTimeOut.Stop()

                    For i As Integer = 0 To UBound(arrReceived)
                        If arrReceived(i).Length > 0 Then
                            str = str & arrReceived(i) & ";"
                        End If
                    Next

                Else

                    str = "1 2 3V;4 5 6V;7 8 9V"

                End If

                Return str


            Case Else
                Return "error@GetValue-command not found-" & name

        End Select

    End Function

    Private Sub Send(ByVal str As String)

        If intState = 0 Then

            Dim strReceived As String = ""
            Dim bolChecksumOK As Boolean = False
            Dim intAttempts As Integer = 0

            Do
                Transmit(str)

                strReceived = GetValue("receive")

                For i As Integer = 0 To UBound(arrReceived)

                    'Debug.Write(arrReceived(i))

                    If strTerminationOK.Length > 0 Then

                        If arrReceived(i).StartsWith(strTerminationOK) = True Or arrReceived(i).EndsWith(strTerminationOK) Then

                            bolChecksumOK = True

                        End If

                    Else

                        If strTerminationOK = "vbCrLf" Then

                            If bolCrLf = True Then

                                bolChecksumOK = True

                            End If

                        Else

                            bolChecksumOK = True

                        End If

                    End If
                Next

                intAttempts += 1
            Loop Until bolChecksumOK = True Or intAttempts = 3

            If intAttempts = 3 Then
                ' fehler
                intState = -7
                RaiseEvent ErrorLog("ComPort " & SerialPort.PortName & " error@checksum")
            End If

            RaiseEvent TextReceived(strReceived)

            intAttempts = Nothing
            strReceived = Nothing

        Else

            RaiseEvent ErrorLog("ComPort " & SerialPort.PortName & " error: " & intState)

        End If

    End Sub

    Private Sub Transmit(ByVal str As String, Optional ByVal clear As Boolean = True)

        'sendet daten über den com-port
        RaiseEvent DebugLog("ComPort " & SerialPort.PortName & " send """ & str & """")

        If clear = True Then
            strReceived.Remove(0, strReceived.Length)

            For i As Integer = 0 To UBound(arrReceived)
                arrReceived(i) = ""
            Next

            bolTextInBuffer = False
        End If

        bolCrLf = False

        str = str & vbCr.ToString

        With SerialPort

            Dim intLen As Integer = str.Length

            Dim byrSend(intLen - 1) As Byte

            For a As Integer = 0 To intLen - 1
                byrSend(a) = Asc(str.Chars(a))
            Next a

            intLen = byrSend.Length

            If str.Length > 1 Then
                If .IsOpen = True Then
                    Try
                        .DiscardInBuffer()
                        .DiscardOutBuffer()
                        .BaudRate = intBaud
                        .BaseStream.Write(byrSend, 0, intLen)
                        'Delay(0.05)
                        'Threading.Thread.Sleep(50)
                    Catch
                        intState = -5
                        RaiseEvent ErrorLog("ComPort " & .PortName & " error@sending")
                    End Try

                Else
                    intState = -1
                    RaiseEvent DebugLog("ComPort " & .PortName & " is not open")
                End If
            End If

        End With

    End Sub

    Private Sub timTimeOut_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timTimeOut.Elapsed

        timTimeOut.Stop()

        'RaiseEvent ErrorLog("ComPort " & SerialPort.PortName & " error@timeout:" & timTimeOut.Interval)
        If strTerminationOK.Length > 0 Then
            intState = -8

            bolTimedOut = True

            strReceived.Remove(0, strReceived.Length)

        End If

        bolTextInBuffer = True

    End Sub

    Private Sub timGetBytes_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timGetBytes.Elapsed
        timGetBytes.Stop()

        'Do Until bolTextInBuffer = False
        '    'Delay(0.1)
        '    Threading.Thread.Sleep(100)
        'Loop

        DataReceived()

        timGetBytes.Start()
    End Sub

    Public Sub DataReceived()

        'Public Sub SerialPort_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort.DataReceived
        If bolDataReceivedEnabled = True And SerialPort.IsOpen = True Then

            Try
                For i As Int32 = 0 To SerialPort.BytesToRead - 1  ' Alle Bytes einzel lesen und anzeigen

                    Dim buf As Byte() = {SerialPort.BaseStream.ReadByte}
                    Dim enc As New System.Text.ASCIIEncoding()

                    strReceive = enc.GetString(buf)


                    If strReceive = Chr(10) Or strReceive = Chr(13) Then

                        bolCrLf = True

                        For r As Integer = 0 To UBound(arrReceived)

                            If arrReceived(r) = "" And strReceived.ToString <> "" Then

                                arrReceived(r) = strReceived.ToString
                                r = UBound(arrReceived)
                                'Debug.WriteLine(strReceived.ToString)

                            End If

                        Next

                        If strReceived.ToString.StartsWith(strTerminationOK) Or arrReceived(i).EndsWith(strTerminationOK) Then

                            bolTextInBuffer = True

                        End If
                        strReceived.Remove(0, strReceived.Length)

                    Else
                        strReceive = strReceive.Replace(Chr(13), Nothing)
                        strReceive = strReceive.Replace(Chr(10), Nothing)
                        TextboxAktualisieren()
                    End If

                Next i
            Catch ex As Exception
                '    'MessageBox.Show("Ein Übertragungsfehler ist aufgetreten.", "Fehler", MessageBoxButtons.OK)
                intState = -6
            End Try
        End If
    End Sub

    Private Sub Wertuebergabe()
        strReceived.Append(strReceive)
    End Sub

    'termination fuer vbCrLf = ""
    Public Sub OpenPort(ByVal termination As String, ByVal timeout As Integer)

        If bolSim = False Then

            strTerminationOK = termination
            intTimeOut = timeout

            If bolPortOpen = False Then

                With SerialPort
                    If .IsOpen = True Then
                        ClosePort()
                    End If
                    .PortName = strSelectedPort
                    If .IsOpen = False Then
                        .BaudRate = intBaud
                        .ReadTimeout = 500
                        .WriteTimeout = 500
                        ClosePort()
                        RaiseEvent DebugLog("ComPort " & strSelectedPort & " opening")
                        Try
                            .Open()
                            bolPortOpen = True
                            RaiseEvent DebugLog("ComPort " & strSelectedPort & " opened")
                        Catch
                            intState = -3
                            bolPortOpen = False
                            RaiseEvent ErrorLog("ComPort " & strSelectedPort & " error@opening")
                        End Try

                    End If
                End With
            End If

        End If

    End Sub

    Public Sub ClosePort()
        With SerialPort
            If .IsOpen = True Then
                RaiseEvent DebugLog("ComPort " & strSelectedPort & " closing")
                Try
                    .Close()
                    bolPortOpen = False
                    RaiseEvent DebugLog("ComPort " & strSelectedPort & " closed")
                Catch
                    intState = -4
                    bolPortOpen = True
                    RaiseEvent ErrorLog("ComPort " & strSelectedPort & " error@closing")
                End Try

            End If
        End With
    End Sub

    Public Sub Init()

        '    RaiseEvent ErrorLog("ComPort Init()")

        '    ' wenn port aus settings gefunden, dann auswaehlen

        '    Array.Clear(arrPorts, 0, arrPorts.Length)
        '    arrPorts = SerialPort.GetPortNames

        '    Dim strRoot As String = "program/settings"

        '    Dim strPortSettings As String = "COM1" ' = XML.readXML(strRoot, "default_comport", "value", "string", "settings.xml")
        '    intTimeOut = "500" ' XML.readXML(strRoot, "default_timeout", "value", "integer", "settings.xml")

        '    strSelectedPort = ""

        '    For i As Integer = 0 To UBound(arrPorts)

        '        If arrPorts(i).StartsWith(strPortSettings) = True Or arrPorts(i).StartsWith("COM24") = True Then

        '            strSelectedPort = arrPorts(i)

        '            RaiseEvent ErrorLog("ComPort " & arrPorts(i) & " selected")

        '            i = UBound(arrPorts)

        '        End If

        '    Next

        '    If strSelectedPort.Length = 0 Then
        '        RaiseEvent ErrorLog("ComPort " & strPortSettings & " not found")
        '        intState = -2
        '    Else

        intState = 0

        '        OpenPort()

        timGetBytes.Interval = 100
        timGetBytes.Start()

        bolDataReceivedEnabled = True

        '    End If


    End Sub





End Class
