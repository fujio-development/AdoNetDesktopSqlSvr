Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class Form1
    Private sqlserver As FileSupportSqlSvr
    Private DbOpenType As Boolean = True
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call FormDesignSetting()

    End Sub

    ''' <summary>商品を全件表示します。</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonQuery_Click(sender As Object, e As EventArgs) Handles ButtonQuery.Click

        Using sqlserver = New FileSupportSqlSvr()
            DatabaseOpen(sqlserver)

            Dim list As List(Of ShohinDto) = sqlserver.DataReader(Of ShohinDto)("select * from ShohinDataDesk order by NumId asc").ToList()
            BindingSource1.DataSource = list
            DataGridView1.DataSource = BindingSource1
            Call DataGridSetting()
            Call TextBoxClear()
            RichTextBox1.AppendText("商品を全件表示しました。" & vbCrLf)
        End Using

    End Sub

    ''' <summary>テキストボックスによる内容で商品を追加します。</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonInsert_Click(sender As Object, e As EventArgs) Handles ButtonInsert.Click

        Dim sqlstr As String = ""

        If Regex.IsMatch(TextBoxShohinNum.Text, "^[0-9]{1,4}$") = False Then
            MessageBox.Show("商品番号は半角数値の0～9999でなければなりません。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using sqlserver = New FileSupportSqlSvr()
            DatabaseOpen(sqlserver)
            sqlstr = "insert into ShohinDataDesk (ShohinNum, ShohinName, EditDate, EditTime, Note) "
            sqlstr &= "values (@ShohinNum, @ShohinName, @EditDate, @EditTime, @Note)"
            Using cmd = New SqlCommand()
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@ShohinNum", SqlDbType.SmallInt)
                cmd.Parameters.Add("@ShohinName", SqlDbType.Char, 50)
                cmd.Parameters.Add("@EditDate", SqlDbType.Decimal, 8)
                cmd.Parameters.Add("@EditTime", SqlDbType.Decimal, 6)
                cmd.Parameters.Add("@Note", SqlDbType.VarChar, 255)

                cmd.Parameters("@ShohinNum").Value = Short.Parse(TextBoxShohinNum.Text)
                cmd.Parameters("@ShohinName").Value = TextBoxShohinName.Text
                cmd.Parameters("@EditDate").Value = Format(Now, "yyyyMMdd")
                cmd.Parameters("@EditTime").Value = Format(Now, "HHmmss")
                cmd.Parameters("@Note").Value = TextBoxNote.Text

                Using tran As SqlTransaction = sqlserver.GoTransaction()
                    Try
                        sqlserver.NonQuery(sqlstr, cmd, tran)
                    Catch ex As SqlException
                        sqlserver.TransactionRollback(tran)
                        Throw
                    End Try
                    sqlserver.TransactionCommit(tran)
                End Using
                RichTextBox1.AppendText("1件追加しました" & vbCrLf)
            End Using
        End Using

    End Sub

    ''' <summary>商品ID(NumId)による商品の更新を行います。</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click

        Dim sqlstr As String = ""

        If DataGridView1.Rows.Count <= 0 Or LabelNumId.Text = "" Then
            MessageBox.Show("更新する商品行が選択できていません。", "商品IDなし", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Regex.IsMatch(TextBoxShohinNum.Text, "^[0-9]{1,4}$") = False Then
            MessageBox.Show("商品番号は半角数値の0～9999でなければなりません。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using sqlserver = New FileSupportSqlSvr()
            DatabaseOpen(sqlserver)
            sqlstr = "update ShohinDataDesk set ShohinNum=@ShohinNum, ShohinName=@ShohinName"
            sqlstr &= ", EditDate=@EditDate, EditTime=@EditTime, Note=@Note where NumId=@NumId"
            Using cmd = New SqlCommand()
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@NumId", SqlDbType.Int)
                cmd.Parameters.Add("@ShohinNum", SqlDbType.SmallInt)
                cmd.Parameters.Add("@ShohinName", SqlDbType.Char, 50)
                cmd.Parameters.Add("@EditDate", SqlDbType.Decimal, 8)
                cmd.Parameters.Add("@EditTime", SqlDbType.Decimal, 6)
                cmd.Parameters.Add("@Note", SqlDbType.VarChar, 255)

                cmd.Parameters("@NumId").Value = Integer.Parse(LabelNumId.Text)
                cmd.Parameters("@ShohinNum").Value = Short.Parse(TextBoxShohinNum.Text)
                cmd.Parameters("@ShohinName").Value = TextBoxShohinName.Text
                cmd.Parameters("@EditDate").Value = Format(Now, "yyyyMMdd")
                cmd.Parameters("@EditTime").Value = Format(Now, "HHmmss")
                cmd.Parameters("@Note").Value = TextBoxNote.Text

                Using tran As SqlTransaction = sqlserver.GoTransaction()
                    Try
                        sqlserver.NonQuery(sqlstr, cmd, tran)
                    Catch ex As SqlException
                        sqlserver.TransactionRollback(tran)
                        Throw
                    End Try
                    sqlserver.TransactionCommit(tran)
                End Using
                RichTextBox1.AppendText("選択された商品を更新しました。" & vbCrLf)
            End Using
        End Using

    End Sub

    ''' <summary>商品ID(NumId)による商品を削除します。</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click

        Dim DelNum As Integer
        Dim sqlstr As String = ""

        If DataGridView1.Rows.Count <= 0 Or LabelNumId.Text = "" Then
            MessageBox.Show("削除する商品行が選択がされていません", "商品IDなし", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelNum = Integer.Parse(LabelNumId.Text)
        Using sqlserver = New FileSupportSqlSvr()
            DatabaseOpen(sqlserver)
            sqlstr = "delete from ShohinDataDesk where NumId = @NumId"
            Using cmd = New SqlCommand()
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@NumId", SqlDbType.Int)
                cmd.Parameters("@NumId").Value = DelNum

                Using tran As SqlTransaction = sqlserver.GoTransaction()
                    Try
                        sqlserver.NonQuery(sqlstr, cmd, tran)
                    Catch ex As SqlException
                        sqlserver.TransactionRollback(tran)
                        Throw
                    End Try
                    sqlserver.TransactionCommit(tran)
                End Using
                RichTextBox1.AppendText(DelNum & "の行を削除しました" & vbCrLf)
            End Using
        End Using

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        LabelNumId.Text = Integer.Parse(DataGridView1.CurrentRow.Cells("NumId").Value)
        TextBoxShohinNum.Text = DataGridView1.CurrentRow.Cells("ShohinNum").Value
        TextBoxShohinName.Text = DataGridView1.CurrentRow.Cells("ShohinName").Value
        TextBoxNote.Text = DataGridView1.CurrentRow.Cells("Note").Value

    End Sub

    Private Sub FormDesignSetting()

        Me.Text = "ADO.NET + デスクトップアプリ + SQL Server"
        Me.Location = New Point(500, 200)
        Me.Size = New Size(800, 600)
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        Me.DataGridView1.Location = New Point(25, 25)
        Me.DataGridView1.Size = New Size(730, 200)

        Me.RichTextBox1.Location = New Point(25, 235)
        Me.RichTextBox1.Size = New Size(350, 100)
        Me.RichTextBox1.ReadOnly = True

        Me.ButtonQuery.Text = "抽出"
        Me.ButtonQuery.Location = New Point(50, 470)
        Me.ButtonQuery.Size = New Size(150, 50)
        Me.ButtonQuery.TabIndex = 3

        Me.ButtonInsert.Text = "追加"
        Me.ButtonInsert.Location = New Point(230, 470)
        Me.ButtonInsert.Size = New Size(150, 50)
        Me.ButtonInsert.TabIndex = 4

        Me.ButtonUpdate.Text = "更新"
        Me.ButtonUpdate.Location = New Point(410, 470)
        Me.ButtonUpdate.Size = New Size(150, 50)
        Me.ButtonUpdate.TabIndex = 5

        Me.ButtonDelete.Text = "削除"
        Me.ButtonDelete.Location = New Point(590, 470)
        Me.ButtonDelete.Size = New Size(150, 50)
        Me.ButtonDelete.TabIndex = 6

        Me.Label1.Location = New System.Drawing.Point(385, 250)
        Me.Label1.AutoSize = False
        Me.Label1.Size = New System.Drawing.Size(75, 25)
        Me.Label1.Text = "商品ID："

        Me.Label2.Location = New System.Drawing.Point(385, 300)
        Me.Label2.AutoSize = False
        Me.Label2.Size = New System.Drawing.Size(75, 25)
        Me.Label2.Text = "商品番号："

        Me.Label3.Location = New System.Drawing.Point(385, 350)
        Me.Label3.AutoSize = False
        Me.Label3.Size = New System.Drawing.Size(75, 25)
        Me.Label3.Text = "商品名："

        Me.Label4.Location = New System.Drawing.Point(385, 400)
        Me.Label4.AutoSize = False
        Me.Label4.Size = New System.Drawing.Size(75, 25)
        Me.Label4.Text = "備考："

        Me.LabelNumId.Location = New Point(690, 250)
        Me.LabelNumId.AutoSize = False
        Me.LabelNumId.Size = New Size(60, 20)
        Me.LabelNumId.Text = ""
        Me.LabelNumId.TextAlign = ContentAlignment.TopRight

        Me.TextBoxShohinNum.Location = New Point(600, 300)
        Me.TextBoxShohinNum.Size = New Size(150, 19)
        Me.TextBoxShohinNum.TabIndex = 0

        Me.TextBoxShohinName.Location = New Point(550, 350)
        Me.TextBoxShohinName.Size = New Size(200, 19)
        Me.TextBoxShohinName.TabIndex = 1

        Me.TextBoxNote.Location = New Point(450, 400)
        Me.TextBoxNote.Size = New Size(300, 19)
        Me.TextBoxNote.TabIndex = 2

    End Sub

    Private Sub TextBoxClear()

        LabelNumId.Text = ""
        TextBoxShohinNum.Text = ""
        TextBoxShohinName.Text = ""
        TextBoxNote.Text = ""

    End Sub

    Private Sub DatabaseOpen(ByVal sqlserver As FileSupportSqlSvr)

        If DbOpenType Then
            Dim path As String = Environment.CurrentDirectory & "\"
            If sqlserver.Open(path, "MsSqlServer.xml") = False Then
                MessageBox.Show("データベース設定ファイルがありません。" & vbCrLf & "アプリケーションを終了します")
                Application.Exit()
            End If
        Else
            sqlserver.Host = "(local)"
            sqlserver.Instance = "SQLEXPRESS"
            sqlserver.LoginMode = True
            sqlserver.Catalog = "AdoNetSample"
            sqlserver.ConnectTimeout = 3
            sqlserver.MultipleActiveResultSets = False
            sqlserver.Open()
        End If

    End Sub

    Private Sub DataGridSetting()

        DataGridView1.Columns("NumId").HeaderText = "商品ID"
        DataGridView1.Columns("ShohinNum").HeaderText = "商品番号"
        DataGridView1.Columns("ShohinName").HeaderText = "商品名"
        DataGridView1.Columns("EditDate").HeaderText = "編集日付"
        DataGridView1.Columns("EditTime").HeaderText = "編集時刻"
        DataGridView1.Columns("Note").HeaderText = "備考"
        DataGridView1.Columns("NumId").Width = 70
        DataGridView1.Columns("Note").Width = 250
        DataGridView1.Columns("EditDate").DefaultCellStyle.Format = "0000/00/00"
        DataGridView1.Columns("EditTime").DefaultCellStyle.Format = "00:00:00"
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.RowHeadersVisible = False
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.ReadOnly = True

    End Sub

End Class