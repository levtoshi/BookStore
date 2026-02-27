using BLL.Services.BookDiscountServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.ViewModels.OtherViewModels;
using System.Windows;

namespace BookStoreUI.Commands.DashboardCommands.BookModelCommands
{
    public class DeleteDiscountCommand : AsyncCommandBase, IDisposable
    {
        private readonly BookDiscountsViewModel _booksDiscountsViewModel;
        private readonly IBookDiscountService _bookDiscountService;

        public DeleteDiscountCommand(BookDiscountsViewModel booksDiscountsViewModel,
            IBookDiscountService bookDiscountService)
        {
            _booksDiscountsViewModel = booksDiscountsViewModel;
            _booksDiscountsViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _bookDiscountService = bookDiscountService;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BookDiscountsViewModel.SelectedDiscount))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _booksDiscountsViewModel.SelectedDiscount != null && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _bookDiscountService.RemoveDiscountAsync(_booksDiscountsViewModel.SelectedDiscount.ProductId);
                await _booksDiscountsViewModel.RefreshAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting discount: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }

        public void Dispose()
        {
            _booksDiscountsViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}