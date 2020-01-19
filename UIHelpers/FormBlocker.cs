using System.Windows;

namespace MonteKarloWPFApp1.UIHelpers
{
    public class FormBlocker
    {
        private MainWindow _mainWindow;

        /// <summary>
        /// Блокировщик всех controls on form
        /// </summary>
        public FormBlocker(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void Execute(bool isDisabled)
        {
            foreach (var item in _mainWindow.MainGrid.Children)
            {
                var uiElement = item as UIElement;
                if (uiElement != null)
                {
                    uiElement.IsEnabled = !isDisabled;
                }
            }
        }
    }
}