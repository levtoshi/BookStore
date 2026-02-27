using BookStoreUI.Navigation.Stores;
using BookStoreUI.ViewModels.BaseViewModels;

namespace BookStoreUI.Navigation.Services.DashboardNavigationServices
{
    public class DashboardNavigationService<TViewModel> : IDashboardNavigationService<TViewModel> where TViewModel : ViewModelsBase
    {
        private readonly DashboardNavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public DashboardNavigationService(DashboardNavigationStore navigationStore,
            Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            if (_navigationStore.CurrentRightPanel != null)
            {
                _navigationStore.CurrentRightPanel.Dispose();
            }
            _navigationStore.CurrentRightPanel = _createViewModel();
        }
    }
}