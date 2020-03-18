Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Class clsTCPClient

    Dim strIPAddress As String = "127.0.0.1"
    Public Address As IPAddress = IPAddress.Parse(strIPAddress)
    Public intPort As Integer = "50290"
    Public Event Log(ByVal str As String)
    Public strLog As String = ""

    Public Event TextReceived(ByVal str As String)
    Public Event DebugLog(ByVal str As String)
    Public Event ErrorLog(ByVal str As String)
    ' Dim strFromServer As String

    Dim bolSim As Boolean = False
    Dim intTimeOut As Integer = 2000
    Dim bolTimedOut As Boolean = False
    Private WithEvents timTimeOut As New System.Timers.Timer
    Dim bolTextInBuffer As Boolean = False

    Dim strTerminationOK As String = ""


    Dim strReceived As String
    Dim arrReceived(0 To 250) As String
    Dim bolDataReceivedEnabled As Boolean = False


    Dim ClientSocket As New TcpClient
    Dim ServerStream As NetworkStream

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        Select Case name

            Case "send"
                If bolSim = False Then
                    Send(str)
                End If

            Case "transmit"
                Transmit(str)

            Case "ip"
                strIPAddress = str
                If str = "SIM" Then
                    bolSim = True
                Else
                    bolSim = False
                End If

            Case "port"
                intPort = Convert.ToInt32(str)

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

                    'For i As Integer = 0 To UBound(arrReceived)
                    '    If arrReceived(i).Length > 0 Then
                    '        str = str & arrReceived(i) & ";"
                    '    End If
                    'Next
                    If strReceived <> "" Then
                        str = strReceived
                    End If

                Else

                    str = "1234"

                End If

                Return str


            Case Else
                Return "error@GetValue-command not found-" & name

        End Select

    End Function

    Public Sub Send(ByVal str As String)

        If intState >= 0 Then

            Dim strReceived As String = ""
            Dim bolChecksumOK As Boolean = False
            Dim intAttempts As Integer = 0

            Do
                Transmit(str, True)

                strReceived = GetValue("receive")

                If strTerminationOK.Length > 0 Then

                    If strReceived.StartsWith(strTerminationOK) = True Or strReceived.EndsWith(strTerminationOK) Then

                        bolChecksumOK = True

                    End If

                Else

                    bolChecksumOK = True

                End If

                intAttempts += 1
            Loop Until bolChecksumOK = True Or intAttempts = 3

            If intAttempts = 3 Then
                ' fehler
                intState = -7
                RaiseEvent ErrorLog("TCP error@checksum")
            End If

            RaiseEvent TextReceived(strReceived)

            intAttempts = Nothing
            strReceived = Nothing

        Else

            RaiseEvent ErrorLog("TCP error: " & intState)

        End If

    End Sub

    Private Sub Transmit(ByVal str As String, Optional ByVal clear As Boolean = True)

        'If str.Length > 0 Then

        If clear = True Then
            'strReceived.Remove(0, strReceived.Length)

            strReceived = ""

            'For i As Integer = 0 To UBound(arrReceived)
            '    arrReceived(i) = ""
            'Next

            bolTextInBuffer = False
        End If

        If ClientSocket.Connected = True Then

            Try

                ServerStream = ClientSocket.GetStream()
                Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(str)

                ServerStream.Write(outStream, 0, outStream.Length)
                'ServerStream.Flush()

                DataReceived()

            Catch ex As Exception
                intState = -5
                RaiseEvent ErrorLog("TCP error@sending " & ex.Message)
            End Try
        Else
            intState = -1
            RaiseEvent ErrorLog("TCP error@server not connected")
        End If

        ' End If

    End Sub

    Private Sub timTimeOut_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timTimeOut.Elapsed

        timTimeOut.Stop()

        'RaiseEvent ErrorLog("ComPort " & SerialPort.PortName & " error@timeout:" & timTimeOut.Interval)
        If strTerminationOK.Length > 0 Then
            intState = -8

            bolTimedOut = True

            strReceived = ""

        End If

        bolTextInBuffer = True

    End Sub

    Public Sub DataReceived()

        If bolDataReceivedEnabled = True And ClientSocket.Connected = True Then

            Try

                Dim bytesFrom(CInt(ClientSocket.ReceiveBufferSize)) As Byte

                ServerStream.Read(bytesFrom, 0, CInt(ClientSocket.ReceiveBufferSize))

                strReceived = System.Text.Encoding.ASCII.GetString(bytesFrom)

                'strReceived = strReceived.Substring(0, strReceived.Length)

                'For r As Integer = 0 To UBound(arrReceived)

                '    If arrReceived(r) = "" And strReceived.ToString <> "" Then

                '        arrReceived(r) = strReceived.ToString
                '        r = UBound(arrReceived)
                '        'Debug.WriteLine(strReceived.ToString)

                '    End If

                'Next

                'If strReceived.ToString.StartsWith(strTerminationOK) Then
                If strReceived <> "" Then

                    bolTextInBuffer = True

                End If
                'End If
                'strReceived.Remove(0, strReceived.Length)


            Catch ex As Exception
                '    'MessageBox.Show("Ein Übertragungsfehler ist aufgetreten.", "Fehler", MessageBoxButtons.OK)
                intState = -6
                RaiseEvent ErrorLog("TCP error@receiving " & ex.Message)
            End Try
        Else
            intState = -1
            RaiseEvent ErrorLog("TCP error@server not connected")
        End If
    End Sub

    Function TestConnection(ByVal address As String) As Boolean

        Try
            My.Computer.Network.Ping(address)
            'If bolDebug = True Then Debug.WriteLine("modUpdate.TestConnection success")
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Sub Connect(ByVal termination As String, ByVal timeout As Integer)

        If bolSim = False Then

            strTerminationOK = termination
            intTimeOut = timeout

            RaiseEvent DebugLog("TCP connecting to server")

            If ClientSocket.Connected = False Then

                If TestConnection(strIPAddress) = True Then

                    Try
                        ClientSocket = New TcpClient
                        ClientSocket.Connect(IPAddress.Parse(strIPAddress), intPort)

                        RaiseEvent DebugLog("TCP connected to server: " & Address.ToString & ":" & intPort)

                    Catch ex As Exception
                        intState = -3
                        'error server not running
                        RaiseEvent ErrorLog("TCP error@connecting to server: " & ex.Message)
                    End Try

                Else
                    intState = -3
                    'error server not running
                    RaiseEvent ErrorLog("TCP error@connecting to server")
                End If

            End If

        End If

    End Sub

    Public Sub Disconnect()

        If ClientSocket.Connected = True Then

            RaiseEvent DebugLog("TCP disconnecting")

            Send("bye")

            RaiseEvent DebugLog("TCP closing socket")
            Try
                ServerStream.Close()
                ClientSocket.Close()

                RaiseEvent DebugLog("TCP socket closed")
            Catch ex As Exception
                intState = -4
                RaiseEvent ErrorLog("TCP error@closing socket: " & ex.Message)
            End Try

        Else
            RaiseEvent DebugLog("already disconnected")
        End If

    End Sub


    Public Sub Init()

        intState = 0

        bolDataReceivedEnabled = True

    End Sub

End Class