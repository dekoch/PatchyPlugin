<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLauncher
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLauncher))
        Me.btnMeasureSource = New System.Windows.Forms.Button()
        Me.btnCOM = New System.Windows.Forms.Button()
        Me.btnDebug = New System.Windows.Forms.Button()
        Me.lblAbout = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnMeasureSource
        '
        Me.btnMeasureSource.Location = New System.Drawing.Point(12, 12)
        Me.btnMeasureSource.Name = "btnMeasureSource"
        Me.btnMeasureSource.Size = New System.Drawing.Size(253, 34)
        Me.btnMeasureSource.TabIndex = 0
        Me.btnMeasureSource.Text = "Measure/Source"
        Me.btnMeasureSource.UseVisualStyleBackColor = True
        '
        'btnCOM
        '
        Me.btnCOM.Location = New System.Drawing.Point(12, 92)
        Me.btnCOM.Name = "btnCOM"
        Me.btnCOM.Size = New System.Drawing.Size(253, 34)
        Me.btnCOM.TabIndex = 1
        Me.btnCOM.Text = "Terminal"
        Me.btnCOM.UseVisualStyleBackColor = True
        '
        'btnDebug
        '
        Me.btnDebug.Location = New System.Drawing.Point(12, 52)
        Me.btnDebug.Name = "btnDebug"
        Me.btnDebug.Size = New System.Drawing.Size(253, 34)
        Me.btnDebug.TabIndex = 2
        Me.btnDebug.Text = "Debug"
        Me.btnDebug.UseVisualStyleBackColor = True
        '
        'lblAbout
        '
        Me.lblAbout.AutoSize = True
        Me.lblAbout.Location = New System.Drawing.Point(3, 167)
        Me.lblAbout.Name = "lblAbout"
        Me.lblAbout.Size = New System.Drawing.Size(45, 13)
        Me.lblAbout.TabIndex = 3
        Me.lblAbout.Text = "lblAbout"
        '
        'frmLauncher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(277, 306)
        Me.Controls.Add(Me.lblAbout)
        Me.Controls.Add(Me.btnDebug)
        Me.Controls.Add(Me.btnCOM)
        Me.Controls.Add(Me.btnMeasureSource)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(186, 239)
        Me.Name = "frmLauncher"
        Me.Text = "frmLauncher"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnMeasureSource As System.Windows.Forms.Button
    Friend WithEvents btnCOM As System.Windows.Forms.Button
    Friend WithEvents btnDebug As System.Windows.Forms.Button
    Friend WithEvents lblAbout As System.Windows.Forms.Label
End Class
