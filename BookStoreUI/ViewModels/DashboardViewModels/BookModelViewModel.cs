using BLL.Services.BookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Commands.DashboardCommands.BookModelCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.Stores.ControlContextStores;
using BookStoreUI.ViewModels.BaseViewModels;
using BookStoreUI.ViewModels.CollectionViewModels;
using BookStoreUI.ViewModels.OtherViewModels;
using System.ComponentModel;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.DashboardViewModels
{
    public class BookModelViewModel : ViewModelsBase
    {
        private readonly IBookService _bookService;
        private readonly SelectedItemStore _selectedItemStore;

        private ProductViewModel _selectedBook => _selectedItemStore.SelectedProduct;
        public ProductViewModel SelectedBook
        {
            get
            {
                return _selectedBook;
            }
        }

        public ICommand AddNewBookCommand { get; }
        public ICommand UpdateBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand AddDiscountCommand { get; }


        public BookModelViewModel(IMainNavigationService<ChangeBookModelViewModel> navigateToChangeBookModelViewModel,
            IMainNavigationService<AddDiscountViewModel> navigateToAddDiscountViewModel,
            IBookService bookService,
            ChangeBookModelControlContextStore changeBookModelControlContextStore,
            SelectedItemStore selectedItemStore,
            ProductsStore productsStore)
        {
            _selectedItemStore = selectedItemStore;
            _bookService = bookService;

            AddNewBookCommand = new RelayCommand((object? s) =>
            {
                changeBookModelControlContextStore.ChangeBookModelControlContextObject.IsUpdateMode = false;
                navigateToChangeBookModelViewModel.Navigate();
            });

            UpdateBookCommand = new RelayCommand(
                (object? s) =>
                {
                    changeBookModelControlContextStore.ChangeBookModelControlContextObject.IsUpdateMode = true;
                    navigateToChangeBookModelViewModel.Navigate();
                },
                (object? s) => SelectedBook != null);

            DeleteBookCommand = new DeleteBookModelCommand(_bookService, _selectedItemStore, productsStore);

            AddDiscountCommand = new RelayCommand(
                (object? s) =>
                {
                    navigateToAddDiscountViewModel.Navigate();
                },
                (object? s) => SelectedBook != null);

            _selectedItemStore.PropertyChanged += OnSelectedItemPropertyChanged;
        }

        private void OnSelectedItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedItemStore.SelectedProduct))
            {
                (UpdateBookCommand as RelayCommand)?.OnCanExecutedChanged();
                (AddDiscountCommand as RelayCommand)?.OnCanExecutedChanged();
            }
        }

        public override void Dispose()
        {
            if (DeleteBookCommand is IDisposable disposable)
            {
                disposable.Dispose();
            }
            _selectedItemStore.PropertyChanged -= OnSelectedItemPropertyChanged;
            base.Dispose();
        }
    }
}