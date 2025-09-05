using BLL.Services.SearchBookServices;
using BookStoreUI.Enums;
using BookStoreUI.ViewModels.BaseViewModels;
using System.Collections.ObjectModel;

namespace BookStoreUI.ViewModels.DashboardViewModels
{
    public class SearchBookViewModel : RightPanelBase
    {
        private readonly ISearchBookService _searchBookService;

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

                _ = SetSeachFilterAsync(_bookName, ((int)(SearchFiltersEnum.BookName)));

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

                _ = SetSeachFilterAsync(_authorFullName, ((int)(SearchFiltersEnum.AuthorFullName)));

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

                _ = SetSeachFilterAsync(_genre, ((int)(SearchFiltersEnum.Genre)));

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

                    _ = SetSeachFilterAsync(_selectedFilter, ((int)(SearchFiltersEnum.Order)));

                    OnPropertyChanged(nameof(SelectedFilter));
                }
            }
        }

        private async Task SetSeachFilterAsync(string value, int filter)
        {
            await _searchBookService.SetBookSearchFilterAsync(value, filter);
        }

        public SearchBookViewModel(ISearchBookService searchBookService)
        {
            _searchBookService = searchBookService;

            FilterOptions = new ObservableCollection<string>()
            {
                "Default",
                "Newest",
                "Oldest"
            };
            _selectedFilter = FilterOptions[0];
        }

        public override async Task SetCollectionToDefault()
        {
            await _searchBookService.SetToDefault();
        }
    }
}