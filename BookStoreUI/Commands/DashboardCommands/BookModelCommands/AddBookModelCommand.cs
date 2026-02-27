using BLL.DTOs;
using BLL.Services.BookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.ViewModels.DashboardViewModels;
using BookStoreUI.ViewModels.OtherViewModels;
using System.Windows;

namespace BookStoreUI.Commands.DashboardCommands.BookModelCommands
{
    public class AddBookModelCommand : AsyncCommandBase, IDisposable
    {
        private readonly ChangeBookModelViewModel _changeBookModelViewModel;
        private readonly IMainNavigationService<DashboardViewModel> _navigateToDashboardViewModelService;
        private readonly IBookService _bookService;
        private readonly ProductsStore _productsStore;

        public AddBookModelCommand(ChangeBookModelViewModel changeBookModelViewModel,
            IMainNavigationService<DashboardViewModel> navigateToDashboardViewModelService,
            IBookService bookService,
            ProductsStore productsStore)
        {
            _changeBookModelViewModel = changeBookModelViewModel;
            _changeBookModelViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _navigateToDashboardViewModelService = navigateToDashboardViewModelService;
            _bookService = bookService;
            _productsStore = productsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string error = string.Empty;

            try
            {
                await _bookService.AddBookModelAsync(new ProductDTO()
                {
                    Amount = _changeBookModelViewModel.AmountInStock,
                    Cost = _changeBookModelViewModel.Cost,
                    Price = _changeBookModelViewModel.Price,
                    Book = new BookDTO()
                    {
                        Name = _changeBookModelViewModel.BookName,
                        Author = new FullNameDTO()
                        {
                            Name = _changeBookModelViewModel.AuthorName,
                            MiddleName = _changeBookModelViewModel.AuthorMiddleName,
                            LastName = _changeBookModelViewModel.AuthorLastName
                        },
                        Producer = new ProducerDTO()
                        {
                            Name = _changeBookModelViewModel.ProducerName,
                        },
                        Genre = new GenreDTO()
                        {
                            Name = _changeBookModelViewModel.Genre,
                        },
                        Year = _changeBookModelViewModel.Year,
                        IsContinuation = _changeBookModelViewModel.IsContinuation,
                        PageAmount = _changeBookModelViewModel.AmountOfPages
                    },
                });

                await _productsStore.RefreshAsync();
                _navigateToDashboardViewModelService.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding book model: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _changeBookModelViewModel.CanChangeBookModel && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChangeBookModelViewModel.CanChangeBookModel))
            {
                OnCanExecuteChanged();
            }
        }

        public void Dispose()
        {
            _changeBookModelViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}