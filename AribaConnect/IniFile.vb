Imports System.Text

Module IniFile

    Private Declare Auto Function GetPrivateProfileString Lib "kernel32" (ByVal lpAppName As String, _
           ByVal lpKeyName As String, _
           ByVal lpDefault As String, _
           ByVal lpReturnedString As StringBuilder, _
           ByVal nSize As Integer, _
           ByVal lpFileName As String) As Integer

    Public Function getIniParameter(ByVal Seccion As String, ByVal Clave As String, ByVal ValorPorDefecto As String) As String
        Dim sb As StringBuilder
        sb = New StringBuilder(500)
        GetPrivateProfileString(Seccion, Clave, ValorPorDefecto, sb, sb.Capacity, Application.StartupPath & "\Config.ini")
        If sb.ToString.Trim <> "" Then
            Return sb.ToString.Trim
        Else
            Return ValorPorDefecto
        End If
    End Function


End Module
