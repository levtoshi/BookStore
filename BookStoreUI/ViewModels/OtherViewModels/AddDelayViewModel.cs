using BLL.Services.DelayBookServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Commands.DashboardCommands.BookStockCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.ViewModels.BaseViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.OtherViewModels
{
    public class AddDelayViewModel : ViewModelsBase, INotifyDataErrorInfo
    {
        private string _customerName;
        public string CustomerName
        {
            get => _customerName;
            set
            {
                _customerName = value;
                OnPropertyChanged(nameof(CustomerName));

                ClearErrors(nameof(CustomerName));

                if (!HasCustomerName)
                {
                    AddError("Customer name cannot be empty.", nameof(CustomerName));
                }

                OnPropertyChanged(nameof(CanDelayBook));
            }
        }

        private string _customerMiddleName;
        public string CustomerMiddleName
        {
            get => _customerMiddleName;
            set
            {
                _customerMiddleName = value;
                OnPropertyChanged(nameof(CustomerMiddleName));

                ClearErrors(nameof(CustomerMiddleName));

                if (!HasCustomerMiddleName)
                {
                    AddError("Customer middle name cannot be empty.", nameof(CustomerMiddleName));
                }

                OnPropertyChanged(nameof(CanDelayBook));
            }
        }

        private string _customerLastName;
        public string CustomerLastName
        {
            get => _customerLastName;
            set
            {
                _customerLastName = value;
                OnPropertyChanged(nameof(CustomerLastName));

                ClearErrors(nameof(CustomerLastName));

                if (!HasCustomerLastName)
                {
                    AddError("Customer name cannot be empty.", nameof(CustomerLastName));
                }

                OnPropertyChanged(nameof(CanDelayBook));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));

                ClearErrors(nameof(Email));

                if (!HasEmail)
                {
                    AddError("Email cannot be empty.", nameof(Email));
                }

                OnPropertyChanged(nameof(CanDelayBook));
            }
        }

        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));

                ClearErrors(nameof(Amount));

                if (!HasAmountGreaterThanZero)
                {
                    AddError("Amount must be greater than zero.", nameof(Amount));
                }

                OnPropertyChanged(nameof(CanDelayBook));
            }
        }

        public bool CanDelayBook =>
            HasCustomerName &&
            HasCustomerMiddleName &&
            HasCustomerLastName &&
            HasEmail &&
            HasAmountGreaterThanZero &&
            !HasErrors;

        private bool HasCustomerName => !String.IsNullOrWhiteSpace(CustomerName);
        private bool HasCustomerMiddleName => !String.IsNullOrWhiteSpace(CustomerMiddleName);
        private bool HasCustomerLastName => !String.IsNullOrWhiteSpace(CustomerLastName);
        private bool HasEmail => !String.IsNullOrWhiteSpace(Email);
        private bool HasAmountGreaterThanZero => Amount > 0;


        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public ICommand GoToPreviousViewCommand { get; }
        public ICommand SubmitCommand { get; }

        public AddDelayViewModel(IMainNavigationService<DashboardViewModel> navigationService,
            IDelayBookService delayBookService,
            SelectedItemStore selectedItemStore)
        {
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
            GoToPreviousViewCommand = new RelayCommand((object? s) => navigationService.Navigate());

            SubmitCommand = new AddDelayCommand(this, navigationService, delayBookService, selectedItemStore);
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