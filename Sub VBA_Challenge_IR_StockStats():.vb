Sub VBA_Challenge_IR_StockStats():

'Define variables
    Dim ticker As String
    Dim number_tickers As Integer
    Dim opening_price As Double
    Dim closing_price As Double
    Dim yearly_change As Double
    Dim percentage_change As Double
    Dim total_volume As Double
    Dim greatest_increase as Double
    Dim greatest_increase_ticker as String
    Dim greatest_drecrease as Double
    Dim greatest_decrease_ticker as String
    Dim greatest_volume as Double
    Dim greates_volume_ticker as String

    'last row of each worksheet
    Dim lastRow As Long

'Loop trough all the stocks
    For Each ws In Worksheets
    ws.Activate

    'Find the last row
    lastRow = ws.Cells(Rows.Count, "A").End(xlUp).Row
    
    'Set variables at 0
    ticker = ""
    number_tickers = 0
    opening_price = 0
    yearly_change = 0
    percentage_change = 0
    total_volume = 0

    'Add print table
    ws.Range("I1").Value = "Ticker"
    ws.Range("J1").Value = "Yearly Change"
    ws.Range("K1").Value = "Percent Change"
    ws.Range("L1").Value = "Total Stock Volume"
    
    'To skip first line (headers)
    For i = 2 To lastRow
    
    'Get ticker value
    ticker = Cells(i, 1).Value
    
    'Get year opening for ticker
        If opening_price = 0 Then
            opening_price = Cells(i, 3).Value
        End If
        
    'Add up the total stock volume
    total_volume = total_volume + Cells(i, 7).Value
        
    'For when we get to a different ticker
        If Cells(i + 1, 1).Value <> ticker Then
            number_tickers = number_tickers + 1
            Cells(number_tickers + 1, 9) = ticker
            
    'Get closing price
    closing_price = Cells(i, 6)
            
    'Get yearly change
    yearly_change = closing_price - opening_price

    'Print yearly change
    Cells(number_tickers + 1, 10).Value = yearly_change

    'Color conditions
    If yearly_change > 0 Then
        Cells(number_tickers + 1, 10).Interior.ColorIndex = 4
    ElseIf yearly_change < 0 Then
        Cells(number_tickers + 1, 10).Interior.ColorIndex = 3
    Else
        Cells(number_tickers + 1, 10).Interior.ColorIndex = 6
    End If
            
            
    'Calculate percent change
    If opening_price = 0 Then
        percentage_change = 0
    Else
        percentage_change = (yearly_change / opening_price)
    End If
        'Format the percent_change value as a percent.
            Cells(number_tickers + 1, 11).Value = Format(percentage_change, "Percent")

    'Set opening price back to 0 as soon as get a different ticker
    opening_price = 0
            
    'Print total volume
    Cells(number_tickers + 1, 12).Value = total_volume
            
    'Set total volume back to 0 as soon as we get a different ticker
    total_volume = 0
    End If
        
    Next i
    
    'BONUS
    Range("O2").Value = "Greatest % Increase"
    Range("O3").Value = "Greatest % Decrease"
    Range("O4").Value = "Greatest Total Volume"
    Range("P1").Value = "Ticker"
    Range("Q1").Value = "Value"
    
    lastRowState = ws.Cells(Rows.Count, "I").End(xlUp).Row
    
    'Initialize variables at first values
    greatest_increase = Cells(2, 11).Value
    greatest_increase_ticker = Cells(2, 9).Value
    greatest_decrease = Cells(2, 11).Value
    greatest_decrease_ticker = Cells(2, 9).Value
    greatest_volume = Cells(2, 12).Value
    greatest_volume_ticker = Cells(2, 9).Value
    
    
    'Loop for bonus
    For i = 2 To lastRowState
    
    'Find ticker with greatest increase
    If Cells(i, 11).Value > greatest_increase Then
        greatest_increase = Cells(i, 11).Value
        greatest_increase_ticker = Cells(i, 9).Value
    End If
        
    'Find ticker with the greatest decrease
    If Cells(i, 11).Value < greatest_decrease Then
        greatest_decrease = Cells(i, 11).Value
        greatest_decrease_ticker = Cells(i, 9).Value
    End If
        
    'Find the ticker with the greatest stock volume.
    If Cells(i, 12).Value > greatest_volume Then
        greatest_volume = Cells(i, 12).Value
        greatest_volume_ticker = Cells(i, 9).Value
    End If
        
    Next i
    
    'PRINT BONUS TABLE
    Range("P2").Value = Format(greatest_increase_ticker, "Percent")
    Range("Q2").Value = Format(greatest_increase, "Percent")
    Range("P3").Value = Format(greatest_decrease_ticker, "Percent")
    Range("Q3").Value = Format(greatest_decrease, "Percent")
    Range("P4").Value = greatest_volume_ticker
    Range("Q4").Value = greatest_volume
    
Next ws


End Sub


