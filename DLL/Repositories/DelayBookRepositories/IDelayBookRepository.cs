using DLL.Entities;

namespace DLL.Repositories.DelayBookRepositories
{
    public interface IDelayBookRepository
    {
        Task<List<Product>> GetAllDelaysAsync();
        Task AddDelayAsync(int productId, Delay delay);
        Task RemoveDelayAsync(int productId);
    }
}