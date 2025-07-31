using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for BookModel.xaml
    /// </summary>
    public partial class BookModel : Window
    {
        public ProductInfo ProductInfo {  get; set; }
        public BookModel()
        {
            InitializeComponent();
        }

        public void SetOptions(string title)
        {
            this.mainTb.Text = title;
            this.SaveBtn.Content = title;
        }

        public void SetProduct(ProductInfo product)
        {
            this.bookNameTb.Text = product.Book.Name;
            this.authNameTb.Text = product.Book.Author.Name;
            this.authMidNameTb.Text = product.Book.Author.MiddleName;
            this.authLastNameTb.Text = product.Book.Author.LastName;
            this.prodTb.Text = product.Book.Producer.Name;
            this.pagesTb.Text = product.Book.PageAmount.ToString();
            this.genreTb.Text = product.Book.Genre.Name;
            this.yearTb.Text = product.Book.Year.ToString();
            this.contTb.Text = product.Book.IsContinuation;

            this.amountTb.Text = product.Amount.ToString();
            this.costTb.Text = product.Cost.ToString();
            this.priceTb.Text = product.Price.ToString();

            if (product.Discount != null)
            {
                this.disNameTb.Text = product.Discount.Name;
                this.disIntTb.Text = product.Discount.Interest.ToString();
                this.disStartDp.SelectedDate = product.Discount.StartDate;
                this.disEndDp.SelectedDate = product.Discount.EndDate;
            }

            if (product.DelayedForCustomer != null)
            {
                this.cusNameTb.Text = product.DelayedForCustomer.Customer.FullName.Name;
                this.cusMidNameTb.Text = product.DelayedForCustomer.Customer.FullName.MiddleName;
                this.cusLastNameTb.Text = product.DelayedForCustomer.Customer.FullName.LastName;
                this.cusEmailTb.Text = product.DelayedForCustomer.Customer.Email;
                this.delayAmountTb.Text = product.DelayedForCustomer.Amount.ToString();
            }
        }
        private void editBt_Click(object sender, RoutedEventArgs e)
        {
            if (PeopleIsValid())
            {
                ProductInfo = new ProductInfo()
                {
                    Book = new BookInfo()
                    {
                        Name = this.bookNameTb.Text,
                        Author = new FullNameInfo()
                        {
                            Name = this.authNameTb.Text,
                            MiddleName = this.authMidNameTb.Text,
                            LastName = this.authLastNameTb.Text
                        },
                        Producer = new ProducerInfo()
                        {
                            Name = this.prodTb.Text
                        },
                        PageAmount = Convert.ToInt16(this.pagesTb.Text),
                        Genre = new GenreInfo()
                        {
                            Name = this.genreTb.Text
                        },
                        Year = Convert.ToInt16(this.yearTb.Text),
                        IsContinuation = (this.contTb.Text != "") ? this.contTb.Text : null
                    },
                    Amount = Convert.ToInt32(this.amountTb.Text),
                    Cost = Convert.ToInt32(this.costTb.Text),
                    Price = Convert.ToInt32(this.priceTb.Text)
                };
                if (this.disNameTb.Text != "" && this.disStartDp.Text != "" && this.disEndDp.Text != "" && this.disIntTb.Text != "")
                {
                    ProductInfo.Discount = new DiscountInfo()
                    {
                        Name = this.disNameTb.Text,
                        Interest = Convert.ToByte(this.disIntTb.Text),
                        StartDate = Convert.ToDateTime(this.disStartDp.SelectedDate),
                        EndDate = Convert.ToDateTime(this.disEndDp.SelectedDate),
                    };
                }
                if (this.cusNameTb.Text != "" && this.cusMidNameTb.Text != "" && this.cusLastNameTb.Text != "" && this.cusEmailTb.Text != "" && this.delayAmountTb.Text != "")
                {
                    ProductInfo.DelayedForCustomer = new DelayInfo()
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
                }
                this.Close();
            }
        }

        private void ClearBorders()
        {
            this.bookNameTb.BorderBrush = Brushes.LightCyan;
            this.authNameTb.BorderBrush = Brushes.LightCyan;
            this.authMidNameTb.BorderBrush = Brushes.LightCyan;
            this.authLastNameTb.BorderBrush = Brushes.LightCyan;
            this.prodTb.BorderBrush = Brushes.LightCyan;
            this.pagesTb.BorderBrush = Brushes.LightCyan;
            this.genreTb.BorderBrush = Brushes.LightCyan;
            this.yearTb.BorderBrush = Brushes.LightCyan;
            this.contTb.BorderBrush = Brushes.LightCyan;

            this.amountTb.BorderBrush = Brushes.LightCyan;
            this.costTb.BorderBrush = Brushes.LightCyan;
            this.priceTb.BorderBrush = Brushes.LightCyan;

            this.disNameTb.BorderBrush = Brushes.LightCyan;
            this.disIntTb.BorderBrush = Brushes.LightCyan;
            this.disStartDp.BorderBrush = Brushes.LightCyan;
            this.disEndDp.BorderBrush = Brushes.LightCyan;

            this.cusNameTb.BorderBrush = Brushes.LightCyan;
            this.cusMidNameTb.BorderBrush = Brushes.LightCyan;
            this.cusLastNameTb.BorderBrush = Brushes.LightCyan;
            this.cusEmailTb.BorderBrush = Brushes.LightCyan;
            this.delayAmountTb.BorderBrush = Brushes.LightCyan;
        }

        private bool PeopleIsValid()
        {
            ClearBorders();
            if (String.IsNullOrEmpty(this.bookNameTb.Text) || String.IsNullOrWhiteSpace(this.bookNameTb.Text))
            {
                this.bookNameTb.BorderBrush = Brushes.Red;
                return false;
            }

            if (String.IsNullOrEmpty(this.authNameTb.Text) || String.IsNullOrWhiteSpace(this.authNameTb.Text))
            {
                this.authNameTb.BorderBrush = Brushes.Red;
                return false;
            }

            if (String.IsNullOrEmpty(this.authMidNameTb.Text) || String.IsNullOrWhiteSpace(this.authMidNameTb.Text))
            {
                this.authMidNameTb.BorderBrush = Brushes.Red;
                return false;
            }

            if (String.IsNullOrEmpty(this.authLastNameTb.Text) || String.IsNullOrWhiteSpace(this.authLastNameTb.Text))
            {
                this.authLastNameTb.BorderBrush = Brushes.Red;
                return false;
            }

            if (String.IsNullOrEmpty(this.prodTb.Text) || String.IsNullOrWhiteSpace(this.prodTb.Text))
            {
                this.prodTb.BorderBrush = Brushes.Red;
                return false;
            }

            //-------------------------------------------------------------------------------------------------------
            if (String.IsNullOrEmpty(this.pagesTb.Text) || String.IsNullOrWhiteSpace(this.pagesTb.Text))
            {
                this.pagesTb.BorderBrush = Brushes.Red;
                return false;
            }
            short pages;
            if (!Int16.TryParse(this.pagesTb.Text, out pages) || pages <= 0)
            {
                this.pagesTb.BorderBrush = Brushes.Red;
                return false;
            }
            //-------------------------------------------------------------------------------------------------------

            if (String.IsNullOrEmpty(this.genreTb.Text) || String.IsNullOrWhiteSpace(this.genreTb.Text))
            {
                this.genreTb.BorderBrush = Brushes.Red;
                return false;
            }

            //-------------------------------------------------------------------------------------------------------
            if (String.IsNullOrEmpty(this.yearTb.Text) || String.IsNullOrWhiteSpace(this.yearTb.Text))
            {
                this.yearTb.BorderBrush = Brushes.Red;
                return false;
            }
            short year;
            if (!Int16.TryParse(this.yearTb.Text, out year) || year <= 0)
            {
                this.yearTb.BorderBrush = Brushes.Red;
                return false;
            }
            //-------------------------------------------------------------------------------------------------------

            //-------------------------------------------------------------------------------------------------------
            if (String.IsNullOrEmpty(this.amountTb.Text) || String.IsNullOrWhiteSpace(this.amountTb.Text))
            {
                this.yearTb.BorderBrush = Brushes.Red;
                return false;
            }
            int amount;
            if (!Int32.TryParse(this.amountTb.Text, out amount) || amount <= 0)
            {
                this.amountTb.BorderBrush = Brushes.Red;
                return false;
            }
            //-------------------------------------------------------------------------------------------------------


            //-------------------------------------------------------------------------------------------------------
            if (String.IsNullOrEmpty(this.costTb.Text) || String.IsNullOrWhiteSpace(this.costTb.Text))
            {
                this.yearTb.BorderBrush = Brushes.Red;
                return false;
            }
            int cost;
            if (!Int32.TryParse(this.costTb.Text, out cost) || cost <= 0)
            {
                this.yearTb.BorderBrush = Brushes.Red;
                return false;
            }
            //-------------------------------------------------------------------------------------------------------

            //-------------------------------------------------------------------------------------------------------
            if (String.IsNullOrEmpty(this.priceTb.Text) || String.IsNullOrWhiteSpace(this.priceTb.Text))
            {
                this.yearTb.BorderBrush = Brushes.Red;
                return false;
            }
            int price;
            if (!Int32.TryParse(this.priceTb.Text, out price) || price <= 0)
            {
                this.yearTb.BorderBrush = Brushes.Red;
                return false;
            }
            //-------------------------------------------------------------------------------------------------------
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
            }
            //-------------------------------------------------------------------------------------------------------
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
            }

            return true;
        }
    }
}