using BLL.DTOs;
using DLL.Entities;
using DLL.Models;

namespace BLL.Services.FilterBookServices
{
    public interface IFilterBookService
    {
        Task<List<ProductDTO>> GetBooksByFilterAsync(FilterInfoDTO filterInfo);
    }
}