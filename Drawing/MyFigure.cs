namespace MonteKarloWPFApp1.Drawing
{
    public class MyFigure
    {
        public MyPoint[] Points { get; }
        public System.Windows.Media.Color ColorStroke { get; }

        public MyFigure(MyPoint[] points, System.Windows.Media.Color colorStroke)
        {
            Points = points;
            ColorStroke = colorStroke;
        }
    }
}