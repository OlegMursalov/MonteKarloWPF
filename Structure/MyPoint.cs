using System.Drawing;

namespace MonteKarloWPFApp1.Structure
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