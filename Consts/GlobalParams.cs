using System.Windows.Media;

namespace MonteKarloWPFApp1.Consts
{
    public static class GlobalParams
    {
        /// <summary>
        /// Коэффициент масштабируемости
        /// </summary>
        public static readonly int ScaleNumber = 3;

        /// <summary>
        /// Цвет фигуры
        /// </summary>
        public static readonly Color FigureColor = Color.FromRgb(51, 78, 255);

        /// <summary>
        /// Начальное значение кол-ва точек для вычисления площади фигуры методом Монте-Карло
        /// </summary>
        public static readonly int InitialAmountOfPointsMC = 1000;

        /// <summary>
        /// Множитель для вычисления площади фигуры методом Монте-Карло
        /// </summary>
        public static readonly int MultiplierMC = 10;

        /// <summary>
        /// Цвет срандомленной точки
        /// </summary>
        public static readonly Color RandColorPoint = Color.FromRgb(0, 0, 0);
    }
}