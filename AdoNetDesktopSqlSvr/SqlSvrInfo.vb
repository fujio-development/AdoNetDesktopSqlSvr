''' <summary>Microsoft SQLServerと接続のための設定値格納</summary>
''' <remarks></remarks>
<Serializable()>
Public Class SqlSvrInfo
    Inherits Config
#Region "フィールド"
    Private _MssqlSvHost As String
    Private _MssqlSvInstance As String
    Private _MssqlSvPort As UShort
    Private _MssqlSvCatalog As String
    Private _MssqlSvLoginMode As Boolean
    Private _MssqlSvUserID As String
    Private _MssqlSvPassword As String
    Private _MssqlSvConnectTimeout As Integer
    Private _MssqlSvMars As Boolean
#End Region

#Region "定数"
    Private Const DefaultFileName = "MsSqlServer"
#End Region

#Region "コンストラクタ"
    Public Sub New()
        _ConfigFileName = DefaultFileName
    End Sub
#End Region

#Region "プロパティ"
    ''' <summary>ホスト名(サーバー名)</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MssqlSvHost As String
        Get
            Return Me._MssqlSvHost
        End Get
        Set(ByVal value As String)
            Me._MssqlSvHost = value
        End Set
    End Property

    ''' <summary>インスタンス名</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MssqlSvInstance As String
        Get
            Return Me._MssqlSvInstance
        End Get
        Set(ByVal value As String)
            Me._MssqlSvInstance = value
        End Set
    End Property

    ''' <summary>ポート(ローカルホストの場合は自動的に無効とします)</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MssqlSvPort As UShort
        Get
            Return Me._MssqlSvPort
        End Get
        Set(ByVal value As UShort)
            Me._MssqlSvPort = value
        End Set
    End Property

    ''' <summary>データベース名</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MssqlSvCatalog As String
        Get
            Return Me._MssqlSvCatalog
        End Get
        Set(ByVal value As String)
            Me._MssqlSvCatalog = value
        End Set
    End Property

    ''' <summary>認証モード(Windows統合認証=true、SQLServer認証=false)</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MssqlSvLoginMode As Boolean
        Get
            Return Me._MssqlSvLoginMode
        End Get
        Set(ByVal value As Boolean)
            Me._MssqlSvLoginMode = value
        End Set
    End Property

    ''' <summary>SQLServer ユーザーID</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MssqlSvUserID As String
        Get
            Return Me._MssqlSvUserID
        End Get
        Set(ByVal value As String)
            Me._MssqlSvUserID = value
        End Set
    End Property

    ''' <summary>SQLServer パスワード</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MssqlSvPassword As String
        Get
            Return Me._MssqlSvPassword
        End Get
        Set(ByVal value As String)
            Me._MssqlSvPassword = value
        End Set
    End Property

    ''' <summary>オープン時の接続タイムアウト(秒単位)</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MssqlSvConnectTimeout As Integer
        Get
            Return Me._MssqlSvConnectTimeout
        End Get
        Set(ByVal value As Integer)
            Me._MssqlSvConnectTimeout = value
        End Set
    End Property

    ''' <summary>MultipleActiveResultSetsを使用するか</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MssqlSvMARS As Boolean
        Get
            Return Me._MssqlSvMars
        End Get
        Set(ByVal value As Boolean)
            Me._MssqlSvMars = value
        End Set
    End Property
#End Region

End Class