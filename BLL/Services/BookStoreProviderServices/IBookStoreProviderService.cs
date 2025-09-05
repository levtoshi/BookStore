using BLL.DTOs;
using System.Collections.Specialized;

namespace BLL.Services.BookStoreProviderServices
{
    public interface IBookStoreProviderService
    {
        event NotifyCollectionChangedEventHandler? CollectionChanged;
        event EventHandler? UpdateStarted;
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
    }
}