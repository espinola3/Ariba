Option Explicit On
Option Strict On

Imports AribaSQL

Public Class Form1
    Private Test_Path As String = ""
    Private _StringConnection As String = "Data Source=172.31.16.152;Initial Catalog=ARIBA;user id=usr_ecom_write;password=LunLN6yI;"
    Private CSV_PATH As String = ""
    Dim thread As System.Threading.Thread

    Public Function Recollect_Data(ByVal Test_Path As String) As Integer
        Dim loading As Integer
        Dim tiempoInicial As Date
        tiempoInicial = DateTime.Now
        loading = 0
        Dim res1 As Integer


        Using conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(Me._StringConnection)
            conn.Open()

            Using cm2 As SqlClient.SqlCommand = New SqlClient.SqlCommand("delete from [dbo].[PriceFile-29005207]", conn)
                Try
                    res1 = cm2.ExecuteNonQuery()

                Catch ex As Exception
                    res1 = -1
                End Try
            End Using

            Using MyReader As New Microsoft.VisualBasic.
                   FileIO.TextFieldParser(
                     Test_Path)
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(",")
                Dim ADAO1 As New AribaPRI
                Dim currentRow As String()
                Dim columna(19) As String
                Dim x As Integer
                Dim UNSPSC As String = " "
                Dim CatType As String
                Dim query As String


                While Not MyReader.EndOfData
                    x = 0
                    Try
                        currentRow = MyReader.ReadFields()
                        Dim currentField As String
                        For Each currentField In currentRow
                            columna(x) = ""
                            columna(x) = currentField
                            x = x + 1
                            If (x = 6) Then
                                query = "select [UNSPSC_Customer] from [dbo].[CatSubCat_UNSPSC_29005207] where [CatSubCat_IM]=" + currentField
                                Using cm3 As SqlClient.SqlCommand = New SqlClient.SqlCommand(query, conn)
                                    Try
                                        UNSPSC = CStr(cm3.ExecuteScalar())

                                    Catch ex As Exception
                                        res1 = -1
                                    End Try
                                End Using
                            End If

                        Next
                        If IsNothing(UNSPSC) Then
                            CatType = Nothing
                        Else
                            CatType = "UNSPSC"
                            If columna(4).Length = 35 Then
                                columna(4) = columna(4).Substring(0, 33)
                            End If
                            columna(18) = "1"
                            If columna(14) = "J" Then
                                columna(18) = "3"
                                'Me.Label1.Text = CStr(loading)
                            End If
                            columna(6) = columna(6).Replace(".", ",")
                            Dim newId As Integer = ADAO1.insertFile(columna(0), columna(1), columna(2), columna(3), columna(4), columna(5), UNSPSC, CatType, CDbl(columna(6)), columna(7), CByte(columna(18)), columna(8),
                                                                    columna(9), columna(10), CInt(columna(11)), columna(12), columna(13), columna(14), columna(15), columna(16), columna(17), Date.Now, conn)
                            loading = loading + 1

                            Me.Label2.Text = CStr(loading)
                            Application.DoEvents()
                        End If




                    Catch ex As Microsoft.VisualBasic.
            FileIO.MalformedLineException
                        MsgBox("Line " & ex.Message &
                        "is not valid and will be skipped.")
                    End Try
                End While
                Me.Label2.Text = CStr(loading)
                Return 1
            End Using
            conn.Close()


        End Using
        Application.Exit()
    End Function






    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        Me.Show()

        Me.CSV_PATH = IniFile.getIniParameter("PATHS", "CSV_PATH", "")
        If Me.CSV_PATH = "" Then
            Exit Sub
        Else
            'Me.lblCSVPath.Text = CSV_Path
        End If

        Dim DateNow As Date
        Dim WeekDayNbr As Integer
        DateNow = Date.Now
        WeekDayNbr = Weekday(DateNow)
        If CInt(WeekDayNbr) = (7 Or 1) Then
            WeekDayNbr = 6
        Else
            Test_Path = "M:\USER\COMUN\EDI\PRICEFILES\29005207_AT55D7\" + CStr(WeekDayNbr - 1) + "\PRICARIF.TXT"

            Dim result As Integer = Me.Recollect_Data(Test_Path)

            If result = 1 Then
                Application.Exit()

            End If
        End If

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        'Form2.Show()
        'Dim ADAO As New AribaDAO


        'Dim newId As Integer = ADAO.insertCustomer("123456", "ERNEST", "domain", "supplierID", "Y", "ftpserver", "user", "pass", "path", "filename", "C")

        'If newId > 0 Then
        'Dim strSql As String = "SELECT * FROM CustomerInfo WHERE id=" & newId.ToString
        'Dim dt As New DataTable
        'Dim res As Integer = ADAO.SimpleSelect(strSql, dt)

        'End If
        End
    End Sub

    Private Sub lblCSVPath_Click(sender As System.Object, e As System.EventArgs)

    End Sub


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click

        Dim DateNow As Date
        Dim WeekDayNbr As Integer
        DateNow = Date.Now
        WeekDayNbr = Weekday(DateNow)
        If CInt(WeekDayNbr) = (7 Or 1) Then
            WeekDayNbr = 6
        Else
            Test_Path = "M:\USER\COMUN\EDI\PRICEFILES\29005207_AT55D7\" + CStr(WeekDayNbr - 1) + "\PRICARIF.TXT"

            Dim result As Integer = Me.Recollect_Data(Test_Path)

            If result = 1 Then
                Application.Exit()

            End If
        End If



    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)
        OpenFileDialog1.ShowDialog()

    End Sub

    'Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
    '   TextBox1.Text = OpenFileDialog1.FileName
    '  Test_Path = TextBox1.Text



    'End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As System.Object, e As System.EventArgs) Handles Label2.Click

    End Sub
End Class
