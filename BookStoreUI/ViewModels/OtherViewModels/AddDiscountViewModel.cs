using BLL.Services.BookDiscountServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Commands.DashboardCommands.BookModelCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.ViewModels.BaseViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.OtherViewModels
{
    public class AddDiscountViewModel : ViewModelsBase, INotifyDataErrorInfo
    {
        private string _discountName;
        public string DiscountName
        {
            get => _discountName;
            set
            {
                _discountName = value;
                OnPropertyChanged(nameof(DiscountName));

                ClearErrors(nameof(DiscountName));

                if (!HasDiscountName)
                {
                    AddError("Discount name cannot be empty.", nameof(DiscountName));
                }

                OnPropertyChanged(nameof(CanAddBookDiscount));
            }
        }

        private byte _interest;
        public byte Interest
        {
            get => _interest;
            set
            {
                _interest = value;
                OnPropertyChanged(nameof(Interest));

                ClearErrors(nameof(Interest));

                if (!HasInterestGreaterThanZero)
                {
                    AddError("Amount must be greater than zero.", nameof(Interest));
                }

                OnPropertyChanged(nameof(CanAddBookDiscount));
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (!HasStartDateBeforeEndDate)
                {
                    AddError("The start date cannot be after the end date.", nameof(StartDate));
                }

                OnPropertyChanged(nameof(CanAddBookDiscount));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (!HasStartDateBeforeEndDate)
                {
                    AddError("The end date cannot be before the start date.", nameof(EndDate));
                }

                OnPropertyChanged(nameof(CanAddBookDiscount));
            }
        }

        public bool CanAddBookDiscount =>
            HasDiscountName &&
            HasInterestGreaterThanZero &&
            HasStartDateBeforeEndDate &&
            !HasErrors;

        private bool HasDiscountName => !String.IsNullOrEmpty(DiscountName);
        private bool HasInterestGreaterThanZero => Interest > 0;
        private bool HasStartDateBeforeEndDate => StartDate < EndDate;


        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public ICommand GoToPreviousViewCommand { get; }
        public ICommand SubmitCommand { get; }

        public AddDiscountViewModel(IMainNavigationService<DashboardViewModel> navigationService,
            IBookDiscountService bookDiscountService,
            SelectedItemStore selectedItemStore)
        {
            _startDate = DateTime.Now;
            _endDate = DateTime.Now.AddDays(1);

            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();

            GoToPreviousViewCommand = new RelayCommand((object? s) => navigationService.Navigate());

            SubmitCommand = new AddDiscountCommand(this, navigationService, bookDiscountService, selectedItemStore);
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public override void Dispose()
        {
            if (SubmitCommand is IDisposable disposable)
            {
                disposable.Dispose();
            }

            base.Dispose();
        }
    }
}