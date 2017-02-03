Option Explicit On
Option Strict On

Imports AribaSQL

Public Class Form1

    Private CSV_Path As String = ""


    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        Me.CSV_Path = IniFile.getIniParameter("PATHS", "CSV_PATH", "")
        If Me.CSV_Path = "" Then
            Exit Sub
        Else
            Me.lblCSVPath.Text = CSV_Path
        End If


    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim ADAO As New AribaDAO

        Dim newId As Integer = ADAO.insertCustomer("123456", "ENRIQUE", "domain", "supplierID", "Y", "ftpserver", "user", "pass", "path", "filename", "C")

        If newId > 0 Then
            Dim strSql As String = "SELECT * FROM CustomerInfo WHERE id=" & newId.ToString
            Dim dt As New DataTable
            Dim res As Integer = ADAO.SimpleSelect(strSql, dt)

        End If

    End Sub

    Private Sub lblCSVPath_Click(sender As System.Object, e As System.EventArgs) Handles lblCSVPath.Click

    End Sub
End Class
