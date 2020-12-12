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

    Public Overrides Sub RunEpoch()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub InitializeOptimizer()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub ComputeObjectiveFunction(positions() As Double, ByRef fitnessValue As Double)
        Throw New NotImplementedException()
    End Sub
End Class
