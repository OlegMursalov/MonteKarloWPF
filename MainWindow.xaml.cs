using MonteKarloWPFApp1.Calcultion;
using MonteKarloWPFApp1.Consts;
using MonteKarloWPFApp1.Drawing;
using MonteKarloWPFApp1.DTO;
using System.Collections.Generic;
using System.Windows;

namespace MonteKarloWPFApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DrawingDTO _drawingDTO;
        private CalculationDTO _calculationDTO;
        private ReportDTO _reportDTO;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawFigure_Click(object sender, RoutedEventArgs e)
        {
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

            if (!(ab >= bc))
            {
                MessageBox.Show(Strings.ABIsntGreaterThanBC_Msg_Str);
                return;
            }

            MainCanvas.Children.Clear();

            var allPoints = new Dictionary<string, MyPoint>
            {
                { "A", new MyPoint(new System.Drawing.Point(spaceLeft, spaceBottom), "A") },
                { "B", new MyPoint(new System.Drawing.Point(spaceLeft, spaceBottom + ab), "B") },
                { "C", new MyPoint(new System.Drawing.Point(spaceLeft + bc, spaceBottom + ab), "C") },
                { "D", new MyPoint(new System.Drawing.Point(spaceLeft + bc, spaceBottom), "D") },
                { "O", new MyPoint(new System.Drawing.Point(spaceLeft, spaceBottom + ab - bc), "O") },
                { "E", new MyPoint(new System.Drawing.Point(spaceLeft + bc, spaceBottom + ab - bc), "E") }
            };

            var colorFigure = GlobalParams.ColorFigure;
            var scaleNumber = GlobalParams.ScaleNumber;
            _drawingDTO = new DrawingDTO();

            _drawingDTO.AbcdFigure = new MyFigure(new MyPoint[]
            {
                allPoints["A"],
                allPoints["B"],
                allPoints["C"],
                allPoints["D"]
            }, colorFigure);
            var abcdFigureDrawer = new CustomDrawer(this, _drawingDTO.AbcdFigure, scaleNumber);
            abcdFigureDrawer.DrawLines();

            _drawingDTO.AoeFigure = new MyFigure(new MyPoint[]
            {
                allPoints["A"],
                allPoints["O"],
                allPoints["E"]
            }, colorFigure);
            var oaeDrawerFigure = new CustomDrawer(this, _drawingDTO.AoeFigure, scaleNumber);
            oaeDrawerFigure.DrawLines();

            _drawingDTO.BeArcFigure = new MyFigure(new MyPoint[]
            {
                allPoints["B"],
                allPoints["E"],
            }, colorFigure);
            var beArcDrawerFigure = new CustomDrawer(this, _drawingDTO.BeArcFigure, scaleNumber);
            beArcDrawerFigure.DrawArc();

            _calculationDTO = null;
            _reportDTO = null;
        }

        private void MainCalc_Click(object sender, RoutedEventArgs e)
        {
            if (_drawingDTO == null)
            {
                MessageBox.Show(Strings.FigureIsntDrawed_Msg_Str);
                return;
            }

            _calculationDTO = new CalculationDTO();

            IScuareCalculation squareCalculation = new SquareCalculationByFormuls(_drawingDTO.AbcdFigure, _drawingDTO.AoeFigure);
            var sByFormuls = squareCalculation.Execute(out long measuredTimeSByFormuls);
            _calculationDTO.InfoByFormuls = new InfoByFormuls
            {
                Square = sByFormuls,
                MeasuredTime = measuredTimeSByFormuls
            };

            var amountOfPointsMC = GlobalParams.InitialAmountOfPointsMC;
            var multiplierMC = GlobalParams.MultiplierMC;
            squareCalculation = new SquareCalculationByMonteCarlo(_drawingDTO.AbcdFigure, _drawingDTO.AoeFigure, amountOfPointsMC, multiplierMC);

            for (int i = 0; i < 5; i++)
            {
                var sByMonteCarlo = squareCalculation.Execute(out long measuredTimeSByMonteCarlo);
                _calculationDTO.InfoByMonteCarlo.Add(new InfoByMonteCarlo
                {
                    Square = sByMonteCarlo,
                    MeasuredTime = measuredTimeSByMonteCarlo,
                    AmountOPoints = amountOfPointsMC
                });
                amountOfPointsMC *= multiplierMC;
            }
        }

        private void GetReport_Click(object sender, RoutedEventArgs e)
        {
            if (_calculationDTO == null)
            {
                MessageBox.Show(Strings.CalcIsntExecuted_Msg_Str);
                return;
            }

            
        }
    }
}