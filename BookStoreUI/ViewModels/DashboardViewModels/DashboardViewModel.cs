using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Interfaces;
using BookStoreUI.Models;
using BookStoreUI.Navigation.Services.DashboardNavigationServices;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Navigation.Stores;
using BookStoreUI.Stores;
using BookStoreUI.ViewModels.AccountViewModels;
using BookStoreUI.ViewModels.BaseViewModels;
using BookStoreUI.ViewModels.CollectionViewModels;
using BookStoreUI.ViewModels.OtherViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.DashboardViewModels
{
    public class DashboardViewModel : ViewModelsBase
    {
        private SelectedItemStore _selectedItemStore;

        public ProductViewModel SelectedBook
        {
            get
            {
                return _selectedItemStore.SelectedProduct;
            }
            set
            {
                _selectedItemStore.SelectedProduct = value;
            }
        }

        private readonly DashboardNavigationStore _navigationStore;

        private ViewModelsBase _currentRightPanel => _navigationStore.CurrentRightPanel;
        public ViewModelsBase CurrentRightPanel
        {
            get
            {
                return _currentRightPanel;
            }
        }

        private readonly BookShopStore _bookShopStore;

        private ObservableCollection<ProductViewModel> _products => _bookShopStore.BookShopObject.Products;
        public ObservableCollection<ProductViewModel> Books
        {
            get
            {
                return _products;
            }
        }

        public bool HasBooks => (Books is not null) ? Books.Any() : false;

        private string _errorMessage => _bookShopStore.BookShopObject.ErrorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private bool _isLoading => _bookShopStore.BookShopObject.IsLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
        }

        public ICommand NavigateToBookModelView { get; }
        public ICommand NavigateToBookStockView { get; }
        public ICommand NavigateToSearchBookView { get; }
        public ICommand NavigateToBookStatisticView { get; }
        public ICommand NavigateToBookDiscountsView { get; }
        public ICommand NavigateToDelayedBooksView { get; }
        public ICommand NavigateToAccountSettingsView { get; }

        public DashboardViewModel(IDashboardNavigationService<BookModelViewModel> navigateToBookModelService,
            IDashboardNavigationService<BookStockViewModel> navigateToBookStockService,
            IDashboardNavigationService<SearchBookViewModel> navigateToSearchBookService,
            IDashboardNavigationService<BookStatisticViewModel> navigateToBookStatisticService,
            IMainNavigationService<BookDiscountsViewModel> navigateToBookDiscountsService,
            IMainNavigationService<DelayedBooksViewModel> navigateToDelayedBooksService,
            IMainNavigationService<AccountSettingsViewModel> navigateToAccountSettingsService,
            DashboardNavigationStore navigationStore,
            SelectedItemStore selectedItemStore,
            BookShopStore bookShopStore,
            CurrentUserStore currentUserStore)
        {

            _selectedItemStore = selectedItemStore;
            _bookShopStore = bookShopStore;
            _navigationStore = navigationStore;

            if (_navigationStore.CurrentRightPanel != null)
            {
                _ = _navigationStore.CurrentRightPanel.SetCollectionToDefault();
            }
            else
            {
                if (currentUserStore.CurrentUser.IsAdmin)
                {
                    navigateToBookModelService.Navigate();
                }
                else
                {
                    navigateToBookStockService.Navigate();
                }
            }
            if (_navigationStore.CurrentRightPanel is ISubscribable subscribable)
            {
                subscribable.SubscribeToEvents();
            }

            NavigateToBookModelView = new AsyncRelayCommand(
                (object? s) => navigateToBookModelService.Navigate(),
                (object? s) => currentUserStore.CurrentUser.IsAdmin && !(CurrentRightPanel is BookModelViewModel));

            NavigateToBookStockView = new AsyncRelayCommand(
                (object? s) => navigateToBookStockService.Navigate(),
                (object? s) => !(CurrentRightPanel is BookStockViewModel));

            NavigateToSearchBookView = new AsyncRelayCommand(
                (object? s) => navigateToSearchBookService.Navigate(),
                (object? s) => !(CurrentRightPanel is SearchBookViewModel));

            NavigateToBookStatisticView = new AsyncRelayCommand(
                (object? s) => navigateToBookStatisticService.Navigate(),
                (object? s) => !(CurrentRightPanel is BookStatisticViewModel));

            NavigateToBookDiscountsView = new RelayCommand(
                (object? s) => navigateToBookDiscountsService.Navigate(),
                (object? s) => !(CurrentRightPanel is BookDiscountsViewModel));

            NavigateToDelayedBooksView = new RelayCommand(
                (object? s) => navigateToDelayedBooksService.Navigate(),
                (object? s) => !(CurrentRightPanel is DelayedBooksViewModel));

            NavigateToAccountSettingsView = new RelayCommand(
                (object? s) => navigateToAccountSettingsService.Navigate(),
                (object? s) => !(CurrentRightPanel is AccountSettingsViewModel));

            _navigationStore.CurrentRightPanelChanged += OnCurrentRightPanelChanged;
            _bookShopStore.BookShopObject.PropertyChanged += OnBookShopPropertyChanged;
        }

        private void OnCurrentRightPanelChanged()
        {
            OnPropertyChanged(nameof(CurrentRightPanel));
            (NavigateToBookModelView as AsyncRelayCommand)?.OnCanExecutedChanged();
            (NavigateToBookStockView as AsyncRelayCommand)?.OnCanExecutedChanged();
            (NavigateToSearchBookView as AsyncRelayCommand)?.OnCanExecutedChanged();
            (NavigateToBookStatisticView as AsyncRelayCommand)?.OnCanExecutedChanged();
            (NavigateToBookDiscountsView as AsyncRelayCommand)?.OnCanExecutedChanged();
            (NavigateToDelayedBooksView as RelayCommand)?.OnCanExecutedChanged();
            (NavigateToAccountSettingsView as RelayCommand)?.OnCanExecutedChanged();
        }

        public void OnBookShopPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BookShop.Products))
            {
                OnPropertyChanged(nameof(Books));
                OnPropertyChanged(nameof(HasBooks));
            }
            if (e.PropertyName == nameof(BookShop.ErrorMessage))
            {
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
            if (e.PropertyName == nameof(BookShop.IsLoading))
            {
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public override void Dispose()
        {
            if (_navigationStore.CurrentRightPanel != null)
            {
                _navigationStore.CurrentRightPanel.Dispose();
            }

            _navigationStore.CurrentRightPanelChanged -= OnCurrentRightPanelChanged;
            _bookShopStore.BookShopObject.PropertyChanged -= OnBookShopPropertyChanged;

            base.Dispose();
        }
    }
}