<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucCOM
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.txtRx = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.txtTx = New System.Windows.Forms.TextBox()
        Me.cmbPort = New System.Windows.Forms.ComboBox()
        Me.txtBaud = New System.Windows.Forms.TextBox()
        Me.lblLog = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtRx
        '
        Me.txtRx.Location = New System.Drawing.Point(100, 30)
        Me.txtRx.Multiline = True
        Me.txtRx.Name = "txtRx"
        Me.txtRx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRx.Size = New System.Drawing.Size(209, 125)
        Me.txtRx.TabIndex = 13
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(3, 82)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(91, 20)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(3, 56)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(91, 20)
        Me.btnOpen.TabIndex = 10
        Me.btnOpen.Text = "Open"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(3, 161)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(91, 20)
        Me.btnSend.TabIndex = 9
        Me.btnSend.Text = "Send"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'txtTx
        '
        Me.txtTx.Location = New System.Drawing.Point(100, 162)
        Me.txtTx.Name = "txtTx"
        Me.txtTx.Size = New System.Drawing.Size(209, 20)
        Me.txtTx.TabIndex = 8
        Me.txtTx.Text = "*IDN?"
        '
        'cmbPort
        '
        Me.cmbPort.FormattingEnabled = True
        Me.cmbPort.Location = New System.Drawing.Point(3, 3)
        Me.cmbPort.Name = "cmbPort"
        Me.cmbPort.Size = New System.Drawing.Size(91, 21)
        Me.cmbPort.TabIndex = 15
        '
        'txtBaud
        '
        Me.txtBaud.Location = New System.Drawing.Point(3, 30)
        Me.txtBaud.Name = "txtBaud"
        Me.txtBaud.Size = New System.Drawing.Size(91, 20)
        Me.txtBaud.TabIndex = 16
        Me.txtBaud.Text = "9600"
        '
        'lblLog
        '
        Me.lblLog.AutoSize = True
        Me.lblLog.Location = New System.Drawing.Point(100, 6)
        Me.lblLog.Name = "lblLog"
        Me.lblLog.Size = New System.Drawing.Size(35, 13)
        Me.lblLog.TabIndex = 17
        Me.lblLog.Text = "lblLog"
        '
        'ucCOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblLog)
        Me.Controls.Add(Me.txtBaud)
        Me.Controls.Add(Me.cmbPort)
        Me.Controls.Add(Me.txtRx)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.txtTx)
        Me.Name = "ucCOM"
        Me.Size = New System.Drawing.Size(314, 188)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtRx As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents txtTx As System.Windows.Forms.TextBox
    Friend WithEvents cmbPort As System.Windows.Forms.ComboBox
    Friend WithEvents txtBaud As System.Windows.Forms.TextBox
    Friend WithEvents lblLog As System.Windows.Forms.Label

End Class
