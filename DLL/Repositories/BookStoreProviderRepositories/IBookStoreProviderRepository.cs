using DLL.Entities;
using System.Collections.Specialized;

namespace DLL.Repositories.BookStoreProviderRepositories
{
    public interface IBookStoreProviderRepository
    {
        event NotifyCollectionChangedEventHandler? CollectionChanged;
        event EventHandler? UpdateStarted;
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}