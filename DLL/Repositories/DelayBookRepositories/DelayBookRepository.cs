using DLL.Entities;
using DLL.Interfaces;
using DLL.Stores;

namespace DLL.Repositories.DelayBookRepositories
{
    public class DelayBookRepository : IDelayBookRepository, ISetCollectionToDefaultRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly ProductsStore _productsStore;

        public DelayBookRepository(BookStoreContext bookStoreContext,
            ProductsStore productsStore)
        {
            _bookStoreContext = bookStoreContext;
            _productsStore = productsStore;
        }

        public async Task SetToDefault()
        {
            _productsStore.FilterFunc = p => p.DelayedForCustomer != null;
            await _productsStore.SetToDefault();
        }

        public async Task AddDelayAsync(int productId, Delay delay)
        {
            Product? tempProduct = await _productsStore.FindElementAsync(productId);
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
        }

        public async Task RemoveDelayAsync(int productId)
        {
            Product? tempProduct = await _productsStore.FindElementAsync(productId);
            if (tempProduct != null)
            {
                tempProduct.DelayedForCustomer = null;
                await _bookStoreContext.SaveChangesAsync();
            }
        }
    }
}