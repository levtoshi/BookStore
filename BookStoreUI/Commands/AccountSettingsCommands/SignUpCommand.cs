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
    public class SignUpCommand : AsyncCommandBase
    {
        private readonly RegiserAccountViewModel _regiserAccountViewModel;
        private readonly IMainNavigationService<DashboardViewModel> _navigationService;
        private readonly IAccountSettingsService _accountSettingsService;
        private readonly CurrentUserStore _currentUserStore;

        public SignUpCommand(RegiserAccountViewModel regiserAccountViewModel,
            IMainNavigationService<DashboardViewModel> navigationService,
            IAccountSettingsService accountSettingsService,
            CurrentUserStore currentUserStore)
        {
            _regiserAccountViewModel = regiserAccountViewModel;
            _regiserAccountViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _navigationService = navigationService;
            _accountSettingsService = accountSettingsService;
            _currentUserStore = currentUserStore;

        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RegiserAccountViewModel.CanSignUp))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _currentUserStore.CurrentUser = UserMapper.ToViewModel(await _accountSettingsService.SignUp(new UserDTO()
                {
                    UserFullName = new FullNameDTO()
                    {
                        Name = FirstCharUpper(_regiserAccountViewModel.Name),
                        MiddleName = FirstCharUpper(_regiserAccountViewModel.MiddleName),
                        LastName = FirstCharUpper(_regiserAccountViewModel.LastName)
                    },
                    SignInInfo = new UserSignInInfoDTO()
                    {
                        Login = _regiserAccountViewModel.Login,
                        Password = _regiserAccountViewModel.Password,
                    },
                    IsAdmin = _regiserAccountViewModel.IsAdmin
                }));
                _navigationService.Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string FirstCharUpper(string oldString)
        {
            return char.ToUpper(oldString[0]) + oldString.Substring(1).ToLower();
        }

        public override bool CanExecute(object parameter)
        {
            return _regiserAccountViewModel.CanSignUp && base.CanExecute(parameter);
        }

        public void Dispose()
        {
            _regiserAccountViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
    }
}