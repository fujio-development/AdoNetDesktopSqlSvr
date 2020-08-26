''' <summary>設定ファイルでアプリケーションをサポートする基底クラス</summary>
''' <remarks></remarks>
<Serializable>
Public MustInherit Class Config

#Region "フィールド"
    <NonSerialized>
    Protected _ConfigFileName As String
#End Region

#Region "プロパティ"
    Public Property ConfigFileName As String
        Get
            Return Me._ConfigFileName
        End Get
        Set(ByVal value As String)
            Me._ConfigFileName = value
        End Set
    End Property
#End Region

End Class