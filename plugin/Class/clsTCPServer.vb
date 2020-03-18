Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Class clsTCPServer

    Dim strIPAddress As String = "127.0.0.1"
    Public Address As IPAddress = IPAddress.Parse(strIPAddress)
    Public Port As Integer = "8888"
    Public Event Log(ByVal str As String, ByVal level As Integer)
    Public Event DataReceived(ByRef str As String)

    Dim bolAbortTask0 As Boolean = True

    Dim ServerIP As IPAddress
    Dim ServerSocket As TcpListener
    Dim ClientSocket As TcpClient

    Dim TCPClient As clsTCPClient

    Dim bolTask0Running As Boolean = False
    Dim task0 As New Threading.Thread(AddressOf GetTCP)


    Public Sub Open()

        If bolAbortTask0 = True Then

            WriteLog("opening server")

            If strIPAddress.Length <> 0 Then
                If Port > 0 Then

                    Try
                        ServerIP = Address
                        ServerSocket = New TcpListener(ServerIP, Port)

                        ServerSocket.Start()

                        bolAbortTask0 = False

                        If bolTask0Running = False Then
                            task0 = New Threading.Thread(AddressOf GetTCP)

                            task0.Start()
                            bolTask0Running = True
                        End If

                        WriteLog("server opened at " & Address.ToString & ":" & Port)

                    Catch ex As Exception
                        'error start Server
                        WriteLog("error starting server: " & ex.Message)
                    End Try
                Else
                    'error Port
                    WriteLog("error wrong port: " & Port)
                End If
            Else
                ' error IPAddress
                WriteLog("error wrong ipaddress: " & strIPAddress)
            End If

        End If

    End Sub


    Public Sub Close()

        If bolAbortTask0 = False Then

            WriteLog("closing server")

            Try

                bolAbortTask0 = True

                TCPClient = New clsTCPClient
                TCPClient.Address = ServerIP
                'TCPClient.Port = Port
                TCPClient.Connect("ACK", 2000)

                Delay(0.5)

                ServerSocket.Stop()

                task0.Abort()
                task0 = Nothing
                bolTask0Running = False

                WriteLog("server closed")

            Catch ex As Exception
                'error closing server
                WriteLog("error closing server: " & ex.Message)
            End Try

        Else
            WriteLog("server already closed")
        End If


    End Sub


    Public Function GetValue(ByVal name As String, Optional ByVal str As String = "")

        Select Case name

            Case "Name"
                Return strPluginName

            Case "Version"
                Return strPluginVersion

            Case "State"
                Return intState

            Case "Log"
                bolLog = False
                Return strLog

            Case Else
                Return -1

        End Select

    End Function


    Private Sub GetTCP()

        WriteLog("waiting for client", 2)
        ClientSocket = ServerSocket.AcceptTcpClient()
        Dim IP As Net.IPEndPoint = ClientSocket.Client.RemoteEndPoint
        WriteLog("client connected: " & IP.Address.ToString, 3)

        Do Until bolAbortTask0 = True

            If ClientSocket.Connected = False Then

                WriteLog("waiting for client", 2)
                ClientSocket = ServerSocket.AcceptTcpClient()
                WriteLog("client connected: " & IP.Address.ToString, 3)

            Else

                WriteLog("loop")

                Try

                    Dim networkStream As NetworkStream = ClientSocket.GetStream()
                    'If networkStream.DataAvailable = True Then

                    Dim bytesFrom(CInt(ClientSocket.ReceiveBufferSize)) As Byte

                    networkStream.Read(bytesFrom, 0, CInt(ClientSocket.ReceiveBufferSize))

                    Dim dataFromClient As String = System.Text.Encoding.ASCII.GetString(bytesFrom)

                    If dataFromClient.Length > 0 Then

                        'MsgBox(dataFromClient)

                        dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"))

                        WriteLog("data from client - """ & dataFromClient & """")

                        If dataFromClient = "bye" Then

                            ClientSocket.Close()

                            WriteLog("client disconnected")

                        Else

                            Dim oldDataFromClient As String = dataFromClient

                            RaiseEvent DataReceived(dataFromClient)

                            WriteLog("sending answer")

                            Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(dataFromClient & "$")

                            networkStream.Write(outStream, 0, outStream.Length)
                            networkStream.Flush()


                        End If

                    End If
                Catch ex As Exception
                    WriteLog("error receiving data: " & ex.Message)
                    ClientSocket.Close()
                End Try


            End If

            Delay(0.1)

        Loop

        ClientSocket.Close()

    End Sub

    Private Sub WriteLog(ByVal str As String, Optional ByVal level As Integer = 0)
        RaiseEvent Log(str, level)
    End Sub


End Class