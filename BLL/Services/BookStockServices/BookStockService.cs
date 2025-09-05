using BLL.Interfaces;
using DLL.Repositories.BookStockRepositories;

namespace BLL.Services.BookStockServices
{
    public class BookStockService : IBookStockService, ISetCollectionToDefaultService
    {
        private readonly IBookStockRepository _bookStockRepository;

        public BookStockService(IBookStockRepository bookStockRepository)
        {
            _bookStockRepository = bookStockRepository;
        }

        public async Task SetToDefault()
        {
            await _bookStockRepository.SetToDefault();
        }

        public async Task AddBookStockAsync(int productId, int amount)
        {
            await _bookStockRepository.AddBookStockAsync(productId, amount);
        }

        public async Task WriteOffBookAsync(int productId, int amount)
        {
            await _bookStockRepository.WriteOffBookAsync(productId, amount);
        }

        public async Task SellBookAsync(int productId, int amount, DateTime dateTime)
        {
            await _bookStockRepository.SellBookAsync(productId, amount, dateTime);
        }
    }
}