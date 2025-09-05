using BLL.Services.BookModelServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Interfaces;
using BookStoreUI.Stores;
using BookStoreUI.ViewModelDTOMappers;
using BookStoreUI.ViewModels.DashboardViewModels;
using System.ComponentModel;
using System.Windows;

namespace BookStoreUI.Commands.DashboardCommands.BookModelCommands
{
    public class DeleteBookModelCommand : AsyncCommandBase, IDisposable, ISubscribable
    {
        private readonly BookModelViewModel _bookModelViewModel;
        private readonly IBookModelService _bookModelService;
        private readonly SelectedItemStore _selectedItemStore;

        public DeleteBookModelCommand(BookModelViewModel bookModelViewModel,
            IBookModelService bookModelService,
            SelectedItemStore selectedItemStore)
        {
            _bookModelViewModel = bookModelViewModel;

            _bookModelService = bookModelService;
            _selectedItemStore = selectedItemStore;
        }

        public void SubscribeToEvents()
        {
            _selectedItemStore.PropertyChanged += OnSelectedItemPropertyChanged;
        }

        private void OnSelectedItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedItemStore.SelectedProduct))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _bookModelService.DeleteBookModelAsync(ProductMapper.ToDTO(_selectedItemStore.SelectedProduct));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _selectedItemStore.SelectedProduct != null && base.CanExecute(parameter);
        }

        public void Dispose()
        {
            _selectedItemStore.PropertyChanged -= OnSelectedItemPropertyChanged;
        }
    }
}