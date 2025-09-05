using BLL.Services.AccountSettingsServices;
using BookStoreUI.Commands.AccountSettingsCommands;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.Stores.PatternStores;
using BookStoreUI.ViewModels.BaseViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.AccountViewModels
{
    public class LoginViewModel : ViewModelsBase, INotifyDataErrorInfo
    {
        private readonly UserDataPatternsStore _patternsStore;

        private string _login;
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));

                ClearErrors(nameof(Login));

                if (!HasLogin)
                {
                    AddError("Login cannot be empty.", nameof(Login));
                }
                if (!HasLoginPattern)
                {
                    AddError($"Login {_patternsStore.LoginError}", nameof(Login));
                }

                OnPropertyChanged(nameof(CanSignIn));
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));

                ClearErrors(nameof(Password));

                if (!HasPassword)
                {
                    AddError("Password cannot be empty.", nameof(Password));
                }
                if (!HasPasswordPattern)
                {
                    AddError($"Password {_patternsStore.PasswordError}", nameof(Password));
                }

                OnPropertyChanged(nameof(CanSignIn));
            }
        }

        private bool _isPasswordShown;
        public bool IsPasswordShown
        {
            get
            {
                return _isPasswordShown;
            }
            set
            {
                _isPasswordShown = value;
                OnPropertyChanged(nameof(IsPasswordShown));
            }
        }

        public bool CanSignIn =>
            HasLogin &&
            HasPassword &&
            HasLoginPattern &&
            HasPasswordPattern &&
            !HasErrors;

        public bool HasLogin => !String.IsNullOrWhiteSpace(Login);
        public bool HasPassword => !String.IsNullOrWhiteSpace(Password);
        public bool HasLoginPattern => Regex.IsMatch(Login, _patternsStore.LoginPattern);
        public bool HasPasswordPattern => Regex.IsMatch(Password, _patternsStore.PasswordPattern);


        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public ICommand PasswordToVisibilityCommand { get; }
        public ICommand SubmitCommand { get; }
        public ICommand NavigateToSignUpCommand { get; }

        public LoginViewModel(IMainNavigationService<RegiserAccountViewModel> navigateToRegiserAccountViewModel,
            IMainNavigationService<DashboardViewModel> navigateToDashboardViewModel,
            IAccountSettingsService accountSettingsService,
            CurrentUserStore currentUserStore,
            UserDataPatternsStore userDataPatternsStore)
        {
            IsPasswordShown = false;
            _patternsStore = userDataPatternsStore;

            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();

            PasswordToVisibilityCommand = new RelayCommand(
                (object? s) => IsPasswordShown = !IsPasswordShown);

            SubmitCommand = new SignInCommand(this, navigateToDashboardViewModel, accountSettingsService, currentUserStore);

            NavigateToSignUpCommand = new RelayCommand(
                (object? s) => navigateToRegiserAccountViewModel.Navigate());
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