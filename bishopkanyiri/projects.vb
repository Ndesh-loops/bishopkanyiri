Imports MySql.Data.MySqlClient
Public Class projects
    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim projectno As String
    Dim projectname As String
    Dim duedate As String
    Dim assigned As String
    Dim status As String
    Dim description As String
    Dim cmd As MySqlCommand
    Private Sub projects_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Connect()
        Call Populate_ListView()

    End Sub

    Public Sub Populate_ListView()
        Dim sql As String
        Dim cmd As MySqlCommand
        Dim reader As MySqlDataReader
        Dim n As Integer
        Dim i As Integer
        Dim calls As Integer
        sql = "SELECT `projectno`,`project-title`,`assigned`,`start-date`, `due-date`, `status`, `description` FROM `projects`"
        cmd = New MySqlCommand(sql, con)
        reader = cmd.ExecuteReader
        ListView1.Clear()
        n = 0
        calls = reader.FieldCount
        Do While reader.Read
            ListView1.Items.Add(n)
            For i = 0 To calls - 1
                ListView1.Items(n).SubItems.Add(reader(i))

            Next

            n += 1
        Loop
        ' We have set listview properties
        ListView1.View = View.Details
        ListView1.GridLines = True
        ListView1.FullRowSelect = True
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

        'set column headers
        ListView1.Columns.Add("SN")



        For i = 0 To calls - 1
            ListView1.Columns.Add(reader.GetName(i))
        Next



        reader.Close()
    End Sub
    Public Sub Connect()
        Dim ConString As String
        ConString = "server=localhost;uid=root;pwd=;database=bishopkanyiri"

        Try
            con.ConnectionString = ConString
            con.Open()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Panel3_MouseEnter(sender As Object, e As EventArgs) Handles Panel3.MouseEnter
        Panel3.BackColor = Color.Silver
    End Sub

    Private Sub Panel3_MouseLeave(sender As Object, e As EventArgs) Handles Panel3.MouseLeave
        Panel3.BackColor = Color.LightGray
    End Sub


    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Button1.BackColor = Color.Red
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.BackColor = Color.Silver
    End Sub

    Private Sub projects_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        con.Close()
    End Sub
End Class