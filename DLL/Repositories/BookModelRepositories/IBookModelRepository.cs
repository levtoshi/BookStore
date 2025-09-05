using DLL.Entities;

namespace DLL.Repositories.BookModelRepositories
{
    public interface IBookModelRepository
    {
        Task SetToDefault();
        Task AddBookModelAsync(Product product);
        Task UpdateBookModelAsync(Product product);
        Task DeleteBookModelAsync(Product product);
    }
}