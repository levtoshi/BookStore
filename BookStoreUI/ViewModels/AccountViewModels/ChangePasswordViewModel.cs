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
    public class ChangePasswordViewModel : ViewModelsBase, INotifyDataErrorInfo
    {
        private readonly UserDataPatternsStore _patternsStore;

        private string _currentPassword;
        public string CurrentPassword
        {
            get
            {
                return _currentPassword;
            }
            set
            {
                _currentPassword = value;
                OnPropertyChanged(nameof(CurrentPassword));

                ClearErrors(nameof(CurrentPassword));
                ClearErrors(nameof(NewPassword));

                if (!HasCurrentPassword)
                {
                    AddError("Current password cannot be empty.", nameof(CurrentPassword));
                }
                if (!HasNewPasswordDifferentFromOld)
                {
                    AddError("Current password must be different from new.", nameof(CurrentPassword));
                }
                if (!HasCurrentPasswordPattern)
                {
                    AddError($"Current password {_patternsStore.PasswordError}.", nameof(CurrentPassword));
                }

                OnPropertyChanged(nameof(CanChangePassword));
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));

                ClearErrors(nameof(CurrentPassword));
                ClearErrors(nameof(NewPassword));
                ClearErrors(nameof(NewPasswordAgain));

                if (!HasNewPassword)
                {
                    AddError("New password cannot be empty.", nameof(NewPassword));
                }
                if (!HasNewPasswordVerified)
                {
                    AddError("New password and new password again fields must be equal.", nameof(NewPassword));
                }
                if (!HasNewPasswordDifferentFromOld)
                {
                    AddError("New password must be different from old.", nameof(NewPassword));
                }
                if (!HasNewPasswordPattern)
                {
                    AddError($"New password {_patternsStore.PasswordError}.", nameof(NewPassword));
                }

                OnPropertyChanged(nameof(CanChangePassword));
            }
        }

        private string _newPasswordAgain;
        public string NewPasswordAgain
        {
            get
            {
                return _newPasswordAgain;
            }
            set
            {
                _newPasswordAgain = value;
                OnPropertyChanged(nameof(NewPasswordAgain));

                ClearErrors(nameof(NewPassword));
                ClearErrors(nameof(NewPasswordAgain));

                if (!HasNewPasswordAgain)
                {
                    AddError("New password again cannot be empty.", nameof(NewPasswordAgain));
                }
                if (!HasNewPasswordVerified)
                {
                    AddError("New password again and new password fields must be equal.", nameof(NewPasswordAgain));
                }
                if (!HasNewPasswordAgainPattern)
                {
                    AddError($"New password again {_patternsStore.PasswordError}.", nameof(NewPasswordAgain));
                }

                OnPropertyChanged(nameof(CanChangePassword));
            }
        }

        private bool _isCurrentPasswordShown;
        public bool IsCurrentPasswordShown
        {
            get
            {
                return _isCurrentPasswordShown;
            }
            set
            {
                _isCurrentPasswordShown = value;
                OnPropertyChanged(nameof(IsCurrentPasswordShown));
            }
        }

        private bool _isNewPasswordShown;
        public bool IsNewPasswordShown
        {
            get
            {
                return _isNewPasswordShown;
            }
            set
            {
                _isNewPasswordShown = value;
                OnPropertyChanged(nameof(IsNewPasswordShown));
            }
        }

        private bool _isNewPasswordAgainShown;
        public bool IsNewPasswordAgainShown
        {
            get
            {
                return _isNewPasswordAgainShown;
            }
            set
            {
                _isNewPasswordAgainShown = value;
                OnPropertyChanged(nameof(IsNewPasswordAgainShown));
            }
        }

        public bool CanChangePassword =>
            HasCurrentPassword &&
            HasNewPassword &&
            HasNewPasswordAgain &&
            HasNewPasswordVerified &&
            HasNewPasswordDifferentFromOld &&

            HasCurrentPasswordPattern &&
            HasNewPasswordPattern &&
            HasNewPasswordAgainPattern &&

            !HasErrors;

        public bool HasCurrentPassword => !String.IsNullOrWhiteSpace(CurrentPassword);
        public bool HasNewPassword => !String.IsNullOrWhiteSpace(NewPassword);
        public bool HasNewPasswordAgain => !String.IsNullOrWhiteSpace(NewPasswordAgain);
        public bool HasNewPasswordVerified => NewPassword == NewPasswordAgain;
        public bool HasNewPasswordDifferentFromOld => NewPassword != CurrentPassword;

        public bool HasCurrentPasswordPattern => Regex.IsMatch(CurrentPassword, _patternsStore.PasswordPattern);
        public bool HasNewPasswordPattern => Regex.IsMatch(NewPassword, _patternsStore.PasswordPattern);
        public bool HasNewPasswordAgainPattern => Regex.IsMatch(NewPasswordAgain, _patternsStore.PasswordPattern);


        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public ICommand CurrentPasswordToVisibilityCommand { get; }
        public ICommand NewPasswordToVisibilityCommand { get; }
        public ICommand NewPasswordAgainToVisibilityCommand { get; }

        public ICommand GoToPreviousViewCommand { get; }
        public ICommand ChangePasswordCommand { get; }

        public ChangePasswordViewModel(IMainNavigationService<AccountSettingsViewModel> navigationService,
            IAccountSettingsService accountSettingsService,
            CurrentUserStore currentUserStore,
            UserDataPatternsStore userDataPatternsStore)
        {
            IsCurrentPasswordShown = false;
            IsNewPasswordShown = false;
            IsNewPasswordAgainShown = false;

            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();

            _patternsStore = userDataPatternsStore;

            GoToPreviousViewCommand = new RelayCommand(
                (object? s) => navigationService.Navigate());

            CurrentPasswordToVisibilityCommand = new RelayCommand(
                (object? s) => IsCurrentPasswordShown = !IsCurrentPasswordShown);

            NewPasswordToVisibilityCommand = new RelayCommand(
                (object? s) => IsNewPasswordShown = !IsNewPasswordShown);

            NewPasswordAgainToVisibilityCommand = new RelayCommand(
                (object? s) => IsNewPasswordAgainShown = !IsNewPasswordAgainShown);

            ChangePasswordCommand = new ChangePasswordCommand(this, navigationService, accountSettingsService, currentUserStore);
        }

        public IEnumerable GetErrors(string? propertyName)
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
            if (ChangePasswordCommand is IDisposable disposable)
            {
                disposable.Dispose();
            }

            base.Dispose();
        }
    }
}