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

    Public Sub New()
    End Sub
    Public Sub New(populationSize As Integer, searchSpaceDimension As Integer, searchSpaceIntervals As List(Of Interval))
        PopulationSize_N = populationSize
        Dimensions_D = searchSpaceDimension
        SearchIntervals = searchSpaceIntervals
        InitializePopulation()
    End Sub

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

    Private _BestSolution As Double()
    Public Overrides ReadOnly Property BestSolution As Double()
        Get
            Return _BestSolution
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

    Private _CurrentBestFitness As Double
    Public Overrides ReadOnly Property CurrentBestFitness As Double
        Get
            Return _CurrentBestFitness
        End Get
    End Property

#Region "BA_params"
    Public Property R0_Param As Double
        Get
            Return R0
        End Get
        Set(value As Double)
            If (value >= 0) AndAlso (value <= 1) Then
                R0 = value
            Else
                R0 = 0.1
                Throw New Exception("R0 must in [0, 1].")
            End If
        End Set
    End Property

    Public Property A0_Param As Double
        Get
            Return A0
        End Get
        Set(value As Double)
            If (value >= 0) AndAlso (value <= 2) Then
                A0 = value
            Else
                A0 = 0.9
                Throw New Exception("A0 must in [0, 2].")
            End If
        End Set
    End Property

    Public Property Alpha_Param As Double
        Get
            Return Alpha
        End Get
        Set(value As Double)
            If (value > 0) AndAlso (value < 1) Then
                Alpha = value
            Else
                Alpha = 0.9
                Throw New Exception("Alpha must in ]0, 1[.")
            End If
        End Set
    End Property

    Public Property Gamma_Param As Double
        Get
            Return Gamma
        End Get
        Set(value As Double)
            If value > 0 Then
                Gamma = value
            Else
                Gamma = 0.1R
                Throw New Exception("Gamma must be >0.")
            End If
        End Set
    End Property

    Public Property Fmin_Param As Double
        Get
            Return Fmin
        End Get
        Set(value As Double)
            Fmin = value
        End Set
    End Property

    Public Property Fmax_Param As Double
        Get
            Return Fmax
        End Get
        Set(value As Double)
            Fmax = value
        End Set
    End Property
#End Region

#Region "BA variables"
    Private N, D, Iindex As Integer
    Private fitnessValue, fitnessNew As Double
    Private Fmax As Double = 2 'maximum frequency
    Private Fmin As Double = 0 'minimum frequency
    Private Alpha As Double = 0.9 'constant for loudness update
    Private Gamma As Double = 0.9 'onstant for emission rate update
    Private R0 As Double = 0.1 'initial pulse emission rate
    Private A0 As Double = 0.9 'initial loudness
    Private eps As Double

    Private A As Double() 'loudness for each BAT
    Private R As Double() 'pulse emission rate for each BAT
    Private F As Double() 'Frequency
    Private V As Double(,) 'Velocities
    Private Fitness As Double()
    Private BestSol As Double()


#End Region
    Public Overrides Sub RunEpoch()

        If CurrentIteration = 1 Then
            InitializeOptimizer()
        End If

        For i = 0 To N
            F(i) = Fmin + ((Fmax - Fmin) * RandomGenerator.NextDouble())  'randomly chose the frequency

            For j = 0 To D
                V(i, j) = V(i, j) + (Population(i)(j) - BestSol(j) * F(i)) 'update the velocity
            Next

            For j = 0 To D
                Population(i)(j) = Population(i)(j) + V(i, j) 'update the BAT position
            Next

            'Apply simple bounds/limits
            For j = 0 To D
                If Population(i)(j) < SearchIntervals(j).Min_Value Then
                    Population(i)(j) = SearchIntervals(j).Min_Value
                End If

                If Population(i)(j) > SearchIntervals(j).Max_Value Then
                    Population(i)(j) = SearchIntervals(j).Max_Value
                End If
            Next

            'Check the condition with R

            If RandomGenerator.NextDouble() > R(i) Then
                eps = -1 + 2 * RandomGenerator.NextDouble()
                For j = 0 To D
                    Population(i)(j) = BestSol(j) + (eps * A.Average())
                Next
            End If

            'Calculate the objective function
            fitnessValue = Double.NaN
            ComputeObjectiveFunction(Population(i), fitnessValue)
            fitnessNew = fitnessValue

            'Update if the solution improves, or not too loud
            If (fitnessNew <= Fitness(i)) AndAlso (RandomGenerator.NextDouble() < A(i)) Then
                Fitness(i) = fitnessNew
                A(i) = Alpha * A(i)
                R(i) = R0 * (1 - Math.Exp(-1 * Gamma * CurrentIteration))
            End If

            If (fitnessNew <= Fmin) Then
                BestSol = Population(i)
                Fmin = fitnessNew
            End If
        Next

        _BestChart.Add(Fmin)
        _CurrentBestFitness = Fmin
        _BestSolution = BestSol
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

        ' Compute fitnesses
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
