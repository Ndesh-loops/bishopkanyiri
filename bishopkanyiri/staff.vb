Imports MySql.Data.MySqlClient
Public Class staff
    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim staffnumber As String
    Dim firstname As String
    Dim surname As String
    Dim email As String
    Dim phone As String
    Dim idnumber As String
    Dim category As String
    Dim workarea As String
    Dim cmd As MySqlCommand


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Public Sub Populate_ListView()
        Dim sql As String
        Dim cmd As MySqlCommand
        Dim reader As MySqlDataReader
        Dim n As Integer
        Dim i As Integer
        Dim calls As Integer


        sql = "SELECT `staffno`,`firstname`,`surname`,`phone`,`email`,`Idnumber`,`Category`,`Workarea` FROM `staff`"
        cmd = New MySqlCommand(sql, con)
        reader = cmd.ExecuteReader
        MyListView.Clear()
        n = 0
        calls = reader.FieldCount
        Do While reader.Read
            MyListView.Items.Add(n)
            For i = 0 To calls - 1
                MyListView.Items(n).SubItems.Add(reader(i))

            Next



            n += 1
        Loop
        ' We have set listview properties
        MyListView.View = View.Details
        MyListView.GridLines = True
        MyListView.FullRowSelect = True
        MyListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

        'set column headers
        MyListView.Columns.Add("SN")



        For i = 0 To calls - 1
            MyListView.Columns.Add(reader.GetName(i))
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
        Panel3.BackColor = Color.Red
    End Sub

    Private Sub Panel3_MouseLeave(sender As Object, e As EventArgs) Handles Panel3.MouseLeave
        Panel3.BackColor = Color.PowderBlue
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub Panel4_MouseEnter(sender As Object, e As EventArgs) Handles Panel4.MouseEnter
        Panel4.BackColor = Color.AliceBlue
    End Sub

    Private Sub Panel4_MouseLeave(sender As Object, e As EventArgs) Handles Panel4.MouseLeave
        Panel4.BackColor = Color.LightBlue
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Dim sql As String
        Dim cmd As MySqlCommand
        sql = "insert into staff(firstname,surname,phone,email,Idnumber,Category,Workarea) values(@firstname,@surname,@phone,@email,@Idnumber,@Category,@workarea)"
        Call Populating()
        cmd = New MySqlCommand(sql, con)
        ''cmd.Parameters.AddWithValue("studentnumber", studentnumber)
        cmd.Parameters.AddWithValue("@firstname", firstname)
        cmd.Parameters.AddWithValue("@surname", surname)
        cmd.Parameters.AddWithValue("@phone", phone)
        cmd.Parameters.AddWithValue("@email", email)
        cmd.Parameters.AddWithValue("@Idnumber", idnumber)
        cmd.Parameters.AddWithValue("@category", category)
        cmd.Parameters.AddWithValue("@Workarea", workarea)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("New staff succesfully added", vbInformation)
            Call Reset()
            Call Populate_ListView()



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Reset()
        StaffnoTextBox.Text = ""
        FrstnmeTextBox.Text = ""
        SurnameTextBox.Text = ""
        PhoneTextBox.Text = ""
        EmailTextBox.Text = ""
        PhoneTextBox.Text = ""
        IDNoTextBox.Text = ""
        CategoryComboBox.Text = ""
        WorkAreaTextBox.Text = ""
    End Sub

    Private Sub Populating()

        firstname = FrstnmeTextBox.Text
        surname = SurnameTextBox.Text
        phone = PhoneTextBox.Text
        email = EmailTextBox.Text
        idnumber = IDNoTextBox.Text
        category = CategoryComboBox.Text
        workarea = WorkAreaTextBox.Text


    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles SaveButton.MouseEnter
        SaveButton.ForeColor = Color.LimeGreen
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles SaveButton.MouseLeave
        SaveButton.ForeColor = Color.White
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        Dim sql As String
        Dim cmd As MySqlCommand
        staffnumber = StaffnoTextBox.Text
        sql = "Update staff set staffno=@staffno,firstname=@firstname,surname=@surname,phone=@phone,email=@email,idnumber=@idnumber,category=@category,workarea=@workarea where staffno=@staffno"
        Call Populating()
        cmd = New MySqlCommand(sql, con)
        cmd.Parameters.AddWithValue("staffno", staffnumber)
        cmd.Parameters.AddWithValue("@firstname", firstname)
        cmd.Parameters.AddWithValue("@surname", surname)
        cmd.Parameters.AddWithValue("@phone", phone)
        cmd.Parameters.AddWithValue("@email", email)
        cmd.Parameters.AddWithValue("@idnumber", idnumber)
        cmd.Parameters.AddWithValue("@category", category)
        cmd.Parameters.AddWithValue("@Workarea", workarea)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Staff succesfully updated", vbInformation)
            Call Reset()
            Call Populate_ListView()

        Catch ex As Exception
            MsgBox("Kindly Click again to confirm alteration")
        End Try
    End Sub

    Private Sub Button3_MouseEnter(sender As Object, e As EventArgs) Handles UpdateButton.MouseEnter
        UpdateButton.ForeColor = Color.LimeGreen
    End Sub

    Private Sub Button3_MouseLeave(sender As Object, e As EventArgs) Handles UpdateButton.MouseLeave
        UpdateButton.ForeColor = Color.White
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        Call Reset()
    End Sub

    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs) Handles ResetButton.MouseEnter
        ResetButton.ForeColor = Color.LimeGreen
    End Sub

    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles ResetButton.MouseLeave
        ResetButton.ForeColor = Color.White
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        Dim sql As String
        Dim cmd As MySqlCommand
        staffnumber = StaffnoTextBox.Text
        sql = "DELETE FROM staff WHERE staffno=" & staffnumber
        cmd = New MySqlCommand(sql, con)
        Try
            cmd.ExecuteNonQuery()
            MsgBox("Staff unregistered Succesfully", vbInformation)
            Call Populate_ListView()
            Call Reset()



        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub Button4_MouseEnter(sender As Object, e As EventArgs) Handles DeleteButton.MouseEnter
        DeleteButton.ForeColor = Color.OrangeRed
    End Sub

    Private Sub Button4_MouseLeave(sender As Object, e As EventArgs) Handles DeleteButton.MouseLeave
        DeleteButton.ForeColor = Color.White
    End Sub

    Private Sub ExitButton_MouseEnter(sender As Object, e As EventArgs) Handles ExitButton.MouseEnter
        ExitButton.ForeColor = Color.Plum
    End Sub

    Private Sub ExitButton_MouseLeave(sender As Object, e As EventArgs) Handles ExitButton.MouseLeave
        ExitButton.ForeColor = Color.White
    End Sub

    Private Sub staff_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Connect()
        Call Populate_ListView()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MyListView.SelectedIndexChanged
        Dim Items As ListView.SelectedListViewItemCollection = Me.MyListView.SelectedItems
        Dim item As ListViewItem
        For Each item In Items
            StaffnoTextBox.Text = item.SubItems(1).Text
        Next item
        Call Populate_ListView()
    End Sub

    Private Sub StaffnoTextBox_TextChanged(sender As Object, e As EventArgs) Handles StaffnoTextBox.TextChanged
        Try
            Dim sql As String
            Dim reader As MySqlDataReader
            staffnumber = StaffnoTextBox.Text
            sql = "SELECT * FROM `staff` WHERE staffno=" & staffnumber
            cmd = New MySqlCommand(sql, con)
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                reader.Read()
                FrstnmeTextBox.Text = reader("firstname")
                SurnameTextBox.Text = reader("surname")
                PhoneTextBox.Text = reader("phone")
                EmailTextBox.Text = reader("email")
                IDNoTextBox.Text = reader("idnumber")
                CategoryComboBox.Text = reader("category")
                WorkAreaTextBox.Text = reader("workarea")


            Else

            End If
            reader.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        Dim sql As String
        Dim SearchValue As String
        SearchValue = TextBox7.Text
        sql = "select * from 'staff' WHERE 'firstname' like '%" & SearchValue & "%'"

    End Sub

    Private Sub staff_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        con.Close()
    End Sub
End Class