using MonteKarloWPFApp1.Consts;
using MonteKarloWPFApp1.Drawing;
using System.Windows;

namespace MonteKarloWPFApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearMainCanvas_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Clear();
        }

        private void DrawFigure_Click(object sender, RoutedEventArgs e)
        {
            int bc, ab;
            if (!(int.TryParse(BC.Text, out bc) && int.TryParse(AB.Text, out ab)))
            {
                MessageBox.Show(Strings.ArgExFormTextBoxesBC_AB_Str);
                return;
            }

            int spaceLeft, spaceBottom;
            if (!(int.TryParse(SpaceLeft.Text, out spaceLeft) && int.TryParse(SpaceBottom.Text, out spaceBottom)))
            {
                MessageBox.Show(Strings.ArgExFormSpaces_Str);
                return;
            }

            if (!(spaceLeft > 0 && spaceBottom > 0 && spaceLeft <= 20 && spaceBottom <= 20))
            {
                MessageBox.Show(Strings.ArgExFormSpaces_Restricts_Str);
                return;
            }

            var abcdFigure = new MyFigure(new MyPoint[]
            {
                new MyPoint(new System.Drawing.Point(spaceLeft, spaceBottom), "A"),
                new MyPoint(new System.Drawing.Point(spaceLeft, spaceBottom + ab), "B"),
                new MyPoint(new System.Drawing.Point(spaceLeft + bc, spaceBottom + ab), "C"),
                new MyPoint(new System.Drawing.Point(spaceLeft + bc, spaceBottom), "D")
            }, System.Windows.Media.Color.FromRgb(45, 67, 234));
            var abcdFigureDrawer = new CustomDrawer(this, abcdFigure, 3);
            abcdFigureDrawer.Draw();

            var aoeFigure = new MyFigure(new MyPoint[]
            {
                new MyPoint(new System.Drawing.Point(spaceLeft, spaceBottom), "A"),
                new MyPoint(new System.Drawing.Point(spaceLeft, spaceBottom + ab - bc), "O"),
                new MyPoint(new System.Drawing.Point(spaceLeft + bc, spaceBottom + ab - bc), "E"),
            }, System.Windows.Media.Color.FromRgb(0, 67, 0));
            var oaeDrawerFigure = new CustomDrawer(this, aoeFigure, 3);
            oaeDrawerFigure.Draw();

            /*var customDrawerPartOfCircle = new CustomDrawer(this, MyFigure.OBE, System.Windows.Media.Color.FromRgb(198, 89, 0), 3);
            customDrawerPartOfCircle.Draw();*/
        }
    }
}