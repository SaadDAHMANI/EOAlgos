Imports System
Imports MonoObjectiveEOALib

Module Program

    Private Optimizer As EvolutionaryAlgoBase

    Dim D As Int32
    Dim N As Int32
    Dim Kmax As Int32
    Dim Intervals As List(Of Interval)

    Sub Main(args As String())

        'initialisation of search sapce dimension
        D = 5

        'initialisation of search agents count 
        N = 40

        Kmax = 1000

        'initialize search space intevalls

        Intervals = New List(Of Interval)

        For i = 0 To (D - 1)
            Intervals.Add(New Interval(-120, 10))
        Next

        TestGSA(N, D, Intervals)

        Console.WriteLine("Comutation time = {0} MS", Optimizer.ComputationTime)

    End Sub

    Private Sub TestGSA(N As Integer, D As Integer, LUBounds As List(Of Interval))

        Optimizer = New GSA_Optimizer(N, D, LUBounds, 100, 5)

        AddHandler Optimizer.ObjectiveFunction, AddressOf BenchmarkFunctions.F1

        'initialize algo

        With Optimizer
            .MaxIterations = Kmax
            .OptimizationType = OptimizationTypeEnum.Minimization

            .LuanchComputation()

        End With

        Console.WriteLine("The best solution with GSA is : ")
        For i = 0 To (D - 1)
            Console.WriteLine(Optimizer.BestSolution(i))
        Next

        'Write the best score for GSA :
        Console.WriteLine(String.Format("GSA best score is : {0}", Optimizer.BestScore))

        Console.WriteLine("End of computation.. GSA...")
    End Sub
End Module
