<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTCP
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblLogServer = New System.Windows.Forms.Label()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.txtSend = New System.Windows.Forms.TextBox()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.lblReceived = New System.Windows.Forms.Label()
        Me.rbtnServer = New System.Windows.Forms.RadioButton()
        Me.rbtnClient = New System.Windows.Forms.RadioButton()
        Me.lblLogClient = New System.Windows.Forms.Label()
        Me.btnReceiveArray = New System.Windows.Forms.Button()
        Me.txtFromServer = New System.Windows.Forms.TextBox()
        Me.txtToClient = New System.Windows.Forms.TextBox()
        Me.chbState1 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'lblLogServer
        '
        Me.lblLogServer.AutoSize = True
        Me.lblLogServer.Location = New System.Drawing.Point(12, 112)
        Me.lblLogServer.Name = "lblLogServer"
        Me.lblLogServer.Size = New System.Drawing.Size(66, 13)
        Me.lblLogServer.TabIndex = 0
        Me.lblLogServer.Text = "lblLogServer"
        '
        'txtIP
        '
        Me.txtIP.Location = New System.Drawing.Point(12, 12)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(118, 20)
        Me.txtIP.TabIndex = 1
        Me.txtIP.Text = "192.168.178.42"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(12, 38)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(118, 20)
        Me.txtPort.TabIndex = 2
        Me.txtPort.Text = "50290"
        '
        'txtSend
        '
        Me.txtSend.Location = New System.Drawing.Point(12, 64)
        Me.txtSend.Name = "txtSend"
        Me.txtSend.Size = New System.Drawing.Size(118, 20)
        Me.txtSend.TabIndex = 3
        Me.txtSend.Text = "hello"
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(137, 35)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(70, 23)
        Me.btnOpen.TabIndex = 4
        Me.btnOpen.Text = "open/con"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(213, 36)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(70, 23)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "close/disc"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(137, 62)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(146, 23)
        Me.btnSend.TabIndex = 6
        Me.btnSend.Text = "send"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'lblReceived
        '
        Me.lblReceived.AutoSize = True
        Me.lblReceived.Location = New System.Drawing.Point(12, 87)
        Me.lblReceived.Name = "lblReceived"
        Me.lblReceived.Size = New System.Drawing.Size(63, 13)
        Me.lblReceived.TabIndex = 7
        Me.lblReceived.Text = "lblReceived"
        '
        'rbtnServer
        '
        Me.rbtnServer.AutoSize = True
        Me.rbtnServer.Location = New System.Drawing.Point(137, 12)
        Me.rbtnServer.Name = "rbtnServer"
        Me.rbtnServer.Size = New System.Drawing.Size(54, 17)
        Me.rbtnServer.TabIndex = 8
        Me.rbtnServer.Text = "server"
        Me.rbtnServer.UseVisualStyleBackColor = True
        '
        'rbtnClient
        '
        Me.rbtnClient.AutoSize = True
        Me.rbtnClient.Checked = True
        Me.rbtnClient.Location = New System.Drawing.Point(197, 12)
        Me.rbtnClient.Name = "rbtnClient"
        Me.rbtnClient.Size = New System.Drawing.Size(50, 17)
        Me.rbtnClient.TabIndex = 9
        Me.rbtnClient.TabStop = True
        Me.rbtnClient.Text = "client"
        Me.rbtnClient.UseVisualStyleBackColor = True
        '
        'lblLogClient
        '
        Me.lblLogClient.AutoSize = True
        Me.lblLogClient.Location = New System.Drawing.Point(12, 131)
        Me.lblLogClient.Name = "lblLogClient"
        Me.lblLogClient.Size = New System.Drawing.Size(61, 13)
        Me.lblLogClient.TabIndex = 10
        Me.lblLogClient.Text = "lblLogClient"
        '
        'btnReceiveArray
        '
        Me.btnReceiveArray.Location = New System.Drawing.Point(137, 244)
        Me.btnReceiveArray.Name = "btnReceiveArray"
        Me.btnReceiveArray.Size = New System.Drawing.Size(143, 23)
        Me.btnReceiveArray.TabIndex = 12
        Me.btnReceiveArray.Text = "receive"
        Me.btnReceiveArray.UseVisualStyleBackColor = True
        '
        'txtFromServer
        '
        Me.txtFromServer.Location = New System.Drawing.Point(137, 156)
        Me.txtFromServer.Multiline = True
        Me.txtFromServer.Name = "txtFromServer"
        Me.txtFromServer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFromServer.Size = New System.Drawing.Size(143, 82)
        Me.txtFromServer.TabIndex = 13
        '
        'txtToClient
        '
        Me.txtToClient.Location = New System.Drawing.Point(12, 156)
        Me.txtToClient.Multiline = True
        Me.txtToClient.Name = "txtToClient"
        Me.txtToClient.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtToClient.Size = New System.Drawing.Size(118, 82)
        Me.txtToClient.TabIndex = 14
        Me.txtToClient.Text = "Line1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Line2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Line3"
        '
        'chbState1
        '
        Me.chbState1.AutoSize = True
        Me.chbState1.Location = New System.Drawing.Point(12, 248)
        Me.chbState1.Name = "chbState1"
        Me.chbState1.Size = New System.Drawing.Size(75, 17)
        Me.chbState1.TabIndex = 15
        Me.chbState1.Text = "chbState1"
        Me.chbState1.UseVisualStyleBackColor = True
        '
        'Demo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 276)
        Me.Controls.Add(Me.chbState1)
        Me.Controls.Add(Me.txtToClient)
        Me.Controls.Add(Me.txtFromServer)
        Me.Controls.Add(Me.btnReceiveArray)
        Me.Controls.Add(Me.lblLogClient)
        Me.Controls.Add(Me.rbtnClient)
        Me.Controls.Add(Me.rbtnServer)
        Me.Controls.Add(Me.lblReceived)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.txtSend)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.txtIP)
        Me.Controls.Add(Me.lblLogServer)
        Me.Name = "Demo"
        Me.Text = "webinterface v1.2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLogServer As System.Windows.Forms.Label
    Friend WithEvents txtIP As System.Windows.Forms.TextBox
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents txtSend As System.Windows.Forms.TextBox
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents lblReceived As System.Windows.Forms.Label
    Friend WithEvents rbtnServer As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnClient As System.Windows.Forms.RadioButton
    Friend WithEvents lblLogClient As System.Windows.Forms.Label
    Friend WithEvents btnReceiveArray As System.Windows.Forms.Button
    Friend WithEvents txtFromServer As System.Windows.Forms.TextBox
    Friend WithEvents txtToClient As System.Windows.Forms.TextBox
    Friend WithEvents chbState1 As System.Windows.Forms.CheckBox

End Class
