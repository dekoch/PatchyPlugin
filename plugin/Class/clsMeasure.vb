Class clsMeasure

    Public arrSensorInput0(51) As Double
    Dim dblTemp0 As Double = 0.0
    Dim intMaxValue As Integer = 0

    Public dummy As String = ""


    Public Sub Feed(ByVal input As Double, ByVal count As Integer) 'neuer wert in mittelwertbildung aufnehmen

        intMaxValue = count

        If intMaxValue < UBound(arrSensorInput0) Then 'array abarbeiten

            arrSensorInput0(intMaxValue) = input

            For i As Integer = 0 To intMaxValue - 1 'letzten wert einen position richtung 0 schieben
                arrSensorInput0(i) = arrSensorInput0(i + 1)
            Next

            dblTemp0 = 0.0

            For i As Integer = 0 To intMaxValue - 1
                dblTemp0 = dblTemp0 + arrSensorInput0(i)
            Next

            'Console.WriteLine("Feed()")
            'Console.WriteLine(intTemp0)
            'Console.WriteLine(intMaxValue)

        End If

    End Sub

    Public Function Pick() As Double

        'Return 550
        'Return Math.Round(intTemp0 / intMaxValue, 0)
        'If intTemp0 = 0 Then
        '    intTemp0 = 1
        'End If

        'Console.WriteLine("Pick()")
        'Console.WriteLine(intTemp0)
        'Console.WriteLine(intMaxValue)

        If intMaxValue = 0 Then
            intMaxValue = 1
        End If

        'Dim str As String = intTemp0 / intMaxValue
        'Console.WriteLine(str)
        'Dim int As Integer = str.Substring(0, str.IndexOf("."))
        'Console.WriteLine(str)

        Return dblTemp0 / intMaxValue

    End Function

End Class
