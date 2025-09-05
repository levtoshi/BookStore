using BLL.Services.BookStoreProviderServices;
using BLL.Services.DelayBookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Commands.DashboardCommands.BookStockCommands;
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
    public class DelayedBooksViewModel : ViewModelsBase
    {
        private readonly IBookStoreProviderService _bookStoreProviderService;

        private DelayViewModel _selectedDelay;
        public DelayViewModel SelectedDelay
        {
            get
            {
                return _selectedDelay;
            }
            set
            {
                _selectedDelay = value;
                OnPropertyChanged(nameof(SelectedDelay));
            }
        }

        private ObservableCollection<DelayViewModel> _delays;
        public ObservableCollection<DelayViewModel> Delays
        {
            get
            {
                return _delays;
            }
            set
            {
                _delays = value;
                OnPropertyChanged(nameof(Delays));
                OnPropertyChanged(nameof(HasDelays));
            }
        }

        public bool HasDelays => (Delays is not null) ? Delays.Any() : false;

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
        public ICommand DeleteDelayCommand { get; }

        public DelayedBooksViewModel(IMainNavigationService<DashboardViewModel> navigationService,
            IDelayBookService delayBookService,
            IBookStoreProviderService bookStoreProviderService)
        {
            _ = delayBookService.SetToDefault();

            GoToPreviousViewCommand = new RelayCommand((object? s) => navigationService.Navigate());

            DeleteDelayCommand = new DeleteDelayCommand(this, delayBookService);

            _bookStoreProviderService = bookStoreProviderService;
            _bookStoreProviderService.UpdateStarted += OnCollectionUpdateStarted;
            _bookStoreProviderService.CollectionChanged += OnCollectionChanged;
        }

        private async Task LoadDelays()
        {
            try
            {
                Delays = new ObservableCollection<DelayViewModel>((await _bookStoreProviderService.GetAllProductsAsync()).Select(p => DelayMapper.ToViewModel(p)));
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
            await LoadDelays();
        }

        public override void Dispose()
        {
            if (DeleteDelayCommand is IDisposable disposable)
            {
                disposable.Dispose();
            }

            _bookStoreProviderService.UpdateStarted -= OnCollectionUpdateStarted;
            _bookStoreProviderService.CollectionChanged -= OnCollectionChanged;

            base.Dispose();
        }
    }
}