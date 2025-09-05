using BLL.DTOs;

namespace BLL.Services.BookModelServices
{
    public interface IBookModelService
    {
        Task SetToDefault();
        Task AddBookModelAsync(ProductDTO productDTO);
        Task UpdateBookModelAsync(ProductDTO productDTO);
        Task DeleteBookModelAsync(ProductDTO productDTO);
    }
}