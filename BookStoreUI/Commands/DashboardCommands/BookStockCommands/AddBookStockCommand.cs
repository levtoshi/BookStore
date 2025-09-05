using BLL.Services.BookStockServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Interfaces;
using BookStoreUI.Navigation.Services.DashboardNavigationServices;
using BookStoreUI.ViewModels.CollectionViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using System.ComponentModel;

namespace BookStoreUI.Commands.DashboardCommands.BookStockCommands
{
    public class AddBookStockCommand : AsyncCommandBase, IDisposable, ISubscribable
    {
        private readonly ChangeBookStockViewModel _changeBookStockViewModel;
        private readonly IDashboardNavigationService<BookStockViewModel> _navigationService;
        private readonly IBookStockService _bookStockService;
        private readonly ProductViewModel _selectedProduct;

        public AddBookStockCommand(ChangeBookStockViewModel changeBookStockViewModel,
            IDashboardNavigationService<BookStockViewModel> navigationService,
            IBookStockService bookStockService,
            ProductViewModel selectedProduct)
        {
            _changeBookStockViewModel = changeBookStockViewModel;

            _navigationService = navigationService;
            _bookStockService = bookStockService;
            _selectedProduct = selectedProduct;
        }

        public void SubscribeToEvents()
        {
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
            await _bookStockService.AddBookStockAsync(_selectedProduct.ProductId, _changeBookStockViewModel.Amount);
            await _navigationService.Navigate();
        }

        public void Dispose()
        {
            _changeBookStockViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}