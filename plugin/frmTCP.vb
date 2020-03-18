Imports System
Imports System.Net
Imports System.Net.Sockets

Class frmTCP

    Dim WithEvents TCPServer As New clsTCPServer
    Dim WithEvents TCPClient As New clsTCPClient

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        If rbtnServer.Checked = True Then


            TCPServer.Address = IPAddress.Parse(txtIP.Text)
            TCPServer.Port = Convert.ToInt32(txtPort.Text)
            TCPServer.Open()

        Else


            TCPClient.Address = IPAddress.Parse(txtIP.Text)
            'TCPClient.Port = Convert.ToInt32(txtPort.Text)
            TCPClient.Connect("ACK", 2000)

        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If rbtnServer.Checked = True Then
            TCPServer.Close()
        Else
            TCPClient.Disconnect()
        End If
    End Sub

    ' Version: 1.0
    'Private Sub TCPServer_DataReceived(ByVal str As String) Handles TCPServer.DataReceived
    '    lblReceived.Text = str
    'End Sub

    Private Sub TCPServer_DataReceived(ByRef str As String) Handles TCPServer.DataReceived
        lblReceived.Text = str

        Select Case str

            Case "load"
                str = ""

                For i As Integer = 0 To txtToClient.Lines.Length - 1
                    str = str & txtToClient.Lines.GetValue(i) & ";"
                Next


            Case "state1"
                If chbState1.Checked = True Then
                    str = "true"
                Else
                    str = "false"
                End If


        End Select

    End Sub

    Private Sub TCPClient_TextReceived(str As String) Handles TCPClient.TextReceived

        Dim arr() As String = str.Split(";".ToCharArray)

        txtFromServer.Text = ""

        For i As Integer = 0 To arr.Length - 1
            txtFromServer.AppendText(arr(i) & vbCrLf)
        Next


    End Sub

    'Private Sub TCPClient_TextReceived(ByRef str As String) Handles TCPClient.TextReceived

    '    Dim arr() As String = str.Split(";".ToCharArray)

    '    txtFromServer.Text = ""

    '    For i As Integer = 0 To arr.Length - 1
    '        txtFromServer.AppendText(arr(i) & vbCrLf)
    '    Next

    'End Sub


    Private Sub rbtnServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnServer.CheckedChanged
        If rbtnServer.Checked = True Then
            rbtnClient.Checked = False
        End If
    End Sub

    Private Sub rbtnClient_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClient.CheckedChanged
        If rbtnClient.Checked = True Then
            rbtnServer.Checked = False
        End If
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        TCPClient.Send(txtSend.Text)
    End Sub

    Private Sub TCPClient_Log(ByVal str As String) Handles TCPClient.Log
        lblLogClient.Text = str
    End Sub

    Private Sub TCPServer_Log(ByVal str As String, ByVal level As Integer) Handles TCPServer.Log
        lblLogServer.Text = str & "," & level
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        TCPClient.Disconnect()
        TCPServer.Close()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
    End Sub


    Private Sub btnReceiveArray_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceiveArray.Click

        TCPClient.Send("load")

    End Sub


End Class
