using BookStoreUI.ViewModels.BaseViewModels;

namespace BookStoreUI.Navigation.Stores
{
    public class DashboardNavigationStore
    {
        private ViewModelsBase _currentRightPanel;
        public ViewModelsBase CurrentRightPanel
        {
            get
            {
                return _currentRightPanel;
            }
            set
            {
                _currentRightPanel = value;
                OnCurrentRightPanelChanged();
            }
        }

        public event Action? CurrentRightPanelChanged;

        private void OnCurrentRightPanelChanged()
        {
            CurrentRightPanelChanged?.Invoke();
        }
    }
}