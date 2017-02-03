

Public Class AribaDAO


    'THIS MUST BE CHANGED!
    Private _StringConnection As String = "Data Source=172.31.16.152;Initial Catalog=ARIBA;Trusted_Connection=True;"


    Public Function insertCustomer(ByVal BrCustNbr As String, ByVal BrCustName As String, ByVal Ariba_SuplierDomain As String, ByVal Ariba_SupplierID As String,
                                   ByVal Ariba_TEST_YN As String, ByVal ImpFTP_Server As String, ByVal ImpFTP_User As String, ByVal ImpFTP_Password As String,
                                   ByVal ImpFTP_Path As String, ByVal ImpFTP_FileName As String, ByVal ImpFTP_Type As String) As Integer

        Dim res As Integer = 0
        Dim id As Integer = 0
        Try
            Using conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(Me._StringConnection)
                conn.Open()
                Using cm As SqlClient.SqlCommand = New SqlClient.SqlCommand("insertCustomer", conn)
                    cm.CommandType = CommandType.StoredProcedure
                    cm.Parameters.Add("@BrCustNbr", SqlDbType.Char, 8).Value = Left(BrCustNbr & Space(8), 8).Trim
                    cm.Parameters.Add("@BrCustName", SqlDbType.VarChar, 35).Value = Left(BrCustName & Space(35), 35).Trim
                    cm.Parameters.Add("@Ariba_SuplierDomain", SqlDbType.VarChar, 50).Value = Left(Ariba_SuplierDomain & Space(50), 50).Trim
                    cm.Parameters.Add("@Ariba_SupplierID", SqlDbType.VarChar, 50).Value = Left(Ariba_SupplierID & Space(50), 50).Trim
                    If Ariba_TEST_YN = "Y" Then
                        cm.Parameters.Add("@Ariba_TEST", SqlDbType.Char, 1).Value = "Y"
                    Else
                        cm.Parameters.Add("@Ariba_TEST", SqlDbType.Char, 1).Value = "N"
                    End If
                    cm.Parameters.Add("@ImpFTP_Server", SqlDbType.VarChar, 255).Value = Left(ImpFTP_Server & Space(255), 255).Trim
                    cm.Parameters.Add("@ImpFTP_User", SqlDbType.VarChar, 20).Value = Left(ImpFTP_User & Space(20), 20).Trim
                    cm.Parameters.Add("@ImpFTP_Password", SqlDbType.VarChar, 20).Value = Left(ImpFTP_Password & Space(20), 20).Trim
                    cm.Parameters.Add("@ImpFTP_Path", SqlDbType.VarChar, 50).Value = Left(ImpFTP_Path & Space(50), 50).Trim
                    cm.Parameters.Add("@ImpFTP_FileName", SqlDbType.VarChar, 12).Value = Left(ImpFTP_FileName & Space(12), 12).Trim
                    cm.Parameters.Add("@ImpFTP_Type", SqlDbType.Char, 1).Value = Left(ImpFTP_Type & Space(1), 1).Trim
                    cm.Parameters.Add("@@id", SqlDbType.Int).Direction = ParameterDirection.Output
                    Try
                        res = cm.ExecuteNonQuery()
                        id = CInt(cm.Parameters("@@id").Value.ToString)
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
