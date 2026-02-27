using BLL.DTOs;

namespace BLL.Services.FilterBookServices
{
    public interface IFilterBookService
    {
        Task<List<ProductDTO>> GetBooksByFilterAsync(FilterInfoDTO filterInfo);
    }
}