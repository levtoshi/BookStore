using BLL.DTOs;

namespace BLL.Services.BookDiscountServices
{
    public interface IBookDiscountService
    {
        Task SetToDefault();
        Task AddDiscountAsync(int productId, DiscountDTO discount);
        Task RemoveDiscountAsync(int productId);
    }
}