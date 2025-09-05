using BookStoreUI.Navigation.Stores;
using BookStoreUI.ViewModels.BaseViewModels;

namespace BookStoreUI.Navigation.Services.MainNavigationServices
{
    public class MainNavigationService<TViewModel> : IMainNavigationService<TViewModel> where TViewModel : ViewModelsBase
    {
        private readonly MainNavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public MainNavigationService(MainNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            if (_navigationStore.CurrentViewModel != null)
            {
                _navigationStore.CurrentViewModel.Dispose();
            }
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}