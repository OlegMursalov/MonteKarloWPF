using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;

namespace MonteKarloWPFApp1.Helpers
{
    public class CoordinatesTransformatorHelper
    {
        private Canvas _canvas;

        public CoordinatesTransformatorHelper(Canvas canvas)
        {
            _canvas = canvas;
        }

        public IEnumerable<Point> GetNormalPoints(IEnumerable<Point> points)
        {
            foreach (var point in points)
            {
                /*double x = ((point.Y * _canvas.ActualWidth) / 360.0) - 180.0;
                double y = ((point.X * _canvas.ActualHeight) / 180.0) - 90.0;
                yield return new Point((int)x, (int)y);*/
                yield return point;
            }
        }
    }
}