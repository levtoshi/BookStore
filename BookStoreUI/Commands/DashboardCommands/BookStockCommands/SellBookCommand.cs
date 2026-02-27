using BLL.Services.BookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Navigation.Services.DashboardNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.ViewModels.CollectionViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using System.ComponentModel;
using System.Windows;

namespace BookStoreUI.Commands.DashboardCommands.BookStockCommands
{
    public class SellBookCommand : AsyncCommandBase, IDisposable
    {
        private readonly SellBookViewModel _sellBookViewModel;
        private readonly IDashboardNavigationService<BookStockViewModel> _navigationService;
        private readonly IBookService _bookService;
        private readonly SelectedItemStore _selectedItemStore;
        private readonly ProductsStore _productsStore;

        public SellBookCommand(SellBookViewModel sellBookViewModel,
            IDashboardNavigationService<BookStockViewModel> navigationService,
            IBookService bookService,
            SelectedItemStore selectedItemStore,
            ProductsStore productsStore)
        {
            _sellBookViewModel = sellBookViewModel;
            _sellBookViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _navigationService = navigationService;
            _bookService = bookService;
            _selectedItemStore = selectedItemStore;
            _productsStore = productsStore;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SellBookViewModel.CanSellBook))
            {
                OnCanExecuteChanged();
            }
        }

        override public bool CanExecute(object? parameter)
        {
            return _sellBookViewModel.CanSellBook && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _bookService.SellBookAsync(_selectedItemStore.SelectedProduct.ProductId, _sellBookViewModel.Amount, _sellBookViewModel.SaleDate);
                await _productsStore.RefreshAsync();
                _navigationService.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selling books: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Dispose()
        {
            _sellBookViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}