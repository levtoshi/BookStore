using BLL.Services.BookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Commands.DashboardCommands.BookStockCommands;
using BookStoreUI.Navigation.Services.DashboardNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.ViewModels.BaseViewModels;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.DashboardViewModels
{
    public class SellBookViewModel : ViewModelsBase, INotifyDataErrorInfo
    {
        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));

                ClearErrors(nameof(Amount));

                if (!HasAmountGreaterThanZero)
                {
                    AddError("Amount must be greater than zero.", nameof(Amount));
                }

                OnPropertyChanged(nameof(CanSellBook));
            }
        }

        private DateTime _saleDate;
        public DateTime SaleDate
        {
            get => _saleDate;
            set
            {
                _saleDate = value;
                OnPropertyChanged(nameof(SaleDate));

                ClearErrors(nameof(SaleDate));

                if (!HasSaleDate)
                {
                    AddError("Sale date cannot be empty.", nameof(SaleDate));
                }

                OnPropertyChanged(nameof(CanSellBook));
            }
        }

        public bool CanSellBook =>
            HasAmountGreaterThanZero &&
            HasSaleDate &&
            !HasErrors;
        public bool HasAmountGreaterThanZero => Amount > 0;
        public bool HasSaleDate => !String.IsNullOrEmpty(SaleDate.ToString());

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public ICommand SubmitCommand { get; }
        public ICommand SetSaleDateToNowCommand { get; }

        public SellBookViewModel(IDashboardNavigationService<BookStockViewModel> navigateToBookStockViewModelService,
            IBookService bookService,
            SelectedItemStore selectedItemStore,
            ProductsStore productsStore)
        {
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
            
            SaleDate = DateTime.Now;

            SubmitCommand = new SellBookCommand(this, navigateToBookStockViewModelService, bookService, selectedItemStore, productsStore);
            SetSaleDateToNowCommand = new RelayCommand((object? s) => SaleDate = DateTime.Now);
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