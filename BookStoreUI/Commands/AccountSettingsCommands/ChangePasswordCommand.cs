using BLL.Services.AccountSettingsServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.ViewModelDTOMappers;
using BookStoreUI.ViewModels.AccountViewModels;
using System.Windows;

namespace BookStoreUI.Commands.AccountSettingsCommands
{
    public class ChangePasswordCommand : AsyncCommandBase, IDisposable
    {
        private readonly ChangePasswordViewModel _changePasswordViewModel;
        private readonly IMainNavigationService<AccountSettingsViewModel> _navigationService;
        private readonly IAccountSettingsService _accountSettingsService;
        private readonly CurrentUserStore _currentUserStore;

        public ChangePasswordCommand(ChangePasswordViewModel changePasswordViewModel,
            IMainNavigationService<AccountSettingsViewModel> navigationService,
            IAccountSettingsService accountSettingsService,
            CurrentUserStore currentUserStore)
        {
            _changePasswordViewModel = changePasswordViewModel;
            _changePasswordViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _navigationService = navigationService;
            _accountSettingsService = accountSettingsService;
            _currentUserStore = currentUserStore;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChangePasswordViewModel.CanChangePassword))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _currentUserStore.CurrentUser = UserMapper.ToViewModel(await _accountSettingsService.ChangePassword(UserMapper.ToDTO(_currentUserStore.CurrentUser), _changePasswordViewModel.CurrentPassword, _changePasswordViewModel.NewPassword));
                _navigationService.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _changePasswordViewModel.CanChangePassword && base.CanExecute(parameter);
        }

        public void Dispose()
        {
            _changePasswordViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}