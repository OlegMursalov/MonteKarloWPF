using System;
using System.Diagnostics;

namespace MonteKarloWPFApp1.Calcultion
{
    public static class TimeCalculation
    {
        public static long MeasureTime(Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}