<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMeasureSource
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMeasureSource))
        Me.timLog = New System.Windows.Forms.Timer(Me.components)
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.rbtnHBS = New System.Windows.Forms.RadioButton()
        Me.rbtnFluke45 = New System.Windows.Forms.RadioButton()
        Me.rbtnMultical = New System.Windows.Forms.RadioButton()
        Me.txtBaud = New System.Windows.Forms.TextBox()
        Me.cmbPort = New System.Windows.Forms.ComboBox()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.grbCommands = New System.Windows.Forms.GroupBox()
        Me.chbDebug = New System.Windows.Forms.CheckBox()
        Me.timVisu1 = New System.Windows.Forms.Timer(Me.components)
        Me.rbtnBUER2 = New System.Windows.Forms.RadioButton()
        Me.rbtnAVRNETIO = New System.Windows.Forms.RadioButton()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.lblCOMPort = New System.Windows.Forms.Label()
        Me.lblBaud = New System.Windows.Forms.Label()
        Me.lblIP = New System.Windows.Forms.Label()
        Me.lblPort = New System.Windows.Forms.Label()
        Me.rbtnAgilent34450 = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'timLog
        '
        Me.timLog.Enabled = True
        Me.timLog.Interval = 50
        '
        'txtLog
        '
        Me.txtLog.Location = New System.Drawing.Point(690, 34)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(287, 301)
        Me.txtLog.TabIndex = 1
        '
        'rbtnHBS
        '
        Me.rbtnHBS.AutoSize = True
        Me.rbtnHBS.Location = New System.Drawing.Point(12, 4)
        Me.rbtnHBS.Name = "rbtnHBS"
        Me.rbtnHBS.Size = New System.Drawing.Size(47, 17)
        Me.rbtnHBS.TabIndex = 2
        Me.rbtnHBS.TabStop = True
        Me.rbtnHBS.Text = "HBS"
        Me.rbtnHBS.UseVisualStyleBackColor = True
        '
        'rbtnFluke45
        '
        Me.rbtnFluke45.AutoSize = True
        Me.rbtnFluke45.Location = New System.Drawing.Point(65, 4)
        Me.rbtnFluke45.Name = "rbtnFluke45"
        Me.rbtnFluke45.Size = New System.Drawing.Size(63, 17)
        Me.rbtnFluke45.TabIndex = 3
        Me.rbtnFluke45.TabStop = True
        Me.rbtnFluke45.Text = "Fluke45"
        Me.rbtnFluke45.UseVisualStyleBackColor = True
        '
        'rbtnMultical
        '
        Me.rbtnMultical.AutoSize = True
        Me.rbtnMultical.Location = New System.Drawing.Point(134, 4)
        Me.rbtnMultical.Name = "rbtnMultical"
        Me.rbtnMultical.Size = New System.Drawing.Size(61, 17)
        Me.rbtnMultical.TabIndex = 4
        Me.rbtnMultical.TabStop = True
        Me.rbtnMultical.Text = "Multical"
        Me.rbtnMultical.UseVisualStyleBackColor = True
        '
        'txtBaud
        '
        Me.txtBaud.Location = New System.Drawing.Point(450, 4)
        Me.txtBaud.Name = "txtBaud"
        Me.txtBaud.Size = New System.Drawing.Size(108, 20)
        Me.txtBaud.TabIndex = 6
        Me.txtBaud.Text = "9600"
        '
        'cmbPort
        '
        Me.cmbPort.FormattingEnabled = True
        Me.cmbPort.Location = New System.Drawing.Point(274, 3)
        Me.cmbPort.Name = "cmbPort"
        Me.cmbPort.Size = New System.Drawing.Size(79, 21)
        Me.cmbPort.TabIndex = 7
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(618, 2)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(66, 21)
        Me.btnOpen.TabIndex = 8
        Me.btnOpen.Text = "start"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(618, 33)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 20)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "stop"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'grbCommands
        '
        Me.grbCommands.Location = New System.Drawing.Point(12, 58)
        Me.grbCommands.Name = "grbCommands"
        Me.grbCommands.Size = New System.Drawing.Size(672, 277)
        Me.grbCommands.TabIndex = 10
        Me.grbCommands.TabStop = False
        '
        'chbDebug
        '
        Me.chbDebug.AutoSize = True
        Me.chbDebug.Checked = True
        Me.chbDebug.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbDebug.Location = New System.Drawing.Point(921, 11)
        Me.chbDebug.Name = "chbDebug"
        Me.chbDebug.Size = New System.Drawing.Size(56, 17)
        Me.chbDebug.TabIndex = 11
        Me.chbDebug.Text = "debug"
        Me.chbDebug.UseVisualStyleBackColor = True
        '
        'timVisu1
        '
        Me.timVisu1.Enabled = True
        '
        'rbtnBUER2
        '
        Me.rbtnBUER2.AutoSize = True
        Me.rbtnBUER2.Location = New System.Drawing.Point(12, 31)
        Me.rbtnBUER2.Name = "rbtnBUER2"
        Me.rbtnBUER2.Size = New System.Drawing.Size(61, 17)
        Me.rbtnBUER2.TabIndex = 12
        Me.rbtnBUER2.TabStop = True
        Me.rbtnBUER2.Text = "BUER2"
        Me.rbtnBUER2.UseVisualStyleBackColor = True
        '
        'rbtnAVRNETIO
        '
        Me.rbtnAVRNETIO.AutoSize = True
        Me.rbtnAVRNETIO.Location = New System.Drawing.Point(79, 31)
        Me.rbtnAVRNETIO.Name = "rbtnAVRNETIO"
        Me.rbtnAVRNETIO.Size = New System.Drawing.Size(86, 17)
        Me.rbtnAVRNETIO.TabIndex = 13
        Me.rbtnAVRNETIO.TabStop = True
        Me.rbtnAVRNETIO.Text = "AVR-NET-IO"
        Me.rbtnAVRNETIO.UseVisualStyleBackColor = True
        '
        'txtIP
        '
        Me.txtIP.Location = New System.Drawing.Point(274, 30)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(135, 20)
        Me.txtIP.TabIndex = 14
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(450, 30)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(108, 20)
        Me.txtPort.TabIndex = 15
        Me.txtPort.Text = "50290"
        '
        'lblCOMPort
        '
        Me.lblCOMPort.AutoSize = True
        Me.lblCOMPort.Location = New System.Drawing.Point(212, 6)
        Me.lblCOMPort.Name = "lblCOMPort"
        Me.lblCOMPort.Size = New System.Drawing.Size(56, 13)
        Me.lblCOMPort.TabIndex = 16
        Me.lblCOMPort.Text = "COM-Port:"
        '
        'lblBaud
        '
        Me.lblBaud.AutoSize = True
        Me.lblBaud.Location = New System.Drawing.Point(409, 6)
        Me.lblBaud.Name = "lblBaud"
        Me.lblBaud.Size = New System.Drawing.Size(35, 13)
        Me.lblBaud.TabIndex = 17
        Me.lblBaud.Text = "Baud:"
        '
        'lblIP
        '
        Me.lblIP.AutoSize = True
        Me.lblIP.Location = New System.Drawing.Point(248, 33)
        Me.lblIP.Name = "lblIP"
        Me.lblIP.Size = New System.Drawing.Size(20, 13)
        Me.lblIP.TabIndex = 18
        Me.lblIP.Text = "IP:"
        '
        'lblPort
        '
        Me.lblPort.AutoSize = True
        Me.lblPort.Location = New System.Drawing.Point(415, 33)
        Me.lblPort.Name = "lblPort"
        Me.lblPort.Size = New System.Drawing.Size(29, 13)
        Me.lblPort.TabIndex = 19
        Me.lblPort.Text = "Port:"
        '
        'rbtnAgilent34450
        '
        Me.rbtnAgilent34450.AutoSize = True
        Me.rbtnAgilent34450.Location = New System.Drawing.Point(171, 31)
        Me.rbtnAgilent34450.Name = "rbtnAgilent34450"
        Me.rbtnAgilent34450.Size = New System.Drawing.Size(55, 17)
        Me.rbtnAgilent34450.TabIndex = 20
        Me.rbtnAgilent34450.TabStop = True
        Me.rbtnAgilent34450.Text = "34450"
        Me.rbtnAgilent34450.UseVisualStyleBackColor = True
        '
        'frmMeasureSource
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(989, 347)
        Me.Controls.Add(Me.rbtnAgilent34450)
        Me.Controls.Add(Me.rbtnAVRNETIO)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.lblPort)
        Me.Controls.Add(Me.lblIP)
        Me.Controls.Add(Me.lblBaud)
        Me.Controls.Add(Me.lblCOMPort)
        Me.Controls.Add(Me.txtIP)
        Me.Controls.Add(Me.rbtnBUER2)
        Me.Controls.Add(Me.chbDebug)
        Me.Controls.Add(Me.grbCommands)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.cmbPort)
        Me.Controls.Add(Me.txtBaud)
        Me.Controls.Add(Me.rbtnMultical)
        Me.Controls.Add(Me.rbtnFluke45)
        Me.Controls.Add(Me.rbtnHBS)
        Me.Controls.Add(Me.txtLog)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMeasureSource"
        Me.Text = "plugin.dll - www.mariokoch.de"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents timLog As System.Windows.Forms.Timer
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents rbtnHBS As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnFluke45 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnMultical As System.Windows.Forms.RadioButton
    Friend WithEvents txtBaud As System.Windows.Forms.TextBox
    Friend WithEvents cmbPort As System.Windows.Forms.ComboBox
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents grbCommands As System.Windows.Forms.GroupBox
    Friend WithEvents chbDebug As System.Windows.Forms.CheckBox
    Friend WithEvents timVisu1 As System.Windows.Forms.Timer
    Friend WithEvents rbtnBUER2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnAVRNETIO As System.Windows.Forms.RadioButton
    Friend WithEvents txtIP As System.Windows.Forms.TextBox
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents lblCOMPort As System.Windows.Forms.Label
    Friend WithEvents lblBaud As System.Windows.Forms.Label
    Friend WithEvents lblIP As System.Windows.Forms.Label
    Friend WithEvents lblPort As System.Windows.Forms.Label
    Friend WithEvents rbtnAgilent34450 As System.Windows.Forms.RadioButton
End Class
