using BLL.Services.BookDiscountServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.ViewModels.OtherViewModels;

namespace BookStoreUI.Commands.DashboardCommands.BookModelCommands
{
    public class DeleteDiscountCommand : AsyncCommandBase, IDisposable
    {
        private readonly BookDiscountsViewModel _booksDiscountsViewModel;
        private readonly IBookDiscountService _bookDiscountService;
        //private readonly IBookStoreProviderService _bookStoreDiscountService;

        public DeleteDiscountCommand(BookDiscountsViewModel booksDiscountsViewModel,
            IBookDiscountService bookDiscountService)//,
                                                     //IBookStoreProviderService bookStoreProviderService)
        {
            _booksDiscountsViewModel = booksDiscountsViewModel;
            _booksDiscountsViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _bookDiscountService = bookDiscountService;
            //_bookStoreDiscountService = bookStoreProviderService;
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
            await _bookDiscountService.RemoveDiscountAsync(_booksDiscountsViewModel.SelectedDiscount.ProductId);
        }

        public void Dispose()
        {
            _booksDiscountsViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}