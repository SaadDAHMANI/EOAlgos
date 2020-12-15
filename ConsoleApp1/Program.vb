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
        N = 50

        Kmax = 1000

        'initialize search space intevalls

        Intervals = New List(Of Interval)

        For i = 0 To (D - 1)
            Intervals.Add(New Interval(-120, 120))
        Next

        TestGSA(N, D, Intervals)
        Console.WriteLine("____________________________________________________")

        TestPSOGSA(N, D, Intervals)
        Console.WriteLine("____________________________________________________")

        TestGWO(N, D, Intervals)
        Console.WriteLine("____________________________________________________")

        TestDBA(N, D, Intervals)
        Console.WriteLine("____________________________________________________")

        TestBA(N, D, Intervals)
        Console.WriteLine("____________________________________________________")


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

        Console.WriteLine("The best solution with {0} is : ", Optimizer.AlgorithmName)
        For i = 0 To (D - 1)
            Console.WriteLine(Optimizer.BestSolution(i))
        Next

        'Write the best score for algo :
        Console.WriteLine(String.Format("{0} best score is : {1}", Optimizer.AlgorithmName, Optimizer.BestScore))

        Console.WriteLine("{0} - Comutation time = {1} MS", Optimizer.AlgorithmName, Optimizer.ComputationTime)

        Console.WriteLine("End computation by {0}.", Optimizer.AlgorithmName)

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

        Console.WriteLine("The best solution with {0} is : ", Optimizer.AlgorithmName)
        For i = 0 To (D - 1)
            Console.WriteLine(Optimizer.BestSolution(i))
        Next

        'Write the best score for algo :
        Console.WriteLine(String.Format("{0} best score is : {1}", Optimizer.AlgorithmName, Optimizer.BestScore))

        Console.WriteLine("{0} - Comutation time = {1} MS", Optimizer.AlgorithmName, Optimizer.ComputationTime)

        Console.WriteLine("End computation by {0}.", Optimizer.AlgorithmName)
    End Sub

    Private Sub TestPSOGSA(N As Integer, D As Integer, LUBounds As List(Of Interval))

        Optimizer = New PSOGSA_Optimizer(N, D, LUBounds, Kmax, 1, 25, 1.5, 1.5)

        AddHandler Optimizer.ObjectiveFunction, AddressOf BenchmarkFunctions.F1

        'initialize algo

        With Optimizer

            .MaxIterations = Kmax
            .OptimizationType = OptimizationTypeEnum.Minimization
            .LuanchComputation()
        End With

        Console.WriteLine("The best solution with {0} is : ", Optimizer.AlgorithmName)
        For i = 0 To (D - 1)
            Console.WriteLine(Optimizer.BestSolution(i))
        Next

        'Write the best score for algo :
        Console.WriteLine(String.Format("{0} best score is : {1}", Optimizer.AlgorithmName, Optimizer.BestScore))

        Console.WriteLine("{0} - Comutation time = {1} MS", Optimizer.AlgorithmName, Optimizer.ComputationTime)

        Console.WriteLine("End computation by {0}.", Optimizer.AlgorithmName)
    End Sub



    Private Sub TestDBA(N As Integer, D As Integer, LUBounds As List(Of Interval))

        Optimizer = New DBA_Optimizer(N, D, LUBounds)

        AddHandler Optimizer.ObjectiveFunction, AddressOf BenchmarkFunctions.F1

        'initialize algo

        With Optimizer
            .MaxIterations = Kmax
            .OptimizationType = OptimizationTypeEnum.Minimization
            .LuanchComputation()
        End With

        Console.WriteLine("The best solution with {0} is : ", Optimizer.AlgorithmName)
        For i = 0 To (D - 1)
            Console.WriteLine(Optimizer.BestSolution(i))
        Next

        'Write the best score for algo :
        Console.WriteLine(String.Format("{0} best score is : {1}", Optimizer.AlgorithmName, Optimizer.BestScore))

        Console.WriteLine("{0} - Comutation time = {1} MS", Optimizer.AlgorithmName, Optimizer.ComputationTime)

        Console.WriteLine("End computation by {0}.", Optimizer.AlgorithmName)
    End Sub

    Private Sub TestBA(N As Integer, D As Integer, LUBounds As List(Of Interval))

        Optimizer = New BA_Optimizer(N, D, LUBounds)

        AddHandler Optimizer.ObjectiveFunction, AddressOf BenchmarkFunctions.F1

        'initialize algo

        With Optimizer
            .MaxIterations = Kmax
            .OptimizationType = OptimizationTypeEnum.Minimization
            .LuanchComputation()
        End With

        Console.WriteLine("The best solution with {0} is : ", Optimizer.AlgorithmName)
        For i = 0 To (D - 1)
            Console.WriteLine(Optimizer.BestSolution(i))
        Next

        'Write the best score for algo :
        Console.WriteLine(String.Format("{0} best score is : {1}", Optimizer.AlgorithmName, Optimizer.BestScore))

        Console.WriteLine("{0} - Comutation time = {1} MS", Optimizer.AlgorithmName, Optimizer.ComputationTime)

        Console.WriteLine("End computation by {0}.", Optimizer.AlgorithmName)
    End Sub

End Module
