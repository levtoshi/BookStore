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
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for BookStockWind.xaml
    /// </summary>
    public partial class BookStockWind : Window
    {
        public int? Amount { get; set; }
        public BookStockWind()
        {
            InitializeComponent();
        }

        private void ClearBorders()
        {
            this.amountTb.BorderBrush = Brushes.LightCyan;
        }

        public void SetOptions(string mainTitle, string btnTitle)
        {
            this.mainTb.Text = mainTitle;
            this.actionBtn.Content = btnTitle;
        }

        private void actionBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ActionIsValid())
            {
                Amount = Convert.ToInt32(this.amountTb.Text);
                this.Close();
            }
        }

        private bool ActionIsValid()
        {
            ClearBorders();
            //-------------------------------------------------------------------------------------------------------
            if (String.IsNullOrEmpty(this.amountTb.Text) || String.IsNullOrWhiteSpace(this.amountTb.Text))
            {
                this.amountTb.BorderBrush = Brushes.Red;
                return false;
            }
            int amount;
            if (!Int32.TryParse(this.amountTb.Text, out amount) || amount <= 0)
            {
                this.amountTb.BorderBrush = Brushes.Red;
                return false;
            }
            //-------------------------------------------------------------------------------------------------------
            return true;
        }
    }
}
