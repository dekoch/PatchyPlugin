Imports System.Net.NetworkInformation

Public Class main

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        intState = 1

        name = name.ToLower

        Dim int As New System.Text.StringBuilder
        For Each c As Char In str
            If Char.IsNumber(c) Then int.Append(c)
        Next

        Select Case True

            Case name.StartsWith("#wait")
                If CheckString(str) = True Then
                    WriteLog("wait " & int.ToString & " seconds")
                    Delay(int.ToString)
                    intState = 0
                End If

            Case name.StartsWith("#autorun_add")
                Autostart(True, str)
                WriteLog(System.Reflection.Assembly.GetEntryAssembly.Location & " " & str & " added to registry")
                intState = 0

            Case name.StartsWith("#autorun_remove")
                Autostart(False, str)
                WriteLog(System.Reflection.Assembly.GetEntryAssembly.Location & " removed from registry")
                intState = 0

            Case name.StartsWith("#reboot")
                If CheckString(str) = True Then
                    WriteLog("reboot system in " & int.ToString & " seconds")
                    intState = -2
                    Shell("shutdown -r -t " & int.ToString)
                End If

            Case name.StartsWith("#shutdown")
                If CheckString(str) = True Then
                    WriteLog("shutdown system in " & int.ToString & " seconds")
                    intState = -2
                    Shell("shutdown -s -t " & int.ToString)
                End If

            Case name.StartsWith("#check_hostname")
                If CheckString(str) = True Then
                    Dim ipproperties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
                    If ipproperties.HostName = str Then
                        WriteLog("hostname is " & ipproperties.HostName)
                        intState = 0
                    Else
                        WriteLog("wrong hostname, hostname is " & ipproperties.HostName)
                        intState = -2
                    End If
                    ipproperties = Nothing
                End If

            Case name.StartsWith("#check_domain")
                If CheckString(str) = True Then
                    Dim ipproperties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
                    If ipproperties.DomainName = str Then
                        WriteLog("domain is " & ipproperties.DomainName)
                        intState = 0
                    Else
                        WriteLog("wrong domain, domain is " & ipproperties.DomainName)
                        intState = -2
                    End If
                    ipproperties = Nothing
                End If

            Case name.StartsWith("#check_os")
                If CheckString(str) = True Then
                    Dim os As String = Environment.OSVersion.VersionString
                    If os = str Then
                        WriteLog("OS is " & os)
                        intState = 0
                    Else
                        WriteLog("wrong OS, OS is " & os)
                        intState = -2
                    End If
                    os = Nothing
                End If

            Case name.StartsWith("#read_hostname")
                Dim ipproperties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
                WriteLog("hostname is " & ipproperties.HostName)
                intState = 0
                ipproperties = Nothing

            Case name.StartsWith("#read_domain")
                Dim ipproperties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
                WriteLog("domain is " & ipproperties.DomainName)
                intState = 0
                ipproperties = Nothing

            Case name.StartsWith("#read_os")
                WriteLog("OS is " & Environment.OSVersion.VersionString)
                intState = 0

            Case name.StartsWith("#user_info")
                intState = 1
                MsgBox(str)
                intState = 0

            Case name.StartsWith("#source")
                If strCompany = "BDG GmbH" Then
                    modSource.SetValue(name, str)
                Else
                    WriteLog("wrong command: " & name)
                    intState = -2
                End If

            Case name.StartsWith("#meas")
                If strCompany = "BDG GmbH" Then
                    modMeasure.SetValue(name, str)
                Else
                    WriteLog("wrong command: " & name)
                    intState = -2
                End If

            Case name.StartsWith("#io")
                'If strCompany = "BDG GmbH" Then
                modIO.SetValue(name, str)
                'Else
                '    WriteLog("wrong command: " & name)
                '    intState = -2
                'End If

            Case name.StartsWith("#log")
                If strCompany = "BDG GmbH" Then
                    modLog.SetValue(name, str)
                Else
                    WriteLog("wrong command: " & name)
                    intState = -2
                End If

            Case name.StartsWith("#plugin_crash")
                Dim dummy As Boolean
                Dim arr(1) As Boolean

                For i As Integer = 0 To 2
                    dummy = arr(i)
                Next

            Case name.StartsWith("#plugin_changelog")
                WriteLog(My.Resources.changelog.ToString)
                intState = 0

            Case name.StartsWith("company")
                strCompany = str
                If strCompany = "BDG GmbH" Then
                    If IO.FileExists(strCurrentPath & "Ivi.Visa.Interop.dll", False) = False Then

                        'kopiere datei aus exe
                        IO.CreateFile(My.Resources.Ivi_Visa_Interop_dll, "Ivi.Visa.Interop.dll", strCurrentPath)

                    End If
                End If

            Case name.StartsWith("currentpath")
                strCurrentPath = str

            Case name.StartsWith("settingspath")
                strSettingsPath = str

            Case name.StartsWith("profilepath")
                strProfilePath = str

            Case name.StartsWith("systempath")
                strSystemPath = str

            Case name.StartsWith("templatepath")
                strTemplatePath = str

            Case name.StartsWith("logpath")
                strLogPath = str

            Case name.StartsWith("lang")
                strLang = str

            Case name.StartsWith("langpath")
                strLangPath = str

            Case name.StartsWith("keypath")
                strKeyPath = str

            Case name.StartsWith("profilename")
                strProfileName = str

            Case name.StartsWith("debug")
                If str = "True" Then
                    bolDebug = True
                Else
                    bolDebug = False
                End If

            Case Else
                WriteLog("wrong command: " & name)
                intState = -2

        End Select

    End Sub


    Public Function GetValue(ByVal name As String, Optional ByVal str As String = "")

        Select Case name

            Case "Name"
                strCurrentPath = My.Application.Info.DirectoryPath
                If strCurrentPath.EndsWith("\") = False Then strCurrentPath = strCurrentPath & "\"

                Return strPluginName

            Case "Version"
                Return strPluginVersion

            Case "Commands"
                Select Case strCompany

                    Case "BDG GmbH"
                        Return arrCommandsBDG

                    Case Else
                        Return arrCommands

                End Select

            Case "State"
                Return intState

            Case "Log"
                bolLog = False
                Return strLog

            Case "Changelog"
                Return My.Resources.changelog.ToString

            Case Else
                Return -1

        End Select

    End Function

    Private Function Autostart(ByVal AutostartEnable As Boolean, ByVal Arguments As String)

        Dim key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        If AutostartEnable = True Then
            key.SetValue(My.Application.Info.AssemblyName, """" & System.Reflection.Assembly.GetEntryAssembly.Location & """" & " " & Arguments)
        Else
            key.DeleteValue(My.Application.Info.AssemblyName, False)
        End If
        key.Close()
        Return AutostartEnable

    End Function

    Private Function CheckString(ByVal str As String) As Boolean
        If str.Length > 0 Then
            Return True
        Else
            WriteLog("error: argument is empty")
            intState = -1
            Return False
        End If
    End Function



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        SetValue(TextBox1.Text, TextBox2.Text)

    End Sub

    Private Sub main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        bolAbortAll = True
        Application.Exit()

    End Sub


End Class
