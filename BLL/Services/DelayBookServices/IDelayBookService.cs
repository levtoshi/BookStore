using BLL.DTOs;

namespace BLL.Services.DelayBookServices
{
    public interface IDelayBookService
    {
        Task SetToDefault();
        Task AddDelayAsync(int productId, DelayDTO delay);
        Task RemoveDelayAsync(int productId);
    }
}