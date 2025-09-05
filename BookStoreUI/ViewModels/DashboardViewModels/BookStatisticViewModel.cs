using BLL.Services.BookStatisticServices;
using BookStoreUI.ViewModels.BaseViewModels;
using System.Collections.ObjectModel;

namespace BookStoreUI.ViewModels.DashboardViewModels
{
    public class BookStatisticViewModel : RightPanelBase
    {

        private readonly IBookStatisticService _bookStatisticService;
        public ObservableCollection<string> PeriodFilterOptions { get; }

        private string _selectedPeriodFilter;
        public string SelectedPeriodFilter
        {
            get
            {
                return _selectedPeriodFilter;
            }
            set
            {
                if (_selectedPeriodFilter != value)
                {
                    _selectedPeriodFilter = value;

                    _ = SetStatisticFilterAsync(PeriodFilterOptions.IndexOf(_selectedPeriodFilter));

                    OnPropertyChanged(nameof(SelectedPeriodFilter));
                }
            }
        }

        private async Task SetStatisticFilterAsync(int periodFilterID)
        {
            await _bookStatisticService.SetBookStatisticFilterAsync(periodFilterID);
        }

        public BookStatisticViewModel(IBookStatisticService bookStatisticService)
        {
            _bookStatisticService = bookStatisticService;
            PeriodFilterOptions = new ObservableCollection<string>()
            {
                "Default",
                "Day",
                "Week",
                "Month",
                "Year"
            };
            _selectedPeriodFilter = PeriodFilterOptions[0];
        }

        public override async Task SetCollectionToDefault()
        {
            await _bookStatisticService.SetToDefault();
        }
    }
}