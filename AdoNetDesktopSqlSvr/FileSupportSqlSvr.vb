Imports System.IO
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Imports System.Reflection

''' <summary>Microsoft SQLServerに接続するための設定値を外部ファイルから取得する支援クラス</summary>
''' <remarks>参照の追加
''' System.Xml.dll
''' System.Runtime.Serialization.dll</remarks>
Public Class FileSupportSqlSvr
    Inherits DatapSqlSvr

#Region "フィールド"
    Private mMssqlSvr As SqlSvrInfo
    Private iHoldPath As String
    Private iHoldFile As String
#End Region

#Region "コンストラクタ"
    Public Sub New()

        mMssqlSvr = New SqlSvrInfo()

    End Sub
#End Region

#Region "メソッド"

    ''' <summary>Microsoft SQLServerに外部設定ファイルを使って接続します</summary>
    ''' <param name="pSetPath">外部設定ファイルのフォルダ階層</param>
    ''' <param name="pSetFile">外部設定ファイルのフォルダ名</param>
    ''' <returns>True=OK、False=ファイル無しエラー</returns>
    Public Overloads Function Open(ByVal pSetPath As String, ByVal pSetFile As String) As Boolean

        Dim success As Boolean = True

        iHoldPath = pSetPath
        iHoldFile = pSetFile
        If File.Exists(pSetPath & pSetFile) Then
            Deserialize()
            AccessorSet()
            ConecString()
            GoOpen()
        Else
            success = False
        End If

        Return success

    End Function

    ''' <summary>標準的な内容で目的のファイルを作成します
    ''' <para>※Openメソッドを実行した引数で作成</para></summary>
    ''' <returns>True=OK、False=ファイル無しエラー</returns>
    Public Overloads Function Create() As Boolean

        Return Create(iHoldPath, iHoldFile)

    End Function

    ''' <summary>標準的な内容で目的のファイルを作成します</summary>
    ''' <returns>True=OK、False=ファイル無しエラー</returns>
    Public Overloads Function Create(ByVal pSetPath As String, ByVal pSetFile As String) As Boolean

        Dim success As Boolean = True

        iHoldPath = pSetPath
        iHoldFile = pSetFile
        If File.Exists(pSetPath & pSetFile) Then
            SerializeDefaultData()
            Serialize()
        Else
            success = False
        End If

        Return success

    End Function

    ''' <summary>外部設定ファイルからの設定値を読み込みます</summary>
    ''' <remarks></remarks>
    Private Sub AccessorSet()

        _Host = mMssqlSvr.MssqlSvHost
        _Instance = mMssqlSvr.MssqlSvInstance
        _Port = mMssqlSvr.MssqlSvPort
        _Catalog = mMssqlSvr.MssqlSvCatalog
        _LoginMode = mMssqlSvr.MssqlSvLoginMode
        _UserID = mMssqlSvr.MssqlSvUserID
        _Password = mMssqlSvr.MssqlSvPassword
        _ConnectTimeout = mMssqlSvr.MssqlSvConnectTimeout
        _MultipleActiveResultSets = mMssqlSvr.MssqlSvMARS

    End Sub

    ''' <summary>外部設定ファイルが無い場合、標準的な内容で作成するための値を書き込みます</summary>
    ''' <remarks></remarks>
    Private Sub SerializeDefaultData()

        mMssqlSvr = New SqlSvrInfo() With
        {
            .MssqlSvHost = "(local)",
            .MssqlSvInstance = "SQLEXPRESS",
            .MssqlSvPort = 1433,
            .MssqlSvCatalog = "TestDatabase",
            .MssqlSvLoginMode = True,
            .MssqlSvUserID = "sa",
            .MssqlSvPassword = "sapassword",
            .MssqlSvConnectTimeout = 15,
            .MssqlSvMARS = True
        }

    End Sub

    ''' <summary>XMLファイルへシリアル化し標準的な内容で書き込みます</summary>
    ''' <remarks></remarks>
    Private Sub Serialize()

        Dim XmSerializer As XmlSerializer

        XmSerializer = New XmlSerializer(mMssqlSvr.GetType())
        Using Stream = New FileStream(iHoldPath & iHoldFile, FileMode.Create, FileAccess.Write)
            XmSerializer.Serialize(Stream, mMssqlSvr)
        End Using

    End Sub

    ''' <summary>XMLファイルを逆シリアル化し読み込みます</summary>
    ''' <remarks></remarks>
    Private Sub Deserialize()

        Dim XmSerializer As XmlSerializer

        XmSerializer = New XmlSerializer(GetType(SqlSvrInfo))
        Using Stream As New FileStream(iHoldPath & iHoldFile, FileMode.Open, FileAccess.Read)
            mMssqlSvr = CType(XmSerializer.Deserialize(Stream), SqlSvrInfo)
        End Using

    End Sub
#End Region

End Class