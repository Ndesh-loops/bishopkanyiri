Imports MySql.Data.MySqlClient
Module Bmodule
    Public cmd As MySqlCommand
    Public reader As MySqlDataReader
    Public n As Integer
    Public con As New MySql.Data.MySqlClient.MySqlConnection


    Public Sub PopulateListView(Ls As ListView, sql As String)

        cmd = New MySqlCommand(sql, con)

        reader = cmd.ExecuteReader


        Dim columns As Integer, i As Integer
        Ls.Clear()
        If reader.HasRows Then

            n = 0
            columns = reader.FieldCount
            Do While reader.Read()
                Ls.Items.Add(n + 1)
                For i = 0 To columns - 1
                    Ls.Items(n).SubItems.Add(reader(i))


                Next
                n += 1
            Loop

            ' We have set listview properties
            Ls.View = View.Details
            Ls.GridLines = True
            Ls.FullRowSelect = True
            Ls.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

            'setting column header

            Ls.Columns.Add("SN")
            For i = 0 To columns - 1
                Ls.Columns.Add(reader.GetName(i))
            Next
        Else
            MessageBox.Show("Nothing to show at this time")
        End If


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
    Public Sub models()   ' To automatically add tables if they don't exist in the database
        Dim admission As String
        Dim projects As String
        Dim staff As String
        Dim inventory As String
        admission = "CREATE TABLE IF NOT EXISTS `admission` (id tinyint(4) Not NULL,`studentnumber` tinyint(4) Not NULL,`firstname` tinytext Not NULL,`lastname` tinytext Not NULL,`gender` tinytext Not NULL,`class` varchar(100) Not NULL,`first_name` tinytext Not NULL,`surname` tinytext Not NULL,`email` varchar(200) Not NULL,`address` varchar(150) Not NULL,`phone` int(4) Not NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

        projects = "CREATE TABLE IF NOT EXISTS `projects` (`id` tinyint(4) Not NULL,`projectno` tinyint(4) Not NULL,`project-title` varchar(800) Not NULL,`start-date` timestamp Not NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),`due-date` varchar(600) Not NULL,`assigned` tinytext Not NULL,`status` tinytext Not NULL,`description` text Not NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"


        staff = "CREATE TABLE IF NOT EXISTS `staff` ( `id` tinyint(11) NOT NULL,`staffno` tinyint(11) NOT NULL,`firstname` tinytext NOT NULL,`surname` tinytext NOT NULL,`phone` varchar(500) NOT NULL,`email` varchar(300) NOT NULL,`idnumber` varchar(200) NOT NULL,`category` tinytext NOT NULL,`workarea` tinytext NOT NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

        inventory = "CREATE TABLE IF NOT EXISTS `inventory` (`id` tinyint(4) NOT NULL,`inveno` tinyint(4) NOT NULL,`itemname` varchar(500) NOT NULL,`serialnumber` varchar(500) NOT NULL,`storage` varchar(500) NOT NULL,`custodian` tinytext NOT NULL,`date-added` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;"
        Call Connect()
        cmd = New MySqlCommand(admission, con)
        cmd.ExecuteNonQuery()
        cmd = New MySqlCommand(projects, con)
        cmd.ExecuteNonQuery()
        cmd = New MySqlCommand(staff, con)
        cmd.ExecuteNonQuery()
        cmd = New MySqlCommand(inventory, con)
        cmd.ExecuteNonQuery()


    End Sub
End Module
