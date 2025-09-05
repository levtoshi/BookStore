using BookStoreUI.ViewModels.BaseViewModels;

namespace BookStoreUI.Navigation.Services.MainNavigationServices
{
    public interface IMainNavigationService<TViewModel> where TViewModel : ViewModelsBase
    {
        void Navigate();
    }
}