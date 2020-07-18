Imports System
Imports MonoObjectiveEOALib

Module Program

Private Optimizer as IEvolutionaryAlgo

Dim D As Int32
dim N as int32
dim Kmax as int32
Dim Intervalls As List(Of Intervalle)

 Sub Main(args As String())

        'initialisation of search sapce dimension
        D = 10

        'initialisation of search agents count 
        N = 50

        'initialisation of max iteration number 
        Kmax = 1000

        'initialize search space intevalls
        Intervalls = new List(of Intervalle)
        For i = 0 To (d - 1)
            intervalls.Add(New Intervalle(-10, 10))
        Next
           '-----------------------------------------------------------------
         Console.WriteLine(" ")
         TestGSA()
         Console.WriteLine(" ")
         TestPSOGSA()
         Console.WriteLine(" ")
         TestGWO()


    End Sub

Private Sub TestGSA()
        Optimizer = New MonoObjectiveEOALib.GSA_Optimizer(100, 20)
        AddHandler Optimizer.ObjectiveFunctionComputation, AddressOf BenchmarkFunctions.F1
        'initialize algo

        With Optimizer 
            .Agents_N = n
            .Dimensions_D = d
            .MaxIterations =kmax
            .Intervalles = Intervalls
            .OptimizationType = OptimizationTypeEnum.Minimization

            .LuanchComputation()
        End With

        ''Write the best solution for GSA
        'Console.WriteLine("The best solution with GSA is : ")
        'For i = 0 To (d - 1)
        '    Console.WriteLine(Optimizer .BestSolution(i))
        'Next

        'Write the best score for GSA :
        Console.WriteLine(String.Format("GSA best score is : {0}", Optimizer.BestScore))

        Console.WriteLine("End of computation.. GSA...")
End Sub

Private Sub TestPSOGSA()

        Optimizer = New MonoObjectiveEOALib.PSOGSA_Optimizer(100, 10, 0.5, 0.5)
        AddHandler Optimizer.ObjectiveFunctionComputation, AddressOf BenchmarkFunctions.F1

        With Optimizer
        .Agents_N=n
        .Dimensions_D=d
        .MaxIterations=kmax
        .OptimizationType= OptimizationTypeEnum.Minimization
        .Intervalles = intervalls
        .LuanchComputation()
        end With

        ''Write the best solution for PSOGSA:
        'Console.WriteLine("The best solution with PSOGSA is : ")
        'For i = 0 To (d - 1)
        '    Console.WriteLine(Optimizer.BestSolution(i))
        'Next

        'Write the best score for PSOGSA :
        Console.WriteLine(String.Format("PSOGSA best score is : {0}", Optimizer.BestScore))
	    Console.WriteLine("End of computation.. PSOGSA...")
end sub

private sub TestGWO()

        Optimizer = New GWO_Optimizer(GWOVersionEnum.StandardGWO, 0.5)
        AddHandler Optimizer.ObjectiveFunctionComputation, AddressOf BenchmarkFunctions.F1

        With optimizer
        .Agents_N=n
        .Dimensions_D=d
        .MaxIterations=kmax
        .OptimizationType= OptimizationTypeEnum.Minimization
        .Intervalles = intervalls
        .LuanchComputation()
        End With

        ''Write the best solution for GWO:
        'Console.WriteLine("The best solution with GWO is : ")
        'For i = 0 To (d - 1)
        '    Console.WriteLine(optimizer.BestSolution(i))
        'Next

        'Write the best score for GWO :
        Console.WriteLine(String.Format("GWO best score is : {0}", optimizer.BestScore))
    Console.WriteLine("End of computation.. GWO...")
end sub 

End Module
