Imports System
Imports MonoObjectiveEOALib
Module Program

    Sub Main(args As String())
        Dim gsaAlgo = New GSA_Optimizer()
        AddHandler gsaAlgo.ObjectiveFunction, AddressOf F1

        'initialisation of search sapce dimension
        Dim d As Int32 = 10

        'initialize search space intevalls
        Dim intervalls As New List(Of Intervalle)
        For i = 0 To (d - 1)
            intervalls.Add(New Intervalle(-100, 100))
        Next

        'initialize algo
        With gsaAlgo
            .Agents_N = 20
            .Dimensions_D = d
            .MaxIterations =700
            .Intervalles = intervalls
            .OptimizationType = OptimizationTypeEnum.Minimization
            .LuanchComputation()
        End With

        'Write the best solution
        Console.WriteLine("The best solution is : ")
        For i = 0 To (d - 1)
            Console.WriteLine(gsaAlgo.BestSolution(i))
        Next

        'Write the best score
        Console.WriteLine(String.Format("The best score is : {0}", gsaAlgo.BestScore))


    End Sub

    ''' <summary>
    ''' Exemple of objective function
    ''' </summary>
    ''' <param name="positions"></param>
    ''' <param name="fitnessValue"></param>
    Private Sub F1(positions() As Double, ByRef fitnessValue As Double)

        fitnessValue = 0R
        For i = 0 To (positions.Length - 1)
            fitnessValue += positions(i) ^ 2 - 5
        Next

    End Sub
End Module
