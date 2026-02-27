using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories.DelayBookRepositories
{
    public class DelayBookRepository : IDelayBookRepository
    {
        private readonly BookStoreContext _bookStoreContext;

        public DelayBookRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
        }

        public async Task<List<Product>> GetAllDelaysAsync()
        {
            return await _bookStoreContext.Products
                .AsNoTracking()
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Author)
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Producer)
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Genre)
                    .Include(p => p.DelayedForCustomer)
                        .ThenInclude(d => d.Customer)
                            .ThenInclude(c => c.FullName)
                .Where(p => p.DelayedForCustomer != null)
                .ToListAsync();
        }

        public async Task AddDelayAsync(int productId, Delay delay)
        {
            Product? tempProduct = await _bookStoreContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (tempProduct != null)
            {
                if (tempProduct.DelayedForCustomer == null)
                {
                    tempProduct.DelayedForCustomer = new Delay();
                    tempProduct.DelayedForCustomer.Customer = new Customer();
                    tempProduct.DelayedForCustomer.Customer.FullName = new FullName();
                }
                tempProduct.DelayedForCustomer.Customer.FullName.Name = delay.Customer.FullName.Name;
                tempProduct.DelayedForCustomer.Customer.FullName.MiddleName = delay.Customer.FullName.MiddleName;
                tempProduct.DelayedForCustomer.Customer.FullName.LastName = delay.Customer.FullName.LastName;
                tempProduct.DelayedForCustomer.Customer.Email = delay.Customer.Email;
                tempProduct.DelayedForCustomer.Amount = delay.Amount;
                await _bookStoreContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no such product in database!");
            }
        }

        public async Task RemoveDelayAsync(int productId)
        {
            Product? tempProduct = await _bookStoreContext.Products
                .Include(p => p.DelayedForCustomer)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (tempProduct != null)
            {
                tempProduct.DelayedForCustomer = null;
                await _bookStoreContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no such product in database!");
            }
        }
    }
}