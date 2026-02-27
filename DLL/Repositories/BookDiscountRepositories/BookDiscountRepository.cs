using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories.BookDiscountRepositories
{
    public class BookDiscountRepository : IBookDiscountRepository
    {
        private readonly BookStoreContext _bookStoreContext;

        public BookDiscountRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
        }

        public async Task<List<Product>> GetAllDiscountsAsync()
        {
            return await _bookStoreContext.Products
                .AsNoTracking()
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Author)
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Producer)
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Genre)
                    .Include(p => p.Discount)
                .Where(p => p.Discount != null)
                .ToListAsync();
        }

        public async Task AddDiscountAsync(int productId, Discount discount)
        {
            Product? tempProduct = await _bookStoreContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (tempProduct != null)
            {
                if (tempProduct.Discount == null)
                {
                    tempProduct.Discount = new Discount();
                }
                tempProduct.Discount.Name = discount.Name;
                tempProduct.Discount.Interest = discount.Interest;
                tempProduct.Discount.StartDate = discount.StartDate;
                tempProduct.Discount.EndDate = discount.EndDate;
                await _bookStoreContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no such product in database!");
            }
        }

        public async Task RemoveDiscountAsync(int productId)
        {
            Product? tempProduct = await _bookStoreContext.Products
                .Include(p => p.Discount)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (tempProduct != null)
            {
                tempProduct.Discount = null;
                await _bookStoreContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no such product in database!");
            }
        }
    }
}