<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMeasure
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
        Me.btnFeed = New System.Windows.Forms.Button()
        Me.txtInput = New System.Windows.Forms.TextBox()
        Me.lblOutput = New System.Windows.Forms.Label()
        Me.txtArray = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnFeed
        '
        Me.btnFeed.Location = New System.Drawing.Point(166, 23)
        Me.btnFeed.Name = "btnFeed"
        Me.btnFeed.Size = New System.Drawing.Size(103, 37)
        Me.btnFeed.TabIndex = 0
        Me.btnFeed.Text = "feed"
        Me.btnFeed.UseVisualStyleBackColor = True
        '
        'txtInput
        '
        Me.txtInput.Location = New System.Drawing.Point(25, 32)
        Me.txtInput.Name = "txtInput"
        Me.txtInput.Size = New System.Drawing.Size(135, 20)
        Me.txtInput.TabIndex = 1
        Me.txtInput.Text = "+1.000E-1"
        '
        'lblOutput
        '
        Me.lblOutput.AutoSize = True
        Me.lblOutput.Location = New System.Drawing.Point(285, 35)
        Me.lblOutput.Name = "lblOutput"
        Me.lblOutput.Size = New System.Drawing.Size(49, 13)
        Me.lblOutput.TabIndex = 3
        Me.lblOutput.Text = "lblOutput"
        '
        'txtArray
        '
        Me.txtArray.Location = New System.Drawing.Point(25, 58)
        Me.txtArray.Multiline = True
        Me.txtArray.Name = "txtArray"
        Me.txtArray.Size = New System.Drawing.Size(135, 205)
        Me.txtArray.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(244, 136)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(103, 37)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "feed"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmMeasure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 308)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtArray)
        Me.Controls.Add(Me.lblOutput)
        Me.Controls.Add(Me.txtInput)
        Me.Controls.Add(Me.btnFeed)
        Me.Name = "frmMeasure"
        Me.Text = "frmMeasure"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnFeed As System.Windows.Forms.Button
    Friend WithEvents txtInput As System.Windows.Forms.TextBox
    Friend WithEvents lblOutput As System.Windows.Forms.Label
    Friend WithEvents txtArray As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
