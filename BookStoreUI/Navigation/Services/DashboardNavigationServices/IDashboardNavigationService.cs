using BookStoreUI.ViewModels.BaseViewModels;

namespace BookStoreUI.Navigation.Services.DashboardNavigationServices
{
    public interface IDashboardNavigationService<TViewModel> where TViewModel : RightPanelBase
    {
        Task Navigate();
    }
}