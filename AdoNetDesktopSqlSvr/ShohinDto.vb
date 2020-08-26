''' <summary>Data Transfer Object(DTO)クラス</summary>
''' <remarks></remarks>
Public Class ShohinDto

#Region "フィールド"
    Private _NumId As Integer
    Private _ShohinNum As Short
    Private _ShohinName As String
    Private _EditDate As Decimal
    Private _EditTime As Decimal
    Private _Note As String
#End Region

#Region "プロパティ"
    Public Property NumId As Integer
        Get
            Return _NumId
        End Get
        Set(ByVal value As Integer)
            _NumId = value
        End Set
    End Property

    Public Property ShohinNum As Short
        Get
            Return _ShohinNum
        End Get
        Set(ByVal value As Short)
            _ShohinNum = value
        End Set
    End Property

    Public Property ShohinName As String
        Get
            Return _ShohinName
        End Get
        Set(ByVal value As String)
            _ShohinName = value
        End Set
    End Property

    Public Property EditDate As Decimal
        Get
            Return _EditDate
        End Get
        Set(ByVal value As Decimal)
            _EditDate = value
        End Set
    End Property

    Public Property EditTime As Decimal
        Get
            Return _EditTime
        End Get
        Set(ByVal value As Decimal)
            _EditTime = value
        End Set
    End Property

    Public Property Note As String
        Get
            Return _Note
        End Get
        Set(ByVal value As String)
            _Note = value
        End Set
    End Property
#End Region

End Class