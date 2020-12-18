Public Class RGA_Optimizer
    Inherits EvolutionaryAlgoBase

    Public Overrides ReadOnly Property AlgorithmName As Object
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Overrides ReadOnly Property AlgorithmFullName As Object
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Overrides ReadOnly Property BestSolution As Double()
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Overrides ReadOnly Property BestChart As List(Of Double)
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Overrides ReadOnly Property WorstChart As List(Of Double)
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Overrides ReadOnly Property MeanChart As List(Of Double)
        Get
            Throw New NotImplementedException()
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
#Region "Properties"

    Private _MutationFrequency As Single = 10.0F
    ''' <summary>
    ''' Mutation frequency in the range [0, 100[.
    ''' </summary>
    ''' <returns></returns>
    Public Property MutationFrequency As Single
        Get
            Return _MutationFrequency
        End Get
        Set(value As Single)
            _MutationFrequency = Math.Max(0, value)
            _MutationFrequency = Math.Min(_MutationFrequency, 100)
        End Set
    End Property


    Private _PopulationLimit As Integer
    Public Property PopulationLimit As Integer
        Get
            Return _PopulationLimit
        End Get
        Set(value As Integer)
            _PopulationLimit = Math.Max(2, value)
        End Set
    End Property
#End Region

#Region "Private_variables"
    Private CrossoverPoint As Integer
    Private MutationIndex As Integer
#End Region

    Public Overrides Sub RunEpoch()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub InitializeOptimizer()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub ComputeObjectiveFunction(positions() As Double, ByRef fitness_Value As Double)
        MyBase.OnObjectiveFunction(positions, fitness_Value)
    End Sub


End Class
