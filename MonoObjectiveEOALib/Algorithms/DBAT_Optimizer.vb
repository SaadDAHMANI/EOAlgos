Public Class DBAT_Optimizer
    Inherits EvolutionaryAlgoBase

    Public Overrides ReadOnly Property AlgorithmName As Object
        Get
            Return "DBA" 
        End Get
    End Property

    Public Overrides ReadOnly Property AlgorithmFullName As Object
        Get
         Return "Directional Bat Algorithm"  
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

#Region "DBA params" 

#End Region


    Public Overrides Sub RunEpoch()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub InitializeOptimizer()
     
    End Sub

    Public Overrides Sub ComputeObjectiveFunction(positions() As Double, ByRef fitnessValue As Double)
        Throw New NotImplementedException()
    End Sub
End Class
