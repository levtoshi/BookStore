using DLL.Entities;

namespace DLL.Repositories.BookRepositories
{
    public interface IBookRepository
    {
        Task<List<Product>> GetAllBooksAsync();
        Task<Product> AddBookModelAsync(Product product);
        Task<Product> UpdateBookModelAsync(Product product);
        Task DeleteBookModelAsync(Product product);
        Task AddBookStockAsync(int productId, int amount);
        Task WriteOffBookAsync(int productId, int amount);
        Task SellBookAsync(int productId, int amount, DateTime dateTime);
    }
}