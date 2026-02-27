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
        private readonly IDelayBookService _delayBookService;

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

        public ObservableCollection<DelayViewModel> Delays { get; } = new ObservableCollection<DelayViewModel>();

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
            IDelayBookService delayBookService)
        {
            _delayBookService = delayBookService;
            GoToPreviousViewCommand = new RelayCommand((object? s) => navigationService.Navigate());
            DeleteDelayCommand = new DeleteDelayCommand(this, delayBookService);

            // refresh
            _ = RefreshAsync();
        }

        public async Task RefreshAsync()
        {
            if (!IsLoading)
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                try
                {
                    var delays = (await _delayBookService.GetAllDelaysAsync()).Select(d => DelayMapper.ToViewModel(d));
                    Delays.Clear();
                    foreach (var delay in delays)
                    {
                        Delays.Add(delay);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Failed to load delays. {ex.Message}";
                }
                finally
                {
                    IsLoading = false;
                    OnPropertyChanged(nameof(HasDelays));
                }
            }
        }

        public override void Dispose()
        {
            if (DeleteDelayCommand is IDisposable disposable)
            {
                disposable.Dispose();
            }

            base.Dispose();
        }
    }
}