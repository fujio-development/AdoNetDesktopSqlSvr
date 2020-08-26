Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Net
''' <summary>/*** .NET Framework Data Provider for SQLServer ***/
''' DAO(Datta Access Objcet)クラス</summary>
''' <remarks></remarks>
Public Class DatapSqlSvr
    Inherits NetDatabase
    Implements IDisposable

#Region "フィールド"
    Protected _Conec As SqlConnection
    Protected _ConecSb As SqlConnectionStringBuilder
    Private PrReader As SqlDataReader
    Private PrCmd As SqlCommand
    Private PrTrans As SqlTransaction
    Private _DataSource As String
    Protected _LoginMode As Boolean
    Protected _ConnectTimeout As Integer
    Protected _MultipleActiveResultSets As Boolean
#End Region

#Region "構造体"
    ''' <summary>構造体引数での接続パラメータをセット</summary>
    ''' <remarks></remarks>
    Public Structure pPropertySet
        Public pHost As String
        Public pInstance As String
        Public pPort As UShort
        Public pCatalog As String
        Public pLoginMode As Boolean
        Public pUserID As String
        Public pPassword As String
        Public pConnectTimeout As Integer
        Public pMultipleActiveResultSets As Boolean
    End Structure
#End Region

#Region "コンストラクタ"
    Public Sub New()

        _Conec = New SqlConnection()
        _ConecSb = New SqlConnectionStringBuilder()
        PrCmd = New SqlCommand()

    End Sub
#End Region

#Region "メソッド"
    ''' <summary>ConnectionString設定</summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConecString()

        Dim LoHostStr() As String = {Dns.GetHostName(), "(local)", "127.0.0.1", ".", "localhost"}
        Dim LoHostCheck As Boolean = False

        '接続先がローカルかチェック
        Dim Ck As Integer = Array.IndexOf(LoHostStr, _Host)
        If Ck >= 0 Then '-1で無いなら(存在するなら)
            LoHostCheck = True
        End If

        '接続先の代入
        If LoHostCheck = True Then  'ローカルなら
            If _Instance = String.Empty Or _Instance = "" Then
                '既定のインスタンス
                _DataSource = _Host
            Else
                '名前付きのインスタンスの場合
                _DataSource = _Host & "\" & _Instance
            End If
        Else
            If _Instance = String.Empty Or _Instance = "" Then
                _DataSource = _Host & "," & _Port
            Else
                '名前付きインスタンス、固定ポートの場合
                _DataSource = _Host & "\" & _Instance & "," & _Port
            End If
        End If

        '**ConnectionStringに代入**
        With _ConecSb
            .DataSource = _DataSource
            .InitialCatalog = _Catalog
            .IntegratedSecurity = _LoginMode
            If .IntegratedSecurity = False Then 'SQLServer認証の場合
                .UserID = _UserID
                .Password = _Password
            End If
            .ConnectTimeout = _ConnectTimeout '秒単位(規定値15秒)
            .MultipleActiveResultSets = _MultipleActiveResultSets
        End With

    End Sub

    Protected Sub GoOpen()

        Call ConecString()
        If _Conec.State = ConnectionState.Closed Then
            _Conec.ConnectionString = _ConecSb.ConnectionString
            _Conec.Open()
        End If

    End Sub

    Private Function GoAdapter(ByVal SelectSql As String) As DataTable

        Dim SelectAdapter As New SqlDataAdapter()
        Dim SelectTable As New DataTable()

        Call ConecString()
        If _Conec.State = ConnectionState.Closed Then
            _Conec.ConnectionString = _ConecSb.ConnectionString
        End If
        Using SelectCmd = New SqlCommand()
            SelectCmd.Connection = _Conec
            SelectCmd.CommandText = SelectSql
            SelectAdapter.SelectCommand = SelectCmd
            SelectAdapter.Fill(SelectTable)   'Open/Close自動
            SelectAdapter.Dispose()
        End Using

        Return SelectTable

    End Function

    ''' <summary>Microsoft SQLServerに接続します(外部設定ファイルを使わずに接続)
    ''' <para>※プロパティ値を基に接続します。最低でも事前にHost、Instance、Catalog、LoginModeプロパティに値を代入して下さい</para></summary>
    ''' <remarks></remarks>
    Public Overrides Sub Open()

        GoOpen()

    End Sub

    ''' <summary>Microsoft SQLServerに接続します(外部設定ファイルを使わずに接続)
    ''' <para>※このメソッドの構造体引数で接続します</para></summary>
    ''' <param name="pSettei">接続文字列のパラメータ(構造体)</param>
    Public Overloads Sub Open(ByVal pSettei As pPropertySet)

        Dim success As Boolean = True

        _Host = pSettei.pHost
        _Instance = pSettei.pInstance
        _Port = pSettei.pPort
        _Catalog = pSettei.pCatalog
        _LoginMode = pSettei.pLoginMode
        _UserID = pSettei.pUserID
        _Password = pSettei.pPassword
        _ConnectTimeout = pSettei.pConnectTimeout
        _MultipleActiveResultSets = pSettei.pMultipleActiveResultSets

        GoOpen()

    End Sub

    Private Sub GoDispose()

        If Not PrReader Is Nothing Then
            PrReader.Close()
            PrReader.Dispose()
            PrReader = Nothing
        End If

        If Not PrTrans Is Nothing Then
            PrTrans.Dispose()
            PrTrans = Nothing
        End If

        If Not PrCmd Is Nothing Then
            PrCmd.Dispose()
            PrCmd = Nothing
        End If

        If Not _Conec Is Nothing And _Conec.State = ConnectionState.Open Then
            _Conec.Close()
            _Conec.Dispose()
            _Conec = Nothing
        End If

    End Sub

    ''' <summary>Microsoft SQLServerに接続しているデータベースを閉じます
    ''' <para>IDisposableインターフェース実装メソッド</para></summary>
    Public Sub Dispose() Implements IDisposable.Dispose

        Call GoDispose()

    End Sub

    ''' <summary>Microsoft SQLServerに接続しているデータベースを閉じます</summary>
    ''' <remarks></remarks>
    Public Overrides Sub Close()

        GoDispose()

    End Sub

    ''' <summary>与えられたSELECT文でSqlDataAdapterクラスで読み取り、DataTableで返します(外部設定ファイルを使わずに接続)
    ''' <para>※プロパティ値を基に接続します。最低でも事前にHost、Instance、Catalog、LoginModeプロパティに値を代入して下さい</para></summary>
    ''' <param name="SelectSql">SELECT文</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Overrides Function DataAdapter(ByVal SelectSql As String) As DataTable

        Dim SelectTable As New DataTable()

        SelectTable = GoAdapter(SelectSql)

        Return SelectTable

    End Function

    ''' <summary>与えられたSELECT文でSqlDataAdapterクラスで読み取り、DataTableで返します(外部設定ファイルを使わずに接続)
    ''' <para>※このメソッドの構造体引数で接続します</para></summary>
    ''' <param name="SelectSql">SELECT文</param>
    ''' <param name="pSettei">接続文字列のパラメータ(構造体)</param>
    ''' <returns>DataTable</returns>
    Public Overloads Function DataAdapter(ByVal SelectSql As String, ByVal pSettei As pPropertySet) As DataTable

        Dim SelectTable As New DataTable()

        _Host = pSettei.pHost
        _Instance = pSettei.pInstance
        _Port = pSettei.pPort
        _Catalog = pSettei.pCatalog
        _LoginMode = pSettei.pLoginMode
        _UserID = pSettei.pUserID
        _Password = pSettei.pPassword
        _ConnectTimeout = pSettei.pConnectTimeout
        _MultipleActiveResultSets = pSettei.pMultipleActiveResultSets
        SelectTable = GoAdapter(SelectSql)

        Return SelectTable

    End Function

    ''' <summary>与えられたSELECT文でSqlCommandクラスのExecuteReaderメソッドを実行しIEnumerable(Of IDictionary(Of 列名, 値))で返します</summary>
    ''' <param name="SelectSql">SELECT文</param>
    ''' <returns>IEnumerable(Of IDictionary(Of String, String))</returns>
    Public Overrides Function DataReader(SelectSql As String) As IEnumerable(Of IDictionary(Of String, String))

        Dim LoList As New List(Of Dictionary(Of String, String))()
        Dim LoDic As Dictionary(Of String, String)

        Using LoCmd = New SqlCommand()
            LoCmd.Connection = _Conec
            LoCmd.CommandText = SelectSql
            Using LoReader As SqlDataReader = LoCmd.ExecuteReader()
                If LoReader.HasRows Then
                    LoList.Clear()
                    Do While LoReader.Read()
                        LoDic = New Dictionary(Of String, String)()
                        LoDic.Clear()
                        For r As Short = 0 To LoReader.FieldCount - 1
                            LoDic.Add(LoReader.GetName(r), LoReader(LoReader.GetName(r)))
                        Next r
                        LoList.Add(LoDic)
                    Loop
                End If
            End Using
        End Using

        Return LoList

    End Function

    ''' <summary>与えられたSELECT文でSqlCommandクラスのExecuteReaderメソッドを実行しIEnumerable(Of T型)で返します</summary>
    ''' <typeparam name="DTO">ジェネリクスT型</typeparam>
    ''' <param name="SelectSql">SELECT文</param>
    ''' <returns>IEnumerable(Of DTO)</returns>
    ''' <remarks></remarks>
    Public Overrides Function DataReader(Of DTO As {Class, New})(ByVal SelectSql As String) As IEnumerable(Of DTO)

        Dim LoList As New List(Of DTO)
        Dim mycls As DTO
        Dim props As List(Of System.Reflection.PropertyInfo)
        props = GetType(DTO).GetProperties().ToList()

        Using LoCmd = New SqlCommand()
            LoCmd.Connection = _Conec
            LoCmd.CommandText = SelectSql
            Using LoReader As SqlDataReader = LoCmd.ExecuteReader()
                If LoReader.HasRows Then
                    LoList.Clear()
                    Do While LoReader.Read()
                        mycls = New DTO()
                        For Each fld As Reflection.PropertyInfo In props
                            fld.SetValue(mycls, LoReader(fld.Name))
                        Next fld
                        LoList.Add(mycls)
                    Loop
                End If
            End Using
        End Using

        Return LoList

    End Function

    ''' <summary>与えられたSELECT文でSqlCommandクラスのExecuteReaderメソッドを実行しDataReaderで返します</summary>
    ''' <param name="SelectSql">SELECT文</param>
    ''' <returns>SqlDataReader</returns>
    Public Function DataReaderDirect(ByVal SelectSql As String) As SqlDataReader

        Using SelectCmd = New SqlCommand()
            SelectCmd.Connection = _Conec
            SelectCmd.CommandText = SelectSql
            PrReader = SelectCmd.ExecuteReader()
        End Using

        Return PrReader

    End Function

    ''' <summary>SqlDataReaderを閉じます</summary>
    ''' <remarks></remarks>
    Public Sub DrClose()

        PrReader.Close()

    End Sub

    ''' <summary>与えられたSELECT文でSqlCommandクラスのExecuteScalarメソッドを実行しIntegerで返します</summary>
    ''' <param name="ScalarSql">SELECT COUNT(*)文</param>
    ''' <returns>Integer</returns>
    ''' <remarks></remarks>
    Public Overrides Function Scalar(ByVal ScalarSql As String) As Integer

        Dim ScalarCnt As Integer = 0

        Using ScalarCmd = New SqlCommand()
            ScalarCmd.Connection = _Conec
            ScalarCmd.CommandText = ScalarSql
            If ScalarCmd.ExecuteScalar() Is DBNull.Value Then
                ScalarCnt = 0 '1件もない場合は0を代入。
            Else
                'ExecuteScalar()はオブジェクトなので数値に変換
                ScalarCnt = Convert.ToInt32(ScalarCmd.ExecuteScalar())
            End If
        End Using

        Return ScalarCnt

    End Function

    ''' <summary>クラス内のSqlCommandクラスのパラメータをクリアーします</summary>
    ''' <remarks></remarks>
    Public Overrides Sub ParametersClear()

        PrCmd.Parameters.Clear()

    End Sub

    ''' <summary>クラス内のSqlCommandによるパラメータの作成と値を代入します</summary>
    ''' <param name="name">パラメータ名</param>
    ''' <param name="type">パラメータ型</param>
    ''' <param name="size">サイズ</param>
    ''' <param name="value">値</param>
    Public Overrides Sub SetParameters(ByVal name As String, ByVal type As SqlDbType, ByVal size As Integer, ByVal value As Object)

        PrCmd.Parameters.Add(name, type, size)
        PrCmd.Parameters(name).Value = value

    End Sub

    ''' <summary>与えられた更新系コマンド文でクラス内のSqlCommandクラスによるExecuteNonQueryメソッドを実行します</summary>
    ''' <param name="LoSql">INSERT,UPDATE,DELETE,CREATE,ALTER,DROP文</param>
    ''' <returns>更新による影響を受けた件数</returns>
    ''' <remarks></remarks>
    Public Overrides Function NonQuery(ByVal LoSql As String) As Integer

        Dim cnt As Integer = 0

        PrTrans = _Conec.BeginTransaction()
        PrCmd.Connection = _Conec
        PrCmd.CommandText = LoSql
        PrCmd.Transaction = PrTrans
        Try
            cnt = PrCmd.ExecuteNonQuery()
        Catch ex As SqlException
            PrTrans.Rollback()
            Throw
        End Try
        PrTrans.Commit()

        Return cnt

    End Function

    ''' <summary>与えられた更新系コマンド文でSqlCommandクラスのExecuteNonQueryメソッドを実行します</summary>
    ''' <param name="LoSql">INSERT,UPDATE,DELETE,CREATE,ALTER,DROP文</param>
    ''' <param name="LoCmd">SqlCommand</param>
    ''' <param name="LoTrans">SqlTransaction</param>
    ''' <returns>更新による影響を受けた件数</returns>
    ''' <remarks></remarks>
    Public Overloads Function NonQuery(ByVal LoSql As String, ByVal LoCmd As SqlCommand, ByVal LoTrans As SqlTransaction) As Integer

        Dim cnt As Integer = 0

        LoCmd.Connection = _Conec
        LoCmd.CommandText = LoSql
        LoCmd.Transaction = LoTrans
        Try
            cnt = LoCmd.ExecuteNonQuery()
        Finally
            LoCmd.Dispose()
        End Try

        Return cnt

    End Function

    ''' <summary>BeginTransactionメソッドでトランザクションを開始し、SqlTransactionで返します</summary>
    ''' <returns>SqlTransaction</returns>
    ''' <remarks></remarks>
    Public Function GoTransaction() As SqlTransaction

        Return _Conec.BeginTransaction()

    End Function

    ''' <summary>与えられたトランザクションをコミットします</summary>
    ''' <param name="Trans">開始されているトランザクション</param>
    Public Sub TransactionCommit(ByVal Trans As SqlTransaction)

        Trans.Commit()

    End Sub

    ''' <summary>与えられたトランザクションをロールバックします</summary>
    ''' <param name="Trans">開始されているトランザクション</param>
    Public Sub TransactionRollback(ByVal Trans As SqlTransaction)

        Trans.Rollback()

    End Sub
#End Region

#Region "プロパティ"
    ''' <summary>SqlConnection読み出し</summary>
    ''' <returns></returns>
    Public ReadOnly Property Conec As SqlConnection
        Get
            Return Me._Conec
        End Get
    End Property

    ''' <summary>SqlConnectionStringBuilder読み出し</summary>
    ''' <returns></returns>
    Public ReadOnly Property ConecSb As SqlConnectionStringBuilder
        Get
            Return Me._ConecSb
        End Get
    End Property

    ''' <summary>サーバー名\インスタンス名</summary>
    ''' <returns></returns>
    Public ReadOnly Property DataSource As String
        Get
            Return Me._DataSource
        End Get
    End Property

    ''' <summary>Windows統合認証=True、SQLServer認証=False</summary>
    ''' <returns></returns>
    Public Property LoginMode As Boolean
        Get
            Return Me._LoginMode
        End Get
        Set(ByVal value As Boolean)
            Me._LoginMode = value
        End Set
    End Property

    ''' <summary>SQLServer接続タイムアウト設定(秒)</summary>
    ''' <returns></returns>
    Public Property ConnectTimeout As Integer
        Get
            Return Me._ConnectTimeout
        End Get
        Set(ByVal value As Integer)
            Me._ConnectTimeout = value
        End Set
    End Property

    ''' <summary>SQLServerでMultipleActiveResultSetsを使用する=True、使用しない=False</summary>
    ''' <returns></returns>
    Public Property MultipleActiveResultSets As Boolean
        Get
            Return Me._MultipleActiveResultSets
        End Get
        Set(ByVal value As Boolean)
            Me._MultipleActiveResultSets = value
        End Set
    End Property
#End Region

End Class