using DLL.Entities;
using DLL.Interfaces;
using DLL.Stores;
using System.Collections.ObjectModel;

namespace DLL.Repositories.SearchBookRepositories
{
    public class SearchBookRepository : ISearchBookRepository, ISetCollectionToDefaultRepository
    {
        private readonly ProductsStore _productsStore;

        private readonly ObservableCollection<string> _mergeFilters;
        private delegate int SearchOperation(Product p, string s);
        private List<Func<Product, string, int>> _searchOperationsDelegates;

        public SearchBookRepository(ProductsStore productsStore)
        {
            _productsStore = productsStore;

            _mergeFilters = new ObservableCollection<string>() { "", "", "", "" };

            _searchOperationsDelegates = new List<Func<Product, string, int>>();

            _searchOperationsDelegates.Add((Product p, string s) => p.Book.Name.ToLower().IndexOf(s));
            _searchOperationsDelegates.Add((Product p, string s) => (p.Book.Author.Name + " " + p.Book.Author.MiddleName + " " + p.Book.Author.LastName).ToLower().IndexOf(s));
            _searchOperationsDelegates.Add((Product p, string s) => p.Book.Genre.Name.ToLower().IndexOf(s));
        }

        public async Task SetToDefault()
        {
            _productsStore.FilterFunc = null;
            await _productsStore.SetToDefault();
        }

        public async Task SetBookSearchFilterAsync(string value, int filterID)
        {
            _mergeFilters[filterID] = value.ToLower();
            await MergeAsync();
        }

        private async Task MergeAsync()
        {
            await _productsStore.SetToDefault();
            IEnumerable<Product> tempProducts = _productsStore.Products;

            for (int i = 0; i < _searchOperationsDelegates.Count; ++i)
            {
                if (_mergeFilters[i] != "")
                {
                    int localIndex = i;
                    tempProducts = await Task.Run(() => tempProducts.Where(p =>
                    _searchOperationsDelegates[localIndex]?.Invoke(p, _mergeFilters[localIndex]) >= 0));
                }
            }

            if (_mergeFilters[3] == "default")
            {
                tempProducts = await Task.Run(() => tempProducts.OrderBy(p => p.Id));
            }
            else if (_mergeFilters[3] == "newest")
            {
                tempProducts = await Task.Run(() => tempProducts.OrderBy(p => p.Book.Year).Reverse());
            }
            else
            {
                tempProducts = await Task.Run(() => tempProducts.OrderBy(p => p.Book.Year));
            }

            await _productsStore.ChangeProducts(tempProducts.ToList());
        }
    }
}