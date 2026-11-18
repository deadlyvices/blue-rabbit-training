using System.Windows;

namespace RomanConverter
{
    /// <summary>
    /// Interaction logic for ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : Window
    {
        public ControlPanel()
        {
            InitializeComponent();
        }

        private void RomanButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow RomanWindow = new MainWindow();
            RomanWindow.ShowDialog();
        }

        private void FahrenheitButton_Click(object sender, RoutedEventArgs e)
        {
            FahrenheitWindow fw = new FahrenheitWindow
            {
                Owner = this
            };
            fw.Show();
        }
    }
}