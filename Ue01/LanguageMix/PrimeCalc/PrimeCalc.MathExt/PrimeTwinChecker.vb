Namespace Global.PrimeCalc
    Public Class PrimeTwinChecker
        Public Shared Function IsPrimeTwin(number As Integer) As Boolean
            IsPrimeTwin = PrimeChecker.IsPrime(number) And PrimeChecker.IsPrime(number + 2)
        End Function
    End Class
End Namespace