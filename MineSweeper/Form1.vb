Imports Microsoft.VisualBasic.ApplicationServices

Public Class Form1


    'For better readability, tools -> options -> text editor -> all languages -> enable word wrap and line numbers
    'Create the array for cells
    Public arrayCells(0, 0) As Button
    Public timerRunning As Boolean



    Private Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click

        'SortLeaderBoard("c:\temp\leaderboard.txt")

        'Set difficulty
        Dim intDifficulty As Integer
        Dim strDifficulty As String = ""
        Do While strDifficulty = ""
            strDifficulty = InputBox("Please type the number of mines you want and then click OK. You can have between 1 and 199 mines.", "Difficulty")

            'User chooses how many bombs they would like to have placed
            If strDifficulty = "" Then
                MessageBox.Show("Please enter a number")
            ElseIf strDifficulty = "0" Then
                MessageBox.Show("Please enter a number greater than 0")
                strDifficulty = ""
            End If
        Loop

        intDifficulty = CInt(strDifficulty)
        tbMines.Text = intDifficulty

        ResetButtons()

        'Place mines
        Dim mines As Integer = intDifficulty
        Dim minesSet As Integer = 0

        Randomize()

        While minesSet < mines
            Dim row As Integer = CInt(Int((10 * Rnd()) + 0)) 'Create random number between 0 and 199, for the indexes of the cells array
            Dim col As Integer = CInt(Int((20 * Rnd()) + 0)) 'Create random number between 0 and 199, for the indexes of the cells array

            'If the button the randomizer lands on does not have a mine then place one
            If arrayCells(row, col).Tag <> 1 Then
                arrayCells(row, col).Tag = 1
                minesSet += 1
            End If
        End While

        'Begin timer
        Dim intTime As Integer
        tbTimer.Text = 0
        timerRunning = True
        Do Until tbMines.Text = 0
            Delay(1)
            If timerRunning = False Then
                Exit Do
            End If
            intTime += 1
            tbTimer.Text = intTime
        Loop

        'When you win:
        If tbMines.Text = 0 Then
            Dim path As String = "c:\temp\Leaderboard.txt"
            Dim leaderboard As System.IO.StreamWriter
            Dim highscore As String = ""

            highscore = InputBox("You Win!                                                                                                                                                                                                                               Time: " & tbTimer.Text & "                                                              Enter your name: ", "Leaderboard")
            If highscore = "" Then
                MessageBox.Show("Please enter your name.")
            Else
                'creates a new text file for leaderboards if one doesn't exist
                If System.IO.File.Exists(path) = False Then
                    leaderboard = System.IO.File.CreateText(path)
                    leaderboard.WriteLine("Name: " & highscore & ".......................... Mines: " & intDifficulty & "/Time: " & tbTimer.Text)
                    leaderboard.Flush()
                    leaderboard.Close()
                Else
                    leaderboard = System.IO.File.AppendText(path)
                    leaderboard.WriteLine("Name: " & highscore & ".......................... Mines: " & intDifficulty & "/Time: " & tbTimer.Text)
                    leaderboard.Flush()
                    leaderboard.Close()

                End If
            End If

            SortLeaderBoard(path)

        End If

    End Sub

    Private Sub SortLeaderBoard(path As String)
        'Sort the high schores by difficulty, then time

        Dim sortedScores() As Score
        Dim allLines() As String
        allLines = System.IO.File.ReadAllLines(path)

        Dim score As Score

        Dim scores(allLines.Length - 1) As Score

        For counter As Integer = 0 To allLines.Length - 1
            Dim line As String
            line = allLines(counter)

            Dim tokens() As String = line.Split(" ")
            'Name: Tucker.......................... Mines: 3/Time: 8
            'token(0) = "Name:"
            'token(1) = "Tucker....................."
            'token(2) = "Mines:"
            'tokens(3) = "3/Time:"
            'tokens(4) = "8"

            Dim mineTokens() As String = tokens(3).Split("/")

            score = New Score()
            score.Name = tokens(1)
            score.Mines = CType(mineTokens(0), Integer)
            score.Time = CType(tokens(4), Integer)
            scores(counter) = score

        Next

        sortedScores = scores.OrderBy(Function(c) c.Time).OrderByDescending(Function(c) c.Mines).ToArray()

        'Deletes old file when new high score is added and creates a new one
        System.IO.File.Delete(path)

        Dim leaderBoard As System.IO.StreamWriter
        leaderBoard = System.IO.File.CreateText(path)

        For counter As Integer = 0 To sortedScores.Length - 1
            leaderBoard.WriteLine("Name: " & sortedScores(counter).Name & " Mines: " & sortedScores(counter).Mines & "/Time: " & sortedScores(counter).Time)
        Next

        leaderBoard.Flush()
        leaderBoard.Close()

    End Sub

    Private Sub ResetButtons()

        'Resets the bomb count to 0 after you finish a game and want to start another
        For r As Integer = 0 To 9
            For c As Integer = 0 To 19
                arrayCells(r, c).BackColor = Color.Transparent
                arrayCells(r, c).Text = ""
                arrayCells(r, c).Tag = 0
            Next
        Next

    End Sub

    Private Sub ShowAllBombs()
        'displays all bomb placement on board when you lose
        timerRunning = False

        For r As Integer = 0 To 9
            For c As Integer = 0 To 19
                arrayCells(r, c).BackColor = Color.White
                If arrayCells(r, c).Tag = 1 Then
                    arrayCells(r, c).Text = "☀"
                End If
            Next
        Next
    End Sub
    Private Sub CountBombs(row As Integer, col As Integer)

        'If you click on a button that does not have a mine then display the number of bombs adjacent to it
        Dim bombCount As Integer

        If (arrayCells(row, col)).Tag = 2 Then
            Return
        End If

        If row > 0 Then
            If arrayCells(row - 1, col).Tag = 1 Then
                bombCount += 1
            End If
        End If

        If row > 0 And col < 19 Then
            If arrayCells(row - 1, col + 1).Tag = 1 Then
                bombCount += 1
            End If
        End If

        If col < 19 Then
            If arrayCells(row, col + 1).Tag = 1 Then
                bombCount += 1
            End If
        End If

        If row < 9 And col < 19 Then
            If arrayCells(row + 1, col + 1).Tag = 1 Then
                bombCount += 1
            End If
        End If

        If row < 9 Then
            If arrayCells(row + 1, col).Tag = 1 Then
                bombCount += 1
            End If
        End If

        If row < 9 And col > 0 Then
            If arrayCells(row + 1, col - 1).Tag = 1 Then
                bombCount += 1
            End If
        End If

        If col > 0 Then
            If arrayCells(row, col - 1).Tag = 1 Then
                bombCount += 1
            End If
        End If

        If row > 0 And col > 0 Then
            If arrayCells(row - 1, col - 1).Tag = 1 Then
                bombCount += 1
            End If
        End If

        arrayCells(row, col).BackColor = Color.White
        arrayCells(row, col).Tag = 2

        If bombCount > 0 Then
            arrayCells(row, col).Text = bombCount
        Else
            CheckAround(row, col)
        End If


    End Sub

    Private Sub CheckAround(row As Integer, col As Integer)

        'If you click a button that does not have a bomb adjacent to it, check for the nearest buttons that do
        If row > 0 Then
            CountBombs(row - 1, col)
        End If

        If row > 0 And col < 19 Then
            CountBombs(row - 1, col + 1)
        End If

        If col < 19 Then
            CountBombs(row, col + 1)
        End If

        If row < 9 And col < 19 Then
            CountBombs(row + 1, col + 1)
        End If

        If row < 9 Then
            CountBombs(row + 1, col)
        End If

        If row < 9 And col > 0 Then
            CountBombs(row + 1, col - 1)
        End If

        If col > 0 Then
            CountBombs(row, col - 1)
        End If

        If row > 0 And col > 0 Then
            CountBombs(row - 1, col - 1)
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'two dimensional array that allows us to look up buttons by row and column
        arrayCells = New Button(,) {{Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9, Button10, Button11, Button12, Button13, Button14, Button15, Button16, Button17, Button18, Button19, Button20},
                                           {Button21, Button22, Button23, Button24, Button25, Button26, Button27, Button28, Button29, Button30, Button31, Button32, Button33, Button34, Button35, Button36, Button37, Button38, Button39, Button40},
                                           {Button41, Button42, Button43, Button44, Button45, Button46, Button47, Button48, Button49, Button50, Button51, Button52, Button53, Button54, Button55, Button56, Button57, Button58, Button59, Button60},
                                            {Button61, Button62, Button63, Button64, Button65, Button66, Button67, Button68, Button69, Button70, Button71, Button72, Button73, Button74, Button75, Button76, Button77, Button78, Button79, Button80},
                                            {Button81, Button82, Button83, Button84, Button85, Button86, Button87, Button88, Button89, Button90, Button91, Button92, Button93, Button94, Button95, Button96, Button97, Button98, Button99, Button100},
                                            {Button101, Button102, Button103, Button104, Button105, Button106, Button107, Button108, Button109, Button110, Button111, Button112, Button113, Button114, Button115, Button116, Button117, Button118, Button119, Button120},
                                            {Button121, Button122, Button123, Button124, Button125, Button126, Button127, Button128, Button129, Button130, Button131, Button132, Button133, Button134, Button135, Button136, Button137, Button138, Button139, Button140},
                                            {Button141, Button142, Button143, Button144, Button145, Button146, Button147, Button148, Button149, Button150, Button151, Button152, Button153, Button154, Button155, Button156, Button157, Button158, Button159, Button160},
                                            {Button161, Button162, Button163, Button164, Button165, Button166, Button167, Button168, Button169, Button170, Button171, Button172, Button173, Button174, Button175, Button176, Button177, Button178, Button179, Button180},
                                            {Button181, Button182, Button183, Button184, Button185, Button186, Button187, Button188, Button189, Button190, Button191, Button192, Button193, Button194, Button195, Button196, Button197, Button198, Button199, Button200}}
    End Sub

    Private Sub Button_MouseDown(sender As Object, e As MouseEventArgs) Handles Button1.MouseDown, Button5.MouseDown, Button4.MouseDown, Button3.MouseDown, Button2.MouseDown, Button9.MouseDown, Button8.MouseDown, Button7.MouseDown, Button60.MouseDown, Button6.MouseDown, Button59.MouseDown, Button58.MouseDown, Button57.MouseDown, Button56.MouseDown, Button55.MouseDown, Button54.MouseDown, Button53.MouseDown, Button52.MouseDown, Button51.MouseDown, Button50.MouseDown, Button49.MouseDown, Button48.MouseDown, Button47.MouseDown, Button46.MouseDown, Button45.MouseDown, Button44.MouseDown, Button43.MouseDown, Button42.MouseDown, Button41.MouseDown, Button40.MouseDown, Button39.MouseDown, Button38.MouseDown, Button37.MouseDown, Button36.MouseDown, Button35.MouseDown, Button34.MouseDown, Button33.MouseDown, Button32.MouseDown, Button31.MouseDown, Button30.MouseDown, Button29.MouseDown, Button28.MouseDown, Button27.MouseDown, Button26.MouseDown, Button25.MouseDown, Button24.MouseDown, Button23.MouseDown, Button22.MouseDown, Button21.MouseDown, Button20.MouseDown, Button19.MouseDown, Button18.MouseDown, Button17.MouseDown, Button16.MouseDown, Button15.MouseDown, Button14.MouseDown, Button13.MouseDown, Button12.MouseDown, Button11.MouseDown, Button10.MouseDown, Button99.MouseDown, Button98.MouseDown, Button97.MouseDown, Button96.MouseDown, Button95.MouseDown, Button94.MouseDown, Button93.MouseDown, Button92.MouseDown, Button91.MouseDown, Button90.MouseDown, Button89.MouseDown, Button88.MouseDown, Button87.MouseDown, Button86.MouseDown, Button85.MouseDown, Button84.MouseDown, Button83.MouseDown, Button82.MouseDown, Button81.MouseDown, Button80.MouseDown, Button79.MouseDown, Button78.MouseDown, Button77.MouseDown, Button76.MouseDown, Button75.MouseDown, Button74.MouseDown, Button73.MouseDown, Button72.MouseDown, Button71.MouseDown, Button70.MouseDown, Button69.MouseDown, Button68.MouseDown, Button67.MouseDown, Button66.MouseDown, Button65.MouseDown, Button64.MouseDown, Button63.MouseDown, Button62.MouseDown, Button61.MouseDown, Button120.MouseDown, Button119.MouseDown, Button118.MouseDown, Button117.MouseDown, Button116.MouseDown, Button115.MouseDown, Button114.MouseDown, Button113.MouseDown, Button112.MouseDown, Button111.MouseDown, Button110.MouseDown, Button109.MouseDown, Button108.MouseDown, Button107.MouseDown, Button106.MouseDown, Button105.MouseDown, Button104.MouseDown, Button103.MouseDown, Button102.MouseDown, Button101.MouseDown, Button100.MouseDown, Button200.MouseDown, Button199.MouseDown, Button198.MouseDown, Button197.MouseDown, Button196.MouseDown, Button195.MouseDown, Button194.MouseDown, Button193.MouseDown, Button192.MouseDown, Button191.MouseDown, Button190.MouseDown, Button189.MouseDown, Button188.MouseDown, Button187.MouseDown, Button186.MouseDown, Button185.MouseDown, Button184.MouseDown, Button183.MouseDown, Button182.MouseDown, Button181.MouseDown, Button180.MouseDown, Button179.MouseDown, Button178.MouseDown, Button177.MouseDown, Button176.MouseDown, Button175.MouseDown, Button174.MouseDown, Button173.MouseDown, Button172.MouseDown, Button171.MouseDown, Button170.MouseDown, Button169.MouseDown, Button168.MouseDown, Button167.MouseDown, Button166.MouseDown, Button165.MouseDown, Button164.MouseDown, Button163.MouseDown, Button162.MouseDown, Button161.MouseDown, Button160.MouseDown, Button159.MouseDown, Button158.MouseDown, Button157.MouseDown, Button156.MouseDown, Button155.MouseDown, Button154.MouseDown, Button153.MouseDown, Button152.MouseDown, Button151.MouseDown, Button150.MouseDown, Button149.MouseDown, Button148.MouseDown, Button147.MouseDown, Button146.MouseDown, Button145.MouseDown, Button144.MouseDown, Button143.MouseDown, Button142.MouseDown, Button141.MouseDown, Button140.MouseDown, Button139.MouseDown, Button138.MouseDown, Button137.MouseDown, Button136.MouseDown, Button135.MouseDown, Button134.MouseDown, Button133.MouseDown, Button132.MouseDown, Button131.MouseDown, Button130.MouseDown, Button129.MouseDown, Button128.MouseDown, Button127.MouseDown, Button126.MouseDown, Button125.MouseDown, Button124.MouseDown, Button123.MouseDown, Button122.MouseDown, Button121.MouseDown
        'Event triggered when any cell is clicked
        Dim clickedBox As Button = CType(sender, Button)

        'Mark a button as one with a bomb with an F for flag
        If e.Button = MouseButtons.Right Then
            If clickedBox.Text = "F" Then
                clickedBox.Text = ""
                clickedBox.BackColor = Color.Transparent
                tbMines.Text += 1
            Else
                clickedBox.Text = "F"
                tbMines.Text -= 1
            End If
            Return
        End If

        While clickedBox.Text = "F"
            clickedBox.Tag = 0
        End While

        'If you left click on a bomb you lose the game
        If clickedBox.Tag = 1 Then
            ShowAllBombs()
            MessageBox.Show("You Lose")
            timerRunning = False
            Return
        End If

        Dim row As Integer
        Dim col As Integer

        'runs the private sub for countbombs
        For r As Integer = 0 To 9
            For c As Integer = 0 To 19
                If arrayCells(r, c).Name = clickedBox.Name Then
                    row = r
                    col = c
                    Exit For
                End If
            Next
        Next

        CountBombs(row, col)

    End Sub

    Private Sub Button_MouseUp(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        timerRunning = False
    End Sub

    Private Sub Button201_Click(sender As Object, e As EventArgs) Handles Button201.Click

        'Display high scores on the leaderboard
        Dim leaderboard As String = "C:\temp\leaderboard.txt"

        If System.IO.File.Exists(leaderboard) = True Then
            Process.Start(leaderboard)
        Else
            MessageBox.Show("No scores have been recorded yet!")
        End If
    End Sub
End Class

Public Class Score
    Public Name As String
    Public Mines As Integer
    Public Time As Integer
End Class
