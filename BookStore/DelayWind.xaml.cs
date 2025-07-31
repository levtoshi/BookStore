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
using BLL.Models;
using DLL.Models;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for DelayWind.xaml
    /// </summary>
    public partial class DelayWind : Window
    {
        public DelayInfo DelayInfo { get; set; }
        public DelayWind()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DelayIsValid())
            {
                DelayInfo = new DelayInfo()
                {
                    Customer = new CustomerInfo()
                    {
                        FullName = new FullNameInfo()
                        {
                            Name = this.cusNameTb.Text,
                            MiddleName = this.cusMidNameTb.Text,
                            LastName = this.cusLastNameTb.Text
                        },
                        Email = this.cusEmailTb.Text
                    },
                    Amount = Convert.ToInt32(this.delayAmountTb.Text)
                };
                this.Close();
            }
        }

        private void ClearBorders()
        {
            this.cusNameTb.BorderBrush = Brushes.LightCyan;
            this.cusMidNameTb.BorderBrush = Brushes.LightCyan;
            this.cusLastNameTb.BorderBrush = Brushes.LightCyan;
            this.cusEmailTb.BorderBrush = Brushes.LightCyan;
            this.delayAmountTb.BorderBrush = Brushes.LightCyan;
        }

        private bool DelayIsValid()
        {
            ClearBorders();
            if (this.cusNameTb.Text != "" && this.cusMidNameTb.Text != "" && this.cusLastNameTb.Text != "" && this.cusEmailTb.Text != "" && this.delayAmountTb.Text != "")
            {
                if (String.IsNullOrEmpty(this.cusNameTb.Text) || String.IsNullOrWhiteSpace(this.cusNameTb.Text))
                {
                    this.cusNameTb.BorderBrush = Brushes.Red;
                    return false;
                }

                if (String.IsNullOrEmpty(this.cusMidNameTb.Text) || String.IsNullOrWhiteSpace(this.cusMidNameTb.Text))
                {
                    this.cusMidNameTb.BorderBrush = Brushes.Red;
                    return false;
                }

                if (String.IsNullOrEmpty(this.cusLastNameTb.Text) || String.IsNullOrWhiteSpace(this.cusLastNameTb.Text))
                {
                    this.cusLastNameTb.BorderBrush = Brushes.Red;
                    return false;
                }

                if (String.IsNullOrEmpty(this.cusEmailTb.Text) || String.IsNullOrWhiteSpace(this.cusEmailTb.Text))
                {
                    this.cusEmailTb.BorderBrush = Brushes.Red;
                    return false;
                }
                //-------------------------------------------------------------------------------------------------------
                if (String.IsNullOrEmpty(this.delayAmountTb.Text) || String.IsNullOrWhiteSpace(this.delayAmountTb.Text))
                {
                    this.delayAmountTb.BorderBrush = Brushes.Red;
                    return false;
                }
                int delAmount;
                if (!Int32.TryParse(this.delayAmountTb.Text, out delAmount) || delAmount <= 0)
                {
                    this.delayAmountTb.BorderBrush = Brushes.Red;
                    return false;
                }
                //-------------------------------------------------------------------------------------------------------
                return true;
            }
            return false;
        }
    }
}