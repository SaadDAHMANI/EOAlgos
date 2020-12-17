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
        D = 30

        'initialisation of search agents count 
        N = 30

        Kmax = 5000

        'initialize search space intevalls

        Intervals = New List(Of Interval)

        For i = 0 To (D - 1)
            Intervals.Add(New Interval(-15, 15))
        Next

        'TestGSA(N, D, Intervals)
        Console.WriteLine("____________________________________________________")

        TestPSOGSA(N, D, Intervals)

        Console.WriteLine("____________________________________________________")

        'TestGWO(N, D, Intervals)
        Console.WriteLine("____________________________________________________")

        TestDBA(N, D, Intervals)
        Console.WriteLine("____________________________________________________")

        'TestBA(N, D, Intervals)
        Console.WriteLine("____________________________________________________")

    End Sub

    Private Sub Luanch_ShowOptimizerResults(optimEngine As IEvolutionaryAlgo)

        'initialize algo
        With Optimizer
            .MaxIterations = Kmax
            .OptimizationType = OptimizationTypeEnum.Minimization
            .Compute()
        End With

        Console.WriteLine("The best solution with {0} is : ", optimEngine.AlgorithmName)
        For Each pValue In optimEngine.BestSolution
            Console.WriteLine(pValue)
        Next

        'Write the best score for algo :
        Console.WriteLine(String.Format("{0} best score is : {1}", optimEngine.AlgorithmName, Optimizer.BestScore))

        Console.WriteLine("{0} - Comutation time = {1} MS", optimEngine.AlgorithmName, Optimizer.ComputationTime)

        Console.WriteLine("End computation by {0}.", optimEngine.AlgorithmName)

        Debug.Print("Algo = {0}, --> Best Score = {1}", optimEngine.AlgorithmName, Optimizer.BestScore)

    End Sub

    Private Sub TestGSA(N As Integer, D As Integer, LUBounds As List(Of Interval))

        Optimizer = New GSA_Optimizer(N, D, LUBounds, 100, 5)

        AddHandler Optimizer.ObjectiveFunction, AddressOf BenchmarkFunctions.F2

        Luanch_ShowOptimizerResults(Optimizer)

    End Sub

    Private Sub TestGWO(N As Integer, D As Integer, LUBounds As List(Of Interval))

        Optimizer = New GWO_Optimizer(N, D, LUBounds)

        AddHandler Optimizer.ObjectiveFunction, AddressOf BenchmarkFunctions.F1

        Luanch_ShowOptimizerResults(Optimizer)

    End Sub

    Private Sub TestPSOGSA(N As Integer, D As Integer, LUBounds As List(Of Interval))

        Optimizer = New PSOGSA_Optimizer(N, D, LUBounds)

        AddHandler Optimizer.ObjectiveFunction, AddressOf BenchmarkFunctions.F1

        Luanch_ShowOptimizerResults(Optimizer)

    End Sub

    Private Sub TestDBA(N As Integer, D As Integer, LUBounds As List(Of Interval))

        Optimizer = New DBA_Optimizer(N, D, LUBounds)

        AddHandler Optimizer.ObjectiveFunction, AddressOf BenchmarkFunctions.F1

        Luanch_ShowOptimizerResults(Optimizer)

    End Sub

    Private Sub TestBA(N As Integer, D As Integer, LUBounds As List(Of Interval))

        Optimizer = New BA_Optimizer(N, D, LUBounds)

        AddHandler Optimizer.ObjectiveFunction, AddressOf BenchmarkFunctions.F1

        Luanch_ShowOptimizerResults(Optimizer)

    End Sub

End Module
