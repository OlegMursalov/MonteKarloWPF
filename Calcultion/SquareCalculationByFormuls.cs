using MonteKarloWPFApp1.Drawing;
using System;
using System.Linq;

namespace MonteKarloWPFApp1.Calcultion
{
    public class SquareCalculationByFormuls : IScuareCalculation
    {
        private MyFigure _abcdFigure;
        private MyFigure _aoeFigure;

        public SquareCalculationByFormuls(MyFigure abcdFigure, MyFigure aoeFigure)
        {
            _abcdFigure = abcdFigure;
            _aoeFigure = aoeFigure;
        }

        public double Execute(out long measuredTime)
        {
            double S = 0;
            measuredTime = TimeCalculation.MeasureTime(() =>
            {
                var oPoint = _aoeFigure.Points.First(mp => mp.Title == "O");
                var ePoint = _aoeFigure.Points.First(mp => mp.Title == "E");
                var aPoint = _abcdFigure.Points.First(mp => mp.Title == "A");
                var bPoint = _abcdFigure.Points.First(mp => mp.Title == "B");

                double oeLength = Math.Abs(ePoint.Point.X - oPoint.Point.X);
                double abLength = Math.Abs(bPoint.Point.Y - aPoint.Point.Y);
                double oaLenth = Math.Abs(abLength - oeLength);

                var partOfCircleSquare = (Math.PI * Math.Pow(oeLength, 2)) / 4;
                var triangleSquare = oaLenth * oeLength / 2;

                S = partOfCircleSquare + triangleSquare;
            });
            return S;
        }
    }
}