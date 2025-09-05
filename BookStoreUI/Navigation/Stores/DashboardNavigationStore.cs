using BookStoreUI.ViewModels.BaseViewModels;

namespace BookStoreUI.Navigation.Stores
{
    public class DashboardNavigationStore
    {
        private RightPanelBase _currentRightPanel;
        public RightPanelBase CurrentRightPanel
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