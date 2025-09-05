using BLL.Services.BookStockServices;
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
        private readonly IBookStockService _bookStockService;
        private readonly SelectedItemStore _selectedItemStore;

        public SellBookCommand(SellBookViewModel sellBookViewModel,
            IDashboardNavigationService<BookStockViewModel> navigationService,
            IBookStockService bookStockService,
            SelectedItemStore selectedItemStore)
        {
            _sellBookViewModel = sellBookViewModel;
            _sellBookViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _navigationService = navigationService;
            _bookStockService = bookStockService;
            _selectedItemStore = selectedItemStore;
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
                await _bookStockService.SellBookAsync(_selectedItemStore.SelectedProduct.ProductId, _sellBookViewModel.Amount, _sellBookViewModel.SaleDate);
                await _navigationService.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Dispose()
        {
            _sellBookViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}