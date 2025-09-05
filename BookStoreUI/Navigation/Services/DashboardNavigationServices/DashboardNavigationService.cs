using BookStoreUI.Interfaces;
using BookStoreUI.Navigation.Stores;
using BookStoreUI.ViewModels.BaseViewModels;

namespace BookStoreUI.Navigation.Services.DashboardNavigationServices
{
    public class DashboardNavigationService<TViewModel> : IDashboardNavigationService<TViewModel> where TViewModel : RightPanelBase
    {
        private readonly DashboardNavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public DashboardNavigationService(DashboardNavigationStore navigationStore,
            Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public async Task Navigate()
        {
            if (_navigationStore.CurrentRightPanel != null)
            {
                _navigationStore.CurrentRightPanel.Dispose();
            }
            _navigationStore.CurrentRightPanel = _createViewModel();
            await _navigationStore.CurrentRightPanel.SetCollectionToDefault();
            if (_navigationStore.CurrentRightPanel is ISubscribable subscribable)
            {
                subscribable.SubscribeToEvents();
            }
        }
    }
}