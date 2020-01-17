using MonteKarloWPFApp1.Consts;
using MonteKarloWPFApp1.Drawing;
using MonteKarloWPFApp1.DTO;
using MonteKarloWPFApp1.UIHelpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace MonteKarloWPFApp1.Structure
{
    public class DrawFigureControlWorkers
    {
        private MainWindow _mainWindow;

        public DrawFigureControlWorkers(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void MainBackgroundWorker_DrawFigure_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _mainWindow.CalculationDTO = null;
            _mainWindow.ReportDTO = null;
        }

        public void MainBackgroundWorker_DrawFigure_DoWork(object sender, DoWorkEventArgs e)
        {
            var isQuit = false;

            var formBlocker = new FormBlocker(_mainWindow);
            formBlocker.Equals(true);

            int bc = 0, ab = 0;
            _mainWindow.Dispatcher.Invoke(() =>
            {
                if (!(int.TryParse(_mainWindow.BC.Text, out bc) && int.TryParse(_mainWindow.AB.Text, out ab)))
                {
                    MessageBox.Show(Strings.FormTextBoxesBC_AB_Msg_Str);
                    isQuit = true;
                }
            });

            int spaceLeft = 0, spaceBottom = 0;
            _mainWindow.Dispatcher.Invoke(() =>
            {
                if (!(int.TryParse(_mainWindow.SpaceLeft.Text, out spaceLeft) && int.TryParse(_mainWindow.SpaceBottom.Text, out spaceBottom)))
                {
                    MessageBox.Show(Strings.FormSpacesMsg_Str);
                    isQuit = true;
                }
            });

            if (isQuit)
            {
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

            _mainWindow.Dispatcher.Invoke(() => _mainWindow.MainCanvas.Children.Clear());

            _mainWindow.DrawingDTO = new DrawingDTO();

            var allPoints = new Dictionary<string, MyPoint>
                {
                    { "A", new MyPoint(new Point(spaceLeft, spaceBottom), "A") },
                    { "B", new MyPoint(new Point(spaceLeft, spaceBottom + ab), "B") },
                    { "C", new MyPoint(new Point(spaceLeft + bc, spaceBottom + ab), "C") },
                    { "D", new MyPoint(new Point(spaceLeft + bc, spaceBottom), "D") },
                    { "O", new MyPoint(new Point(spaceLeft, spaceBottom + ab - bc), "O") },
                    { "E", new MyPoint(new Point(spaceLeft + bc, spaceBottom + ab - bc), "E") }
                };

            var colorFigure = GlobalParams.ColorFigure;
            var scaleNumber = GlobalParams.ScaleNumber;

            _mainWindow.DrawingDTO.AbcdFigure = new MyFigure(new MyPoint[]
            {
                allPoints["A"],
                allPoints["B"],
                allPoints["C"],
                allPoints["D"]
            }, colorFigure);
            var abcdFigureDrawer = new CustomDrawer(_mainWindow, _mainWindow.DrawingDTO.AbcdFigure, scaleNumber);
            abcdFigureDrawer.DrawLines();

            _mainWindow.DrawingDTO.AoeFigure = new MyFigure(new MyPoint[]
            {
                allPoints["A"],
                allPoints["O"],
                allPoints["E"]
            }, colorFigure);
            var oaeDrawerFigure = new CustomDrawer(_mainWindow, _mainWindow.DrawingDTO.AoeFigure, scaleNumber);
            oaeDrawerFigure.DrawLines();

            _mainWindow.DrawingDTO.BeArcFigure = new MyFigure(new MyPoint[]
            {
                allPoints["B"],
                allPoints["E"],
            }, colorFigure);
            var beArcDrawerFigure = new CustomDrawer(_mainWindow, _mainWindow.DrawingDTO.BeArcFigure, scaleNumber);
            beArcDrawerFigure.DrawArc();
        }
    }
}