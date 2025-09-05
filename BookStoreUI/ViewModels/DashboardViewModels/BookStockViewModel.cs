using BLL.Services.BookStockServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Interfaces;
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
    public class BookStockViewModel : RightPanelBase, ISubscribable
    {
        private readonly SelectedItemStore _selectedItemStore;
        private readonly IBookStockService _bookStockService;

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

        public BookStockViewModel(IDashboardNavigationService<ChangeBookStockViewModel> navigateToAddBookStockService,
            IDashboardNavigationService<ChangeBookStockViewModel> navigateToWriteOffBookService,
            IDashboardNavigationService<SellBookViewModel> navigateToSellBookService,
            IMainNavigationService<AddDelayViewModel> navigateToAddDelayService,
            IBookStockService bookStockService,
            SelectedItemStore selectedItemStore,
            ChangeBookStockControlContextStore changeBookStockControlContextStore,
            CurrentUserStore currentUserStore)
        {
            _selectedItemStore = selectedItemStore;
            _bookStockService = bookStockService;

            AddBookStockCommand = new AsyncRelayCommand(
                async (object? s) =>
                {
                    changeBookStockControlContextStore.ChangeBookStockControlContextObject.IsWriteOffMode = false;
                    await navigateToAddBookStockService.Navigate();
                },
                (object? s) => SelectedBook != null);

            WriteOffBookCommand = new AsyncRelayCommand(
                async (object? s) =>
                {
                    changeBookStockControlContextStore.ChangeBookStockControlContextObject.IsWriteOffMode = true;
                    await navigateToWriteOffBookService.Navigate();
                },
                (object? s) => currentUserStore.CurrentUser.IsAdmin && SelectedBook != null);

            SellBookCommand = new AsyncRelayCommand(
                async (object? s) =>
                {
                    await navigateToSellBookService.Navigate();
                },
                (object? s) => SelectedBook != null);

            AddBookDelayCommand = new RelayCommand(
                (object? s) =>
                {
                    navigateToAddDelayService.Navigate();
                },
                (object? s) => SelectedBook != null);
        }

        public void SubscribeToEvents()
        {
            _selectedItemStore.PropertyChanged += OnSelectedItemStorePropertyChanged;
        }

        private void OnSelectedItemStorePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedItemStore.SelectedProduct))
            {
                (AddBookStockCommand as AsyncRelayCommand)?.OnCanExecutedChanged();
                (WriteOffBookCommand as AsyncRelayCommand)?.OnCanExecutedChanged();
                (SellBookCommand as AsyncRelayCommand)?.OnCanExecutedChanged();
                (AddBookDelayCommand as RelayCommand)?.OnCanExecutedChanged();
            }
        }

        public override async Task SetCollectionToDefault()
        {
            await _bookStockService.SetToDefault();
        }

        public override void Dispose()
        {
            _selectedItemStore.PropertyChanged -= OnSelectedItemStorePropertyChanged;

            base.Dispose();
        }
    }
}