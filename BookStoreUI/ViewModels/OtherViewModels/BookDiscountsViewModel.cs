using BLL.Services.BookDiscountServices;
using BLL.Services.BookStoreProviderServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Commands.DashboardCommands.BookModelCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.ViewModelDTOMappers;
using BookStoreUI.ViewModels.BaseViewModels;
using BookStoreUI.ViewModels.CollectionViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.OtherViewModels
{
    public class BookDiscountsViewModel : ViewModelsBase
    {
        private readonly IBookStoreProviderService _bookStoreProviderService;

        private DiscountViewModel _selectedDiscount;
        public DiscountViewModel SelectedDiscount
        {
            get
            {
                return _selectedDiscount;
            }
            set
            {
                _selectedDiscount = value;
                OnPropertyChanged(nameof(SelectedDiscount));
            }
        }

        private ObservableCollection<DiscountViewModel> _discounts;
        public ObservableCollection<DiscountViewModel> Discounts
        {
            get
            {
                return _discounts;
            }
            set
            {
                _discounts = value;
                OnPropertyChanged(nameof(Discounts));
                OnPropertyChanged(nameof(HasDiscounts));
            }
        }

        public bool HasDiscounts => (Discounts is not null) ? Discounts.Any() : false;

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));

                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand GoToPreviousViewCommand { get; }
        public ICommand DeleteDiscountCommand { get; }

        public BookDiscountsViewModel(IMainNavigationService<DashboardViewModel> navigationService,
            IBookDiscountService discountBookService,
            IBookStoreProviderService bookStoreProviderService)
        {
            _ = discountBookService.SetToDefault();

            GoToPreviousViewCommand = new RelayCommand((object? s) => navigationService.Navigate());

            DeleteDiscountCommand = new DeleteDiscountCommand(this, discountBookService);

            _bookStoreProviderService = bookStoreProviderService;
            _bookStoreProviderService.UpdateStarted += OnCollectionUpdateStarted;
            _bookStoreProviderService.CollectionChanged += OnCollectionChanged;
        }

        private async Task LoadDiscounts()
        {
            try
            {
                Discounts = new ObservableCollection<DiscountViewModel>((await _bookStoreProviderService.GetAllProductsAsync()).Select(p => DiscountMapper.ToViewModel(p)));
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to load books. {ex.Message}";
            }

            IsLoading = false;
        }

        private void OnCollectionUpdateStarted(object? sender, EventArgs e)
        {
            ErrorMessage = String.Empty;
            IsLoading = true;
        }

        public async void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            await LoadDiscounts();
        }

        public override void Dispose()
        {
            if (DeleteDiscountCommand is IDisposable disposable)
            {
                disposable.Dispose();
            }
            _bookStoreProviderService.UpdateStarted -= OnCollectionUpdateStarted;
            _bookStoreProviderService.CollectionChanged -= OnCollectionChanged;
            base.Dispose();
        }
    }
}