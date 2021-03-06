Imports MySql.Data.MySqlClient
Public Class inventory
    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim inventorynumber As String
    Dim itemname As String
    Dim serialnumber As String
    Dim storage As String
    Dim custodian As String
    Dim cmd As MySqlCommand

    Public Sub Populate_ListView()
        Dim sql As String
        Dim cmd As MySqlCommand
        Dim reader As MySqlDataReader
        Dim n As Integer
        Dim i As Integer
        Dim calls As Integer

        sql = "SELECT `inveno`,`itemname`, `serialnumber`, `storage`, `custodian`,`date-added` FROM `inventory`"
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
    Private Sub inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Connect()
        Call Populate_ListView()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub Button5_MouseEnter(sender As Object, e As EventArgs) Handles Button5.MouseEnter
        Button5.BackColor = Color.Red
    End Sub

    Private Sub Button5_MouseLeave(sender As Object, e As EventArgs) Handles Button5.MouseLeave
        Button5.BackColor = Color.DarkGray
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Panel2_MouseLeave(sender As Object, e As EventArgs) Handles Panel2.MouseLeave
        Panel2.BackColor = Color.LightGray
    End Sub

    Private Sub Panel2_MouseEnter(sender As Object, e As EventArgs) Handles Panel2.MouseEnter
        Panel2.BackColor = Color.Silver
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Dim sql As String
        Dim cmd As MySqlCommand
        sql = "insert into inventory(itemname,serialnumber,storage,custodian) values(@itemname,@serialnumber,@storage,@custodian)"
        Call Populating()
        cmd = New MySqlCommand(sql, con)
        'cmd.Parameters.AddWithValue("studentnumber", studentnumber)
        cmd.Parameters.AddWithValue("@itemname", itemname)
        cmd.Parameters.AddWithValue("@serialnumber", serialnumber)
        cmd.Parameters.AddWithValue("@storage", storage)
        cmd.Parameters.AddWithValue("@custodian", custodian)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("New item succesfully added", vbInformation)
            Call Reset()
            Call Populate_ListView()



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Reset()
        invenoTextBox.Text = ""
        ItemNameTextBox.Text = ""
        SerialnoTextBox.Text = ""
        StorageTextBox.Text = ""
        CustodianTextbox.Text = ""
    End Sub

    Private Sub Populating()
        inventorynumber = invenoTextBox.Text
        itemname = ItemNameTextBox.Text
        serialnumber = SerialnoTextBox.Text
        storage = StorageTextBox.Text
        custodian = CustodianTextbox.Text

    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        Dim sql As String
        Dim cmd As MySqlCommand
        inventorynumber = invenoTextBox.Text
        sql = "Update inventory set inveno=@inveno,itemname=@itemname,serialnumber=@serialnumber,storage=@storage,custodian=@custodian where inveno=@inveno"
        Call Populating()
        cmd = New MySqlCommand(sql, con)
        cmd.Parameters.AddWithValue("inveno", inventorynumber)
        cmd.Parameters.AddWithValue("@itemname", itemname)
        cmd.Parameters.AddWithValue("@serialnumber", serialnumber)
        cmd.Parameters.AddWithValue("@storage", storage)
        cmd.Parameters.AddWithValue("@custodian", custodian)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Item succesfully updated", vbInformation)
            Call Reset()
            Call Populate_ListView()

        Catch ex As Exception
            MsgBox("Kindly Click again to confirm alteration")
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim Items As ListView.SelectedListViewItemCollection = Me.ListView1.SelectedItems
        Dim item As ListViewItem
        For Each item In Items
            invenoTextBox.Text = item.SubItems(1).Text
        Next item
        Call Populate_ListView()
    End Sub

    Private Sub invenoTextBox_TextChanged(sender As Object, e As EventArgs) Handles invenoTextBox.TextChanged
        Try
            Dim sql As String
            Dim reader As MySqlDataReader
            inventorynumber = invenoTextBox.Text
            sql = "SELECT * FROM `inventory` WHERE inveno=" & inventorynumber
            cmd = New MySqlCommand(sql, con)
            reader = cmd.ExecuteReader
            If reader.HasRows Then
                reader.Read()
                ItemNameTextBox.Text = reader("itemname")
                SerialnoTextBox.Text = reader("serialnumber")
                StorageTextBox.Text = reader("storage")
                CustodianTextbox.Text = reader("custodian")

            Else

            End If
            reader.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        Dim sql As String
        Dim cmd As MySqlCommand

        inventorynumber = invenoTextBox.Text
        sql = "DELETE FROM inventory WHERE inveno=" & inventorynumber
        cmd = New MySqlCommand(sql, con)
        Try
            cmd.ExecuteNonQuery()
            MsgBox("Item removed Succesfully", vbInformation)
            Call Populate_ListView()
            Call Reset()



        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub inventory_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        con.Close()
    End Sub
End Class