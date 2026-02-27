using BLL.Services.BookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Commands.DashboardCommands.BookModelCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.Stores.ControlContextStores;
using BookStoreUI.ViewModels.BaseViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.OtherViewModels
{
    public class ChangeBookModelViewModel : ViewModelsBase, INotifyDataErrorInfo
    {
        private string _bookName;
        public string BookName
        {
            get => _bookName;
            set
            {
                _bookName = value;
                OnPropertyChanged(nameof(BookName));

                ClearErrors(nameof(BookName));

                if (!HasBookName)
                {
                    AddError("Book name cannot be empty.", nameof(BookName));
                }

                OnPropertyChanged(nameof(CanChangeBookModel));
            }
        }

        private string _authorName;
        public string AuthorName
        {
            get => _authorName;
            set
            {
                _authorName = value;
                OnPropertyChanged(nameof(AuthorName));

                ClearErrors(nameof(AuthorName));

                if (!HasAuthorName)
                {
                    AddError("Author name cannot be empty.", nameof(AuthorName));
                }

                OnPropertyChanged(nameof(CanChangeBookModel));
            }
        }

        private string _authorMiddleName;
        public string AuthorMiddleName
        {
            get => _authorMiddleName;
            set
            {
                _authorMiddleName = value;
                OnPropertyChanged(nameof(AuthorMiddleName));

                ClearErrors(nameof(AuthorMiddleName));

                if (!HasAuthorMiddleName)
                {
                    AddError("Author middle name cannot be empty.", nameof(AuthorMiddleName));
                }

                OnPropertyChanged(nameof(CanChangeBookModel));
            }
        }

        private string _authorLastName;
        public string AuthorLastName
        {
            get => _authorLastName;
            set
            {
                _authorLastName = value;
                OnPropertyChanged(nameof(AuthorLastName));

                ClearErrors(nameof(AuthorLastName));

                if (!HasAuthorLastName)
                {
                    AddError("Author last name cannot be empty.", nameof(AuthorLastName));
                }

                OnPropertyChanged(nameof(CanChangeBookModel));
            }
        }

        private string _producerName;
        public string ProducerName
        {
            get => _producerName;
            set
            {
                _producerName = value;
                OnPropertyChanged(nameof(ProducerName));

                ClearErrors(nameof(ProducerName));

                if (!HasProducerName)
                {
                    AddError("Producer name cannot be empty.", nameof(ProducerName));
                }

                OnPropertyChanged(nameof(CanChangeBookModel));
            }
        }

        private short _amountOfPages;
        public short AmountOfPages
        {
            get => _amountOfPages;
            set
            {
                _amountOfPages = value;
                OnPropertyChanged(nameof(AmountOfPages));

                ClearErrors(nameof(AmountOfPages));

                if (!HasAmountOfPagesGreaterThanZero)
                {
                    AddError("Amount of pages must be greater than zero.", nameof(AmountOfPages));
                }

                OnPropertyChanged(nameof(CanChangeBookModel));
            }
        }

        private string _genre;
        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));

                ClearErrors(nameof(Genre));

                if (!HasGenre)
                {
                    AddError("Genre cannot be empty.", nameof(Genre));
                }

                OnPropertyChanged(nameof(CanChangeBookModel));
            }
        }

        private short _year;
        public short Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));

                ClearErrors(nameof(Year));

                if (!HasYearGreaterThanZero)
                {
                    AddError("Year must be greater than zero.", nameof(Year));
                }

                OnPropertyChanged(nameof(CanChangeBookModel));
            }
        }

        private string? _isContinuation;
        public string? IsContinuation
        {
            get => _isContinuation;
            set
            {
                _isContinuation = value;
                OnPropertyChanged(nameof(IsContinuation));
            }
        }

        private int _amountInStock;
        public int AmountInStock
        {
            get => _amountInStock;
            set
            {
                _amountInStock = value;
                OnPropertyChanged(nameof(AmountInStock));

                ClearErrors(nameof(AmountInStock));

                if (!HasAmountInStockGreaterThanZero)
                {
                    AddError("Amount in stock must be greater than zero.", nameof(AmountInStock));
                }

                OnPropertyChanged(nameof(CanChangeBookModel));
            }
        }

        private int _cost;
        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));

                ClearErrors(nameof(Cost));

                if (!HasCostGreaterThanZero)
                {
                    AddError("Cost must be greater than zero.", nameof(Cost));
                }

                OnPropertyChanged(nameof(CanChangeBookModel));
            }
        }

        private int _price;
        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));

                ClearErrors(nameof(Price));

                if (!HasPriceGreaterThanZero)
                {
                    AddError("Price must be greater than zero.", nameof(Price));
                }

                OnPropertyChanged(nameof(CanChangeBookModel));
            }
        }

        public bool CanChangeBookModel =>
            HasBookName &&
            HasAuthorName &&
            HasAuthorMiddleName &&
            HasAuthorLastName &&
            HasProducerName &&
            HasAmountOfPagesGreaterThanZero &&
            HasGenre &&
            HasYearGreaterThanZero &&
            HasAmountInStockGreaterThanZero &&
            HasCostGreaterThanZero &&
            HasPriceGreaterThanZero &&
            !HasErrors;

        private bool HasBookName => !String.IsNullOrWhiteSpace(BookName);
        private bool HasAuthorName => !String.IsNullOrWhiteSpace(AuthorName);
        private bool HasAuthorMiddleName => !String.IsNullOrWhiteSpace(AuthorMiddleName);
        private bool HasAuthorLastName => !String.IsNullOrWhiteSpace(AuthorLastName);
        private bool HasProducerName => !String.IsNullOrWhiteSpace(ProducerName);
        private bool HasAmountOfPagesGreaterThanZero => AmountOfPages > 0;
        private bool HasGenre => !String.IsNullOrWhiteSpace(Genre);
        private bool HasYearGreaterThanZero => Year > 0;
        private bool HasAmountInStockGreaterThanZero => AmountInStock > 0;
        private bool HasCostGreaterThanZero => Cost > 0;
        private bool HasPriceGreaterThanZero => Price > 0;

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool IsUpdateMode { get; }

        public ICommand GoToPreviousViewCommand { get; }
        public ICommand SubmitCommand { get; }

        public ChangeBookModelViewModel(IMainNavigationService<DashboardViewModel> navigationService,
            IBookService bookModelService,
            ChangeBookModelControlContextStore changeBookModelControlContextStore,
            SelectedItemStore selectedItemStore,
            ProductsStore productsStore)
        {
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
            IsUpdateMode = changeBookModelControlContextStore.ChangeBookModelControlContextObject.IsUpdateMode;

            GoToPreviousViewCommand = new RelayCommand((object? s) => navigationService.Navigate());

            SubmitCommand = IsUpdateMode ?
                new UpdateBookModelCommand(this, navigationService, bookModelService, selectedItemStore, productsStore) :
                new AddBookModelCommand(this, navigationService, bookModelService, productsStore);

            if (IsUpdateMode && selectedItemStore.SelectedProduct is not null)
            {
                BookName = selectedItemStore.SelectedProduct.Name;
                AuthorName = selectedItemStore.SelectedProduct.AuthorFullName.Split(' ')[0];
                AuthorMiddleName = selectedItemStore.SelectedProduct.AuthorFullName.Split(' ')[1];
                AuthorLastName = selectedItemStore.SelectedProduct.AuthorFullName.Split(' ')[2];
                ProducerName = selectedItemStore.SelectedProduct.ProducerName;
                AmountOfPages = selectedItemStore.SelectedProduct.PagesAmount;
                Genre = selectedItemStore.SelectedProduct.Genre;
                Year = selectedItemStore.SelectedProduct.Year;
                IsContinuation = selectedItemStore.SelectedProduct.IsContinuation;
                AmountInStock = selectedItemStore.SelectedProduct.AmountInStock;
                Cost = selectedItemStore.SelectedProduct.Cost;
                Price = selectedItemStore.SelectedProduct.Price;
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public override void Dispose()
        {
            if (SubmitCommand is IDisposable disposable)
            {
                disposable.Dispose();
            }
            base.Dispose();
        }
    }
}