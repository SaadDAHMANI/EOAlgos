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

Private  _A0 as Double=0.9
 Public Property A0 As Double
     Get
         Return _A0
     End Get
     Set(value As Double)
         _A0=value 
     End Set
 End Property

Private _Ainf as Double =0.6
Public Property Ainf As Double
    Get
        Return _Ainf
    End Get
    Set(value As Double)
        _Ainf=value 
    End Set
End Property

Private _R0 as Double =0.1
Public Property R0 As Type
    Get
        Return _R0
    End Get
    Set(value As Double)
        _R0= value
    End Set
End Property

Private _Rinf as Double=0.7
Public Property Rinf As Double
    Get
        Return _Rinf
    End Get
    Set(value As Double)
        _Rinf=value
    End Set
End Property

Private N as integer =0
Private D as integer =0

Private W0 as Double[]
Private Winf as Double[]
Private A as Double[]
Private R as Double[] 
Private Fit as Double[]
Private fitnessValue as Double
Private Fmin as Double
Private Fitinn as Double
Private Iindex as Integer

Private ii as Integer = 0
Private Best as Double[]
Private q as Double =2

Private W as Double[,]
Private V as Double[,] 

#End Region


Public Overrides Sub RunEpoch()
    If CurrentIteration = 1 Then
        InitializeOptimizer()
    End If

    For i As Integer = 0 To N
        
        ii=RandomGenerator.Next(0, (N+1))
        While ii=i
        ii=RandomGenerator.Next(0, (N+1))
        Next
        
        q=2

        If Fit(ii)<Fit(i) Then
            For j as Integer=0 to D
            V[i,j]=(Population(ii)(j)-Population(i)(j))*RandomGenerator.NextDouble()*q+ (Best[j]-Population(i)(j))*RandomGenerator.NextDouble()*q
            Next
        End if






            
        Next
        



    End Sub

   

    Public Overrides Sub InitializeOptimizer()
     if SearchIntervals.Count<Dimensions_D Then throw new Exception("Search space intervals must be equal search space dimension.")

        _BestChart = New List(Of Double)
        _MeanChart = New List(Of Double)
        _WorstChart = New List(Of Double)

        D = Dimensions_D - 1
        N = PopulationSize_N - 1
        W0=new Double[D]{}
        Winf=new Double[D]{}
        A = new Double[N]{}
        R= new Double[N]{}
        Fit= new Double[N]{}
        W=new Double[N,D]{}
        V=new Double[N,D]{}

        For j as Integer = 0 to D
            W0[j]=(SearchIntervals[j].Max_Value-SearchIntervals[j].Min_Value)/4
            Winf[j]=W0[j]/100
        Next    

        For i as integer = 0 to N
            A[i]=A0
            R[i]=R0
        Next        
       
        'Inintilize population
         InitializePopulation()

        For i As Integer = 0 To N
            fitnessValue = Double.NaN
            ComputeObjectiveFunction(Population(i), fitnessValue)
            Fit(i)=fitnessValue

            For j As Integer = 0 To D
                W[i,j]=W0
                V[i,j]=0
            Next                 
        Next
         
        Fmin=Fit.Min()
        Iindex =Fit.IndexOf(Fmin)
        Fitinn=Fmin
        Best=Population(Iindex)
    End Sub

    Public Overrides Sub ComputeObjectiveFunction(positions() As Double, ByRef fitnessValue As Double)
       MyBase.OnObjectiveFunction(positions, fitnessValue)
    End Sub
End Class
