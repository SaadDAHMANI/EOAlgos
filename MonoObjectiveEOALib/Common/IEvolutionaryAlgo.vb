Public Interface IEvolutionaryAlgo
    Sub RunEpoch()
    Sub LuanchComputation()

    Property Dimensions_D As Integer
    Property Agents_N As Integer

    Property Intervalles As List(Of Intervalle)
    Property MaxIterations As Integer
    Property OptimizationType As OptimizationTypeEnum

    ReadOnly Property ComputationTime As Long
    ReadOnly Property BestSolution As Double()
    ReadOnly Property BestScore As Double

    ReadOnly Property BestChart As List(Of Double)
    ReadOnly Property WorstChart As List(Of Double)
    ReadOnly Property MeanChart As List(Of Double)
    ReadOnly Property Solution_Fitness As Dictionary(Of String, Double)
    ReadOnly Property CurrentBestFitness As Double

    Event ObjectiveFunction(positions() As Double, ByRef fitnessValue As Double)
End Interface
