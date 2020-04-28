Imports System
Imports MonoObjectiveEOALib
Module Program

    Sub Main(args As String())
        Dim gsaAlgo = New GSA_Optimizer()
        AddHandler gsaAlgo.ObjectiveFunctionComputation, AddressOf BenchmarkFunctions.F0

        'initialisation of search sapce dimension
        Dim d As Int32 = 10

        'initialize search space intevalls
        Dim intervalls As New List(Of Intervalle)
        For i = 0 To (d - 1)
            intervalls.Add(New Intervalle(-10, 10))
        Next

        'initialize algo
        With gsaAlgo
            .Agents_N = 50
            .Dimensions_D = d
            .MaxIterations =2000
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

Console.WriteLine("End of computation.....")
    End Sub
End Module
