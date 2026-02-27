using BLL.Services.FilterBookServices;
using BookStoreUI.Commands.DashboardCommands.BookModelCommands;
using BookStoreUI.Stores;
using BookStoreUI.ViewModels.BaseViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookStoreUI.ViewModels.DashboardViewModels
{
    public class FilterBookViewModel : ViewModelsBase
    {
        private readonly IFilterBookService _filterBooksService;
        private readonly ProductsStore _productsStore;

        private string _bookName;
        public string BookName
        {
            get
            {
                return _bookName;
            }
            set
            {
                _bookName = value;
                OnPropertyChanged(nameof(BookName));
            }
        }

        private string _authorFullName;
        public string AuthorFullName
        {
            get
            {
                return _authorFullName;
            }
            set
            {
                _authorFullName = value;
                OnPropertyChanged(nameof(AuthorFullName));
            }
        }

        private string _genre;
        public string Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        public ObservableCollection<string> FilterOptions { get; }

        private string _selectedFilter;
        public string SelectedFilter
        {
            get
            {
                return _selectedFilter;
            }
            set
            {
                if (_selectedFilter != value)
                {
                    _selectedFilter = value;
                    OnPropertyChanged(nameof(SelectedFilter));
                }
            }
        }

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
                    OnPropertyChanged(nameof(SelectedPeriodFilter));
                }
            }
        }

        public bool CanFilterBooks =>
            !string.IsNullOrWhiteSpace(BookName) ||
            !string.IsNullOrWhiteSpace(AuthorFullName) ||
            !string.IsNullOrWhiteSpace(Genre) ||
            !string.IsNullOrEmpty(SelectedFilter) ||
            !string.IsNullOrEmpty(SelectedPeriodFilter);

        public ICommand FilterBooksCommand { get; }

        public FilterBookViewModel(IFilterBookService filterBooksService,
            ProductsStore productsStore)
        {
            _filterBooksService = filterBooksService;
            _productsStore = productsStore;

            FilterOptions = new ObservableCollection<string>()
            {
                "Default",
                "Newest",
                "Oldest"
            };
            _selectedFilter = FilterOptions[0];

            PeriodFilterOptions = new ObservableCollection<string>()
            {
                "Default",
                "Day",
                "Week",
                "Month",
                "Year"
            };
            _selectedPeriodFilter = PeriodFilterOptions[0];

            FilterBooksCommand = new FilterBooksCommand(this, _filterBooksService, productsStore);
        }

        public override void Dispose()
        {
            _ = _productsStore.RefreshAsync();
            base.Dispose();
        }
    }
}