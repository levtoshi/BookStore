using BLL.Services.BookDiscountServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Commands.DashboardCommands.BookModelCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.ViewModelDTOMappers;
using BookStoreUI.ViewModels.BaseViewModels;
using BookStoreUI.ViewModels.CollectionViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using DLL.Entities;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.OtherViewModels
{
    public class BookDiscountsViewModel : ViewModelsBase
    {
        private readonly IBookDiscountService _bookDiscountService;

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

        public ObservableCollection<DiscountViewModel> Discounts { get; } = new ObservableCollection<DiscountViewModel>();

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
            IBookDiscountService discountBookService)
        {
            _bookDiscountService = discountBookService;

            GoToPreviousViewCommand = new RelayCommand((object? s) => navigationService.Navigate());
            DeleteDiscountCommand = new DeleteDiscountCommand(this, discountBookService);

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
                    var discounts = (await _bookDiscountService.GetAllDiscountsAsync()).Select(d => DiscountMapper.ToViewModel(d));
                    Discounts.Clear();
                    foreach (var discount in discounts)
                    {
                        Discounts.Add(discount);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Failed to load discounts. {ex.Message}";
                }
                finally
                {
                    IsLoading = false;
                    OnPropertyChanged(nameof(HasDiscounts));
                }
            }
        }

        public override void Dispose()
        {
            if (DeleteDiscountCommand is IDisposable disposable)
            {
                disposable.Dispose();
            }
            base.Dispose();
        }
    }
}