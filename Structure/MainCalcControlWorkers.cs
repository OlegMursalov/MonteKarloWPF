using MonteKarloWPFApp1.Calcultion;
using MonteKarloWPFApp1.Consts;
using MonteKarloWPFApp1.DTO;
using MonteKarloWPFApp1.UIHelpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace MonteKarloWPFApp1.Structure
{
    public class MainCalcControlWorkers
    {
        private MainWindow _mainWindow;

        public MainCalcControlWorkers(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void MainBackgroundWorker_MainCalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _mainWindow.ReportDTO = null;
        }

        public void MainBackgroundWorker_MainCalc_DoWork(object sender, DoWorkEventArgs e)
        {
            var formBlocker = new FormBlocker(_mainWindow);
            formBlocker.Equals(true);

            if (_mainWindow.DrawingDTO == null)
            {
                MessageBox.Show(Strings.FigureIsntDrawed_Msg_Str);
                return;
            }

            _mainWindow.CalculationDTO = new CalculationDTO();

            var squareCalculationByFormuls = new SquareCalculationByFormuls(_mainWindow.DrawingDTO.AbcdFigure, _mainWindow.DrawingDTO.AoeFigure);
            var sByFormuls = squareCalculationByFormuls.Execute(out long measuredTimeSByFormuls);
            _mainWindow.CalculationDTO.InfoByFormuls = new InfoByFormuls
            {
                Square = sByFormuls,
                MeasuredTime = measuredTimeSByFormuls
            };

            var amountOfPointsMC = GlobalParams.InitialAmountOfPointsMC;
            var multiplierMC = GlobalParams.MultiplierMC;
            var squareCalculationByMonteCarlo = new SquareCalculationByMonteCarlo(_mainWindow.DrawingDTO.AbcdFigure, _mainWindow.DrawingDTO.AoeFigure, amountOfPointsMC, multiplierMC);

            for (int i = 0; i < 5; i++)
            {
                var sByMonteCarlo = squareCalculationByMonteCarlo.Execute(out long measuredTimeSByMonteCarlo, out IEnumerable<Point> randPoints);

                // CustomDrawer.DrawPoints(MainCanvas, GlobalParams.ScaleNumber, randPoints); // Рисование

                _mainWindow.CalculationDTO.InfoByMonteCarlo.Add(new InfoByMonteCarlo
                {
                    Square = sByMonteCarlo,
                    MeasuredTime = measuredTimeSByMonteCarlo,
                    AmountOfPoints = amountOfPointsMC
                });
                amountOfPointsMC *= multiplierMC;
            }
        }
    }
}