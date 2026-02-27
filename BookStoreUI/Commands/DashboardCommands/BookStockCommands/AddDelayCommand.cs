using BLL.DTOs;
using BLL.Services.DelayBookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.ViewModels.CollectionViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using BookStoreUI.ViewModels.OtherViewModels;
using System.Windows;

namespace BookStoreUI.Commands.DashboardCommands.BookStockCommands
{
    public class AddDelayCommand : AsyncCommandBase, IDisposable
    {
        private readonly AddDelayViewModel _addDelayViewModel;
        private readonly IMainNavigationService<DashboardViewModel> _navigationService;
        private readonly IDelayBookService _delayBookService;
        private readonly ProductViewModel _selectedProduct;

        public AddDelayCommand(AddDelayViewModel addDelayViewModel,
            IMainNavigationService<DashboardViewModel> navigationService,
            IDelayBookService delayBookService,
            SelectedItemStore selectedItemStore)
        {
            _addDelayViewModel = addDelayViewModel;
            _addDelayViewModel.PropertyChanged += OnViewModelPropertyChanged;
            _navigationService = navigationService;

            _delayBookService = delayBookService;
            _selectedProduct = selectedItemStore.SelectedProduct;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddDelayViewModel.CanDelayBook))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _addDelayViewModel.CanDelayBook && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _delayBookService.AddDelayAsync(_selectedProduct.ProductId,
                new DelayDTO()
                {
                    Amount = _addDelayViewModel.Amount,
                    Customer = new CustomerDTO()
                    {
                        FullName = new FullNameDTO()
                        {
                            Name = _addDelayViewModel.CustomerName,
                            MiddleName = _addDelayViewModel.CustomerMiddleName,
                            LastName = _addDelayViewModel.CustomerLastName,
                        },
                        Email = _addDelayViewModel.Email
                    }
                });
                _navigationService.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding delay: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }

        public void Dispose()
        {
            _addDelayViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}