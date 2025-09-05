using BLL.DTOEntityMappers;
using BLL.DTOs;
using DLL.Repositories.BookStoreProviderRepositories;
using System.Collections.Specialized;

namespace BLL.Services.BookStoreProviderServices
{
    public class BookStoreProviderService : IBookStoreProviderService, INotifyCollectionChanged, IDisposable
    {
        private readonly IBookStoreProviderRepository _bookStoreProviderRepository;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event EventHandler? UpdateStarted;

        public BookStoreProviderService(IBookStoreProviderRepository bookStoreProviderRepository)
        {
            _bookStoreProviderRepository = bookStoreProviderRepository;
            _bookStoreProviderRepository.CollectionChanged += OnCollectionChanged;
            _bookStoreProviderRepository.UpdateStarted += OnCollectionUpdateStarted;
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(sender, e);
        }

        private void OnCollectionUpdateStarted(object? sender, EventArgs e)
        {
            UpdateStarted?.Invoke(sender, e);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            return (await _bookStoreProviderRepository.GetAllProductsAsync()).Select(p => ProductMapper.ToDTO(p));
        }

        public void Dispose()
        {
            _bookStoreProviderRepository.CollectionChanged -= OnCollectionChanged;
            _bookStoreProviderRepository.UpdateStarted -= OnCollectionUpdateStarted;
        }
    }
}