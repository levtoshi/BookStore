using BLL.Services.AccountSettingsServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Stores;
using BookStoreUI.ViewModelDTOMappers;
using BookStoreUI.ViewModels.AccountViewModels;
using System.Windows;

namespace BookStoreUI.Commands.AccountSettingsCommands
{
    public class ChangeLoginCommand : AsyncCommandBase, IDisposable
    {
        private readonly AccountSettingsViewModel _accountSettingsViewModel;
        private readonly IAccountSettingsService _accountSettingsService;
        private readonly CurrentUserStore _currentUserStore;

        public ChangeLoginCommand(AccountSettingsViewModel accountSettingsViewModel,
            IAccountSettingsService accountSettingsService,
            CurrentUserStore currentUserStore)
        {
            _accountSettingsViewModel = accountSettingsViewModel;
            _accountSettingsViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _accountSettingsService = accountSettingsService;
            _accountSettingsService = accountSettingsService;
            _currentUserStore = currentUserStore;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AccountSettingsViewModel.CanChangeLogin))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _currentUserStore.CurrentUser = UserMapper.ToViewModel(await _accountSettingsService.ChangeLogin(UserMapper.ToDTO(_currentUserStore.CurrentUser), _accountSettingsViewModel.NewLogin));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _accountSettingsViewModel.CanChangeLogin && base.CanExecute(parameter);
        }

        public void Dispose()
        {
            _accountSettingsViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}