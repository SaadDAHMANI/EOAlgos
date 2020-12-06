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

        Kmax = 700

        'initialize search space intevalls

        Intervals = New List(Of Interval)

        For i = 0 To (D - 1)
            Intervals.Add(New Interval(-120, 10))
        Next

        TestGSA(N, D, Intervals)
        TestGWO(N, D, Intervals)

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

        Console.WriteLine("The best solution with {0} is : ", Optimizer.AlgorithmeName)
        For i = 0 To (D - 1)
            Console.WriteLine(Optimizer.BestSolution(i))
        Next

        'Write the best score for algo :
        Console.WriteLine(String.Format("{0} best score is : {1}", Optimizer.AlgorithmeName, Optimizer.BestScore))

        Console.WriteLine("{0} - Comutation time = {1} MS", Optimizer.AlgorithmeName, Optimizer.ComputationTime)

        Console.WriteLine("End computation by {0}.", Optimizer.AlgorithmeName)

    End Sub


    Private Sub TestGWO(N As Integer, D As Integer, LUBounds As List(Of Interval))

        Optimizer = New GWO_Optimizer(N, D, LUBounds)

        AddHandler Optimizer.ObjectiveFunction, AddressOf BenchmarkFunctions.F1

        'initialize algo

        With Optimizer
            .MaxIterations = Kmax
            .OptimizationType = OptimizationTypeEnum.Minimization
            .LuanchComputation()
        End With

        Console.WriteLine("The best solution with {0} is : ", Optimizer.AlgorithmeName)
        For i = 0 To (D - 1)
            Console.WriteLine(Optimizer.BestSolution(i))
        Next

        'Write the best score for algo :
        Console.WriteLine(String.Format("{0} best score is : {1}", Optimizer.AlgorithmeName, Optimizer.BestScore))

        Console.WriteLine("{0} - Comutation time = {1} MS", Optimizer.AlgorithmeName, Optimizer.ComputationTime)

        Console.WriteLine("End computation by {0}.", Optimizer.AlgorithmeName)
    End Sub
End Module
