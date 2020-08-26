<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LabelNumId = New System.Windows.Forms.Label()
        Me.TextBoxNote = New System.Windows.Forms.TextBox()
        Me.TextBoxShohinName = New System.Windows.Forms.TextBox()
        Me.TextBoxShohinNum = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ButtonDelete = New System.Windows.Forms.Button()
        Me.ButtonUpdate = New System.Windows.Forms.Button()
        Me.ButtonInsert = New System.Windows.Forms.Button()
        Me.ButtonQuery = New System.Windows.Forms.Button()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelNumId
        '
        Me.LabelNumId.Location = New System.Drawing.Point(690, 250)
        Me.LabelNumId.Name = "LabelNumId"
        Me.LabelNumId.Size = New System.Drawing.Size(60, 20)
        Me.LabelNumId.TabIndex = 27
        Me.LabelNumId.Text = "---"
        Me.LabelNumId.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxNote
        '
        Me.TextBoxNote.Location = New System.Drawing.Point(450, 400)
        Me.TextBoxNote.Name = "TextBoxNote"
        Me.TextBoxNote.Size = New System.Drawing.Size(300, 19)
        Me.TextBoxNote.TabIndex = 2
        '
        'TextBoxShohinName
        '
        Me.TextBoxShohinName.Location = New System.Drawing.Point(550, 350)
        Me.TextBoxShohinName.Name = "TextBoxShohinName"
        Me.TextBoxShohinName.Size = New System.Drawing.Size(200, 19)
        Me.TextBoxShohinName.TabIndex = 1
        '
        'TextBoxShohinNum
        '
        Me.TextBoxShohinNum.Location = New System.Drawing.Point(600, 300)
        Me.TextBoxShohinNum.Name = "TextBoxShohinNum"
        Me.TextBoxShohinNum.Size = New System.Drawing.Size(150, 19)
        Me.TextBoxShohinNum.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(385, 400)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 25)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "備考："
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(385, 350)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 25)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "商品名："
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(385, 300)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 25)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "商品番号："
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(385, 250)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 25)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "商品ID："
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(25, 235)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(350, 100)
        Me.RichTextBox1.TabIndex = 20
        Me.RichTextBox1.Text = ""
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(25, 25)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.Size = New System.Drawing.Size(730, 200)
        Me.DataGridView1.TabIndex = 18
        '
        'ButtonDelete
        '
        Me.ButtonDelete.Location = New System.Drawing.Point(590, 470)
        Me.ButtonDelete.Name = "ButtonDelete"
        Me.ButtonDelete.Size = New System.Drawing.Size(150, 50)
        Me.ButtonDelete.TabIndex = 6
        Me.ButtonDelete.Text = "削除"
        Me.ButtonDelete.UseVisualStyleBackColor = True
        '
        'ButtonUpdate
        '
        Me.ButtonUpdate.Location = New System.Drawing.Point(410, 470)
        Me.ButtonUpdate.Name = "ButtonUpdate"
        Me.ButtonUpdate.Size = New System.Drawing.Size(150, 50)
        Me.ButtonUpdate.TabIndex = 5
        Me.ButtonUpdate.Text = "更新"
        Me.ButtonUpdate.UseVisualStyleBackColor = True
        '
        'ButtonInsert
        '
        Me.ButtonInsert.Location = New System.Drawing.Point(230, 470)
        Me.ButtonInsert.Name = "ButtonInsert"
        Me.ButtonInsert.Size = New System.Drawing.Size(150, 50)
        Me.ButtonInsert.TabIndex = 4
        Me.ButtonInsert.Text = "追加"
        Me.ButtonInsert.UseVisualStyleBackColor = True
        '
        'ButtonQuery
        '
        Me.ButtonQuery.Location = New System.Drawing.Point(50, 470)
        Me.ButtonQuery.Name = "ButtonQuery"
        Me.ButtonQuery.Size = New System.Drawing.Size(150, 50)
        Me.ButtonQuery.TabIndex = 3
        Me.ButtonQuery.Text = "抽出"
        Me.ButtonQuery.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.LabelNumId)
        Me.Controls.Add(Me.TextBoxNote)
        Me.Controls.Add(Me.TextBoxShohinName)
        Me.Controls.Add(Me.TextBoxShohinNum)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ButtonDelete)
        Me.Controls.Add(Me.ButtonUpdate)
        Me.Controls.Add(Me.ButtonInsert)
        Me.Controls.Add(Me.ButtonQuery)
        Me.Location = New System.Drawing.Point(500, 200)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.Text = "ADO.NET + デスクトップアプリ + SQL Server"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelNumId As Label
    Friend WithEvents TextBoxNote As TextBox
    Friend WithEvents TextBoxShohinName As TextBox
    Friend WithEvents TextBoxShohinNum As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ButtonDelete As Button
    Friend WithEvents ButtonUpdate As Button
    Friend WithEvents ButtonInsert As Button
    Friend WithEvents ButtonQuery As Button
    Friend WithEvents BindingSource1 As BindingSource
End Class
