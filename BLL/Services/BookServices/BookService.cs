using BLL.DTOEntityMappers;
using BLL.DTOs;
using DLL.Entities;
using DLL.Repositories.BookRepositories;

namespace BLL.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<ProductDTO>> GetAllBooksAsync()
        {
            return (await _bookRepository.GetAllBooksAsync()).Select(p => ProductMapper.ToDTO(p)).ToList();
        }

        public async Task<ProductDTO> AddBookModelAsync(ProductDTO productDTO)
        {
            return ProductMapper.ToDTO(await _bookRepository.AddBookModelAsync(ProductMapper.ToEntity(productDTO)));
        }

        public async Task<ProductDTO> UpdateBookModelAsync(ProductDTO productDTO)
        {
            return ProductMapper.ToDTO(await _bookRepository.UpdateBookModelAsync(ProductMapper.ToEntity(productDTO)));
        }

        public async Task DeleteBookModelAsync(ProductDTO productDTO)
        {
            await _bookRepository.DeleteBookModelAsync(ProductMapper.ToEntity(productDTO));
        }

        public async Task AddBookStockAsync(int productId, int amount)
        {
            await _bookRepository.AddBookStockAsync(productId, amount);
        }

        public async Task WriteOffBookAsync(int productId, int amount)
        {
            await _bookRepository.WriteOffBookAsync(productId, amount);
        }

        public async Task SellBookAsync(int productId, int amount, DateTime dateTime)
        {
            await _bookRepository.SellBookAsync(productId, amount, dateTime);
        }
    }
}