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
                var oPoint = _aoeFigure.Points.First(mp => mp.Title == "O").Point;
                var ePoint = _aoeFigure.Points.First(mp => mp.Title == "E").Point;
                var aPoint = _abcdFigure.Points.First(mp => mp.Title == "A").Point;
                var bPoint = _abcdFigure.Points.First(mp => mp.Title == "B").Point;

                double oeLength = Math.Abs(ePoint.X - oPoint.X);
                double abLength = Math.Abs(bPoint.Y - aPoint.Y);
                double oaLenth = Math.Abs(abLength - oeLength);

                var partOfCircleSquare = (Math.PI * Math.Pow(oeLength, 2)) / 4;
                var triangleSquare = oaLenth * oeLength / 2;

                S = partOfCircleSquare + triangleSquare;
            });
            return S;
        }
    }
}