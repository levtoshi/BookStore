using DLL.Entities;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories.FilterBookRepositories
{
    public class FilterBookRepository : IFilterBookRepository
    {
        private readonly BookStoreContext _bookStoreContext;

        public FilterBookRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
        }

        public async Task<List<Product>> GetBooksByFilterAsync(FilterInfo filterInfo)
        {
            var query = _bookStoreContext.Products
                .AsNoTracking()
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Author)
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Producer)
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Genre)
                .Where(p =>
                    (string.IsNullOrEmpty(filterInfo.Name) ||
                        p.Book.Name.Contains(filterInfo.Name)) &&

                    (string.IsNullOrEmpty(filterInfo.Author) ||
                        (p.Book.Author.Name + " " +
                         p.Book.Author.MiddleName + " " +
                         p.Book.Author.LastName)
                        .Contains(filterInfo.Author)) &&

                    (string.IsNullOrEmpty(filterInfo.Genre) ||
                        p.Book.Genre.Name.Contains(filterInfo.Genre))
                );

            if (filterInfo.Period != "Default")
            {
                DateTime fromDate = filterInfo.Period switch
                {
                    "Day" => DateTime.Today,
                    "Week" => DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek),
                    "Month" => new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
                    "Year" => new DateTime(DateTime.Today.Year, 1, 1),
                    _ => DateTime.MinValue
                };

                var salesQuery = _bookStoreContext.Sales
                    .AsNoTracking()
                    .Where(s => s.SoldTime >= fromDate)
                    .GroupBy(s => s.Product.Id)
                    .Select(g => new
                    {
                        ProductId = g.Key,
                        Count = g.Count()
                    });

                query = salesQuery
                    .Join(query,
                          x => x.ProductId,
                          p => p.Id,
                          (x, p) => new
                          {
                              Product = p,
                              SalesCount = x.Count
                          })
                    .OrderByDescending(x => x.SalesCount)
                    .Select(x => x.Product);
            }

            query = filterInfo.Order switch
            {
                "Newest" => query.OrderByDescending(p => p.Book.Year),
                "Oldest" => query.OrderBy(p => p.Book.Year),
                _ => query.OrderBy(p => p.Id)
            };

            return await query.ToListAsync();
        }
    }
}