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
                _amountOfPoints *= _multiplier;
            });
            return S;
        }

        private IEnumerable<Point> GenerateRandPoints(int amountOfPoints)
        {
            var rand = new Random();

            var aPoint = _abcdFigure.Points.First(mp => mp.Title == "A");
            var dPoint = _abcdFigure.Points.First(mp => mp.Title == "D");
            var bPoint = _abcdFigure.Points.First(mp => mp.Title == "B");

            for (int i = 0; i < amountOfPoints; i++)
            {
                var x = rand.Next(aPoint.Point.X, dPoint.Point.X);
                var y = rand.Next(aPoint.Point.Y, bPoint.Point.Y);
                yield return new Point(x, y);
            }
        }
    }