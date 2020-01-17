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
                sb.AppendLine(string.Format(Strings.StartReport_Str, DateTime.Now));
                sb.AppendLine(string.Format(Strings.Report_S_ByFormuls_Str, _mainWindow.CalculationDTO.InfoByFormuls.Square));
                sb.AppendLine(string.Format(Strings.Report_S_ByFormuls_MeasuredTime_Str, _mainWindow.CalculationDTO.InfoByFormuls.MeasuredTime));
                sb.AppendLine(Strings.Report_S_ByMonteCarlo_Str);
                foreach (var item in _mainWindow.CalculationDTO.InfoByMonteCarlo)
                {
                    var offsetS = (Math.Abs(_mainWindow.CalculationDTO.InfoByFormuls.Square - item.Square) / _mainWindow.CalculationDTO.InfoByFormuls.Square) * 100;
                    sb.AppendLine(string.Format(Strings.Report_Info_Item_ForMonteCarlo_Str, item.AmountOfPoints, item.Square, item.MeasuredTime, offsetS));
                }

                MessageBox.Show(sb.ToString(), Strings.EndReport_Str);
            });
        }
    }
}