using BLL.DTOs;
using BLL.Services.BookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Stores;
using BookStoreUI.ViewModelDTOMappers;
using BookStoreUI.ViewModels.DashboardViewModels;
using DLL.Entities;
using System.ComponentModel;
using System.Windows;

namespace BookStoreUI.Commands.DashboardCommands.BookModelCommands
{
    public class DeleteBookModelCommand : AsyncCommandBase, IDisposable
    {
        private readonly IBookService _bookService;
        private readonly SelectedItemStore _selectedItemStore;
        private readonly ProductsStore _productsStore;

        public DeleteBookModelCommand(IBookService bookService,
            SelectedItemStore selectedItemStore,
            ProductsStore productsStore)
        {
            _bookService = bookService;
            _selectedItemStore = selectedItemStore;
            _productsStore = productsStore;
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
                await _bookService.DeleteBookModelAsync(ProductMapper.ToDTO(_selectedItemStore.SelectedProduct));
                await _productsStore.RefreshAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting book model: {ex.Message}", "Error", MessageBoxButton.OK);
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