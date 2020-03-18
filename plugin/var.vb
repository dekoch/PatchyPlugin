Module var

    Public strPluginName As String = "sofie"
    Public strPluginVersion As String = "2.472"
    Public arrCommands() As String _
        = {"#wait",
            "#check_hostname",
            "#check_domain",
            "#check_os",
            "#read_hostname",
            "#read_domain",
            "#read_os",
            "#autorun_add",
            "#autorun_remove",
            "#reboot",
            "#shutdown",
            "#user_info",
            "#profile_read-only",
            "#IO0_BIND_AVR-NET-IO",
            "#IO0_IP",
            "#IO0_PORT",
            "#IO0_START",
            "#IO0_STOP",
            "#IO0_GET_IP",
            "#IO0_GET_DI",
            "#IO0_GET_AI",
            "#IO0_SET_DO",
            "#IO0_WAIT_DI",
            "#plugin_changelog"}

    '"#SOURCE0_BIND_BUER2",
    Public arrCommandsBDG() As String _
       = {"#wait",
          "#check_hostname",
          "#check_domain",
          "#check_os",
          "#read_hostname",
          "#read_domain",
          "#read_os",
          "#autorun_add",
          "#autorun_remove",
          "#reboot",
          "#shutdown",
          "#user_info",
          "#profile_read-only",
          "#SOURCE0_BIND_HBS",
          "#SOURCE0_START",
          "#SOURCE0_STOP",
          "#SOURCE0_ID",
          "#SOURCE0_COM-PORT",
          "#SOURCE0_COM-BAUD",
          "#SOURCE0_SET_V_AC",
          "#SOURCE0_SET_V_DC",
          "#SOURCE0_SET_V_TDC",
          "#SOURCE0_SET_FREQ",
          "#SOURCE0_SET_I",
          "#SOURCE0_SET_I_OPTION",
          "#SOURCE0_SET_TDC",
          "#SOURCE0_SET_OUTPUT",
          "#SOURCE0_SET_OSCILLATOR",
          "#SOURCE0_MEAS_V",
          "#SOURCE0_MEAS_I",
          "#SOURCE0_MEAS_I_PEAK",
          "#SOURCE0_MEAS_CREST",
          "#SOURCE0_MEAS_POWER",
          "#SOURCE0_MEAS_VA",
          "#SOURCE0_MEAS_W",
          "#SOURCE0_MEAS_FREQ",
          "#SOURCE0_GET_OPTIONS",
          "#SOURCE0_Terminal",
          "#MEAS0_BIND_FLUKE45",
          "#MEAS0_BIND_AGILENT34450",
          "#MEAS0_BIND_MULTICAL",
          "#MEAS0_START",
          "#MEAS0_STOP",
          "#MEAS0_ID",
          "#MEAS0_COM-PORT",
          "#MEAS0_VISA-ALIAS",
          "#MEAS0_COM-BAUD",
          "#MEAS0_SPEED",
          "#MEAS0_SET_V_AC",
          "#MEAS0_SET_V_DC",
          "#MEAS0_SET_V_TDC",
          "#MEAS0_SET_A_AC",
          "#MEAS0_SET_A_DC",
          "#MEAS0_SET_A_TDC",
          "#MEAS0_SET_FREQ",
          "#MEAS0_MEAS_VALUE",
          "#MEAS0_Terminal",
          "#IO0_BIND_AVR-NET-IO",
          "#IO0_IP",
          "#IO0_PORT",
          "#IO0_START",
          "#IO0_STOP",
          "#IO0_GET_IP",
          "#IO0_GET_DI",
          "#IO0_GET_AI",
          "#IO0_SET_DO",
          "#IO0_WAIT_DI",
          "#LOG_NEW_POINT",
          "#LOG_WRITE",
          "#LOG_WRITE-FILE",
          "#plugin_changelog"}


#Region "global"

    Public strLog As String = ""
    Public bolLog As Boolean = False

    'Public strCurrentPath As String = ""
    'Public strSettingsPath As String = "plugin.xml"
    'Public strLogPath As String = ""
    Public strLang As String = "en"
    Public strCompany As String = ""

    Public bolAbortAll As Boolean = False

    ' intState: -2=beenden angefordert -1=fehler 0=bereit 1=wird ausgefuehrt
    Public intState As Integer = 0

    Public bolDebug As Boolean = False

    Public arrMeasurePointer(0 To 9) As String
    Public arrSourcePointer(0 To 9) As String

    Public intMeasurePoint As Integer = 0
    Public arrMeasureValue(0 To 9, 0 To 200) As String
    Public arrMeasureID(0 To 9) As String

    Public arrSourceValue(0 To 9, 0 To 200) As String
    Public arrSourceID(0 To 9) As String

    'public arrSourceValueV(0 To 9, 0 To 200) As String
    'public arrSourceValueI(0 To 9, 0 To 200) As String

    Public strSourceRx As String = ""
    Public strMeasureRx As String = ""
    Public strIORx As String = ""

    Public bolManualControl As Boolean = False

    Public strProfileName As String


#End Region


#Region "paths"
    Public strCurrentPath As String = ""
    Public strSettingsPath As String = "plugin.xml"
    Public strProfilePath As String = ""
    Public strSystemPath As String = ""
    Public strTemplatePath As String = ""
    Public strLogPath As String = ""
    Public strLangPath As String = ""
    Public strKeyPath As String = ""
#End Region


End Module
