using DLL.Entities;
using DLL.Models;

namespace DLL.Repositories.FilterBookRepositories
{
    public interface IFilterBookRepository
    {
        Task<List<Product>> GetBooksByFilterAsync(FilterInfo filterInfo);
    }
}