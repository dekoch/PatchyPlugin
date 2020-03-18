Imports System.IO

Module IO

    Public Sub WriteLog(ByVal str As String)

        If bolDebug = True Then Debug.WriteLine("WriteLog " & str)

        '  Log schreiben "str"
        Do Until bolLog = False
            Delay(0.05)
        Loop

        bolLog = True
        strLog = str

        Do Until bolLog = False
            Delay(0.05)
        Loop

        strLog = ""

    End Sub

    Public Sub CreateFile(ByVal src As Byte(), ByVal name As String, ByVal dest As String)
        If bolDebug = True Then Debug.WriteLine("IO.CreateFile " & dest & name)

        DriveExists(dest & name)

        If bolAbortAll = False Then

            If src.Length > 0 And name.Length > 0 Then

                If Directory.Exists(dest) = False Then
                    CreateDirectory(dest)
                End If

                If Directory.Exists(dest) = True Then
                    ' wenn datei nicht da, dann kopieren
                    If File.Exists(dest & name) = False Then

                        File.WriteAllBytes(dest & name, src)
                        If bolDebug = True Then Debug.WriteLine("create: " & dest & name)

                        If File.Exists(dest & name) = True Then

                            ' Log schreiben "Datei erstellt"
                            'WriteLog("file created " & dest & name)

                        Else
                            MsgBox("failed to create " & dest & name, MsgBoxStyle.Information)
                            Application.Exit()
                        End If
                    End If
                End If
            End If
        Else
            ' Log schreiben "Datei nicht geladen"
            'WriteLog(udtLang.strFile & udtLang.strNot & udtLang.strCreated & dest & name)
        End If
    End Sub


    Public Sub CreateDirectory(ByVal name As String)
        If bolDebug = True Then Debug.WriteLine("IO.CreateDirectory " & name)
        ' wenn ordner nicht da ist, dann erstellen

        DriveExists(name)

        If bolAbortAll = False Then
            If name.Length > 0 Then

                If Directory.Exists(name) = False Then

                    Directory.CreateDirectory(name)
                    If bolDebug = True Then Debug.WriteLine("create: " & name)

                    If Directory.Exists(name) = True Then

                        ' Log schreiben "Ordner erstellt"
                        'WriteLog(udtLang.strDirectory & udtLang.strCreated & name)

                    Else
                        MsgBox("failed to create " & name & " directory", MsgBoxStyle.Information)
                        Application.Exit()
                    End If
                End If
            End If
        Else
            ' Log schreiben "Ordner nicht geladen"
            'WriteLog(udtLang.strDirectory & udtLang.strNot & udtLang.strCreated & name)
        End If
    End Sub


    Public Sub DriveExists(ByVal name As String)
        If bolAbortAll = False And name.Length Then

            Try
                If bolDebug = True Then Debug.WriteLine("IO.DriveExists vor dim " & name)

                Dim Drive As New System.IO.DriveInfo(strCurrentPath & name)

                If bolDebug = True Then Debug.WriteLine("IO.DriveExists nach dim " & name)

                If bolDebug = True Then Debug.WriteLine("IO.DriveExists Drive.IsReady " & Drive.IsReady)

            Catch ex As Exception
                If bolDebug = True Then Debug.WriteLine("IO.DriveExists not " & name)
                bolAbortAll = True
            End Try

        End If
    End Sub


    Public Function DirectoryExists(ByVal name As String)
        If bolDebug = True Then Debug.WriteLine("IO.DirectoryExists " & name)

        DriveExists(name)

        If bolAbortAll = False Then
            If name.Length > 0 Then

                If Directory.Exists(name) = True Then

                    Return True
                Else

                    ' Log schreiben "Ordner nicht gefunden"
                    'WriteLog(udtLang.strDirectory & udtLang.strNot & udtLang.strFound & name)

                    Return False

                End If
            Else

                Return False

            End If

        Else

            ' Log schreiben "Ordner nicht gefunden"
            'WriteLog(udtLang.strDirectory & udtLang.strNot & udtLang.strFound & name)

            Return False

        End If

    End Function



    Public Function FileExists(ByVal name As String, Optional ByVal enablelog As Boolean = True)
        If bolDebug = True Then Debug.WriteLine("IO.FileExists " & name)

        DriveExists(name)

        If bolAbortAll = False Then
            If name.Length > 0 Then
                If File.Exists(name) = True Then

                    Return True
                Else

                    If enablelog = True Then

                        ' Log schreiben "Datei nicht gefunden"
                        'WriteLog(udtLang.strFile & udtLang.strNot & udtLang.strFound & name)
                        If bolDebug = True Then Debug.WriteLine("Write Log IO.FileExists " & name)

                    End If

                    Return False

                End If
            Else
                Return False
            End If
        Else
            If enablelog = True Then

                ' Log schreiben "Datei nicht gefunden"
                'WriteLog(udtLang.strFile & udtLang.strNot & udtLang.strFound & name)

            End If

            Return False
        End If

    End Function


    Public Sub DeleteFile(ByVal name As String)
        If bolDebug = True Then Debug.WriteLine("IO.DeleteFile " & name)

        If FileExists(name) = True Then

            File.Delete(name)

            If File.Exists(name) = False Then
                ' Log schreiben "Datei geloescht"
                'WriteLog(udtLang.strFile & udtLang.strDeleted & name)

            End If

        End If

    End Sub



    'Public Function DriveExists(ByVal sDrive As String) As Boolean
    '    Try
    '        Dim Drive As New System.IO.DriveInfo(sDrive)
    '        Return Drive.IsReady
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function



    Public Function FileUpdate(ByVal oldDir As String, ByVal oldFile As String, ByVal oldVersion As String, ByVal newDir As String, ByVal newFile As String) As Boolean

        If IO.DirectoryExists(oldDir) = True Then

            If IO.DirectoryExists(newDir) = True Then

                Dim int As Integer = 0
                oldVersion = Replace(oldVersion, ".", "x")

                Do Until File.Exists(newDir & newFile & "_" & int & ".v" & oldVersion) = False

                    int += 1
                Loop

                Dim strNewName As String = newDir & newFile & "_" & int & ".v" & oldVersion

                Rename(oldDir & oldFile, strNewName)

                If File.Exists(strNewName) = True Then
                    ' Log schreiben "Datei umbenannt"
                    'WriteLog(udtLang.strFile & udtLang.strRenamed & oldDir & oldFile & " -> " & strNewName)

                    Return True

                Else

                    Return False

                End If

            End If

        End If

        Return False

    End Function




    ' Verzoegerung
    Sub Delay(ByVal dblSecs As Double)

        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
        Do Until Now > dblWaitTil
            Application.DoEvents()
            Threading.Thread.Sleep(1)
        Loop

        dblWaitTil = Nothing

    End Sub


    Public Sub errorXML()

        Select Case strLang

            Case "de"
                MsgBox("Fehler beim lesen/schreiben." & vbCrLf & "Bitte löschen sie die Datei settings.xml", MsgBoxStyle.Critical)

            Case "en"
                MsgBox("error reading/writing" & vbCrLf & "Please delete settings.xml", MsgBoxStyle.Critical)

            Case Else

                MsgBox("error reading/writing" & vbCrLf & "Please delete settings.xml", MsgBoxStyle.Critical)

        End Select

        Application.Exit()
    End Sub


    Public Function GetTimestamp(ByVal FromDateTime As DateTime) As Integer
        Dim Startdate As DateTime = #1/1/1970#
        Dim Spanne As TimeSpan

        Spanne = FromDateTime.Subtract(Startdate)
        Return CType(Math.Abs(Spanne.TotalSeconds()), Integer)
    End Function

End Module







'Module IO

'    Public Sub WriteLog(ByVal str As String)

'        If bolDebug = True Then Debug.WriteLine("WriteLog " & str)

'        '  Log schreiben "str"
'        Do Until bolLog = False
'            Delay(0.05)
'        Loop

'        bolLog = True
'        strLog = str

'        Do Until bolLog = False
'            Delay(0.05)
'        Loop

'        strLog = ""

'    End Sub

'    ' Verzoegerung
'    Sub Delay(ByVal dblSecs As Double)

'        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
'        Dim dblWaitTil As Date
'        Now.AddSeconds(OneSec)
'        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
'        Do Until Now > dblWaitTil
'            Application.DoEvents()
'            Threading.Thread.Sleep(1)
'        Loop

'        dblWaitTil = Nothing

'    End Sub



'End Module
