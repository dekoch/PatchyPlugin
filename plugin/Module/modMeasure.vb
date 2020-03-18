Module modMeasure

    Dim Fluke45(0 To 9) As clsFluke45
    Dim Multical(0 To 9) As clsMultical
    Dim Agilent34450(0 To 9) As clsAgilent34450

    Dim strSub As String

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        Select Case True

            '#measX_bind_ 
            Case name.StartsWith("#meas")

                strSub = name.Substring(5, 1)

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

                        If name.Length >= 10 Then
                            For i As Integer = 7 To 10
                                strBindName.Append(name.Substring(i, 1))
                            Next
                        End If

                        If strBindName.ToString = "bind" Then

                            strBindName.Remove(0, strBindName.Length)
                            For i As Integer = 12 To name.Length - 1
                                strBindName.Append(name.Substring(i, 1))
                            Next

                            If arrMeasurePointer(Convert.ToInt32(strSub)) <> strBindName.ToString Then
                                arrMeasurePointer(Convert.ToInt32(strSub)) = strBindName.ToString

                                If bolDebug = True Then
                                    WriteLog("bind measuring device " & strBindName.ToString)
                                End If

                                Select Case strBindName.ToString

                                    Case "fluke45"
                                        Fluke45(Convert.ToInt32(strSub)) = New clsFluke45
                                        Delay(0.1)
                                        Fluke45(Convert.ToInt32(strSub)).intNr = Convert.ToInt32(strSub)                                        

                                    Case "multical"
                                        Multical(Convert.ToInt32(strSub)) = New clsMultical
                                        Delay(0.1)
                                        Multical(Convert.ToInt32(strSub)).intNr = Convert.ToInt32(strSub)

                                    Case "agilent34450"
                                        Agilent34450(Convert.ToInt32(strSub)) = New clsAgilent34450
                                        Delay(0.1)
                                        Agilent34450(Convert.ToInt32(strSub)).intNr = Convert.ToInt32(strSub)

                                End Select

                                '20140506 neu:
                                arrMeasureID(Convert.ToInt32(strSub)) = ""

                                Delay(0.4)

                            End If

                        Else

                            Select Case arrMeasurePointer(Convert.ToInt32(strSub))

                                Case "fluke45"
                                    Fluke45(Convert.ToInt32(strSub)).SetValue(name.Replace("#meas" & Convert.ToInt32(strSub) & "_", ""), str)

                                Case "multical"
                                    Multical(Convert.ToInt32(strSub)).SetValue(name.Replace("#meas" & Convert.ToInt32(strSub) & "_", ""), str)

                                Case "agilent34450"
                                    Agilent34450(Convert.ToInt32(strSub)).SetValue(name.Replace("#meas" & Convert.ToInt32(strSub) & "_", ""), str)


                                Case Else
                                    WriteLog("please bind a measuring device")
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
