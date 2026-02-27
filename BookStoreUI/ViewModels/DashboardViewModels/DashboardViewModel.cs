using BLL.Services.BookServices;
using BookStoreUI.Commands.BaseCommands;
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
        private readonly ProductsStore _productsStore;

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

        public ObservableCollection<ProductViewModel> Books => _productsStore.Products;

        public bool HasBooks => _productsStore.HasProducts;
        public string ErrorMessage => _productsStore.ErrorMessage;
        public bool HasErrorMessage => _productsStore.HasErrorMessage;
        public bool IsLoading => _productsStore.IsLoading;

        public ICommand NavigateToBookModelView { get; }
        public ICommand NavigateToBookStockView { get; }
        public ICommand NavigateToFilterBookView { get; }
        public ICommand NavigateToBookStatisticView { get; }
        public ICommand NavigateToBookDiscountsView { get; }
        public ICommand NavigateToDelayedBooksView { get; }
        public ICommand NavigateToAccountSettingsView { get; }

        public DashboardViewModel(IDashboardNavigationService<BookModelViewModel> navigateToBookModelService,
            IDashboardNavigationService<BookStockViewModel> navigateToBookStockService,
            IDashboardNavigationService<FilterBookViewModel> navigateToFilterBookService,
            IMainNavigationService<BookDiscountsViewModel> navigateToBookDiscountsService,
            IMainNavigationService<DelayedBooksViewModel> navigateToDelayedBooksService,
            IMainNavigationService<AccountSettingsViewModel> navigateToAccountSettingsService,
            IBookService bookService,
            DashboardNavigationStore navigationStore,
            SelectedItemStore selectedItemStore,
            CurrentUserStore currentUserStore,
            ProductsStore productsStore)
        {
            _productsStore = productsStore;
            _selectedItemStore = selectedItemStore;
            _navigationStore = navigationStore;

            if (currentUserStore.CurrentUser.IsAdmin)
            {
                navigateToBookModelService.Navigate();
            }
            else
            {
                navigateToBookStockService.Navigate();
            }

            NavigateToBookModelView = new RelayCommand(
                (object? s) => navigateToBookModelService.Navigate(),
                (object? s) => currentUserStore.CurrentUser.IsAdmin && !(CurrentRightPanel is BookModelViewModel));

            NavigateToBookStockView = new RelayCommand(
                (object? s) => navigateToBookStockService.Navigate(),
                (object? s) => !(CurrentRightPanel is BookStockViewModel));

            NavigateToFilterBookView = new RelayCommand(
                (object? s) => navigateToFilterBookService.Navigate(),
                (object? s) => !(CurrentRightPanel is FilterBookViewModel));

            NavigateToBookDiscountsView = new RelayCommand(
                (object? s) => navigateToBookDiscountsService.Navigate(),
                (object? s) => !(CurrentRightPanel is BookDiscountsViewModel));

            NavigateToDelayedBooksView = new RelayCommand(
                (object? s) => navigateToDelayedBooksService.Navigate(),
                (object? s) => !(CurrentRightPanel is DelayedBooksViewModel));

            NavigateToAccountSettingsView = new RelayCommand(
                (object? s) => navigateToAccountSettingsService.Navigate(),
                (object? s) => !(CurrentRightPanel is AccountSettingsViewModel));

            _productsStore.PropertyChanged += OnProductsStorePropertyChanged;
            _navigationStore.CurrentRightPanelChanged += OnCurrentRightPanelChanged;

            _ = _productsStore.RefreshAsync();
        }

        private void OnProductsStorePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProductsStore.IsLoading) ||
                e.PropertyName == nameof(ProductsStore.ErrorMessage) ||
                e.PropertyName == nameof(ProductsStore.Products))
            {
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
                OnPropertyChanged(nameof(Books));
                OnPropertyChanged(nameof(HasBooks));
            }
        }

        private void OnCurrentRightPanelChanged()
        {
            OnPropertyChanged(nameof(CurrentRightPanel));
            (NavigateToBookModelView as RelayCommand)?.OnCanExecutedChanged();
            (NavigateToBookStockView as RelayCommand)?.OnCanExecutedChanged();
            (NavigateToFilterBookView as RelayCommand)?.OnCanExecutedChanged();
            (NavigateToBookDiscountsView as RelayCommand)?.OnCanExecutedChanged();
            (NavigateToDelayedBooksView as RelayCommand)?.OnCanExecutedChanged();
            (NavigateToAccountSettingsView as RelayCommand)?.OnCanExecutedChanged();
        }

        public override void Dispose()
        {
            if (_navigationStore.CurrentRightPanel != null)
            {
                _navigationStore.CurrentRightPanel.Dispose();
            }

            _navigationStore.CurrentRightPanelChanged -= OnCurrentRightPanelChanged;
            _productsStore.PropertyChanged -= OnProductsStorePropertyChanged;

            base.Dispose();
        }
    }
}