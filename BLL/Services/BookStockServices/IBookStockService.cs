namespace BLL.Services.BookStockServices
{
    public interface IBookStockService
    {
        Task SetToDefault();
        Task AddBookStockAsync(int productId, int amount);
        Task WriteOffBookAsync(int productId, int amount);
        Task SellBookAsync(int productId, int amount, DateTime dateTime);
    }
}