using BLL.DTOs;
using BLL.Services.BookDiscountServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.ViewModels.DashboardViewModels;
using BookStoreUI.ViewModels.OtherViewModels;
using System.Windows;

namespace BookStoreUI.Commands.DashboardCommands.BookModelCommands
{
    public class AddDiscountCommand : AsyncCommandBase, IDisposable
    {
        private readonly AddDiscountViewModel _addDiscountViewModel;
        private readonly IMainNavigationService<DashboardViewModel> _navigationService;
        private readonly IBookDiscountService _bookDiscountService;
        private readonly SelectedItemStore _selectedItemStore;

        public AddDiscountCommand(AddDiscountViewModel addDiscountViewModel,
            IMainNavigationService<DashboardViewModel> navigationService,
            IBookDiscountService bookDiscountService,
            SelectedItemStore selectedItemStore)
        {
            _addDiscountViewModel = addDiscountViewModel;
            _addDiscountViewModel.PropertyChanged += OnViewModelPropertyChanged;
            
            _navigationService = navigationService;

            _bookDiscountService = bookDiscountService;
            _selectedItemStore = selectedItemStore;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddDiscountViewModel.CanAddBookDiscount))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _addDiscountViewModel.CanAddBookDiscount && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _bookDiscountService.AddDiscountAsync(_selectedItemStore.SelectedProduct.ProductId,
                    new DiscountDTO()
                    {
                        Name = _addDiscountViewModel.DiscountName,
                        Interest = _addDiscountViewModel.Interest,
                        StartDate = _addDiscountViewModel.StartDate,
                        EndDate = _addDiscountViewModel.EndDate
                    });
                _selectedItemStore.SelectedProduct = null;
                _navigationService.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding discount: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }

        public void Dispose()
        {
            _addDiscountViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}