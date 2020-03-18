<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucManualControl
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
        Me.cmbCommand = New System.Windows.Forms.ComboBox()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.lblRx = New System.Windows.Forms.Label()
        Me.txtTx = New System.Windows.Forms.TextBox()
        Me.chbRefresh = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'cmbCommand
        '
        Me.cmbCommand.FormattingEnabled = True
        Me.cmbCommand.Location = New System.Drawing.Point(0, 3)
        Me.cmbCommand.Name = "cmbCommand"
        Me.cmbCommand.Size = New System.Drawing.Size(191, 21)
        Me.cmbCommand.TabIndex = 0
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(300, 3)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(62, 21)
        Me.btnSend.TabIndex = 1
        Me.btnSend.Text = "send"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'lblRx
        '
        Me.lblRx.AutoSize = True
        Me.lblRx.Location = New System.Drawing.Point(389, 7)
        Me.lblRx.Name = "lblRx"
        Me.lblRx.Size = New System.Drawing.Size(30, 13)
        Me.lblRx.TabIndex = 2
        Me.lblRx.Text = "lblRx"
        '
        'txtTx
        '
        Me.txtTx.Location = New System.Drawing.Point(197, 3)
        Me.txtTx.Name = "txtTx"
        Me.txtTx.Size = New System.Drawing.Size(97, 20)
        Me.txtTx.TabIndex = 3
        '
        'chbRefresh
        '
        Me.chbRefresh.AutoSize = True
        Me.chbRefresh.Location = New System.Drawing.Point(368, 7)
        Me.chbRefresh.Name = "chbRefresh"
        Me.chbRefresh.Size = New System.Drawing.Size(15, 14)
        Me.chbRefresh.TabIndex = 4
        Me.chbRefresh.UseVisualStyleBackColor = True
        '
        'ucManualControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.chbRefresh)
        Me.Controls.Add(Me.txtTx)
        Me.Controls.Add(Me.lblRx)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.cmbCommand)
        Me.Name = "ucManualControl"
        Me.Size = New System.Drawing.Size(620, 25)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbCommand As System.Windows.Forms.ComboBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents lblRx As System.Windows.Forms.Label
    Friend WithEvents txtTx As System.Windows.Forms.TextBox
    Friend WithEvents chbRefresh As System.Windows.Forms.CheckBox

End Class
