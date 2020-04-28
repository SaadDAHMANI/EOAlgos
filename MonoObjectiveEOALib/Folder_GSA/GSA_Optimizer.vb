
Public Class GSA_Optimizer
    Implements IEvolutionaryAlgo

    Public Sub New()

    End Sub
    Public Sub New(agents As Integer, dimensions As Integer, iterationMax As Integer)
        Dimensions_D = dimensions
        Agents_N = agents
        MaxIterations = iterationMax
    End Sub

    Public Sub New(agents As Integer, dimensions As Integer, iterationMax As Integer, gO As Double, alpha_g As Double)
        Dimensions_D = dimensions
        Agents_N = agents
        MaxIterations = iterationMax
        G0 = gO
        Me.Alpha = alpha_g
    End Sub

    Private Shared Rand As Random = New Random()

#Region "Common of Interface: IEvolutionaryAlgo"

    Dim dimensionD As Integer
    Public Property Dimensions_D As Integer Implements IEvolutionaryAlgo.Dimensions_D
        Get
            Return dimensionD
        End Get
        Set(value As Integer)
            dimensionD = Math.Max(value, 1)
            Me.D = dimensionD - 1
        End Set
    End Property

    Dim mAgents_N As Integer
    Public Property Agents_N As Integer Implements IEvolutionaryAlgo.Agents_N
        Get
            Return mAgents_N
        End Get
        Set(value As Integer)
            mAgents_N = Math.Max(value, 1)
            Me.N = mAgents_N - 1
        End Set
    End Property

    Dim mIntervalles As List(Of Intervalle)
    Public Property Intervalles As List(Of Intervalle) Implements IEvolutionaryAlgo.Intervalles
        Get
            Return mIntervalles
        End Get
        Set(value As List(Of Intervalle))
            mIntervalles = value
        End Set
    End Property

    Dim iterationsMax As Integer
    Public Property MaxIterations As Integer Implements IEvolutionaryAlgo.MaxIterations
        Get
            Return iterationsMax
        End Get
        Set(value As Integer)
            iterationsMax = Math.Max(value, 0)
        End Set
    End Property

    Dim mOptimizationType As OptimizationTypeEnum = OptimizationTypeEnum.Minimization
    Public Property OptimizationType As OptimizationTypeEnum Implements IEvolutionaryAlgo.OptimizationType
        Get
            Return mOptimizationType
        End Get
        Set(value As OptimizationTypeEnum)
            mOptimizationType = value
        End Set
    End Property

    Dim Chronos As Stopwatch
    Public ReadOnly Property ComputationTime As Long Implements IEvolutionaryAlgo.ComputationTime
        Get
            If Object.Equals(Chronos, Nothing) Then
                Return 0
            Else
                Return Chronos.ElapsedMilliseconds
            End If
        End Get
    End Property

    Public ReadOnly Property BestSolution As Double() Implements IEvolutionaryAlgo.BestSolution
        Get
            Return BestLine
        End Get
    End Property

    Dim mBestChart As List(Of Double)
    Public ReadOnly Property BestChart As List(Of Double) Implements IEvolutionaryAlgo.BestChart
        Get
            Return mBestChart
        End Get
    End Property

    Dim mWorstChart As List(Of Double)
    Public ReadOnly Property WorstChart As List(Of Double) Implements IEvolutionaryAlgo.WorstChart
        Get
            Return mWorstChart
        End Get
    End Property

    Dim mMeanChart As List(Of Double)
    Public ReadOnly Property MeanChart As List(Of Double) Implements IEvolutionaryAlgo.MeanChart
        Get
            Return mMeanChart
        End Get
    End Property

    Dim mSolution_Fitness As Dictionary(Of String, Double)
    Public ReadOnly Property Solution_Fitness As Dictionary(Of String, Double) Implements IEvolutionaryAlgo.Solution_Fitness
        Get
            Return mSolution_Fitness
        End Get
    End Property

    Public Event ObjectiveFunction(positions() As Double, ByRef fitnessValue As Double) Implements IEvolutionaryAlgo.ObjectiveFunction

    Dim mCurrentFitness As Double = Double.NaN
    Public ReadOnly Property CurrentBestFitness As Double Implements IEvolutionaryAlgo.CurrentBestFitness
        Get
            Return mCurrentFitness
        End Get
    End Property


#End Region

#Region "GSA_Optimization"

    Private BestLine() As Double
    Private Positions() As Double

    Public Property Alpha As Double = 20.0R
    Public Property G0 As Double = 100.0R
    Private Const Eps As Double = 0.00000000000000022204
    Private N As Integer
    Private D As Integer
    Private Rnorme As Integer = 2I
    Private Rpower As Integer = 1I

    Dim best As Double = Double.NaN
    Dim best_X As Integer = 0I
    Dim Fbest As Double = Double.NaN
    Dim Lbest() As Double 'Best line (solution)

    Dim meanValue As Double = Double.NaN
    Dim worstValue As Double = Double.NaN
    '---------------------------------
    Dim M() As Double
    Dim Ms() As Double
    Dim Ds() As Integer
    '---------------------------------
    Dim Gvalue As Double = Double.NaN
    Dim accelerations(,) As Double
    Dim E(,) As Double
    Dim V(,) As Double
    Dim iteration As Integer = 0I

    ''' <summary>
    ''' Matrix of solutions
    ''' </summary>
    Private X(,) As Double

    Private Fitness() As Double = Nothing

    ''' <summary>
    ''' ElitistCheck: If ElitistCheck=1, algorithm runs with eq.21 and if =0, runs with eq.9.
    ''' </summary>
    ''' <returns></returns>
    Public Property ElitistCheck As ElitistCheckEnum = ElitistCheckEnum.Equation21

    Public ReadOnly Property BestScore As Double Implements IEvolutionaryAlgo.BestScore
        Get
            If (Equals(BestChart, Nothing) OrElse (BestChart.Count = 0)) Then
                If OptimizationType = OptimizationTypeEnum.Minimization Then
                    Return Double.MaxValue
                Else
                    Return Double.MinValue
                End If
            Else
                Return BestChart.Last()
            End If
        End Get
    End Property

    Private Sub Initialize()

        For Each interval In Me.Intervalles
            If interval.Max_Value < interval.Min_Value Then
                Throw New Exception("interval.Max_Value must be > interval.Min_Value ")
            End If
        Next

        mBestChart = New List(Of Double)
        mMeanChart = New List(Of Double)
        mWorstChart = New List(Of Double)

        BestLine = New Double((Me.dimensionD - 1)) {}
        Positions = New Double((Me.dimensionD - 1)) {}
        Fitness = New Double(N) {}

        '-------------------------------------------
        Lbest = New Double(D) {} 'Best line (solution)
        M = New Double(N) {}
        Ms = New Double(N) {}
        Ds = New Integer(N) {}
        accelerations = New Double(N, D) {}
        E = New Double(N, D) {}
        V = New Double(N, D) {}

        'Dim bestSolution((Me.D_Dimensions - 1)) As Double

        'Initialize solutions:
        Initialize_X()

    End Sub

    Private Sub Initialize_X()

        X = New Double(N, D) {}

        Try

            Dim value As Double = 0R
            Dim signe As Integer = 0

            For j As Integer = 0 To D
                value = (Me.Intervalles.Item(j).Max_Value - Me.Intervalles.Item(j).Min_Value) + Me.Intervalles.Item(j).Min_Value

                For i As Integer = 0 To N
                    While signe = 0
                        signe = Rand.Next(-1, 2)
                    End While
                    X(i, j) = (value * signe * Rand.NextDouble())
                    signe = 0
                Next
            Next
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub Space_Bound()
        'from matlab site :
        'https://www.mathworks.com/matlabcentral/answers/311735-hi-i-try-to-convert-this-matlab-code-to-vb-net-or-c-codes-help-me-please
        'outofrange = X(i, : ) > up | X(i, :) < low;
        'X(i, outofrange) = rand(1, sum(outofrange)) * (up - low) + low;

        'or-----
        ' Function X = space_bound(X, up, low)
        '   outofrange = X > up | X < low
        '   X(outofrange) = rand(1, sum(outofrange)) * (up - low) + low;
        ' End
        Dim write As Boolean = False

        'Dim rand As New Random()

        Dim Tp(D) As Int32
        Dim Tm(D) As Int32
        Dim TpTildeTm(D) As Int32
        Dim value As Integer = 0I
        Dim TmpArray(D) As Double
        Dim randiDimm(D) As Double

        'For i As Integer = 0 To Me.N

        '    For j As Integer = 0 To Me.D
        '        If Me.X(i, j) > Up Then
        '            Tp(j) = 1I
        '            'Debug.Print(String.Format("{0}, Iter = {1}", Me.X(i, j).ToString(), CurrentIteration))
        '            'write = True
        '            'Show_X(X, "X , > UP : Before.....>")

        '        Else
        '            Tp(j) = 0I
        '        End If

        '        If Me.X(i, j) < Me.Down Then
        '            Tm(j) = 1I

        '            'write = True
        '            'Debug.Print(String.Format("{0}, Iter = {1}", Me.X(i, j).ToString(), CurrentIteration))
        '            'Show_X(X, "X , < Down : Before.....>")
        '        Else
        '            Tm(j) = 0I
        '        End If

        '        value = Tp(j) + Tm(j)

        '        If value = 0 Then
        '            TpTildeTm(j) = 1I
        '        Else
        '            TpTildeTm(j) = 0I
        '        End If
        '    Next

        '    '------------------------------------
        '    For j As Integer = 0 To Me.D
        '        TmpArray(j) = Me.X(i, j) * TpTildeTm(j)
        '    Next
        '    '-----------------------------------
        '    For t = 0 To Me.D
        '        randiDimm(t) = (Rand.NextDouble() * (Me.Up - Me.Down) + Me.Down) * (Tp(t) + Tm(t))

        '    Next

        '    For t = 0 To Me.D
        '        Me.X(i, t) = TmpArray(t) + randiDimm(t)
        '    Next

        '    'If write Then
        '    '    Show_X(X, "X : After.....>")
        '    'End If
        'Next
        '----------------------------------------------------------------

        For i As Integer = 0 To Me.N

            For j As Integer = 0 To Me.D
                If Me.X(i, j) > Me.Intervalles.Item(j).Max_Value Then
                    Tp(j) = 1I
                    'Debug.Print(String.Format("{0}, Iter = {1}", Me.X(i, j).ToString(), CurrentIteration))
                    'write = True
                    'Show_X(X, "X , > UP : Before.....>")

                Else
                    Tp(j) = 0I
                End If

                If Me.X(i, j) < Me.Intervalles.Item(j).Min_Value Then
                    Tm(j) = 1I

                    'write = True
                    'Debug.Print(String.Format("{0}, Iter = {1}", Me.X(i, j).ToString(), CurrentIteration))
                    ' Show_X(X, "X , < Down : Before.....>")
                Else
                    Tm(j) = 0I
                End If

                value = Tp(j) + Tm(j)

                If value = 0 Then
                    TpTildeTm(j) = 1I
                Else
                    TpTildeTm(j) = 0I
                End If
            Next

            '------------------------------------
            For j As Integer = 0 To Me.D
                TmpArray(j) = Me.X(i, j) * TpTildeTm(j)
            Next
            '-----------------------------------
            For t = 0 To Me.D
                randiDimm(t) = (Rand.NextDouble() * (Me.Intervalles.Item(t).Max_Value - Me.Intervalles.Item(t).Min_Value) + Me.Intervalles.Item(t).Min_Value) * (Tp(t) + Tm(t))

            Next

            For t = 0 To Me.D
                Me.X(i, t) = TmpArray(t) + randiDimm(t)
            Next

            'If write Then
            '    Show_X(X, "X : After.....>")
            '    Stop
            'End If
        Next

    End Sub

#Region "Fitness_Evaluation"

    Dim fitnessValue As Double = 0R
    ''' <summary>
    ''' Evaluate fintess for All
    ''' </summary>
    ''' <param name="fitnessArray">Fitness array of Agents X(N,D)</param>
    Private Sub EvaluateFitness(ByRef fitnessArray() As Double)
        Try
            For i = 0 To N
                fitnessValue = 0R
                For j = 0 To D
                    Positions(j) = Me.X(i, j)
                Next
                RaiseEvent ObjectiveFunction(Positions, fitnessValue)
                fitnessArray(i) = fitnessValue
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetBestLine(ByRef Xx As Double(,), lbest() As Double, ByVal best_x As Integer)
        Try
            For i As Integer = 0 To Me.D
                lbest(i) = Xx(best_x, i)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Function Get_MeanValue(ByRef fitness As Double()) As Double

        Dim meanValue As Double = Double.NaN
        Dim dimI As Integer = fitness.GetLength(0)
        Dim sum As Double = fitness.Sum
        meanValue = (sum / dimI)
        Return meanValue

    End Function

    Private Function Get_WorstValue(ByRef fitness As Double()) As Double

        Dim worstValue As Double = Double.NaN

        If Me.OptimizationType = OptimizationTypeEnum.Minimization Then
            worstValue = Double.MinValue
            For i As Integer = 0 To (fitness.GetLength(0) - 1)
                If worstValue < fitness(i) Then
                    worstValue = fitness(i)
                End If
            Next
        Else
            worstValue = Double.MaxValue
            For i As Integer = 0 To (fitness.GetLength(0) - 1)
                If worstValue > fitness(i) Then
                    worstValue = fitness(i)
                End If
            Next
        End If

        Return worstValue

    End Function

    Private Sub Mass_Calculation(ByRef massM() As Double, ByRef fitnes() As Double)
        If IsNothing(massM) Then Return
        If IsNothing(fitnes) Then Return

        Dim fmax As Double = fitnes.Max
        Dim fmin As Double = fitnes.Min

        If fmin = fmax Then
            For i As Int32 = 0 To Me.N
                massM(i) = 1.0R
            Next

        Else
            Dim best, worst As Double

            If OptimizationType = OptimizationTypeEnum.Minimization Then
                best = fmin
                worst = fmax
            Else
                best = fmax
                worst = fmin
            End If

            Dim denominator As Double = (best - worst)

            For i As Integer = 0 To Me.N
                massM(i) = (fitnes(i) - worst) / denominator
            Next

            Dim sumMi As Double = massM.Sum

            For i As Integer = 0 To Me.N
                massM(i) = massM(i) / sumMi
            Next

        End If

    End Sub

    Private Function Gconstant(ByVal iteration As Integer, ByVal max_it As Integer) As Double
        Dim gValue As Double = 0R
        Dim expose As Double = (-1 * Me.Alpha * iteration) / max_it
        gValue = G0 * Math.Exp(expose)
        Return gValue
    End Function

    Private Sub Gfield(ByVal iteration As Integer, ByRef M() As Double, ByRef Ms() As Double, ByRef Ds() As Integer, ByRef E(,) As Double, ByRef accelerations(,) As Double, ByRef Gval As Double)

        Dim final_per = 2
        Dim kbestDbl As Double
        Dim kbest As Integer

        'total force calculation :

        If Me.ElitistCheck = ElitistCheckEnum.Equation9 Then

            kbest = N

        ElseIf Me.ElitistCheck = ElitistCheckEnum.Equation21 Then

            kbestDbl = final_per + ((1 - (iteration / MaxIterations)) * (100 - final_per))
            kbestDbl = (N * kbestDbl) / 100
            kbest = Convert.ToInt32(Math.Round(kbestDbl))
        End If

        'Descend
        Sort(M, Ms, Ds)
        '----------------------------------------------------------
        Dim j As Integer = 0I
        Dim R As Double = 0R

        For i As Integer = 0 To N
            'Initialisation :
            For t As Integer = 0 To D
                E(i, t) = 0R
            Next

            For ii As Integer = 0 To (kbest - 1)
                j = Ds(ii)

                If i <> j Then
                    R = Norm(X, i, j)
                    R = (R ^ Rpower) + Eps

                    For k = 0 To Me.D
                        ' E(i, k) = E(i, k) + (0.5 * (M(j) * ((X(j, k) - X(i, k)) / R)))
                        E(i, k) = E(i, k) + (Rand.NextDouble() * (M(j) * ((X(j, k) - X(i, k)) / R)))
                    Next

                End If
            Next
        Next
        'Acceleration
        'a = E.* G; %note that Mp(i)/Mi(i)=1
        For s As Integer = 0 To N
            For t As Integer = 0 To D
                accelerations(s, t) = Gval * E(s, t)
            Next
        Next

        'Show_X(E, "E := ")
        'Show_X(accelerations, "Accelerations := ")

    End Sub

    Function Norm(ByRef Xij As Double(,), ByRef iIndex As Integer, ByRef jIndex As Integer, Optional norme As Integer = 2) As Double
        Dim result As Double = 0R
        If IsNothing(Xij) Then Return Nothing
        Try
            Dim summ As Double = 0R
            Dim jCount As Integer = (Xij.GetLength(1) - 1)
            Dim tmpValue As Double = 0R

            For j As Integer = 0 To jCount

                tmpValue = (Xij(iIndex, j) - Xij(jIndex, j))
                summ += tmpValue ^ norme
            Next

            result = Math.Sqrt(summ)

        Catch ex As Exception
            Throw ex
        End Try
        Return result
    End Function

    Private Sub Move(ByRef Xx(,) As Double, ByRef accelerations(,) As Double, ByRef V(,) As Double)

        For i As Integer = 0 To N
            For j As Integer = 0 To D
                V(i, j) = ((Rand.NextDouble() * V(i, j))) + accelerations(i, j)
                'V(i, j) = (0.5 * V(i, j)) + accelerations(i, j)
            Next
        Next
        For r As Integer = 0 To Me.N
            For s As Integer = 0 To Me.D
                Xx(r, s) = Xx(r, s) + V(r, s)
            Next
        Next

    End Sub

    Private Sub Sort(ByRef M() As Double, ByRef Ms() As Double, ByRef Ds() As Integer)

        If IsNothing(M) Then Return
        If IsNothing(Ms) Then Return
        If IsNothing(Ds) Then Return

        Dim tmpM(Me.N) As Double

        For i As Integer = 0 To Me.N
            Ds(i) = i
            tmpM(i) = M(i)
        Next

        Dim minValue As Double = (tmpM.Min() - 10)
        Dim maxValue As Double = (tmpM.Max)

        For i As Integer = 0 To Me.N

            For j = 0 To Me.N

                maxValue = tmpM.Max
                If tmpM(j) = maxValue Then
                    Ms(i) = maxValue
                    Ds(i) = j
                    tmpM(j) = minValue
                    Exit For
                End If
            Next
        Next

    End Sub
#End Region

    Public Sub RunEpoch() Implements IEvolutionaryAlgo.RunEpoch

        If iteration = 0 Then
            Initialize()
        End If
        'For iteration As Integer = 1 To MaxIterations

        'Me.CurrentIteration = iteration

        '0: Checking allowable range :
        Space_Bound()

        '1: Fitness Evaluation :
        'Dim fitness As Double() = EvaluateF()
        EvaluateFitness(Me.Fitness)
        '-----------------------
        'Show_X_D(fitness, "----------------->Fitness<----------------")

        '-----------------------
        If OptimizationType = OptimizationTypeEnum.Minimization Then
            best = Fitness.Min
            best_X = Array.IndexOf(Fitness, best)
        Else
            best = Fitness.Max
            best_X = Array.IndexOf(Fitness, best)
        End If

        If iteration = 0 Then
            Fbest = best
            GetBestLine(X, Lbest, best_X)
        End If

        If OptimizationType = OptimizationTypeEnum.Minimization Then
            '%minimization.
            If best < Fbest Then
                Fbest = best
                GetBestLine(X, Lbest, best_X)

            End If

        Else
            '%maximization
            If best > Fbest Then
                Fbest = best
                GetBestLine(X, Lbest, best_X)
            End If

        End If

        Me.BestChart.Add(Fbest)

        meanValue = Get_MeanValue(Fitness)
        worstValue = Get_WorstValue(Fitness)

        Me.MeanChart.Add(meanValue)
        Me.WorstChart.Add(worstValue)


        '2: Calculation of M. eq.14-20 :
        Mass_Calculation(M, Fitness)

        'Show_X(M, "Masses :")

        '3 : Calculation of Gravitational constant. eq.13 :
        Gvalue = Gconstant(iteration, MaxIterations)
        iteration += 1
        ' Console.WriteLine(Gvalue.ToString())

        '4: Calculation of accelaration in gravitational field. eq.7-10,21 :
        Gfield(iteration, M, Ms, Ds, E, accelerations, Gvalue)

        '5: Agent movement. eq.11-12
        Move(X, accelerations, V)

        'Show_X(V, "V := ")
        'Show_X(X, "X := ")

        '--------------Event---------------------------------------------------------
        'For t = 0 To (Lbest.Count - 1)
        '    eArg.CurrentSate(t) = Lbest(t)
        'Next
        'eArg.ProgressPercentage = iteration
        'RaiseEvent ProgressChanged(Me, eArg)
        '-----------------------------------------------------------------------------
        '---------------------------------------------------------
        ' Next
        mCurrentFitness = Fbest
        BestLine = Lbest

    End Sub


#End Region


    Public Sub LuanchComputation() Implements IEvolutionaryAlgo.LuanchComputation
        For i = 0 To (MaxIterations - 1)
            RunEpoch()
        Next
    End Sub

End Class

Public Enum ElitistCheckEnum
    Equation9 = 0
    Equation21 = 1
End Enum
