using DLL.Entities;
using DLL.Stores;
using System.Collections.Specialized;

namespace DLL.Repositories.BookStoreProviderRepositories
{
    public class BookStoreProviderRepository : IBookStoreProviderRepository, INotifyCollectionChanged, IDisposable
    {
        private readonly ProductsStore _productsStore;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event EventHandler? UpdateStarted;

        public BookStoreProviderRepository(ProductsStore productsStore)
        {
            _productsStore = productsStore;
            _productsStore.Products.CollectionChanged += OnCollectionChanged;
            _productsStore.Products.UpdateStarted += OnCollectionUpdateStarted;
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(sender, e);
        }

        private void OnCollectionUpdateStarted(object? sender, EventArgs e)
        {
            UpdateStarted?.Invoke(sender, e);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await Task.Run(() => _productsStore.Products);
        }

        public void Dispose()
        {
            _productsStore.Products.CollectionChanged -= OnCollectionChanged;
            _productsStore.Products.UpdateStarted -= OnCollectionUpdateStarted;
        }
    }
}