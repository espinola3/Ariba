

Public Class AribaPRI


    Private _StringConnection As String = "Data Source=172.31.16.152;Initial Catalog=ARIBA;Trusted_Connection=True;"


    Public Function insertFile(ByVal ChangeCode As String, ByVal SKU As String, ByVal VPN As String, ByVal Description1 As String,
                                   ByVal Description2 As String, ByVal CatSubCat As String, ByVal ExternalCategory As String, ByVal CatType As String,
                                   ByVal CustPrice As Double, ByVal UnitsOfMeasure As String, ByVal VendorName As String, ByVal SubVendorName As String,
                                   ByVal BackOrder As String, ByVal Stock As Integer, ByVal SKUClass As String, ByVal CRC As String, ByVal Comp_IH_SW As String,
                                   ByVal SKUType As String, ByVal VendorNumber As String, ByVal MediaCode As String, ByVal DateInsert As Date) As Integer


        Dim res As Integer = 0
        Dim id As Integer = 0
        Try
            Using conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(Me._StringConnection)
                conn.Open()
                Using cm As SqlClient.SqlCommand = New SqlClient.SqlCommand("insertFile", conn)
                    cm.CommandType = CommandType.StoredProcedure
                    cm.Parameters.Add("@ChangeCode", SqlDbType.Char, 1).Value = Left(ChangeCode & Space(1), 1).Trim
                    cm.Parameters.Add("@@SKU", SqlDbType.Char, 7).Value = Left(SKU & Space(7), 7).Trim
                    cm.Parameters.Add("@VPN", SqlDbType.VarChar, 20).Value = Left(VPN & Space(20), 20).Trim
                    cm.Parameters.Add("@Description1", SqlDbType.VarChar, 31).Value = Left(Description1 & Space(31), 31).Trim
                    cm.Parameters.Add("@Description2", SqlDbType.VarChar, 35).Value = Left(Description2 & Space(35), 35).Trim
                    cm.Parameters.Add("@CatSubCat", SqlDbType.Char, 4).Value = Left(CatSubCat & Space(4), 4).Trim
                    cm.Parameters.Add("@ExternalCategory", SqlDbType.VarChar, 10).Value = Left(ExternalCategory & Space(10), 10).Trim
                    cm.Parameters.Add("@CatType", SqlDbType.VarChar, 20).Value = Left(CatType & Space(20), 20).Trim
                    cm.Parameters.Add("@CustPrice", SqlDbType.Money, 10).Value = Left(CustPrice & Space(10), 10).Trim
                    cm.Parameters.Add("@UnitsOfMeasure", SqlDbType.VarChar, 2).Value = Left(UnitsOfMeasure & Space(2), 2).Trim
                    cm.Parameters.Add("@VendorName", SqlDbType.VarChar, 20).Value = Left(VendorName & Space(20), 20).Trim
                    cm.Parameters.Add("@SubVendorName", SqlDbType.VarChar, 35).Value = Left(SubVendorName & Space(35), 35).Trim
                    cm.Parameters.Add("@BackOrder", SqlDbType.Char, 1).Value = Left(Stock & Space(1), 1).Trim
                    cm.Parameters.Add("@Stock", SqlDbType.Int, 10).Value = Left(BackOrder & Space(10), 10).Trim
                    cm.Parameters.Add("@SKUClass", SqlDbType.Char, 1).Value = Left(SKUClass & Space(1), 1).Trim
                    cm.Parameters.Add("@CRC", SqlDbType.VarChar, 20).Value = Left(CRC & Space(20), 20).Trim
                    cm.Parameters.Add("@Comp_IH_SW", SqlDbType.Char, 1).Value = Left(Comp_IH_SW & Space(1), 1).Trim
                    cm.Parameters.Add("@SKUType", SqlDbType.Char, 1).Value = Left(SKUType & Space(1), 1).Trim
                    cm.Parameters.Add("@VendorNumber", SqlDbType.Char, 4).Value = Left(VendorNumber & Space(4), 4).Trim
                    cm.Parameters.Add("@MediaCode", SqlDbType.Char, 4).Value = Left(MediaCode & Space(4), 4).Trim
                    cm.Parameters.Add("@DateInsert", SqlDbType.Date).Value = DateInsert
                    Try
                        res = cm.ExecuteNonQuery()

                    Catch ex As Exception
                        res = -1
                    End Try
                End Using
                conn.Close()
            End Using
        Catch ex As Exception
            Return -2
        End Try
        If id > 0 Then Return id
        Return res
    End Function

    Public Function SimpleSelect(ByVal SqlSelect As String, ByRef DT As DataTable) As Integer
        SimpleSelect = 0
        Dim oConn As SqlClient.SqlConnection
        oConn = New SqlClient.SqlConnection(Me._StringConnection.Trim)
        Try
            oConn.Open()
        Catch ex As Exception
            oConn.Dispose()
            oConn = Nothing
            Return -1
        End Try
        Try
            Dim cm As New SqlClient.SqlCommand(SqlSelect, oConn)
            Dim da As New SqlClient.SqlDataAdapter
            da.SelectCommand = cm
            da.Fill(DT)
            da.Dispose()
            da = Nothing
            cm.Dispose()
            oConn.Close()
            oConn = Nothing
        Catch ex As Exception
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn.Dispose()
                oConn = Nothing
            End If
            Return -2
        End Try
        SimpleSelect = DT.Rows.Count
    End Function

End Class
