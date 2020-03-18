Imports System.IO

Module modLog

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        'Select Case name

        'Case "#log_write"

        If name.StartsWith("#log_write") = True Then

            Dim strTemp As String = ""
            Dim strLog As String = ""
            Dim strLogFile As String = ""

            strTemp = vbCrLf & vbCrLf & "##################################" & vbCrLf & vbCrLf

            strLog = strLog & strTemp
            strTemp = ""

            ' strTemp = strTemp & "meas0" & vbTab & "source0:V" & vbTab & "source0:I" & vbCrLf

            For t As Integer = 0 To 9
                If arrMeasurePointer(t) <> "" Then

                    '20140506: strTemp = strTemp & "meas" & t & vbTab & vbTab
                    strTemp = strTemp & "meas" & t & vbTab & arrMeasureID(t) & vbTab
                End If
            Next

            For t As Integer = 0 To 9
                If arrSourcePointer(t) <> "" Then

                    '20140506: strTemp = strTemp & "source" & t & vbTab & vbTab
                    strTemp = strTemp & "source" & t & vbTab & arrSourceID(t) & vbTab
                End If
            Next

            strTemp = strTemp & vbCrLf

            strLogFile = strLogFile & strTemp
            strLog = strLog & strTemp
            strTemp = ""

            For i As Integer = 0 To intMeasurePoint
                For t As Integer = 0 To 9
                    If arrMeasurePointer(t) <> "" Then
                        If arrMeasureValue(t, i) <> "" Then

                            strTemp = strTemp & arrMeasureValue(t, i) & vbTab
                        Else

                            strTemp = strTemp & vbTab & vbTab
                        End If

                        arrMeasureValue(t, i) = ""
                    End If
                Next

                For t As Integer = 0 To 9
                    If arrSourcePointer(t) <> "" Then
                        If arrSourceValue(t, i) <> "" Then

                            strTemp = strTemp & arrSourceValue(t, i) & vbTab
                        Else

                            strTemp = strTemp & vbTab & vbTab
                        End If

                        arrSourceValue(t, i) = ""
                    End If
                Next

                strTemp = strTemp & vbCrLf

                'strTemp = strTemp & arrMeasureValue(0, i) & vbTab & arrSourceValueV(0, i) & vbTab & arrSourceValueI(0, i) & vbCrLf

                'arrMeasureValue(0, i) = ""
                'arrSourceValueV(0, i) = ""
                'arrSourceValueI(0, i) = ""

            Next

            strLogFile = strLogFile & strTemp

            strTemp = strTemp & vbCrLf & "##################################" & vbCrLf

            strLog = strLog & strTemp
            strTemp = ""

            If name.StartsWith("#log_write-file") = True Then

                CreateDirectory(strLogPath)

                If DirectoryExists(strLogPath) = True Then

                    Dim strCSV As String = ""

                    If str <> "" Then
                        strCSV = str
                    Else
                        strCSV = GetTimestamp(Date.UtcNow) & Date.UtcNow.Millisecond & "_" & strProfileName
                    End If

                    strCSV = strCSV & ".csv"

                    WriteLog("write " & strLogPath & strCSV)

                    Try

                        Dim fs As FileStream
                        fs = New FileStream(strLogPath & strCSV, FileMode.Create, FileAccess.Write)
                        ' --- Stream öffnen
                        Dim w As StreamWriter = New StreamWriter(fs)
                        ' --- Anfügen am Ende
                        'w.BaseStream.Seek(0, SeekOrigin.End)
                        ' --- Zeilen schreiben
                        w.Write(strLogFile)

                        w.Close()
                        fs.Close()

                        fs.Dispose()
                        w.Dispose()

                    Catch ex As Exception
                        WriteLog("error@plugin: failed to write " & strLogPath & strCSV)
                    End Try

                End If

                'MsgBox("""" & strLogFile & """")
            End If

            WriteLog(strLog)
            intMeasurePoint = 0
            intState = 0

        End If

        Select Case name

            Case "#log_new_point"
                intMeasurePoint += 1
                intState = 0

        End Select

    End Sub

End Module
