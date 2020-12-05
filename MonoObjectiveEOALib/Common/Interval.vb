Public Class Interval
    Public Sub New()
    End Sub

    Public Sub New(min As Double, max As Double)
        Min_Value = min
        Max_Value = max
    End Sub

    Public Sub New(name As String, min As Double, max As Double)
        Me.Name = name
        Min_Value = min
        Max_Value = max
    End Sub

    Public Min_Value As Double
    Public Max_Value As Double

    Public Property Name As String = "Search Space Interval"
End Class
