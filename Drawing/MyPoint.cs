using System.Drawing;

namespace MonteKarloWPFApp1.Drawing
{
    public class MyPoint
    {
        public Point Point { get; }
        public string Title { get; }

        public MyPoint(Point point, string title)
        {
            Point = point;
            Title = title;
        }
    }
}