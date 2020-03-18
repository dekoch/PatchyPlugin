Imports System.Globalization

Class frmMeasure

    Dim meas As clsMeasure

    Dim strTemp0 As String = "+1.000E+2"
    Dim strEx As String = ""

    Private Sub frmMeasure_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        meas = New clsMeasure

    End Sub

    Private Sub btnFeed_Click(sender As Object, e As EventArgs) Handles btnFeed.Click

        Dim dbl As Double



        dbl = Double.Parse(txtInput.Text, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"))


        meas.Feed(dbl, 3)

        lblOutput.Text = meas.Pick

        txtArray.Text = ""

        For i As Integer = 0 To UBound(meas.arrSensorInput0)

            If meas.arrSensorInput0.ToString.Length > 0 Then

                txtArray.AppendText(meas.arrSensorInput0(i) & vbCrLf)

            End If

        Next

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        strTemp0 = txtInput.Text

        Dim bolNumber0 As Boolean = False
        Dim bolEx As Boolean = False
        Dim bolUseSign As Boolean = False
        Dim strSign As String = ""

        Dim strNumber0 As New System.Text.StringBuilder
        Dim strNumber1 As New System.Text.StringBuilder
        Dim strEx As New System.Text.StringBuilder

        For Each c As Char In strTemp0

            If bolNumber0 = False Then

                If c = "+" Or c = "-" Then

                    strSign = c
                    'strNumber0.Append(c)
                    bolUseSign = True
                Else

                    If Char.IsNumber(c) Then
                        strNumber0.Append(c)
                    Else
                        bolNumber0 = True
                    End If
                End If
            Else

                If bolEx = False Then

                    If Char.IsNumber(c) Then
                        strNumber1.Append(c)
                    Else
                        bolEx = True
                    End If
                Else

                    If Char.IsNumber(c) Or Char.IsLetterOrDigit("43") Or Char.IsLetterOrDigit("45") Then
                        strEx.Append(c)
                    End If
                End If
            End If
        Next

        'MsgBox(strNumber0.ToString)
        'MsgBox(strNumber1.ToString)
        'MsgBox(strEx.ToString)

        Dim strOutput As New System.Text.StringBuilder

        Dim intCharCount As Integer = 0
        Dim intOffset As Integer = 1

        'If bolUseSign = True Then
        '    intOffset += 1
        'End If

        If strEx.ToString.StartsWith("+") = True Then

            For Each c As Char In strNumber0.ToString & strNumber1.ToString

                If intCharCount = Convert.ToInt32(strEx.ToString) + intOffset Then
                    strOutput.Append(",")
                    strOutput.Append(c)
                Else
                    strOutput.Append(c)
                End If

                intCharCount += 1
            Next

        ElseIf strEx.ToString.StartsWith("-") = True Then

            'MsgBox(strEx.ToString - strNumber0.ToString.Length - intOffset)

            'MsgBox("""" & strNumber0.ToString & """")

            ''For i As Integer = 0 To strEx.ToString - strNumber0.ToString.Length - 1
            'If strNumber0.ToString.Length = 1 Then

            '    strOutput.Append("0")


            'End If


            For Each c As Char In strNumber0.ToString & strNumber1.ToString

                If intCharCount = Convert.ToInt32(strEx.ToString) + intOffset Then

                    If strOutput.ToString = "" Then

                        Do


                            strOutput.Append("0")
                            intCharCount += 1

                        Loop Until intCharCount = Convert.ToInt32(strEx.ToString) + intOffset

                    End If

                    strOutput.Append(",")
                    strOutput.Append(c)
                Else
                    strOutput.Append(c)
                End If

                intCharCount += 1
            Next


        End If

        strTemp0 = strSign & strOutput.ToString

        Me.Text = strTemp0

    End Sub
End Class