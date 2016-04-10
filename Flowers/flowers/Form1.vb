Public Class Form1


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click
        Dim ArrayTextBoxes() As TextBox = {TextBox1,
                                            TextBox2,
                                            TextBox3,
                                            TextBox4,
                                            TextBox5,
                                            TextBox6,
                                            TextBox7,
                                            TextBox8,
                                            TextBox9,
                                            TextBox10,
                                            TextBox11,
                                            TextBox12,
                                            TextBox13,
                                            TextBox14,
                                            TextBox15,
                                            TextBox16,
                                            TextBox17,
                                            TextBox18,
                                            TextBox19,
                                            TextBox20,
                                            TextBox21,
                                            TextBox22,
                                            TextBox23,
                                            TextBox24,
                                            TextBox25,
                                            TextBox26,
                                            TextBox27,
                                            TextBox28,
                                            TextBox29,
                                            TextBox30,
                                            TextBox31,
                                            TextBox32,
                                            TextBox33,
                                            TextBox34,
                                            TextBox35,
                                            TextBox36,
                                            TextBox37,
                                            TextBox38,
                                            TextBox39,
                                            TextBox40,
                                            TextBox41,
                                            TextBox42,
                                            TextBox43,
                                            TextBox44,
                                            TextBox45,
                                            TextBox46,
                                            TextBox47,
                                            TextBox48,
                                            TextBox49,
                                            TextBox50,
                                            TextBox51,
                                            TextBox52,
                                            TextBox53,
                                            TextBox54,
                                            TextBox55,
                                            TextBox56,
                                            TextBox57,
                                            TextBox58,
                                            TextBox59,
                                            TextBox60,
                                            TextBox61,
                                            TextBox62,
                                            TextBox63,
                                            TextBox64,
                                            TextBox65,
                                            TextBox66,
                                            TextBox67,
                                            TextBox68,
                                            TextBox69,
                                            TextBox70,
                                            TextBox71,
                                            TextBox72,
                                            TextBox73,
                                            TextBox74,
                                            TextBox75,
                                            TextBox76,
                                            TextBox77,
                                            TextBox78,
                                            TextBox79,
                                            TextBox80,
                                            TextBox81,
                                            TextBox82,
                                            TextBox83,
                                            TextBox84,
                                            TextBox85,
                                            TextBox86,
                                            TextBox87,
                                            TextBox88,
                                            TextBox89,
                                            TextBox90,
                                            TextBox91,
                                            TextBox92,
                                            TextBox93,
                                            TextBox94,
                                            TextBox95,
                                            TextBox96,
                                            TextBox97,
                                            TextBox98,
                                            TextBox99,
                                            TextBox100}

        Dim Gen, Chance As Integer
        'Assume the user wants 15 generations if they do not specify
        If TextBoxGen.Text = "" Then
            Gen = 15
        Else
            Gen = TextBoxGen.Text
        End If

        'Assume the user wants chance of 25 if they do not specify
        If TextBoxChance.Text = "" Then
            Chance = 25
        Else
            Chance = TextBoxChance.Text
        End If

        'Allows user to set delay time in seconds between each generation and each flower
        Dim delay1 As Double
        If TextBoxDelay1.Text = "" Then
            delay1 = 0.005
        Else
            delay1 = CDbl(TextBoxDelay1.Text)
        End If
        Dim delay2 As Double
        If TextBoxDelay2.Text = "" Then
            delay2 = 0.1
        Else
            delay2 = CDbl(TextBoxDelay1.Text)
        End If

        Dim Generation As Integer = -1

        'Initialize all boxes to red
        For i As Integer = 0 To ArrayTextBoxes.Length - 1
            ArrayTextBoxes(i).BackColor = Color.Red
        Next

        'Complete for every generation
        For c As Integer = 0 To Gen
            Dim random As Integer

            'At each box, chance to change color to purple
            For i As Integer = 0 To ArrayTextBoxes.Length - 1
                Dim rn As New Random
                random = rn.Next(1, 100)
                TextBoxTest.Text = random
                If random < Chance Then
                    ArrayTextBoxes(i).BackColor = Color.Purple
                Else
                    ArrayTextBoxes(i).BackColor = Color.Red
                End If
                Delay(delay1)
            Next
            Delay(delay2)
            Generation += 1
            TextBoxGeneration.Text = Generation
        Next

    End Sub

    Private Sub ButtonReset_Click(sender As Object, e As EventArgs) Handles ButtonReset.Click
        Dim ArrayTextBoxes() As TextBox = {TextBox1,
                                            TextBox2,
                                            TextBox3,
                                            TextBox4,
                                            TextBox5,
                                            TextBox6,
                                            TextBox7,
                                            TextBox8,
                                            TextBox9,
                                            TextBox10,
                                            TextBox11,
                                            TextBox12,
                                            TextBox13,
                                            TextBox14,
                                            TextBox15,
                                            TextBox16,
                                            TextBox17,
                                            TextBox18,
                                            TextBox19,
                                            TextBox20,
                                            TextBox21,
                                            TextBox22,
                                            TextBox23,
                                            TextBox24,
                                            TextBox25,
                                            TextBox26,
                                            TextBox27,
                                            TextBox28,
                                            TextBox29,
                                            TextBox30,
                                            TextBox31,
                                            TextBox32,
                                            TextBox33,
                                            TextBox34,
                                            TextBox35,
                                            TextBox36,
                                            TextBox37,
                                            TextBox38,
                                            TextBox39,
                                            TextBox40,
                                            TextBox41,
                                            TextBox42,
                                            TextBox43,
                                            TextBox44,
                                            TextBox45,
                                            TextBox46,
                                            TextBox47,
                                            TextBox48,
                                            TextBox49,
                                            TextBox50,
                                            TextBox51,
                                            TextBox52,
                                            TextBox53,
                                            TextBox54,
                                            TextBox55,
                                            TextBox56,
                                            TextBox57,
                                            TextBox58,
                                            TextBox59,
                                            TextBox60,
                                            TextBox61,
                                            TextBox62,
                                            TextBox63,
                                            TextBox64,
                                            TextBox65,
                                            TextBox66,
                                            TextBox67,
                                            TextBox68,
                                            TextBox69,
                                            TextBox70,
                                            TextBox71,
                                            TextBox72,
                                            TextBox73,
                                            TextBox74,
                                            TextBox75,
                                            TextBox76,
                                            TextBox77,
                                            TextBox78,
                                            TextBox79,
                                            TextBox80,
                                            TextBox81,
                                            TextBox82,
                                            TextBox83,
                                            TextBox84,
                                            TextBox85,
                                            TextBox86,
                                            TextBox87,
                                            TextBox88,
                                            TextBox89,
                                            TextBox90,
                                            TextBox91,
                                            TextBox92,
                                            TextBox93,
                                            TextBox94,
                                            TextBox95,
                                            TextBox96,
                                            TextBox97,
                                            TextBox98,
                                            TextBox99,
                                            TextBox100}

        'Initialize all boxes to white
        For i As Integer = 0 To ArrayTextBoxes.Length - 1
            ArrayTextBoxes(i).BackColor = Color.White
        Next

        TextBoxGen.Text = ""
        TextBoxGeneration.Text = ""
        TextBoxChance.Text = ""
        TextBoxTest.Text = ""

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        MessageBox.Show("This program simulates the genetics of imaginary flowers. At each generation, every flower has a chance to turn purple. Type whole numbers into the Maximum Generations and Chance of Purple boxes and click Start. At the end of the last generation, click Reset to clear all the boxes and try again. If you do not specify a number of generations or a chance for purple, the program uses the defaults.")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ButtonAdvanced.Click
        LabelRandom.Show()
        TextBoxTest.Show()
        LabelDelay1.Show()
        TextBoxDelay1.Show()
        LabelDelay2.Show()
        TextBoxDelay2.Show()
        Me.Height = 500

    End Sub
End Class
