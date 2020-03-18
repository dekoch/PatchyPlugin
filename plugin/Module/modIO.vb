Module modIO

    Public AVRNETIO(0 To 9) As clsAVRNETIO

    Dim strSub As String

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        Select Case True

            '#ioX_bind_ 
            Case name.StartsWith("#io")

                strSub = name.Substring(3, 1)

                Dim strTxt As New System.Text.StringBuilder
                Dim intTxt As New System.Text.StringBuilder

                For Each c As Char In strSub
                    If Char.IsNumber(c) Then intTxt.Append(c)
                    If Not Char.IsNumber(c) Then strTxt.Append(c)
                Next

                If strTxt.Length > 0 Or intTxt.Length = 0 Then
                    'keine nummer
                    WriteLog("wrong format: " & strSub & " --> 0...9")
                Else

                    If Convert.ToInt32(strSub) <= 9 Then

                        Dim strBindName As New System.Text.StringBuilder

                        '#ioX_bind_
                        '"bind" aus dem sting holen
                        If name.Length >= 8 Then
                            For i As Integer = 5 To 8
                                strBindName.Append(name.Substring(i, 1))
                            Next
                        End If

                        If strBindName.ToString = "bind" Then

                            strBindName.Remove(0, strBindName.Length)
                            For i As Integer = 10 To name.Length - 1
                                strBindName.Append(name.Substring(i, 1))
                            Next

                            If arrSourcePointer(Convert.ToInt32(strSub)) <> strBindName.ToString Then
                                arrSourcePointer(Convert.ToInt32(strSub)) = strBindName.ToString

                                If bolDebug = True Then
                                    WriteLog("bind source " & strBindName.ToString)
                                End If

                                Select Case strBindName.ToString

                                    Case "avr-net-io"
                                        AVRNETIO(Convert.ToInt32(strSub)) = New clsAVRNETIO
                                        Delay(0.1)
                                        AVRNETIO(Convert.ToInt32(strSub)).intNr = Convert.ToInt32(strSub)

                                End Select

                                Delay(0.4)

                                'For i As Integer = 0 To UBound(HBS(Convert.ToInt32(strSub)).arrValueV) - 1
                                '    HBS(Convert.ToInt32(strSub)).arrValueV(i) = ""
                                'Next

                            End If

                        Else

                            Select Case arrSourcePointer(Convert.ToInt32(strSub))

                                Case "avr-net-io"
                                    AVRNETIO(Convert.ToInt32(strSub)).SetValue(name.Replace("#io" & Convert.ToInt32(strSub) & "_", ""), str)

                                Case Else
                                    WriteLog("please bind a source")
                                    intState = -10

                            End Select

                        End If

                    End If

                End If

        End Select

        If intState > 0 Then
            intState = 0
        End If

    End Sub

End Module
