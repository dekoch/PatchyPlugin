Module modSource

    Public HBS(0 To 9) As clsHBS
    Public BUER2(0 To 9) As clsBUER2
    Public AVRNETIO(0 To 9) As clsAVRNETIO

    Dim strSub As String

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        Select Case True

            '#sourceX_bind_ 
            Case name.StartsWith("#source")

                strSub = name.Substring(7, 1)

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

                        '#sourceX_bind_XYZ
                        '"bind" aus dem sting holen
                        If name.Length >= 12 Then
                            For i As Integer = 9 To 12
                                strBindName.Append(name.Substring(i, 1))
                            Next
                        End If

                        If strBindName.ToString = "bind" Then

                            strBindName.Remove(0, strBindName.Length)
                            For i As Integer = 14 To name.Length - 1
                                strBindName.Append(name.Substring(i, 1))
                            Next

                            If arrSourcePointer(Convert.ToInt32(strSub)) <> strBindName.ToString Then
                                arrSourcePointer(Convert.ToInt32(strSub)) = strBindName.ToString

                                If bolDebug = True Then
                                    WriteLog("bind source " & strBindName.ToString)
                                End If

                                Select Case strBindName.ToString

                                    Case "hbs"
                                        HBS(Convert.ToInt32(strSub)) = New clsHBS
                                        Delay(0.1)
                                        HBS(Convert.ToInt32(strSub)).intNr = Convert.ToInt32(strSub)

                                    Case "buer2"
                                        BUER2(Convert.ToInt32(strSub)) = New clsBUER2
                                        Delay(0.1)
                                        BUER2(Convert.ToInt32(strSub)).intNr = Convert.ToInt32(strSub)

                                        'Case "avr-net-io"
                                        '    AVRNETIO(Convert.ToInt32(strSub)) = New clsAVRNETIO
                                        '    Delay(0.1)
                                        '    AVRNETIO(Convert.ToInt32(strSub)).intNr = Convert.ToInt32(strSub)

                                End Select

                                '20140506 neu:
                                arrSourceID(Convert.ToInt32(strSub)) = ""

                                Delay(0.4)

                                'For i As Integer = 0 To UBound(HBS(Convert.ToInt32(strSub)).arrValueV) - 1
                                '    HBS(Convert.ToInt32(strSub)).arrValueV(i) = ""
                                'Next

                            End If

                        Else

                            Select Case arrSourcePointer(Convert.ToInt32(strSub))

                                Case "hbs"
                                    HBS(Convert.ToInt32(strSub)).SetValue(name.Replace("#source" & Convert.ToInt32(strSub) & "_", ""), str)

                                Case "buer2"
                                    BUER2(Convert.ToInt32(strSub)).SetValue(name.Replace("#source" & Convert.ToInt32(strSub) & "_", ""), str)

                                    'Case "avr-net-io"
                                    '    AVRNETIO(Convert.ToInt32(strSub)).SetValue(name.Replace("#source" & Convert.ToInt32(strSub) & "_", ""), str)


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
