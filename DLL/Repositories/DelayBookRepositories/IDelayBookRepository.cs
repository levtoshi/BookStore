using DLL.Entities;

namespace DLL.Repositories.DelayBookRepositories
{
    public interface IDelayBookRepository
    {
        Task SetToDefault();
        Task AddDelayAsync(int productId, Delay delay);
        Task RemoveDelayAsync(int productId);
    }
}