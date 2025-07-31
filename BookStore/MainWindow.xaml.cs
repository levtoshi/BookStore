using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Interfaces;
using BLL.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
     
     
    // IMPORTANT!!!!!!!!!!!
    // insert Users values ('Satoshi', 'Nakomoto', 1);

    public partial class MainWindow : Window
    {
        private IService<ProductInfo> _bookStoreService;
        public ObservableCollection<ProductInfo> Products;

        private BookModel _bookModel;
        private SellWind _sellWind;
        private BookStockWind _stockWind;
        private DiscountWind _discountWind;
        private DelayWind _delayWind;
        private LoginWind _loginWind;

        public MainWindow(IService<ProductInfo> service)
        {
            InitializeComponent();

            this._bookStoreService = service;
            VerifyUser();

            Products = new ObservableCollection<ProductInfo>();
            InitializedDataGrid();
            ((this.authorCb as ComboBox).Items[0] as ComboBoxItem).IsSelected = true;
            ((this.genreCb as ComboBox).Items[0] as ComboBoxItem).IsSelected = true;
        }

        private void VerifyUser()
        {
            int counter = 0;
            do
            {
                _loginWind = new LoginWind();
                if(!_bookStoreService.VerifyUser(_loginWind.GetLoginData()) && counter > 0)
                {
                    _loginWind.ErrorData();
                }
                _loginWind.ShowDialog();
                counter++;
            } while (!_bookStoreService.VerifyUser(_loginWind.GetLoginData()) && !_loginWind.MustClose);
            if(!_loginWind.MustClose)
            {
                this.Show();
                this.usernameTb.Text = _bookStoreService.GetUsername();
            }
            else
            {
                this.Close();
            }
        }

        private void InitializedBinding()
        {
            this.mainGrid.Columns.Clear();
            if (_bookStoreService.ShowType == 0 || _bookStoreService.ShowType == null)
            {
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("Book.Name") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Author Name", Binding = new Binding("Book.Author.Name") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "MiddleName", Binding = new Binding("Book.Author.MiddleName") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "LastName", Binding = new Binding("Book.Author.LastName") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Prod", Binding = new Binding("Book.Producer.Name") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Pages", Binding = new Binding("Book.PageAmount") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Genre", Binding = new Binding("Book.Genre.Name") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Year", Binding = new Binding("Book.Year") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Continuation", Binding = new Binding("Book.IsContinuation") });

                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Amount", Binding = new Binding("Amount") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Cost", Binding = new Binding("Cost") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Price", Binding = new Binding("Price") });

                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Discount", Binding = new Binding("Discount.Name") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Dis Interest", Binding = new Binding("Discount.Interest") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Dis Start", Binding = new Binding("Discount.StartDate") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Dis End", Binding = new Binding("Discount.EndDate") });

                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Delay Customer Name", Binding = new Binding("DelayedForCustomer.Customer.FullName.Name") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "MiddleName", Binding = new Binding("DelayedForCustomer.Customer.FullName.MiddleName") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "LastName", Binding = new Binding("DelayedForCustomer.Customer.FullName.LastName") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Email", Binding = new Binding("DelayedForCustomer.Customer.Email") });
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Delay Amount", Binding = new Binding("DelayedForCustomer.Amount") });
            }
            else if (_bookStoreService.ShowType == 1)
            {
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Author full name", Binding = new Binding(".") });
                this.mainGrid.ItemsSource = _bookStoreService.GetAllAuthors().Distinct();
            }
            else if (_bookStoreService.ShowType == 2)
            {
                this.mainGrid.Columns.Add(new DataGridTextColumn { Header = "Genre", Binding = new Binding(".") });
                this.mainGrid.ItemsSource = _bookStoreService.GetAllGenres().Distinct();
            }
        }

        private void InitializedDataGrid()
        {
            InitializedBinding();
            if(_bookStoreService.ShowDuration == null && _bookStoreService.ShowType == null)
            {
                InitializedAuthors();
                InitializedGenres();
            }
            
            if (_bookStoreService.ShowType == 0 || _bookStoreService.ShowType == null)
                InitializedCollection();
        }

        private void InitializedCollection()
        {
            Products.Clear();
            foreach (var product in this._bookStoreService.GetAll())
            {
                Products.Add(product);
            }
            this.mainGrid.ItemsSource = Products;
        }


        private void InitializedAuthors()
        {
            ComboBox comboBox = (this.authorCb as ComboBox);
            List<string> authors = this._bookStoreService.GetAllAuthors().Distinct().ToList();

            comboBox.Items.Clear();
            comboBox.Items.Add(new ComboBoxItem()
            {
                Content = new TextBlock() { Text = "Default" },
            });
            foreach (string author in authors)
            {
                comboBox.Items.Add(new ComboBoxItem() {
                    Content = new TextBlock() { Text = author }
                });
            }
        }

        private void InitializedGenres()
        {
            ComboBox comboBox = (this.genreCb as ComboBox);
            List<string> genres = this._bookStoreService.GetAllGenres().Distinct().ToList();

            comboBox.Items.Clear();
            comboBox.Items.Add(new ComboBoxItem()
            {
                Content = new TextBlock() { Text = "Default" },
            });
            foreach (string genre in genres)
            {
                comboBox.Items.Add(new ComboBoxItem()
                {
                    Content = new TextBlock() { Text = genre }
                });
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        //Events

        private void AddBookBtn_Click(object sender, RoutedEventArgs e)
        {
            _bookModel = new BookModel();
            _bookModel.SetOptions("Add new book");
            _bookModel.ShowDialog();
            if (_bookModel.ProductInfo != null)
            {
                this._bookStoreService.AddNewBook(_bookModel.ProductInfo);
                InitializedDataGrid();
            }
        }

        private void UpdateBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainGrid.SelectedItem != null && this.mainGrid.SelectedItem is ProductInfo)
            {
                int id = (this.mainGrid.SelectedItem as ProductInfo).Id;
                _bookModel = new BookModel();
                _bookModel.SetOptions("Update Book");
                _bookModel.SetProduct(this.mainGrid.SelectedItem as ProductInfo);
                _bookModel.ShowDialog();
                if (_bookModel.ProductInfo != null)
                {
                    _bookModel.ProductInfo.Id = id;
                    this._bookStoreService.UpdateBook(_bookModel.ProductInfo);
                    InitializedDataGrid();
                }
            }
        }

        private void DeleteBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainGrid.SelectedItem != null && this.mainGrid.SelectedItem is ProductInfo)
            {
                ProductInfo tempPeople = this.mainGrid.SelectedItem as ProductInfo;
                try
                {
                    this._bookStoreService.DeleteBook(tempPeople);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!");
                }
                InitializedDataGrid();
            }
        }
        private void AddDiscountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainGrid.SelectedItem != null && this.mainGrid.SelectedItem is ProductInfo)
            {
                _discountWind = new DiscountWind();
                _discountWind.ShowDialog();
                if (_discountWind.DiscountInfo != null)
                {
                    this._bookStoreService.AddDiscount((this.mainGrid.SelectedItem as ProductInfo), _discountWind.DiscountInfo);
                    InitializedDataGrid();
                }
            }
        }


        private void AddBooksBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainGrid.SelectedItem != null && this.mainGrid.SelectedItem is ProductInfo)
            {
                _stockWind = new BookStockWind();
                _stockWind.SetOptions("Sell books", "Sell");
                _stockWind.ShowDialog();
                if (_stockWind.Amount != null)
                {
                    this._bookStoreService.AddBooks((this.mainGrid.SelectedItem as ProductInfo), Convert.ToInt32(_stockWind.Amount));
                    InitializedDataGrid();
                }
            }
        }

        private void WriteOffBooksBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainGrid.SelectedItem != null && this.mainGrid.SelectedItem is ProductInfo)
            {
                _stockWind = new BookStockWind();
                _stockWind.SetOptions("Write off books", "Write off");
                _stockWind.ShowDialog();
                if (_stockWind.Amount != null)
                {
                    this._bookStoreService.WriteOffBooks((this.mainGrid.SelectedItem as ProductInfo), Convert.ToInt32(_stockWind.Amount));
                    InitializedDataGrid();
                }
            }
        }

        private void SellBooksBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainGrid.SelectedItem != null && this.mainGrid.SelectedItem is ProductInfo)
            {
                _sellWind = new SellWind();
                _sellWind.ShowDialog();
                if (_sellWind.Amount != null && _sellWind.Time != null)
                {
                    this._bookStoreService.SellBooks((this.mainGrid.SelectedItem as ProductInfo), Convert.ToInt32(_sellWind.Amount), Convert.ToDateTime(_sellWind.Time));
                    InitializedDataGrid();
                }
            }
        }

        private void DelayBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainGrid.SelectedItem != null && this.mainGrid.SelectedItem is ProductInfo)
            {
                _delayWind = new DelayWind();
                _delayWind.ShowDialog();
                if (_delayWind.DelayInfo != null)
                {
                    this._bookStoreService.DelayBooks((this.mainGrid.SelectedItem as ProductInfo), _delayWind.DelayInfo);
                    InitializedDataGrid();
                }
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.nameSearchTb.Text != "" && _bookStoreService.ShowDuration == null && _bookStoreService.ShowType == null)
            {
                _bookStoreService.BookName = this.nameSearchTb.Text;
                InitializedDataGrid();
            }
        }

        private void SearchDefaultBtn_Click(object sender, RoutedEventArgs e)
        {
            _bookStoreService.BookName = null;
            InitializedDataGrid();
        }

        private void authorCb_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (sender as ComboBox);
            if (!comboBox.Items.IsEmpty)
            {
                _bookStoreService.BookAuthor = ((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text != "Default") ? (((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text : null;
                _bookStoreService.BookName = null;
                InitializedDataGrid();
            }
        }

        private void genreCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (sender as ComboBox);
            if (!comboBox.Items.IsEmpty)
            {
                _bookStoreService.BookGenre = ((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text != "Default") ? (((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text : null;
                _bookStoreService.BookName = null;
                InitializedDataGrid();
            }
        }

        private void orderCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (sender as ComboBox);
            if((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text == "Default")
            {
                _bookStoreService.BookOrder = null;
            }
            else if ((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text == "Newest")
            {
                _bookStoreService.BookOrder = false;
            }
            else
            {
                _bookStoreService.BookOrder = true;
            }
            InitializedDataGrid();
        }

        private void popularTypeCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (sender as ComboBox);
            if ((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text == "Default")
            {
                _bookStoreService.ShowType = null;
                InitializedDataGrid();
            }
            else if ((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text == "Book")
            {
                _bookStoreService.ShowType = 0;
            }
            else if ((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text == "Author")
            {
                _bookStoreService.ShowType = 1;
            }
            else
            {
                _bookStoreService.ShowType = 2;
            }
            if (_bookStoreService.ShowDuration != null)
            {
                InitializedDataGrid();
            }
        }

        private void popularDurationCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (sender as ComboBox);
            if ((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text == "Default")
            {
                _bookStoreService.ShowDuration = null;
                InitializedDataGrid();
            }
            else if ((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text == "Day")
            {
                _bookStoreService.ShowDuration = 0;
            }
            else if ((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text == "Week")
            {
                _bookStoreService.ShowDuration = 1;
            }
            else if ((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as TextBlock).Text == "Month")
            {
                _bookStoreService.ShowDuration = 2;
            }
            else
            {
                _bookStoreService.ShowDuration = 3;
            }

            if(_bookStoreService.ShowType != null)
            {
                InitializedDataGrid();
            }
        }
    }
}