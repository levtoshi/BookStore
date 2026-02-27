using BLL.DTOs;

namespace BLL.Services.BookServices
{
    public interface IBookService
    {
        Task<List<ProductDTO>> GetAllBooksAsync();
        Task<ProductDTO> AddBookModelAsync(ProductDTO product);
        Task<ProductDTO> UpdateBookModelAsync(ProductDTO product);
        Task DeleteBookModelAsync(ProductDTO product);
        Task AddBookStockAsync(int productId, int amount);
        Task WriteOffBookAsync(int productId, int amount);
        Task SellBookAsync(int productId, int amount, DateTime dateTime);
    }
}