using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RomanConverter
{
    /// <summary>
    /// action logic for MainWindow.xaml
    /// </summary>
    ///
    
    public struct MyType
    {
        public int MyInt;
        public string MyString;
    }

    public partial class MainWindow : Window
    {
        private bool _isChanging = false;

        public MainWindow()
        {
            InitializeComponent();


        }

        private void RomanBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isChanging)
            {
                return;
            }
            _isChanging = true;

            if (RomanToArabic(RomanBox.Text.ToUpper(), out long arabicValue))
            {
                ArabicBox.Text = arabicValue.ToString();
            }
            else
            {
                ArabicBox.Text = "Invalid Roman numeral";
            }
            _isChanging = false;
        }

        private bool RomanToArabic(string romanBoxText, out long total)
        {
            int maxval = 0;
            total = 0;
            foreach (char digit in romanBoxText.ToUpper().Reverse())
            {
                if (DigitVal(digit) > 0)
                {
                    int digitVal = DigitVal(digit);
                    if (digitVal < maxval)
                        total = total - digitVal;
                    else
                    {
                        total = total + digitVal;
                        maxval = digitVal;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;

            //local function to get the value of a Roman numeral digit
            //return zero if unrecognised character
            static int DigitVal(char digit)
            {
                //expectation is that the input string has been converted
                //to uppercase before calling this function
               switch (digit)
                {
                    case 'I': return 1;
                    case 'V': return 5;
                    case 'X': return 10;
                    case 'L': return 50;
                    case 'C': return 100;
                    case 'D': return 500;
                    case 'M': return 1000;
                    default: return 0;
                }
            }
        }

        private void ArabicBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isChanging)
            {
                return;
            }

            _isChanging = true;
            if (ArabicToRoman(ArabicBox.Text, out string romanValue))
            {
                RomanBox.Text = romanValue;
            }
            else
            {
                RomanBox.Text = "Invalid Arabic numeral";
            }
            _isChanging = false;
        }

        private bool ArabicToRoman(string arabicBoxText, out string s)
        {
            s = "";
            if (int.TryParse(arabicBoxText, out int arabicValue))
            {
                s = ConvertToRoman(arabicValue);
                return true;
            }
            return false;
        }

        private string ConvertToRoman(int arabicValue)
        {
            if (arabicValue < 1 || arabicValue > 3999)
                return "Invalid Arabic numeral";

            string roman = "";
            int[] values = [1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1];
            string[] symbols = ["M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"];

            for (int i = 0; i < values.Length; i++)
            {
                while (arabicValue >= values[i])
                {
                    roman += symbols[i];
                    arabicValue -= values[i];
                }
            }

            return roman;
        }

        private string ConvertToWords(long arabicValue)
        {
            StringBuilder sb = new StringBuilder();
            long[] powersOfThousands = new long[5];

            string[] descriptions = new string[5];

            int index = 4;

            for (long multiplier = 1000_000_000_000; multiplier >= 1; multiplier /= 1000)
            {
                powersOfThousands[index] = arabicValue / multiplier;
                arabicValue = arabicValue % multiplier;
                index--;
            }

            string words = ToHTU(powersOfThousands);

            return words.Trim();

            string ToHTU(long[] values)
            {
                string[] tens = ["", "Ten", "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety "
                ];
                string[] ones = ["", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine"];
                string[] teens = ["", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"];
                string[] powersInWords = ["", "Thousand", "Million", "Billion", "Trillion"];
                for (long power = 4; power >= 0; power--)
                {
                    string units = powersInWords[power];
                    long teensvalue = 0;
                    long tensvalue = 0;

                    long value = values[power];

                    long hundredsvalue = value / 100;

                    long residual = value % 100;
                    
                    string ten = "";

                    if (residual >= 11 & residual < 20)
                    {
                        teensvalue = residual - 10;
                        ten = teens[teensvalue];
                    }
                    else
                    {
                        tensvalue = residual / 10;
                        ten = tens[tensvalue] + ones[residual % 10];
                    }


                    string conj = "";

                    if (hundredsvalue > 0)
                    {
                        if (ten != "")
                        {
                            conj = " Hundred and ";
                        }
                        else
                        {
                            conj = " Hundred ";
                        }
                    }

                    string chunk = ones[hundredsvalue] + conj
                                                       + ten;
                    chunk = chunk.Trim();
                    if (!string.IsNullOrEmpty(chunk))
                    {
                        string slug = chunk + " " + units;
                        if (sb.ToString() == "")
                        {
                            sb.Append(slug);
                        }
                        else
                        {
                            sb.Append(", " + slug);
                        }
                    }
                }
                return sb.ToString();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void RomanBox_GotFocus(object sender, RoutedEventArgs e)
        {
            RomanBox.Select(0, RomanBox.Text.Length);
        }

        private void ArabicBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ArabicBox.Select(0, ArabicBox.Text.Length);
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ConvertToWords(3_487_103_652_143));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}