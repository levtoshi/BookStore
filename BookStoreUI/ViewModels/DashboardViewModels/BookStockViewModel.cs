using BLL.Services.BookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Navigation.Services.DashboardNavigationServices;
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
    public class BookStockViewModel : ViewModelsBase
    {
        private readonly SelectedItemStore _selectedItemStore;

        private ProductViewModel _selectedBook => _selectedItemStore.SelectedProduct;
        public ProductViewModel SelectedBook
        {
            get
            {
                return _selectedBook;
            }
        }

        public ICommand AddBookStockCommand { get; }
        public ICommand WriteOffBookCommand { get; }
        public ICommand SellBookCommand { get; }
        public ICommand AddBookDelayCommand { get; }

        public BookStockViewModel(IDashboardNavigationService<ChangeBookStockViewModel> navigateToChangeBookStockService,
            IDashboardNavigationService<SellBookViewModel> navigateToSellBookService,
            IMainNavigationService<AddDelayViewModel> navigateToAddDelayService,
            IBookService bookService,
            SelectedItemStore selectedItemStore,
            ChangeBookStockControlContextStore changeBookStockControlContextStore,
            CurrentUserStore currentUserStore)
        {
            _selectedItemStore = selectedItemStore;

            AddBookStockCommand = new RelayCommand(
                (object? s) =>
                {
                    changeBookStockControlContextStore.ChangeBookStockControlContextObject.IsWriteOffMode = false;
                    navigateToChangeBookStockService.Navigate();
                },
                (object? s) => SelectedBook != null);

            WriteOffBookCommand = new RelayCommand(
                (object? s) =>
                {
                    changeBookStockControlContextStore.ChangeBookStockControlContextObject.IsWriteOffMode = true;
                    navigateToChangeBookStockService.Navigate();
                },
                (object? s) => currentUserStore.CurrentUser.IsAdmin && SelectedBook != null);

            SellBookCommand = new RelayCommand(
                (object? s) =>
                {
                    navigateToSellBookService.Navigate();
                },
                (object? s) => SelectedBook != null);

            AddBookDelayCommand = new RelayCommand(
                (object? s) =>
                {
                    navigateToAddDelayService.Navigate();
                },
                (object? s) => SelectedBook != null);

            selectedItemStore.PropertyChanged += OnSelectedItemStorePropertyChanged;
        }

        private void OnSelectedItemStorePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedItemStore.SelectedProduct))
            {
                (AddBookStockCommand as RelayCommand)?.OnCanExecutedChanged();
                (WriteOffBookCommand as RelayCommand)?.OnCanExecutedChanged();
                (SellBookCommand as RelayCommand)?.OnCanExecutedChanged();
                (AddBookDelayCommand as RelayCommand)?.OnCanExecutedChanged();
            }
        }

        public override void Dispose()
        {
            _selectedItemStore.PropertyChanged -= OnSelectedItemStorePropertyChanged;

            base.Dispose();
        }
    }
}