using System.Windows;
using System.Windows.Controls;

namespace RomanConverter
{
    /// <summary>
    /// Interaction logic for FahrenheitWindow.xaml
    /// </summary>
    public partial class FahrenheitWindow : Window
    {
        private bool _isChanging = false;

        public FahrenheitWindow()
        {
            InitializeComponent();
        }

        private void FahrenheitBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isChanging)
            {
                _isChanging = true;
                double celsius = (double.Parse(FahrenheitBox.Text) - 32) * 5 / 9;
                CelsiusBox.Text = celsius.ToString();
                _isChanging = false;
            }
        }

        private void CelsiusBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isChanging)
            {
                _isChanging = true;
                double fahrenheit = (double.Parse(CelsiusBox.Text) * 9 / 5) + 32;
                FahrenheitBox.Text = fahrenheit.ToString();
                _isChanging = false;
            }
        }
    }
}