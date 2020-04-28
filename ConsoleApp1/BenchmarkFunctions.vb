Module BenchmarkFunctions

Public Sub F0(positions() As Double, ByRef fitnessValue As Double)

        fitnessValue = 0R
        For i = 0 To (positions.Length - 1)
            fitnessValue += positions(i)
        Next

    End Sub


    ''' <summary>
    ''' Exemple of objective function
    ''' </summary>
    ''' <param name="positions"></param>
    ''' <param name="fitnessValue"></param>
    Public Sub F1(positions() As Double, ByRef fitnessValue As Double)

        fitnessValue = 0R
        For i = 0 To (positions.Length - 1)
            fitnessValue += positions(i) ^ 2
        Next

    End Sub

    Public Sub F2(positions() As Double, ByRef fitnessValue As Double)

        fitnessValue = 0R
        For i = 0 To (positions.Length - 1)
            fitnessValue += positions(i) ^ 3
        Next

    End Sub



End Module
