using BookStoreUI.ViewModels.BaseViewModels;
namespace BookStoreUI.Navigation.Stores
{
    public class MainNavigationStore
    {
        private ViewModelsBase _currentViewModel;
        public ViewModelsBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action? CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}