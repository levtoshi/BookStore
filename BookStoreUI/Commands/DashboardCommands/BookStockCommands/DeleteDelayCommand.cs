using BLL.Services.DelayBookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.ViewModels.OtherViewModels;

namespace BookStoreUI.Commands.DashboardCommands.BookStockCommands
{
    public class DeleteDelayCommand : AsyncCommandBase, IDisposable
    {
        private readonly DelayedBooksViewModel _delayBooksViewModel;
        private readonly IDelayBookService _delayBookService;

        public DeleteDelayCommand(DelayedBooksViewModel delayBooksViewModel,
            IDelayBookService delayBookService)
        {
            _delayBooksViewModel = delayBooksViewModel;
            _delayBooksViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _delayBookService = delayBookService;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DelayedBooksViewModel.SelectedDelay))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _delayBooksViewModel.SelectedDelay != null && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _delayBookService.RemoveDelayAsync(_delayBooksViewModel.SelectedDelay.ProductId);
        }

        public void Dispose()
        {
            _delayBooksViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}