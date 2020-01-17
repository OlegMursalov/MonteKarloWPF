using MonteKarloWPFApp1.Calcultion;
using MonteKarloWPFApp1.Consts;
using MonteKarloWPFApp1.Drawing;
using MonteKarloWPFApp1.DTO;
using MonteKarloWPFApp1.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
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

        private void DrawFigure_Click(object sender, RoutedEventArgs e)
        {
            _mainBackgroundWorker = new BackgroundWorker();
            var drawFigureControlWorkers = new DrawFigureControlWorkers(this);
            _mainBackgroundWorker.DoWork += drawFigureControlWorkers.MainBackgroundWorker_DrawFigure_DoWork;
            _mainBackgroundWorker.RunWorkerCompleted += drawFigureControlWorkers.MainBackgroundWorker_DrawFigure_RunWorkerCompleted;
            _mainBackgroundWorker.RunWorkerAsync();
        }

        private void MainCalc_Click(object sender, RoutedEventArgs e)
        {
            _mainBackgroundWorker = new BackgroundWorker();
            var mainCalcControlWorkers = new MainCalcControlWorkers(this);
            _mainBackgroundWorker.DoWork += mainCalcControlWorkers.MainBackgroundWorker_MainCalc_DoWork;
            _mainBackgroundWorker.RunWorkerCompleted += mainCalcControlWorkers.MainBackgroundWorker_MainCalc_RunWorkerCompleted;
            _mainBackgroundWorker.RunWorkerAsync();
        }

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