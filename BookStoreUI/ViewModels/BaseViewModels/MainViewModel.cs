using BookStoreUI.Navigation.Stores;

namespace BookStoreUI.ViewModels.BaseViewModels
{
    public class MainViewModel : ViewModelsBase
    {
        private readonly MainNavigationStore _navigationStore;

        private ViewModelsBase _currentViewModel => _navigationStore.CurrentViewModel;
        public ViewModelsBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
        }

        public MainViewModel(MainNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            if (_navigationStore.CurrentViewModel != null)
            {
                _navigationStore.CurrentViewModel.Dispose();
            }
            _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
            base.Dispose();
        }
    }
}