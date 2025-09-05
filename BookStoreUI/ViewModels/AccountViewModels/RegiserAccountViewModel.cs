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
    public class RegiserAccountViewModel : ViewModelsBase, INotifyDataErrorInfo
    {
        private readonly UserDataPatternsStore _patternsStore;

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));

                ClearErrors(nameof(Name));

                if (!HasName)
                {
                    AddError("Name cannot be empty.", nameof(Name));
                }
                if (!HasNamePattern)
                {
                    AddError($"Name  {_patternsStore.NameError}", nameof(Name));
                }

                OnPropertyChanged(nameof(CanSignUp));
            }
        }

        private string _middleName;
        public string MiddleName
        {
            get
            {
                return _middleName;
            }
            set
            {
                _middleName = value;
                OnPropertyChanged(nameof(MiddleName));

                ClearErrors(nameof(MiddleName));

                if (!HasMiddleName)
                {
                    AddError("Middle name cannot be empty.", nameof(MiddleName));
                }
                if (!HasMiddleNamePattern)
                {
                    AddError($"Middle name {_patternsStore.NameError}", nameof(MiddleName));
                }

                OnPropertyChanged(nameof(CanSignUp));
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));

                ClearErrors(nameof(LastName));

                if (!HasLastName)
                {
                    AddError("Last name cannot be empty.", nameof(LastName));
                }
                if (!HasLastNamePattern)
                {
                    AddError($"Last name {_patternsStore.NameError}", nameof(LastName));
                }

                OnPropertyChanged(nameof(CanSignUp));
            }
        }

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

                OnPropertyChanged(nameof(CanSignUp));
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
                    AddError($"Password  {_patternsStore.PasswordError}", nameof(Password));
                }

                OnPropertyChanged(nameof(CanSignUp));
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

        private bool _isAdmin;
        public bool IsAdmin
        {
            get
            {
                return _isAdmin;
            }
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool CanSignUp =>
            HasName &&
            HasMiddleName &&
            HasLastName &&
            HasLogin &&
            HasPassword &&
            HasNamePattern &&
            HasMiddleNamePattern &&
            HasLastNamePattern &&
            HasLoginPattern &&
            HasPasswordPattern &&
            !HasErrors;

        public bool HasName => !String.IsNullOrWhiteSpace(Name);
        public bool HasMiddleName => !String.IsNullOrWhiteSpace(MiddleName);
        public bool HasLastName => !String.IsNullOrWhiteSpace(LastName);
        public bool HasLogin => !String.IsNullOrWhiteSpace(Login);
        public bool HasPassword => !String.IsNullOrWhiteSpace(Password);


        public bool HasNamePattern => Regex.IsMatch(Name, _patternsStore.NamePattern);
        public bool HasMiddleNamePattern => Regex.IsMatch(MiddleName, _patternsStore.NamePattern);
        public bool HasLastNamePattern => Regex.IsMatch(LastName, _patternsStore.NamePattern);
        public bool HasLoginPattern => Regex.IsMatch(Login, _patternsStore.LoginPattern);
        public bool HasPasswordPattern => Regex.IsMatch(Password, _patternsStore.PasswordPattern);


        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public ICommand PasswordToVisibilityCommand { get; }
        public ICommand SubmitCommand { get; }
        public ICommand NavigateToSignInCommand { get; }

        public RegiserAccountViewModel(IMainNavigationService<LoginViewModel> navigateToLoginViewModel,
            IMainNavigationService<DashboardViewModel> navigateToDashboardViewModel,
            IAccountSettingsService accountSettingsService,
            CurrentUserStore currentUserStore,
            UserDataPatternsStore userDataPatternsStore)
        {
            IsPasswordShown = false;
            IsAdmin = false;
            _patternsStore = userDataPatternsStore;

            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();

            PasswordToVisibilityCommand = new RelayCommand(
                (object? s) => IsPasswordShown = !IsPasswordShown);

            SubmitCommand = new SignUpCommand(this, navigateToDashboardViewModel, accountSettingsService, currentUserStore);

            NavigateToSignInCommand = new RelayCommand(
                (object? s) => navigateToLoginViewModel.Navigate());
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