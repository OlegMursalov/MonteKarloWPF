using MonteKarloWPFApp1.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteKarloWPFApp1.Calcultion
{
    public class SquareCalculationByMonteCarlo : IScuareCalculation
    {
        private MyFigure _abcdFigure;
        private MyFigure _aoeFigure;
        private int _amountOfPoints;
        private int _multiplier;

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
        }

        public double Execute(out long measuredTime)
        {
            double S = 0;
            measuredTime = TimeCalculation.MeasureTime(() =>
            {
                var randPoints = GenerateRandPoints(_amountOfPoints);
                S = GetAmountOfRandPointsInFigure(randPoints);
                _amountOfPoints *= _multiplier;
            });
            return S;
        }

        /// <summary>
        /// Определяет кол-во точек, попавших в фигуру
        /// </summary>
        private int GetAmountOfRandPointsInFigure(IEnumerable<Point> points)
        {
            var amountPointsInFigure = 0;

            var oPoint = _abcdFigure.Points.First(mp => mp.Title == "O").Point;
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
                        amountPointsInFigure++; // Точка принадлежит 1/4 окружности
                    }
                }
                else
                {
                    var m1 = (oPoint.X - point.X) * (aPoint.Y - oPoint.Y) - (aPoint.X - oPoint.X) * (oPoint.Y - point.Y);
                    var m2 = (aPoint.X - point.X) * (ePoint.Y - aPoint.Y) - (ePoint.X - aPoint.X) * (aPoint.Y - point.Y);
                    var m3 = (ePoint.X - point.X) * (oPoint.Y - ePoint.Y) - (oPoint.X - ePoint.X) * (ePoint.Y - point.Y);
                    if (CheckPointInTriangle(m1, m2, m3))
                    {
                        amountPointsInFigure++; // Точка принадлежит треугольнику
                    }
                }
            }

            return amountPointsInFigure;
        }

        private bool CheckPointInTriangle(int m1, int m2, int m3)
        {
            // Точка лежит на одной из сторон
            var boolPart1 = (m1 == 0 && m2 != 0 && m3 != 0) || (m1 != 0 && m2 == 0 && m3 != 0) || (m1 != 0 && m2 != 0 && m3 == 0);
            // Точка лежит внутри треугольника
            var boolPart2 = (m1 > 0 && m2 > 0 && m3 > 0) || (m1 < 0 && m2 < 0 && m3 < 0);
            return boolPart1 || boolPart2;
        }

        private IEnumerable<Point> GenerateRandPoints(int amountOfPoints)
        {
            var rand = new Random();

            var aPoint = _abcdFigure.Points.First(mp => mp.Title == "A").Point;
            var dPoint = _abcdFigure.Points.First(mp => mp.Title == "D").Point;
            var bPoint = _abcdFigure.Points.First(mp => mp.Title == "B").Point;

            for (int i = 0; i < amountOfPoints; i++)
            {
                var x = rand.Next(aPoint.X, dPoint.X);
                var y = rand.Next(aPoint.Y, bPoint.Y);
                yield return new Point(x, y);
            }
        }
    }
}