Imports MySql.Data.MySqlClient
Public Class admissions
    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim studentnumber As String
    Dim id As String
    Dim firstname As String
    Dim lastname As String
    Dim gender As String
    Dim sclass As String
    Dim age As String
    Dim first_name As String
    Dim surname As String
    Dim email As String
    Dim phone As String
    Dim address As String
    Dim cmd As MySqlCommand

    Private Sub admissions_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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


        sql = "SELECT `studentnumber`,`firstname`, `lastname`, `gender`, `class`, `first_name`, `surname`, `email`, `address`,`phone` FROM `admission`"
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

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub ExitButton_MouseEnter(sender As Object, e As EventArgs) Handles ExitButton.MouseEnter
        ExitButton.ForeColor = Color.Red
    End Sub

    Private Sub ExitButton_MouseLeave(sender As Object, e As EventArgs) Handles ExitButton.MouseLeave
        ExitButton.ForeColor = Color.LimeGreen
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        Dim sql As String
        Dim cmd As MySqlCommand
        sql = "insert into admission(firstname,lastname,gender,class,first_name,surname,email,address,phone) values(@firstname,@lastname,@gender,@class,@first_name,@surname,@email,@address,@phone)"
        Call Populating()
        cmd = New MySqlCommand(sql, con)
        ''cmd.Parameters.AddWithValue("studentnumber", studentnumber)
        cmd.Parameters.AddWithValue("@firstname", firstname)
        cmd.Parameters.AddWithValue("@lastname", lastname)
        cmd.Parameters.AddWithValue("@gender", gender)
        cmd.Parameters.AddWithValue("@class", sclass)
        cmd.Parameters.AddWithValue("@first_name", first_name)
        cmd.Parameters.AddWithValue("@surname", surname)
        cmd.Parameters.AddWithValue("@email", email)
        cmd.Parameters.AddWithValue("@phone", phone)
        cmd.Parameters.AddWithValue("@address", address)



        Try
            cmd.ExecuteNonQuery()
            MsgBox("New student succesfully added", vbInformation)
            Call Reset()
            Call Populate_ListView()



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Reset()
        StdnoTextBox.Text = ""
        SfirstnameTextBox.Text = ""
        lastnameTextBox.Text = ""
        GenderComboBox.Text = ""
        ClassComboBox.Text = ""
        PfirstnameTextBox.Text = ""
        SurnameTextBox.Text = ""
        EmailTextBox.Text = ""
        PhoneTextBox.Text = ""
        AddressTextBox.Text = ""

    End Sub

    Private Sub Populating()
        studentnumber = StdnoTextBox.Text.ToLower
        firstname = SfirstnameTextBox.Text.ToLower
        lastname = lastnameTextBox.Text.ToLower
        gender = GenderComboBox.Text.ToLower
        sclass = ClassComboBox.Text.ToLower
        first_name = PfirstnameTextBox.Text.ToLower
        surname = SurnameTextBox.Text.ToLower
        email = EmailTextBox.Text.ToLower
        phone = PhoneTextBox.Text.ToLower
        address = AddressTextBox.Text.ToLower


    End Sub

    Private Sub SaveButton_MouseEnter(sender As Object, e As EventArgs) Handles SaveButton.MouseEnter
        SaveButton.ForeColor = Color.LimeGreen
    End Sub

    Private Sub SaveButton_MouseLeave(sender As Object, e As EventArgs) Handles SaveButton.MouseLeave
        SaveButton.ForeColor = Color.White
    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        Dim sql As String
        Dim cmd As MySqlCommand
        studentnumber = StdnoTextBox.Text
        sql = "Update admission set studentnumber=@studentnumber,firstname=@firstname,lastname=@lastname,gender=@gender,class=@class,first_name=@first_name,surname=@surname,email=@email,address=@address,phone=@phone where studentnumber=@studentnumber"
        Call Populating()
        cmd = New MySqlCommand(sql, con)
        cmd.Parameters.AddWithValue("studentnumber", studentnumber)
        cmd.Parameters.AddWithValue("@firstname", firstname)
        cmd.Parameters.AddWithValue("@lastname", lastname)
        cmd.Parameters.AddWithValue("@gender", gender)
        cmd.Parameters.AddWithValue("@class", sclass)
        cmd.Parameters.AddWithValue("@first_name", first_name)
        cmd.Parameters.AddWithValue("@surname", surname)
        cmd.Parameters.AddWithValue("@email", email)
        cmd.Parameters.AddWithValue("@phone", phone)
        cmd.Parameters.AddWithValue("@address", address)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Student succesfully updated", vbInformation)
            Call Reset()
            Call Populate_ListView()

        Catch ex As Exception
            MsgBox("Kindly Click again to confirm alteration")
        End Try
    End Sub

    Private Sub UpdateButton_MouseEnter(sender As Object, e As EventArgs) Handles UpdateButton.MouseEnter
        UpdateButton.ForeColor = Color.LimeGreen
    End Sub

    Private Sub UpdateButton_MouseLeave(sender As Object, e As EventArgs) Handles UpdateButton.MouseLeave
        UpdateButton.ForeColor = Color.White
    End Sub

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        Call Reset()
    End Sub

    Private Sub ResetButton_MouseEnter(sender As Object, e As EventArgs) Handles ResetButton.MouseEnter
        ResetButton.ForeColor = Color.LimeGreen
    End Sub

    Private Sub ResetButton_MouseLeave(sender As Object, e As EventArgs) Handles ResetButton.MouseLeave
        ResetButton.ForeColor = Color.White
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        Dim sql As String
        Dim cmd As MySqlCommand
        studentnumber = StdnoTextBox.Text
        sql = "DELETE FROM admission WHERE studentnumber=" & studentnumber
        cmd = New MySqlCommand(sql, con)
        Try
            cmd.ExecuteNonQuery()
            MsgBox("Student unregistered Succesfully", vbInformation)
            Call Populate_ListView()
            Call Reset()



        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub DeleteButton_MouseEnter(sender As Object, e As EventArgs) Handles DeleteButton.MouseEnter
        DeleteButton.ForeColor = Color.OrangeRed
    End Sub

    Private Sub DeleteButton_MouseLeave(sender As Object, e As EventArgs) Handles DeleteButton.MouseLeave
        DeleteButton.ForeColor = Color.White
    End Sub

    Private Sub PhoneTextBox_TextChanged(sender As Object, e As EventArgs) Handles PhoneTextBox.TextChanged

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim Items As ListView.SelectedListViewItemCollection = Me.ListView1.SelectedItems
        Dim item As ListViewItem
        For Each item In Items
            StdnoTextBox.Text = item.SubItems(1).Text
        Next item
        Call Populate_ListView()
    End Sub

    Private Sub StdnoTextBox_TextChanged(sender As Object, e As EventArgs) Handles StdnoTextBox.TextChanged
        Try
            Dim sql As String
            Dim reader As MySqlDataReader
            studentnumber = StdnoTextBox.Text
            sql = "SELECT * FROM `admission` WHERE studentnumber=" & studentnumber
            cmd = New MySqlCommand(sql, con)
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                reader.Read()
                SfirstnameTextBox.Text = reader("firstname")
                lastnameTextBox.Text = reader("lastname")
                GenderComboBox.Text = reader("gender")
                ClassComboBox.Text = reader("class")
                PfirstnameTextBox.Text = reader("first_name")
                SurnameTextBox.Text = reader("surname")
                EmailTextBox.Text = reader("email")
                AddressTextBox.Text = reader("address")
                PhoneTextBox.Text = reader("phone")

            Else

            End If
            reader.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles SearchTextBox.TextChanged
        Dim sql As String
        Dim SearchValue As String
        SearchValue = SearchTextBox.Text
        sql = "select * from `admission` WHERE `firstname` like '%" & SearchValue & "%' OR `lastname` like '%" & SearchValue & "%' OR `class` like '%" & SearchValue & "%' "
        Call PopulateListView(ListView1, sql)
    End Sub

    Private Sub admissions_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        con.Close()
    End Sub
End Class