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
    public class AccountSettingsViewModel : ViewModelsBase, INotifyDataErrorInfo
    {
        private readonly UserDataPatternsStore _patternsStore;
        private readonly CurrentUserStore _currentUserStore;
        
        private string _login => _currentUserStore.CurrentUser.Login;
        public string Login
        {
            get
            {
                return _login;
            }
        }

        private string _newLogin;
        public string NewLogin
        {
            get
            {
                return _newLogin;
            }
            set
            {
                _newLogin = value;
                OnPropertyChanged(nameof(NewLogin));

                ClearErrors(nameof(NewLogin));

                if (!HasNewLogin)
                {
                    AddError("New login cannot be empty.", nameof(NewLogin));
                }
                if (!HasNewLoginPattern)
                {
                    AddError($"New login  {_patternsStore.LoginError}.", nameof(NewLogin));
                }
                if (!HasNewLoginDifferentFromOld)
                {
                    AddError("New login must be different from old.", nameof(NewLogin));
                }

                OnPropertyChanged(nameof(CanChangeLogin));
            }
        }

        public bool IsAdmin { get; }
        public string Name { get; }
        public string MiddleName { get; }
        public string LastName { get; }

        public bool CanChangeLogin =>
            HasNewLogin &&
            HasNewLoginPattern &&
            HasNewLoginDifferentFromOld &&
            !HasErrors;

        public bool HasNewLogin => !String.IsNullOrWhiteSpace(NewLogin);
        public bool HasNewLoginPattern => Regex.IsMatch(NewLogin, _patternsStore.LoginPattern);
        public bool HasNewLoginDifferentFromOld => NewLogin != Login;


        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public ICommand GoToPreviousViewCommand { get; }
        public ICommand ChangeLoginCommand { get; }
        public ICommand ChangePasswordCommand { get; }

        public AccountSettingsViewModel(IMainNavigationService<DashboardViewModel> navigateToDashboardViewModel,
            IMainNavigationService<ChangePasswordViewModel> navigateToChangePasswordViewModel,
            IAccountSettingsService accountSettingsService,
            CurrentUserStore currentUserStore,
            UserDataPatternsStore userDataPatternsStore)
        {
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();

            _currentUserStore = currentUserStore;
            _currentUserStore.PropertyChanged += OnCurrentUserViewModelPropertyChanged;

            _patternsStore = userDataPatternsStore;

            GoToPreviousViewCommand = new RelayCommand(
                (object? s) => navigateToDashboardViewModel.Navigate());

            ChangePasswordCommand = new RelayCommand(
                (object? s) => navigateToChangePasswordViewModel.Navigate());

            ChangeLoginCommand = new ChangeLoginCommand(this, accountSettingsService,  currentUserStore);

            IsAdmin = currentUserStore.CurrentUser.IsAdmin;
            Name = currentUserStore.CurrentUser.Name;
            MiddleName = currentUserStore.CurrentUser.MiddleName;
            LastName = currentUserStore.CurrentUser.LastName;
        }

        private void OnCurrentUserViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentUserStore.CurrentUser))
            {
                OnPropertyChanged(nameof(Login));
            }
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
            if (ChangeLoginCommand is IDisposable disposable)
            {
                disposable.Dispose();
            }
            _currentUserStore.CurrentUser.PropertyChanged -= OnCurrentUserViewModelPropertyChanged;

            base.Dispose();
        }
    }
}