using MonteKarloWPFApp1.Calcultion;
using MonteKarloWPFApp1.Consts;
using MonteKarloWPFApp1.Drawing;
using MonteKarloWPFApp1.DTO;
using MonteKarloWPFApp1.Structure;
using MonteKarloWPFApp1.UIHelpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace MonteKarloWPFApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal DrawingDTO DrawingDTO { get; set; }
        internal CalculationDTO CalculationDTO { get; set; }
        internal ReportDTO ReportDTO { get; set; }

        private BackgroundWorker _mainBackgroundWorker;

        public MainWindow()
        {
            InitializeComponent();
        }

        // sync
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

            DrawingDTO = new DrawingDTO();
            CalculationDTO = null;
            ReportDTO = null;

            var allPoints = new Dictionary<string, MyPoint>
            {
                { "A", new MyPoint(new Point(spaceLeft, spaceBottom), "A") },
                { "B", new MyPoint(new Point(spaceLeft, spaceBottom + ab), "B") },
                { "C", new MyPoint(new Point(spaceLeft + bc, spaceBottom + ab), "C") },
                { "D", new MyPoint(new Point(spaceLeft + bc, spaceBottom), "D") },
                { "O", new MyPoint(new Point(spaceLeft, spaceBottom + ab - bc), "O") },
                { "E", new MyPoint(new Point(spaceLeft + bc, spaceBottom + ab - bc), "E") }
            };

            var formBlocker = new FormBlocker(this);
            formBlocker.Execute(isDisabled: true);

            var colorFigure = GlobalParams.FigureColor;
            var scaleNumber = GlobalParams.ScaleNumber;
            
            DrawingDTO.AbcdFigure = new MyFigure(new MyPoint[]
            {
                allPoints["A"],
                allPoints["B"],
                allPoints["C"],
                allPoints["D"]
            }, colorFigure);
            var abcdFigureDrawer = new CustomDrawer(this, DrawingDTO.AbcdFigure, scaleNumber);
            abcdFigureDrawer.DrawFigureByLines();

            DrawingDTO.AoeFigure = new MyFigure(new MyPoint[]
            {
                allPoints["A"],
                allPoints["O"],
                allPoints["E"]
            }, colorFigure);
            var oaeDrawerFigure = new CustomDrawer(this, DrawingDTO.AoeFigure, scaleNumber);
            oaeDrawerFigure.DrawFigureByLines();

            DrawingDTO.BeArcFigure = new MyFigure(new MyPoint[]
            {
                allPoints["B"],
                allPoints["E"],
            }, colorFigure);
            var beArcDrawerFigure = new CustomDrawer(this, DrawingDTO.BeArcFigure, scaleNumber);
            beArcDrawerFigure.DrawFigureArc();

            formBlocker.Execute(isDisabled: false);
        }

        // async
        private void MainCalc_Click(object sender, RoutedEventArgs e)
        {
            if (DrawingDTO == null)
            {
                MessageBox.Show(Strings.FigureIsntDrawed_Msg_Str);
                return;
            }

            CalculationDTO = new CalculationDTO();

            var formBlocker = new FormBlocker(this);
            formBlocker.Execute(isDisabled: true);

            _mainBackgroundWorker = new BackgroundWorker();
            var mainCalcControlWorkers = new MainCalcControlWorkers(this);
            _mainBackgroundWorker.DoWork += mainCalcControlWorkers.MainBackgroundWorker_MainCalc_DoWork;
            _mainBackgroundWorker.RunWorkerCompleted += mainCalcControlWorkers.MainBackgroundWorker_MainCalc_RunWorkerCompleted;
            _mainBackgroundWorker.RunWorkerAsync(MainCalcProgressBar);
        }

        // async
        private void GetReport_Click(object sender, RoutedEventArgs e)
        {
            _mainBackgroundWorker = new BackgroundWorker();
            var getReportControlWorkers = new GetReportControlWorkers(this);
            _mainBackgroundWorker.DoWork += getReportControlWorkers.MainBackgroundWorker_GetReport_DoWork;
            _mainBackgroundWorker.RunWorkerCompleted += getReportControlWorkers.MainBackgroundWorker_GetReport_RunWorkerCompleted;
            _mainBackgroundWorker.RunWorkerAsync();
        }
    }
}