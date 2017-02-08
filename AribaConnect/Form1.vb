Option Explicit On
Option Strict On

Imports AribaSQL

Public Class Form1
    Private Test_Path As String = ""


    Private CSV_Path As String = ""

    Public Class Line 
        Public ChangeCode As String
        Public SKU As String
        Public VPN As String
        Public Description1 As String
        Public Description2 As String
        Public CatSubCat As String
        Public ExternalCategory As String
        Public CatType As String
        Public CustPrice As Double
        Public UnitsOfMeasure As String
        Public VendorName As String
        Public SubVendorName As String
        Public BackOrder As String
        Public Stock As Integer
        Public SKUClass As String
        Public CRC As String
        Public Comp_IH_SW As String
        Public SKUType As String
        Public VendorNumber As String
        Public MediaCode As String
        Public DateInsert As Date
    End Class

    Private Function Recollect_Data(ByVal Test_Path As String) As Integer

        Using MyReader As New Microsoft.VisualBasic.
                      FileIO.TextFieldParser(
                        Test_Path)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim ADAO1 As New AribaPRI
            Dim currentRow As String()
            Dim columna(19) As String
            Dim x As Integer


            While Not MyReader.EndOfData
                x = 0
                Try
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String
                    For Each currentField In currentRow
                        columna(x) = ""
                        columna(x) = currentField
                        x = x + 1
                        'MsgBox(currentField)'

                    Next

                    Dim newId As Integer = ADAO1.insertFile(columna(0), columna(1), columna(2), columna(3), columna(4), columna(5), "", "", CDbl(columna(6)), columna(7), columna(8),
                                                            columna(9), columna(10), CInt(columna(11)), columna(12), columna(13), columna(14), columna(15), columna(16), columna(17), Date.Now)
                Catch ex As Microsoft.VisualBasic.
        FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message &
                    "is not valid and will be skipped.")
                End Try
            End While
            Return 1
        End Using

    End Function






    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        Me.CSV_Path = IniFile.getIniParameter("PATHS", "CSV_PATH", "")
        If Me.CSV_Path = "" Then
            Exit Sub
        Else
            'Me.lblCSVPath.Text = CSV_Path
        End If


    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim ADAO As New AribaDAO


        Dim newId As Integer = ADAO.insertCustomer("123456", "ERNEST", "domain", "supplierID", "Y", "ftpserver", "user", "pass", "path", "filename", "C")

        If newId > 0 Then
            Dim strSql As String = "SELECT * FROM CustomerInfo WHERE id=" & newId.ToString
            Dim dt As New DataTable
            Dim res As Integer = ADAO.SimpleSelect(strSql, dt)

        End If

    End Sub

    Private Sub lblCSVPath_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click

        Dim result As Integer = Me.Recollect_Data(Test_Path)

        Dim ADAO1 As New AribaPRI


        Dim newId As Integer = ADAO1.insertFile("A", "B090003", "18617", "CAMERA STAND METAL", "F/ 200/200+/2100/2120/2420",
                                               "1926", "ExtCat", "CatType", CDbl("0000020.31"), "EA", "AXIS", "AXIS COMMUNICATION",
                                               "Y", 0, "S", "STD", " ", " ", "7245", "ACCS", Date.Parse("2017-02-06"))

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        OpenFileDialog1.ShowDialog()

    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        TextBox1.Text = OpenFileDialog1.FileName
        Test_Path = TextBox1.Text



    End Sub
End Class
