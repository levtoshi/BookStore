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
    /// Interaction logic for SellWind.xaml
    /// </summary>
    public partial class SellWind : Window
    {
        public int? Amount;
        public DateTime? Time;
        public SellWind()
        {
            InitializeComponent();
        }

        private void nowBtn_Click(object sender, RoutedEventArgs e)
        {
            Time = DateTime.Now;
            this.timeDp.SelectedDate = DateTime.Now;
        }

        private void ClearBorders()
        {
            this.amountTb.BorderBrush = Brushes.LightCyan;
            this.timeDp.BorderBrush = Brushes.LightCyan;
        }

        private void sellBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SellingIsValid())
            {
                Amount = Convert.ToInt32(this.amountTb.Text);
                Time = Convert.ToDateTime(this.timeDp.Text);
                this.Close();
            }
        }

        private bool SellingIsValid()
        {
            ClearBorders();
            if (String.IsNullOrEmpty(this.timeDp.Text) || String.IsNullOrWhiteSpace(this.timeDp.Text))
            {
                this.timeDp.BorderBrush = Brushes.Red;
                return false;
            }
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