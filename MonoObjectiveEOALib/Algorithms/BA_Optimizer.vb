''____________________________________________________________________________________________
''Bat optimization algorithm (BA)
''
'' This code Is based On :
'' Yang, X. S. (2010). “A New Metaheuristic Bat-Inspired Algorithm, 
'' in: Nature Inspired Cooperative Strategies for Optimization (NISCO 2010)”. 
'' Studies in Computational Intelligence. 284: 65–74
'' 
'' Matlab code :  Abhishek Gupta (2020). BAT optimization Algorithm 
'' (https//www.mathworks.com/matlabcentral/fileexchange/68981-bat-optimization-algorithm), 
'' MATLAB Central File Exchange. Retrieved December 8, 2020. 
''
'' Writen in VB.NET by S.DAHMANI (sd.dahmani2000@gmail.com)
''___________________________________________________________________________________________
Public Class BA_Optimizer
    Inherits EvolutionaryAlgoBase
    Public Overrides ReadOnly Property AlgorithmName As Object
        Get
            Return "BA"
        End Get
    End Property

    Public Overrides ReadOnly Property AlgorithmFullName As Object
        Get
            Return "Bat Algorithm"
        End Get
    End Property

    Public Overrides ReadOnly Property BestSolution As Double()
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Private _BestChart As List(Of Double)
    Public Overrides ReadOnly Property BestChart As List(Of Double)
        Get
            Return _BestChart
        End Get
    End Property

    Private _WorstChart As List(Of Double)
    Public Overrides ReadOnly Property WorstChart As List(Of Double)
        Get
            Return _WorstChart
        End Get
    End Property

    Private _MeanChart As List(Of Double)
    Public Overrides ReadOnly Property MeanChart As List(Of Double)
        Get
            Return _MeanChart
        End Get
    End Property

    Public Overrides ReadOnly Property Solution_Fitness As Dictionary(Of String, Double)
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Overrides ReadOnly Property CurrentBestFitness As Double
        Get
            Throw New NotImplementedException()
        End Get
    End Property
#Region "BA variables"
    Private N, D, Iindex As Integer
    Private fitnessValue As Double
    Private Fmax As Double = 2 'maximum frequency
    Private Fmin As Double = 0 'minimum frequency
    Private Alpha As Double = 0.9 'constant for loudness update
    Private Gamma As Double = 0.9 'onstant for emission rate update
    Private R0 As Double = 0.1 'initial pulse emission rate
    Private A0 As Double = 0.9 'initial loudness

    Private A As Double() 'loudness for each BAT
    Private R As Double() 'pulse emission rate for each BAT
    Private F As Double() 'Frequency
    Private V As Double(,) 'Velocities
    Private Fitness As Double()
    Private BestSol As Double()


#End Region
    Public Overrides Sub RunEpoch()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub InitializeOptimizer()
        _BestChart = New List(Of Double)
        _MeanChart = New List(Of Double)
        _WorstChart = New List(Of Double)

        N = PopulationSize_N - 1
        D = Dimensions_D - 1

        A = New Double(N) {}
        R = New Double(N) {}
        F = New Double(N) {}
        V = New Double(N, D) {}
        Fitness = New Double(N) {}

        For i = 0 To N
            A(i) = A0
            R(i) = R0
        Next

        'Inintilize search population
        InitializePopulation()

        '
        For i As Integer = 0 To N
            fitnessValue = Double.NaN
            ComputeObjectiveFunction(Population(i), fitnessValue)
            Fitness(i) = fitnessValue
        Next

        Fmin = Fitness.Min()
        Iindex = Array.IndexOf(Fitness, Fmin)
        _BestChart.Add(Fmin)
        BestSol = Population(Iindex)

    End Sub

    Public Overrides Sub ComputeObjectiveFunction(positions() As Double, ByRef fitness_Value As Double)
        MyBase.OnObjectiveFunction(positions, fitness_Value)
    End Sub
End Class
