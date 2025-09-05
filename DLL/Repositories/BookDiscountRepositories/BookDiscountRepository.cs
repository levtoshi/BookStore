using DLL.Entities;
using DLL.Interfaces;
using DLL.Stores;

namespace DLL.Repositories.BookDiscountRepositories
{
    public class BookDiscountRepository : IBookDiscountRepository, ISetCollectionToDefaultRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly ProductsStore _productsStore;

        public BookDiscountRepository(BookStoreContext bookStoreContext,
            ProductsStore productsStore)
        {
            _bookStoreContext = bookStoreContext;

            _productsStore = productsStore;
        }

        public async Task SetToDefault()
        {
            _productsStore.FilterFunc = p => p.Discount != null;
            await _productsStore.SetToDefault();
        }

        public async Task AddDiscountAsync(int productId, Discount discount)
        {
            Product? tempProduct = await _productsStore.FindElementAsync(productId);
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
        }

        public async Task RemoveDiscountAsync(int productId)
        {
            Product? tempProduct = await _productsStore.FindElementAsync(productId);
            if (tempProduct != null)
            {
                tempProduct.Discount = null;
                await _bookStoreContext.SaveChangesAsync();
            }
        }
    }
}