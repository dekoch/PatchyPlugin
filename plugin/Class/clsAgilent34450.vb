Imports System.Globalization

Class clsAgilent34450

    Public WithEvents VISA As New clsVISA
    Dim meas As clsMeasure

    Dim bolLoaded As Boolean = False
    Dim strCOMState As String
    Dim WithEvents timReadValue As Timer
    Dim strCommand As String = ""
    Public intNr As Integer = 0
    Dim strOldRange As String = ""
    Dim strTemp0 As String = ""
    Dim dblTemp0 As Double
    Dim strMeasRange As String = "auto"
    Dim strOldMeasRange As String = ""

    Public Sub SetValue(ByVal name As String, ByVal str As String)

        If bolLoaded = False Then

            meas = New clsMeasure

            timReadValue = New Timer
            timReadValue.Interval = 100

            bolLoaded = True
        End If

        strCommand = name

        If bolManualControl = True Then
            VISA.SetValue("reset", "")
        End If

        '"#MEAS1_Start",
        '   "#MEAS1_Stop",
        '   "#MEAS1_COM-Port",
        '   "#MEAS1_COM-Baud",
        '   "#MEAS1_SET_V_AC",
        '   "#MEAS1_SET_V_DC",
        '   "#MEAS1_SET_A_AC",
        '   "#MEAS1_SET_A_DC",
        '   "#MEAS1_SET_FREQ",
        '   "#MEAS1_GET_VALUE"

        'CONFigure:VOLTage:AC auto,3.00E-5 = fast
        'CONFigure:VOLTage:AC auto,2.00E-5 = medium
        'CONFigure:VOLTage:AC auto,1.00E-5 = slow

        If name = "visa-alias" Then
            VISA.Init()
        End If

        '20140505 bereich kann nun ueber variable 
        'eingestellt werden
        If str <> "" Then
            strMeasRange = str
        Else
            strMeasRange = "auto"
        End If

        If strMeasRange <> strOldMeasRange Then
            strOldRange = ""
        End If

        Select Case name

            Case "start"
                strOldRange = ""
                VISA.OpenPort("", 2000)
                timReadValue = New Timer
                timReadValue.Interval = 100
                timReadValue.Start()

            Case "stop"
                strOldRange = ""
                VISA.ClosePort()
                bolLoaded = False

            Case "visa-alias"
                strOldRange = ""
                Send("port", str)

            Case "id"
                Send("read", "True")
                Send("send", "*IDN?")
                GetValue(VISA.GetValue("receive"))

            Case "set_v_ac"
                If strOldRange <> name Then
                    Send("read", "False")
                    Send("send", "*CLS")
                    Send("send", "CONFigure:VOLTage:AC " & strMeasRange & ",2.00E-5")
                    Send("send", "TRIG:SOUR IMM")
                    Send("send", "INIT")
                    Send("send", "FETC?")
                    Delay(5.0)
                    strOldRange = name
                    strOldMeasRange = strMeasRange
                End If

            Case "set_v_dc"
                If strOldRange <> name Then
                    Send("read", "False")
                    Send("send", "*CLS")
                    Send("send", "CONFigure:VOLTage:DC " & strMeasRange & ",2.00E-5")
                    Send("send", "TRIG:SOUR IMM")
                    Send("send", "INIT")
                    Send("send", "FETC?")
                    Delay(5.0)
                    strOldRange = name
                    strOldMeasRange = strMeasRange
                End If

            Case "set_a_ac"
                If strOldRange <> name Then
                    Send("read", "False")
                    Send("send", "*CLS")
                    Send("send", "CONFigure:CURRent:AC " & strMeasRange & ",2.00E-5")
                    Send("send", "TRIG:SOUR IMM")
                    Send("send", "INIT")
                    Send("send", "FETC?")
                    Delay(5.0)
                    strOldRange = name
                    strOldMeasRange = strMeasRange
                End If

            Case "set_a_dc"
                If strOldRange <> name Then
                    Send("read", "False")
                    Send("send", "*CLS")
                    Send("send", "CONFigure:CURRent:DC " & strMeasRange & ",2.00E-5")
                    Send("send", "TRIG:SOUR IMM")
                    Send("send", "INIT")
                    Send("send", "FETC?")
                    Delay(5.0)
                    strOldRange = name
                    strOldMeasRange = strMeasRange
                End If

                'Case "set_a_tdc"
                '    Send("send", "AACDC")

            Case "set_freq"
                If strOldRange <> name Then
                    Send("read", "False")
                    Send("send", "*CLS")
                    Send("send", "CONFigure:FREQuency")
                    Delay(5.0)
                    strOldRange = name
                End If

            Case "meas_value"
                GetValue(meas.Pick)

            Case "terminal"
                'Send("read", "False")
                'Send("send", "*CLS")
                Send("read", "True")
                Send("send", str)
                GetValue(VISA.GetValue("receive"))

            Case Else
                WriteLog("not supported command " & name)
                intState = -11

        End Select


        If intState > 0 Then

            intState = 0
        End If


    End Sub

    Private Sub Send(ByVal name As String, ByVal str As String)

        VISA.SetValue(name, str)

    End Sub

    Private Sub VISA_DebugLog(ByVal str As String) Handles VISA.DebugLog
        If bolDebug = True Then
            WriteLog(str)
        End If
    End Sub

    Private Sub VISA_ErrorLog(ByVal str As String) Handles VISA.ErrorLog
        WriteLog(str)
        strCOMState = VISA.GetValue("state")
        If strCOMState < 0 Then
            intState = strCOMState
        End If
    End Sub

    Private Sub GetValue(ByVal str As String)

        strMeasureRx = str

        strMeasureRx = strMeasureRx.Replace(vbCrLf, "")
        strMeasureRx = strMeasureRx.Replace(vbCr, "")
        strMeasureRx = strMeasureRx.Replace(vbLf, "")

        Select Case strCommand

            Case "id"
                arrMeasureID(intNr) = strMeasureRx
                WriteLog("ID: " & strMeasureRx)

            Case "meas_value"
                'WriteLog(intState & "-" & strOldRange)
                arrMeasureValue(intNr, intMeasurePoint) = strMeasureRx.Replace(".", ",") & vbTab

            Case Else
                WriteLog("get: " & strMeasureRx)

        End Select
    End Sub

   

    Private Sub timReadValue_Tick(sender As Object, e As EventArgs) Handles timReadValue.Tick
        timReadValue.Stop()

        If intState >= 0 And strOldRange <> "" Then

            Send("read", "False") '
            Send("send", "*CLS") '
            Send("read", "True")
            Send("send", "READ?")

            'messwert von visa holen
            strTemp0 = VISA.GetValue("receive")

            'bereinigen
            strTemp0 = strTemp0.Replace(vbCrLf, "")
            strTemp0 = strTemp0.Replace(vbCr, "")
            strTemp0 = strTemp0.Replace(vbLf, "")

            'exponentialschreibweise in double konvertieren
            'achtung: +1.000E+0 = 1000
            'punkte werden auf deutschen systemen vernachlaessigt
            'deshalb mit Culture en-US
            dblTemp0 = Double.Parse(strTemp0, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"))

            'WriteLog(dblTemp0)

            'mittelwertbildung
            If "set_freq" = strOldRange Then
                meas.Feed(dblTemp0, 1)
            Else
                meas.Feed(dblTemp0, 7)
            End If

        End If

        timReadValue.Start()
    End Sub
End Class
