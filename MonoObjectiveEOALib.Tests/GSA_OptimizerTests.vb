Imports System
Imports Xunit
Imports MonoObjectiveEOALib

Namespace MonoObjectiveEOALib.Tests

    Public Class GSA_OptimizerTests
    
    Dim  gsaOptim_uts as GSA_Optimizer
    '''
    '''Constructor
    '''
    Public sub New ()
    gsaOptim_uts= new GSA_Optimizer()
    End sub

        <Fact>
        Sub Dimensions_DWhenNegativeInput()
        gsaOptim_uts.Dimensions_D=-10
        Assert.Equal(1,gsaOptim_uts.Dimensions_D) 
        End Sub

        <Fact>
        Sub Dimensions_DWhenNullInput()
        gsaOptim_uts.Dimensions_D=0
        Assert.Equal(1,gsaOptim_uts.Dimensions_D) 
        End Sub

    End Class

End Namespace

