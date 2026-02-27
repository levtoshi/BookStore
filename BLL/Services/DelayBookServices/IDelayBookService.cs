using BLL.DTOs;

namespace BLL.Services.DelayBookServices
{
    public interface IDelayBookService
    {
        Task<List<ProductDTO>> GetAllDelaysAsync();
        Task AddDelayAsync(int productId, DelayDTO delayDTO);
        Task RemoveDelayAsync(int productId);
    }
}