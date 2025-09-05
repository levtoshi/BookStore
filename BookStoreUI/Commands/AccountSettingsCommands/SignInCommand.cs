using BLL.DTOs;
using BLL.Services.AccountSettingsServices;
using BookStoreUI.Commands.BaseCommands;
using BookStoreUI.Navigation.Services.MainNavigationServices;
using BookStoreUI.Stores;
using BookStoreUI.ViewModelDTOMappers;
using BookStoreUI.ViewModels.AccountViewModels;
using BookStoreUI.ViewModels.DashboardViewModels;
using System.ComponentModel;
using System.Windows;

namespace BookStoreUI.Commands.AccountSettingsCommands
{
    public class SignInCommand : AsyncCommandBase, IDisposable
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IMainNavigationService<DashboardViewModel> _navigationService;
        private readonly IAccountSettingsService _accountSettingsService;
        private readonly CurrentUserStore _currentUserStore;

        public SignInCommand(LoginViewModel loginViewModel,
            IMainNavigationService<DashboardViewModel> navigationService,
            IAccountSettingsService accountSettingsService,
            CurrentUserStore currentUserStore)
        {
            _loginViewModel = loginViewModel;
            _loginViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _navigationService = navigationService;
            _accountSettingsService = accountSettingsService;
            _currentUserStore = currentUserStore;

        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.CanSignIn))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _currentUserStore.CurrentUser = UserMapper.ToViewModel(await _accountSettingsService.SignIn(new UserSignInInfoDTO()
                {
                    Login = _loginViewModel.Login,
                    Password = _loginViewModel.Password
                }));
                _navigationService.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _loginViewModel.CanSignIn && base.CanExecute(parameter);
        }

        public void Dispose()
        {
            _loginViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
    }
}