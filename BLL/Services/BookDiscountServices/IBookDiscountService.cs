using BLL.DTOs;
using DLL.Entities;

namespace BLL.Services.BookDiscountServices
{
    public interface IBookDiscountService
    {
        Task<List<ProductDTO>> GetAllDiscountsAsync();
        Task AddDiscountAsync(int productId, DiscountDTO discountDTO);
        Task RemoveDiscountAsync(int productId);
    }
}