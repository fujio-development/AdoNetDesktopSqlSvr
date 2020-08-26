''' <summary>ADO.NET基底クラス</summary>
''' <remarks></remarks>
Public MustInherit Class NetDatabase
    Inherits LastException

#Region "フィールド"
    Protected _Host As String
    Protected _Instance As String
    Protected _Port As UShort
    Protected _Catalog As String
    Protected _UserID As String
    Protected _Password As String
#End Region

#Region "抽象メソッド"
    Protected MustOverride Sub ConecString()
    Public MustOverride Sub Open()
    Public MustOverride Sub Close()
    Public MustOverride Function DataAdapter(ByVal SelectSql As String) As DataTable
    Public MustOverride Function DataReader(ByVal SelectSql As String) As IEnumerable(Of IDictionary(Of String, String))
    Public MustOverride Function DataReader(Of DTO As {Class, New})(ByVal SelectSql As String) As IEnumerable(Of DTO)
    Public MustOverride Function Scalar(ByVal ScalarSql As String) As Integer
    Public MustOverride Sub ParametersClear()
    Public MustOverride Sub SetParameters(ByVal name As String, ByVal type As SqlDbType, ByVal size As Integer, ByVal value As Object)
    Public MustOverride Function NonQuery(ByVal LoSql As String) As Integer
#End Region

#Region "プロパティ"
    ''' <summary>ホスト名(サーバー名)またはIPアドレス</summary>
    ''' <returns></returns>
    ''' <remarks>サーバー／クライアント型のみ必要</remarks>
    Public Property Host As String
        Get
            Return Me._Host
        End Get
        Set(ByVal value As String)
            Me._Host = value
        End Set
    End Property

    ''' <summary>インスタンス名</summary>
    ''' <returns></returns>
    ''' <remarks>サーバー／クライアント型のみ必要</remarks>
    Public Property Instance As String
        Get
            Return Me._Instance
        End Get
        Set(ByVal value As String)
            Me._Instance = value
        End Set
    End Property

    ''' <summary>ネットワーク ポート番号</summary>
    ''' <returns></returns>
    ''' <remarks>サーバー／クライアント型のみ必要</remarks>
    Public Property Port As UShort
        Get
            Return Me._Port
        End Get
        Set(ByVal value As UShort)
            Me._Port = value
        End Set
    End Property

    ''' <summary>データベース名</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Catalog As String
        Get
            Return Me._Catalog
        End Get
        Set(ByVal value As String)
            Me._Catalog = value
        End Set
    End Property

    ''' <summary>ログインユーザー名</summary>
    ''' <returns></returns>
    ''' <remarks>OS統合認証の場合は必要ありません</remarks>
    Public Property UserID As String
        Get
            Return Me._UserID
        End Get
        Set(ByVal value As String)
            Me._UserID = value
        End Set
    End Property

    ''' <summary>ログインパスワード</summary>
    ''' <returns></returns>
    ''' <remarks>パスワード認証が無い場合は必要ありません</remarks>
    Public Property Password As String
        Get
            Return Me._Password
        End Get
        Set(ByVal value As String)
            Me._Password = value
        End Set
    End Property

#End Region

End Class