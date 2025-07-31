using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
using BLL.Models;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for DiscountWind.xaml
    /// </summary>
    public partial class DiscountWind : Window
    {
        public DiscountInfo DiscountInfo { get; set; }
        public DiscountWind()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DiscountIsValid())
            {
                DiscountInfo = new DiscountInfo()
                {
                    Name = this.disNameTb.Text,
                    Interest = Convert.ToByte(this.disIntTb.Text),
                    StartDate = Convert.ToDateTime(this.disStartDp.SelectedDate),
                    EndDate = Convert.ToDateTime(this.disEndDp.SelectedDate),
                };
                this.Close();
            }
        }

        private void ClearBorders()
        {
            this.disNameTb.BorderBrush = Brushes.LightCyan;
            this.disIntTb.BorderBrush = Brushes.LightCyan;
            this.disStartDp.BorderBrush = Brushes.LightCyan;
            this.disEndDp.BorderBrush = Brushes.LightCyan;
        }

        private bool DiscountIsValid()
        {
            ClearBorders();
            if (this.disNameTb.Text != "" && this.disStartDp.Text != "" && this.disEndDp.Text != "" && this.disIntTb.Text != "")
            {
                if (String.IsNullOrEmpty(this.disNameTb.Text) || String.IsNullOrWhiteSpace(this.disNameTb.Text))
                {
                    this.disNameTb.BorderBrush = Brushes.Red;
                    return false;
                }
                if (String.IsNullOrEmpty(this.disStartDp.Text) || String.IsNullOrWhiteSpace(this.disStartDp.Text))
                {
                    this.disStartDp.BorderBrush = Brushes.Red;
                    return false;
                }
                if (String.IsNullOrEmpty(this.disEndDp.Text) || String.IsNullOrWhiteSpace(this.disEndDp.Text))
                {
                    this.disEndDp.BorderBrush = Brushes.Red;
                    return false;
                }
                //-------------------------------------------------------------------------------------------------------
                if (String.IsNullOrEmpty(this.disIntTb.Text) || String.IsNullOrWhiteSpace(this.disIntTb.Text))
                {
                    this.disIntTb.BorderBrush = Brushes.Red;
                    return false;
                }
                byte interest;
                if (!Byte.TryParse(this.disIntTb.Text, out interest) || interest <= 0)
                {
                    this.disIntTb.BorderBrush = Brushes.Red;
                    return false;
                }
                //-------------------------------------------------------------------------------------------------------
                return true;
            }
            return false;
        }
    }
}
