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
    public class WriteOffBookCommand : AsyncCommandBase
    {
        private readonly ChangeBookStockViewModel _changeBookStockViewModel;
        private readonly IDashboardNavigationService<BookStockViewModel> _navigationService;
        private readonly IBookService _bookService;
        private readonly ProductViewModel _selectedProduct;
        private readonly ProductsStore _productsStore;

        public WriteOffBookCommand(ChangeBookStockViewModel changeBookStockViewModel,
            IDashboardNavigationService<BookStockViewModel> navigationService,
            IBookService bookService,
            ProductViewModel selectedProduct,
            ProductsStore productsStore)
        {
            _changeBookStockViewModel = changeBookStockViewModel;

            _navigationService = navigationService;
            _bookService = bookService;
            _selectedProduct = selectedProduct;
            _productsStore = productsStore;

            _changeBookStockViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChangeBookStockViewModel.CanChangeBookStock))
            {
                OnCanExecuteChanged();
            }
        }

        override public bool CanExecute(object? parameter)
        {
            return _changeBookStockViewModel.CanChangeBookStock && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _bookService.WriteOffBookAsync(_selectedProduct.ProductId, _changeBookStockViewModel.Amount);
                await _productsStore.RefreshAsync();
                _navigationService.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing off book from stock: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }

        public void Dispose()
        {
            _changeBookStockViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}