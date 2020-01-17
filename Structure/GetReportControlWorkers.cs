using MonteKarloWPFApp1.Consts;
using MonteKarloWPFApp1.UIHelpers;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace MonteKarloWPFApp1.Structure
{
    public class GetReportControlWorkers
    {
        private MainWindow _mainWindow;

        public GetReportControlWorkers(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void MainBackgroundWorker_GetReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var formBlocker = new FormBlocker(_mainWindow);
            formBlocker.Equals(false);
        }

        public void MainBackgroundWorker_GetReport_DoWork(object sender, DoWorkEventArgs e)
        {
            _mainWindow.Dispatcher.Invoke(() =>
            {
                var formBlocker = new FormBlocker(_mainWindow);
                formBlocker.Equals(true);

                if (_mainWindow.CalculationDTO == null)
                {
                    MessageBox.Show(Strings.CalcIsntExecuted_Msg_Str);
                    return;
                }

                var sb = new StringBuilder();
                sb.AppendLine($"Отчет за {DateTime.Now}");
                sb.AppendLine($"Площадь, вычисленная математически - {_mainWindow.CalculationDTO.InfoByFormuls.Square}");
                sb.AppendLine($"Время, потраченное на вычисление площади математически - {_mainWindow.CalculationDTO.InfoByFormuls.MeasuredTime} мс");
                sb.AppendLine($"Площадь, вычисленная методом Монте Карло:");
                foreach (var item in _mainWindow.CalculationDTO.InfoByMonteCarlo)
                {
                    var offsetS = (Math.Abs(_mainWindow.CalculationDTO.InfoByFormuls.Square - item.Square) / _mainWindow.CalculationDTO.InfoByFormuls.Square) * 100;
                    sb.AppendLine($"Кол-во сгенерированых точек - {item.AmountOfPoints}, площадь - {item.Square}, время - {item.MeasuredTime} мс, погрешность - {offsetS} %");
                }

                MessageBox.Show(sb.ToString(), "Выполненные расчеты");
            });
        }
    }
}