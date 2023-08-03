Imports System.IO
Public Class Form1

    Dim fileReader As StreamReader
    Dim fileWriter As StreamWriter
    Dim getFileName As String
    Dim writingfile As String
    Dim readingfile As String
    Dim keyword1 As String
    Dim keyword2 As String
    Dim d As Date = Date.Today
    Dim updatedfilename As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Browsefile()

    End Sub
    Public Sub Browsefile()
        Dim dialogbox As New OpenFileDialog()

        dialogbox.Title = "Select File"
        dialogbox.InitialDirectory = "E:\"
        dialogbox.Filter = "All files(*.*)|*.*|All files(*.*)|*.*"
        dialogbox.FilterIndex = 2
        dialogbox.RestoreDirectory = True
        If DialogResult.OK = dialogbox.ShowDialog Then
            getFileName = dialogbox.FileName
            TextBox3.Text = dialogbox.FileName
            btnBrowse.Enabled = False
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ReadFile()
    End Sub
    Public Sub ReadFile()
        If TextBox3.Text = "" Then
            MessageBox.Show("Please browse the file")
            Exit Sub
        End If
        updatedfilename = getFileName.Substring(3)
        fileReader = My.Computer.FileSystem.OpenTextFileReader(getFileName)
        readingfile = fileReader.ReadToEnd()
        fileReader.Close()
        fileWriter = My.Computer.FileSystem.OpenTextFileWriter("E:\result.csv", True)

        keyword1 = TextBox1.Text
        keyword2 = TextBox2.Text
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            MessageBox.Show("Nothing Found!, Please enter keyword")
        End If
        If TextBox1.Text <> "" Then
            If readingfile.Contains(keyword1) Then
                Label4.Visible = True
                Label8.Text = keyword1
                fileWriter.WriteLine(Environment.NewLine + "," + keyword1 + "," + updatedfilename + "," + d.Date + "," + Now.ToLongTimeString)
            Else
                Label6.Visible = True
                Label6.Text = "Keyword 1 not found"
            End If
        End If
        If TextBox2.Text <> "" Then
            If readingfile.Contains(keyword2) Then
                Label5.Visible = True
                Label9.Text = keyword2
                fileWriter.WriteLine(Environment.NewLine + "," + keyword2 + "," + updatedfilename + "," + d.Date + "," + Now.ToLongTimeString)
            Else
                Label7.Visible = True
                Label7.Text = "Keyword 2 not found"
            End If
        End If
        fileWriter.Close()
        Button2.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cleartext()
    End Sub
    Public Sub cleartext()
        Label6.Text = ""
        Label7.Text = ""
        Label4.Text = ""
        Label5.Text = ""
        Label8.Text = ""
        Label9.Text = ""
        TextBox3.Text = ""
        btnBrowse.Enabled = True
        Button2.Enabled = True
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        clearAll()
    End Sub
    Public Sub clearAll()
        cleartext()
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        browseFolder()
    End Sub

    Public Sub browseFolder()
        Dim f As String
        keyword1 = TextBox1.Text
        keyword2 = TextBox2.Text
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            MessageBox.Show("Nothing Found!, Please enter keyword")
            Exit Sub
        End If
        Dim folderBrowseDialog As FolderBrowserDialog = New FolderBrowserDialog()
        folderBrowseDialog.SelectedPath = "E:\vbtestproject\"
        Dim driNFO As New DirectoryInfo(folderBrowseDialog.SelectedPath)
        Dim txtFiles As FileInfo() = driNFO.GetFiles("*.txt")

        For Each txt_file As FileInfo In txtFiles
            f = txt_file.Name
            'MessageBox.Show(f)
            fileReader = My.Computer.FileSystem.OpenTextFileReader("E:\vbtestproject\" + f)
            readingfile = fileReader.ReadToEnd()
            fileReader.Close()
            fileWriter = My.Computer.FileSystem.OpenTextFileWriter("E:\result.csv", True)
            'MessageBox.Show(readingfile)


            If TextBox1.Text <> "" Then
                If readingfile.Contains(keyword1) Then
                    Label4.Visible = True
                    Label8.Text = keyword1
                    fileWriter.WriteLine(Environment.NewLine + "," + keyword1 + "," + f + "," + d.Date + "," + Now.ToLongTimeString)

                End If
            End If
            If TextBox2.Text <> "" Then
                If readingfile.Contains(keyword2) Then
                    Label5.Visible = True
                    Label9.Text = keyword2
                    fileWriter.WriteLine(Environment.NewLine + "," + keyword2 + "," + f + "," + d.Date + "," + Now.ToLongTimeString)

                End If
            End If
            fileWriter.Close()

        Next
        MessageBox.Show("File Writing Done")
        'Button2.Enabled = False
    End Sub

End Class
