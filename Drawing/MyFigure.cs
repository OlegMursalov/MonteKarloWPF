using System.Windows.Media;

namespace MonteKarloWPFApp1.Drawing
{
    public class MyFigure
    {
        public MyPoint[] Points { get; }
        public Color ColorStroke { get; }

        public MyFigure(MyPoint[] points, Color colorStroke)
        {
            Points = points;
            ColorStroke = colorStroke;
        }
    }
}