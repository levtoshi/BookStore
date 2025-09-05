using BLL.Services.BookStoreProviderServices;
using BookStoreUI.Models;
using BookStoreUI.ViewModelDTOMappers;
using BookStoreUI.ViewModels.CollectionViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BookStoreUI.Stores
{
    public class BookShopStore : IDisposable
    {
        private readonly IBookStoreProviderService _bookStoreProviderService;

        private readonly BookShop _bookShopObject;
        public BookShop BookShopObject
        {
            get
            {
                return _bookShopObject;
            }
        }

        public BookShopStore(IBookStoreProviderService bookStoreProviderService)
        {
            _bookStoreProviderService = bookStoreProviderService;
            _bookStoreProviderService.CollectionChanged += OnCollectionChanged;
            _bookStoreProviderService.UpdateStarted += OnCollectionUpdateStarted;

            _bookShopObject = new BookShop();
        }

        public async Task GetAsync()
        {
            //await Task.Delay(2000);
            try
            {
                BookShopObject.Products = new ObservableCollection<ProductViewModel>((await _bookStoreProviderService.GetAllProductsAsync()).Select(p => ProductMapper.ToViewModel(p)));
            }
            catch (Exception ex)
            {
                BookShopObject.ErrorMessage = $"Failed to load books. {ex.Message}";
            }

            BookShopObject.IsLoading = false;
        }

        private void OnCollectionUpdateStarted(object? sender, EventArgs e)
        {
            BookShopObject.ErrorMessage = "";
            BookShopObject.IsLoading = true;
        }

        public async void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            await GetAsync();
        }

        public void Dispose()
        {
            _bookStoreProviderService.CollectionChanged -= OnCollectionChanged;
            _bookStoreProviderService.UpdateStarted -= OnCollectionUpdateStarted;
        }
    }
}