using DLL.Entities;

namespace DLL.Repositories.BookDiscountRepositories
{
    public interface IBookDiscountRepository
    {
        Task<List<Product>> GetAllDiscountsAsync();
        Task AddDiscountAsync(int productId, Discount discount);
        Task RemoveDiscountAsync(int productId);
    }
}