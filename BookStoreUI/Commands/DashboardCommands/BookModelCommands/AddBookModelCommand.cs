using BLL.DTOs;
using BLL.Services.BookModelServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.ViewModels.DashboardViewModels;
using BookStoreUI.ViewModels.OtherViewModels;

namespace BookStoreUI.Commands.DashboardCommands.BookModelCommands
{
    public class AddBookModelCommand : AsyncCommandBase, IDisposable
    {
        private readonly ChangeBookModelViewModel _changeBookModelViewModel;
        private readonly IMainNavigationService<DashboardViewModel> _navigateToDashboardViewModelService;
        private readonly IBookModelService _bookModelService;

        public AddBookModelCommand(ChangeBookModelViewModel changeBookModelViewModel,
            IMainNavigationService<DashboardViewModel> navigateToDashboardViewModelService,
            IBookModelService bookModelService)
        {
            _changeBookModelViewModel = changeBookModelViewModel;
            _changeBookModelViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _navigateToDashboardViewModelService = navigateToDashboardViewModelService;
            _bookModelService = bookModelService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _bookModelService.AddBookModelAsync(new ProductDTO()
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

            _navigateToDashboardViewModelService.Navigate();
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