using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Decimal LeftNum { get; set; }
        public string op { get; set; }
        public Decimal RightNum { get; set; }
        public string LastButtonPress { get; set; }
        public bool EqualsLastPressed { get => LastButtonPress == "="; }
        public bool OpLastPressed { get => LastButtonPress == "+" || LastButtonPress == "-" || LastButtonPress == "*" || LastButtonPress == "/";  }
        public string Expression
        {
            get
            {
                if (EqualsLastPressed)
                {
                    return $"{LeftNum} {op} {RightNum} =";
                }
                else
                {
                    return $"{LeftNum} {op}";
                }
            }
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNumber.Text == string.Empty)
            {
                txtNumber.Text = "0";
                txtNumber.CaretIndex = 1;
            }
            else if (txtNumber.Text.StartsWith('0') && !txtNumber.Text.StartsWith("0.") && txtNumber.Text.Length > 1)
            {
                txtNumber.Text = txtNumber.Text.Substring(1);
                txtNumber.CaretIndex = 1;
            }
        }

        private void btnNumber_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var num = btn.Content.ToString();
            if (EqualsLastPressed)
            {
                LeftNum = 0;
                op = "";
                RightNum = 0;
                txtNumber.Text = num;
                LastButtonPress = num;
                lblExpression.Content = string.Empty;
            }
            else if (OpLastPressed)
            {
                txtNumber.Text = num;
                LastButtonPress = num;
                lblExpression.Content = Expression;
            }
            else
            {
                txtNumber.Text = $"{txtNumber.Text}{num}";
                LastButtonPress = num;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            txtNumber.Text = txtNumber.Text.Substring(0, txtNumber.Text.Length - 1);
        }

        private void btnDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (txtNumber.Text.EndsWith("/") || txtNumber.Text.EndsWith("*") || txtNumber.Text.EndsWith("-") || txtNumber.Text.EndsWith("+"))
            {   //ensure we have a leading zero in front of the decimal 
                txtNumber.Text = $"{txtNumber.Text}0";
            }
            txtNumber.Text = $"{txtNumber.Text}.";
            LastButtonPress = ".";
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (op == "")
                return;

            if (EqualsLastPressed)
                LeftNum = Convert.ToDecimal(txtNumber.Text);
            else
                RightNum = Convert.ToDecimal(txtNumber.Text);

            string result = "";
            if (op == "/")
                result = (LeftNum / RightNum).ToString();
            else if (op == "*")
                result = (LeftNum * RightNum).ToString();
            else if (op == "-")
                result = (LeftNum - RightNum).ToString();
            else if (op == "+")
                result = (LeftNum + RightNum).ToString();

            txtNumber.Text = result;

            LastButtonPress = "=";
            lblExpression.Content = Expression;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (OpLastPressed) 
            {
                return;
            }
            else if (EqualsLastPressed)
            {
                LeftNum = Convert.ToDecimal(txtNumber.Text);
                op = "+";
                RightNum = 0;
                LastButtonPress = "+";
                lblExpression.Content = Expression;
                return;
            }

            op = "+";
            RightNum = Convert.ToDecimal(txtNumber.Text);
            LeftNum = LeftNum + RightNum;
            txtNumber.Text = LeftNum.ToString();
            RightNum = 0;
            LastButtonPress = "+";
            lblExpression.Content = Expression;

        }

    }
}
