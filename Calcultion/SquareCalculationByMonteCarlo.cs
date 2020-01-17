using MonteKarloWPFApp1.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Point = System.Windows.Point;

namespace MonteKarloWPFApp1.Calcultion
{
    public class SquareCalculationByMonteCarlo
    {
        private MyFigure _abcdFigure;
        private MyFigure _aoeFigure;
        private int _amountOfPoints;
        private int _multiplier;
        private double _rectSquare;

        /// <summary>
        /// SquareCalculationByMonteCarlo
        /// </summary>
        /// <param name="abcdFigure">Прямоугольник ABCD</param>
        /// <param name="aoeFigure">Треугольник AOE</param>
        /// <param name="initialAmountOfPoints">Начальное кол-во точек</param>
        /// <param name="multiplier">Множитель</param>
        public SquareCalculationByMonteCarlo(MyFigure abcdFigure, MyFigure aoeFigure, int initialAmountOfPoints, int multiplier)
        {
            _abcdFigure = abcdFigure;
            _aoeFigure = aoeFigure;
            _amountOfPoints = initialAmountOfPoints;
            _multiplier = multiplier;

            var aPoint = _abcdFigure.Points.First(mp => mp.Title == "A").Point;
            var bPoint = _abcdFigure.Points.First(mp => mp.Title == "B").Point;
            var cPoint = _abcdFigure.Points.First(mp => mp.Title == "C").Point;

            double abLength = Math.Abs(aPoint.Y - bPoint.Y);
            double bcLength = Math.Abs(bPoint.X - cPoint.X);

            _rectSquare = abLength * bcLength;
        }

        public double Execute(out long measuredTime, out IEnumerable<Point> randPoints)
        {
            double S = 0;
            IEnumerable<Point> randPointsLocal = null;
            measuredTime = TimeCalculation.MeasureTime(() =>
            {
                randPointsLocal = GenerateRandPoints(_amountOfPoints);
                var pointsInFigure = GetAmountOfRandPointsInFigure(randPointsLocal);
                S = ((double)pointsInFigure.Count() / _amountOfPoints) * _rectSquare;
                _amountOfPoints *= _multiplier;
            });
            randPoints = randPointsLocal;
            return S;
        }

        /// <summary>
        /// Определяет кол-во точек, попавших в фигуру
        /// </summary>
        private IEnumerable<Point> GetAmountOfRandPointsInFigure(IEnumerable<Point> points)
        {
            var oPoint = _aoeFigure.Points.First(mp => mp.Title == "O").Point;
            var ePoint = _aoeFigure.Points.First(mp => mp.Title == "E").Point;
            var aPoint = _aoeFigure.Points.First(mp => mp.Title == "A").Point;

            double oeLength = Math.Abs(ePoint.X - oPoint.X); // Radius

            foreach (var point in points)
            {
                if (point.Y >= oPoint.Y)
                {
                    var xOffset = Math.Abs(point.X - oPoint.X);
                    var yOffset = Math.Abs(point.Y - oPoint.Y);
                    var l = Math.Pow(Math.Pow(xOffset, 2) + Math.Pow(yOffset, 2), 0.5);
                    if (l < oeLength)
                    {
                        yield return point; // Точка принадлежит 1/4 окружности
                    }
                }
                else
                {
                    var m1 = (oPoint.X - point.X) * (aPoint.Y - oPoint.Y) - (aPoint.X - oPoint.X) * (oPoint.Y - point.Y);
                    var m2 = (aPoint.X - point.X) * (ePoint.Y - aPoint.Y) - (ePoint.X - aPoint.X) * (aPoint.Y - point.Y);
                    var m3 = (ePoint.X - point.X) * (oPoint.Y - ePoint.Y) - (oPoint.X - ePoint.X) * (ePoint.Y - point.Y);
                    if (CheckPointInTriangle(m1, m2, m3))
                    {
                        yield return point; // Точка принадлежит треугольнику
                    }
                }
            }
        }

        private bool CheckPointInTriangle(double m1, double m2, double m3)
        {
            // Точка лежит на одной из сторон
            var boolPart1 = (m1 == 0 && m2 != 0 && m3 != 0) || (m1 != 0 && m2 == 0 && m3 != 0) || (m1 != 0 && m2 != 0 && m3 == 0);
            // Точка лежит внутри треугольника
            var boolPart2 = (m1 > 0 && m2 > 0 && m3 > 0) || (m1 < 0 && m2 < 0 && m3 < 0);
            return boolPart1 || boolPart2;
        }

        /// <summary>
        /// Рандомим точки (точки имеют координаты X и Y вещественных чисел до тысячной)
        /// </summary>
        private IEnumerable<Point> GenerateRandPoints(int amountOfPoints)
        {
            var rand = new Random();

            var aPoint = _abcdFigure.Points.First(mp => mp.Title == "A").Point;
            var dPoint = _abcdFigure.Points.First(mp => mp.Title == "D").Point;
            var bPoint = _abcdFigure.Points.First(mp => mp.Title == "B").Point;

            for (int i = 0; i < amountOfPoints; i++)
            {
                var x = rand.Next((int)aPoint.X, (int)dPoint.X);
                var y = rand.Next((int)aPoint.Y, (int)bPoint.Y);
                var dvR = rand.Next(1, 1000) * 0.001;
                yield return new Point(x + dvR, y + dvR);
            }
        }
    }
}