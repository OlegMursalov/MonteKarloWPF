using MonteKarloWPFApp1.Calcultion;
using MonteKarloWPFApp1.Consts;
using MonteKarloWPFApp1.Drawing;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;

namespace MonteKarloWPFApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isFigureDrawed;
        private bool _isCalcExecuted;
        private MyFigure _abcdFigure;
        private MyFigure _aoeFigure;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawFigure_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Clear();

            int bc, ab;
            if (!(int.TryParse(BC.Text, out bc) && int.TryParse(AB.Text, out ab)))
            {
                MessageBox.Show(Strings.FormTextBoxesBC_AB_Msg_Str);
                return;
            }

            int spaceLeft, spaceBottom;
            if (!(int.TryParse(SpaceLeft.Text, out spaceLeft) && int.TryParse(SpaceBottom.Text, out spaceBottom)))
            {
                MessageBox.Show(Strings.FormSpacesMsg_Str);
                return;
            }

            if (!(spaceLeft > 0 && spaceBottom > 0 && spaceLeft <= 20 && spaceBottom <= 20))
            {
                MessageBox.Show(Strings.FormSpaces_RestrictsMsg_Str);
                return;
            }

            _abcdFigure = new MyFigure(new MyPoint[]
            {
                new MyPoint(new System.Drawing.Point(spaceLeft, spaceBottom), "A"),
                new MyPoint(new System.Drawing.Point(spaceLeft, spaceBottom + ab), "B"),
                new MyPoint(new System.Drawing.Point(spaceLeft + bc, spaceBottom + ab), "C"),
                new MyPoint(new System.Drawing.Point(spaceLeft + bc, spaceBottom), "D")
            }, System.Windows.Media.Color.FromRgb(45, 67, 234));
            var abcdFigureDrawer = new CustomDrawer(this, _abcdFigure, GlobalParams.ScaleNumber);
            abcdFigureDrawer.DrawLines();

            _aoeFigure = new MyFigure(new MyPoint[]
            {
                new MyPoint(new System.Drawing.Point(spaceLeft, spaceBottom), "A"),
                new MyPoint(new System.Drawing.Point(spaceLeft, spaceBottom + ab - bc), "O"),
                new MyPoint(new System.Drawing.Point(spaceLeft + bc, spaceBottom + ab - bc), "E"),
            }, System.Windows.Media.Color.FromRgb(0, 67, 0));
            var oaeDrawerFigure = new CustomDrawer(this, _aoeFigure, GlobalParams.ScaleNumber);
            oaeDrawerFigure.DrawLines();

            var bPoint = _abcdFigure.Points.First(mp => mp.Title == "B").Point;
            var ePoint = _aoeFigure.Points.First(mp => mp.Title == "E").Point;
            oaeDrawerFigure.DrawArc(bPoint, ePoint);

            _isFigureDrawed = true;
            _isCalcExecuted = false;
        }

        private void MainCalc_Click(object sender, RoutedEventArgs e)
        {
            if (!_isFigureDrawed)
            {
                MessageBox.Show(Strings.FigureIsntDrawed_Msg_Str);
                return;
            }

            IScuareCalculation squareCalculation = new SquareCalculationByFormuls(_abcdFigure, _aoeFigure);
            var sByFormuls = squareCalculation.Execute(out long measuredTimeSByFormuls);

            var dictionary = new Dictionary<double, long>();
            squareCalculation = new SquareCalculationByMonteCarlo(_abcdFigure, _aoeFigure, 1000, 10);
            for (int i = 0; i < 5; i++)
            {
                var sByMonteCarlo = squareCalculation.Execute(out long measuredTimeSByMonteCarlo);
                dictionary.Add(sByMonteCarlo, measuredTimeSByMonteCarlo);
            }

            _isCalcExecuted = true;
        }

        private void GetReport_Click(object sender, RoutedEventArgs e)
        {
            if (!_isCalcExecuted)
            {
                MessageBox.Show(Strings.CalcIsntExecuted_Msg_Str);
                return;
            }
        }
    }
}