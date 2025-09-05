using BLL.Interfaces;
using DLL.Repositories.SearchBookRepositories;

namespace BLL.Services.SearchBookServices
{
    public class SearchBookService : ISearchBookService, ISetCollectionToDefaultService
    {
        private readonly ISearchBookRepository _searchBookRepository;

        public SearchBookService(ISearchBookRepository searchBookRepository)
        {
            _searchBookRepository = searchBookRepository;
        }

        public async Task SetToDefault()
        {
            await _searchBookRepository.SetToDefault();
        }

        public async Task SetBookSearchFilterAsync(string value, int filterID)
        {
            await _searchBookRepository.SetBookSearchFilterAsync(value, filterID);
        }
    }
}