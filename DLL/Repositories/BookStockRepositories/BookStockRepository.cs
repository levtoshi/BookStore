using DLL.Entities;
using DLL.Interfaces;
using DLL.Stores;

namespace DLL.Repositories.BookStockRepositories
{
    public class BookStockRepository : IBookStockRepository, ISetCollectionToDefaultRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly ProductsStore _productsStore;

        public BookStockRepository(BookStoreContext bookStoreContext,
            ProductsStore productsStore)
        {
            _bookStoreContext = bookStoreContext;
            _productsStore = productsStore;
        }

        public async Task SetToDefault()
        {
            _productsStore.FilterFunc = null;
            await _productsStore.SetToDefault();
        }

        public async Task AddBookStockAsync(int productId, int amount)
        {
            Product? tempProduct = await _productsStore.FindElementAsync(productId);
            if (tempProduct != null)
            {
                tempProduct.Amount += amount;
                await _bookStoreContext.SaveChangesAsync();
            }
        }

        public async Task WriteOffBookAsync(int productId, int amount)
        {
            Product? tempProduct = await _productsStore.FindElementAsync(productId);
            if (tempProduct != null)
            {
                tempProduct.Amount -= (amount < tempProduct.Amount) ? amount : tempProduct.Amount;
                await _bookStoreContext.SaveChangesAsync();
            }
        }

        public async Task SellBookAsync(int productId, int amount, DateTime dateTime)
        {
            Product? tempProduct = await _productsStore.FindElementAsync(productId);
            if (tempProduct != null)
            {
                if (tempProduct.Amount != 0)
                {
                    int amountClear = (amount < tempProduct.Amount) ? amount : tempProduct.Amount;
                    await _bookStoreContext.Sales.AddAsync(new Sale()
                    {
                        Amount = amountClear,
                        SoldTime = dateTime,
                        Product = tempProduct
                    });
                    tempProduct.Amount -= amountClear;
                    await _bookStoreContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("You can't sell a book with amount of 0!");
                }
            }
        }
    }
}
