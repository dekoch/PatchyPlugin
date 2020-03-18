Imports System
Imports System.Net
Imports System.Net.Sockets

Class clsAVRNETIO

    Dim WithEvents TCPClient As New clsTCPClient

    Dim strCOMState As String

    Dim strCommand As String = ""

    Public intNr As Integer = 0


    '"#IO0_BIND_AVR-NET-IO",
    '"#IO0_IP",
    '"#IO0_PORT",
    '"#IO0_START",
    '"#IO0_STOP",
    '"#IO0_GET_IP",
    '"#IO0_GET_DI",
    '"#IO0_GET_AI",
    '"#IO0_SET_DO",

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        'str = str.Replace(",", ".")

        strCommand = name

        If bolManualControl = True Then
            TCPClient.SetValue("reset", "")
        End If

        If name = "ip" Then
            TCPClient.Init()
        End If

        Select Case name

            Case "start"
                TCPClient.Connect("", 2000)

            Case "stop"
                TCPClient.Disconnect()

            Case "ip"
                Send("ip", str)

            Case "port"
                Send("port", str)

            Case "get_ip"
                Send("send", "GETIP")
                WriteLog(strIORx)

                'Case "get_mask"
                '    Send("send", "GETMASK")

            Case "get_di"
                Send("send", "GETPORT " & str)
                WriteLog(strIORx)

            Case "get_ai"
                Send("send", "GETADC " & str)
                WriteLog(strIORx)

            Case "set_do"
                Send("send", "SETPORT " & str)

            Case "wait_di"
                Do
                    Send("send", "GETPORT " & str)
                    Delay(0.1)
                Loop Until strIORx.StartsWith("1") = True

            Case Else
                WriteLog("not supported command " & name)
                intState = -11

        End Select

        If intState > 0 Then
            intState = 0
        End If

    End Sub

    Private Sub Send(ByVal name As String, ByVal str As String)

        Dim strReceived As String = ""
        Dim bolChecksumOK As Boolean = False
        Dim intAttempts As Integer = 0

        Do

            TCPClient.SetValue(name, str)

            If name = "send" Then

                strReceived = TCPClient.GetValue("receive")
               
                If strReceived.StartsWith("NAK") Then
                    bolChecksumOK = False
                Else
                    bolChecksumOK = True
                    strIORx = strReceived
                End If

            Else

                bolChecksumOK = True

            End If

            intAttempts += 1

            'frmMeasureSource.Text = intAttempts
        Loop Until bolChecksumOK = True Or intAttempts = 10

        If intAttempts = 10 Then
            ' fehler
            intState = -7
            WriteLog("TCP error@checksum")
        End If

    End Sub

    Private Sub COM_DebugLog(ByVal str As String) Handles TCPClient.DebugLog
        If bolDebug = True Then
            WriteLog(str)
        End If
    End Sub

    Private Sub COM_ErrorLog(ByVal str As String) Handles TCPClient.ErrorLog
        WriteLog(str)
        strCOMState = TCPClient.GetValue("state")
        If strCOMState < 0 Then
            intState = strCOMState
        End If
    End Sub

End Class
