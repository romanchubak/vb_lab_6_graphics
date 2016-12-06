Public Class Form1
    Dim Graph1 As Graphics
    Dim brush3 As New SolidBrush(Color.Red)
    Dim x As Integer, y As Integer, r As Integer

    Dim list_start_Pol(18) As Poligon 'As New List(Of Poligon)
    Dim list_cur_pol(18) As Poligon 'As New List(Of Poligon)
    Dim N As Integer = 0

    Class Poligon
        Public Points As New List(Of Point)
        Public color As Color

        Public Sub New()

        End Sub

        Public Sub New(ByVal list As List(Of Point))
            Points = list
        End Sub

        Public Sub New(ByVal Pol As Poligon)
            Me.color = Pol.color
            For i = 0 To Pol.Points.Count - 1
                Me.Points.Add(Pol.Points(i))
            Next
        End Sub

        Public Sub addPoint(ByVal new_p As Point)
            Points.Add(new_p)
        End Sub

        Public Function getArr()
            Dim rez(Points.Count - 1) As Point

            For i = 0 To Points.Count - 1
                rez(i) = Points(i)
            Next
            Return rez
        End Function
    End Class

    Private Sub initialize()
        N = 0
        Graph1 = Me.PictureBox1.CreateGraphics()




        Dim P_outside(12) As Point, P_inside(12) As Point
        Dim gradus As Double = -90.5, radian As Double
        For i = 0 To 12
            radian = (gradus) * (Math.PI / 180)
            P_outside(i).X = x + r + r * Math.Cos(radian)
            P_outside(i).Y = y + r + r * Math.Sin(radian)

            radian = (gradus + 14) * (Math.PI / 180)
            P_inside(i).X = x + r + r / 2 * Math.Cos(radian)
            P_inside(i).Y = y + r + r / 2 * Math.Sin(radian)
            gradus += 30

        Next



        For i = 1 To 10
            Dim P As New Poligon
            P.addPoint(New Point(P_outside(i)))
            P.addPoint(New Point(P_inside(i - 1)))
            P.addPoint(New Point((x + r), (y + r)))
            P.addPoint(New Point(P_inside(i + 1)))
            P.addPoint(New Point(P_outside(i + 1)))
            P.addPoint(New Point(P_inside(i)))
            list_start_Pol(N) = P
            list_cur_pol(N) = New Poligon(P)
            N += 1

            i += 1
        Next
        Dim P1 As New Poligon
        P1.addPoint(New Point(P_outside(11)))
        P1.addPoint(New Point(P_inside(10)))
        P1.addPoint(New Point(((x + r)), (y + r)))
        P1.addPoint(New Point(P_inside(0)))
        P1.addPoint(New Point(P_outside(0)))
        P1.addPoint(New Point(P_inside(11)))
        list_start_Pol(N) = P1
        list_cur_pol(N) = New Poligon(P1)
        N += 1


        For i = 0 To 10
            Dim P2 As New Poligon
            P2.addPoint(New Point(P_outside(i)))
            P2.addPoint(New Point(P_outside(i + 1)))
            P2.addPoint(New Point(P_inside(i)))
            list_start_Pol(N) = P2
            list_cur_pol(N) = New Poligon(P2)
            N += 1

        Next
        Dim P3 As New Poligon
        P3.addPoint(New Point(P_outside(11)))
        P3.addPoint(New Point(P_outside(0)))
        P3.addPoint(New Point(P_inside(11)))
        list_start_Pol(N) = P3
        list_cur_pol(N) = New Poligon(P3)
        N += 1

        For i = 0 To 17

            Graph1.FillPolygon(brush3, list_cur_pol(i).getArr())
            'Graph1.FillPolygon(brush1, list_cur_pol(5).getArr())
        Next





    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        initialize()
        Timer1.Enabled = True

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Enabled = False
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Graph1 = Me.PictureBox1.CreateGraphics()
        Graph1.Clear(Color.White)
        r = TrackBar1.Value * 20
        x = PictureBox1.Size.Width / 2 - r
        y = PictureBox1.Size.Height / 2 - r

        Graph1.FillEllipse(brush3, x, y, r * 2, r * 2)



    End Sub

    Public Function Parabolalala(P As Point, x As Integer, a As Integer, b As Integer)
        Dim Kvadrat = Function(q) q * q

        Dim y = (Kvadrat((x - (P.X + a)) / 10.0)) + P.Y - b

        Return y
    End Function
    Public Function Parabolalala2(P As Point, x As Integer, a As Integer, b As Integer)
        Dim Kvadrat = Function(q) q * q

        Dim y = Kvadrat((x - (P.X - a)) / 10.0) + P.Y - b
        Return y
    End Function



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Graph1.Clear(Color.White)

        For j = 0 To 17
            If j < 3 And list_cur_pol(j).Points(0).Y < 415 Then
                For i = 0 To list_cur_pol(j).Points.Count - 1
                    Dim point = list_cur_pol(j).Points(i)
                    point.X = point.X + (7 - j)
                    point.Y = Parabolalala(list_start_Pol(j).Points(i), point.X, 50, 50)
                    list_cur_pol(j).Points(i) = point
                Next

                Graph1.FillPolygon(brush3, list_cur_pol(j).getArr())
            Else
                Graph1.FillPolygon(brush3, list_cur_pol(j).getArr())

            End If


            If j >= 3 And j < 6 And list_cur_pol(j).Points(0).Y < 415 Then
                For i = 0 To list_cur_pol(j).Points.Count - 1
                    Dim point = list_cur_pol(j).Points(i)
                    point.X = point.X - j - 5
                    point.Y = Parabolalala2(list_start_Pol(j).Points(i), point.X, 50, 50 - (6 - j) * 10)
                    list_cur_pol(j).Points(i) = point
                Next
                Graph1.FillPolygon(brush3, list_cur_pol(j).getArr())
            Else
                Graph1.FillPolygon(brush3, list_cur_pol(j).getArr())

            End If



            If j >= 6 And j < 12 And list_cur_pol(j).Points(0).Y < 415 Then
                For i = 0 To list_cur_pol(j).Points.Count - 1
                    Dim point = list_cur_pol(j).Points(i)
                    point.X = point.X + (17 - j)
                    point.Y = Parabolalala(list_start_Pol(j).Points(i), point.X, 50, 50)
                    list_cur_pol(j).Points(i) = point
                Next
                Graph1.FillPolygon(brush3, list_cur_pol(j).getArr())
            Else
                Graph1.FillPolygon(brush3, list_cur_pol(j).getArr())

            End If

            If j >= 12 And j < 18 And list_cur_pol(j).Points(0).Y < 415 Then
                For i = 0 To list_cur_pol(j).Points.Count - 1
                    Dim point = list_cur_pol(j).Points(i)
                    point.X = point.X - j + 6
                    point.Y = Parabolalala2(list_start_Pol(j).Points(i), point.X, 50, 50)
                    list_cur_pol(j).Points(i) = point
                Next
                Graph1.FillPolygon(brush3, list_cur_pol(j).getArr())
            Else
                Graph1.FillPolygon(brush3, list_cur_pol(j).getArr())

            End If

        Next

        Timer1.Enabled = True
    End Sub
End Class
