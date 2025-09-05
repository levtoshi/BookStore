using DLL.Entities;
using DLL.Interfaces;
using DLL.Stores;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories.BookModelRepositories
{
    public class BookModelRepository : IBookModelRepository, ISetCollectionToDefaultRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly ProductsStore _productsStore;

        public BookModelRepository(BookStoreContext bookStoreContext,
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

        public async Task AddBookModelAsync(Product product)
        {
            await _bookStoreContext.Products.AddAsync(product);
            await _bookStoreContext.SaveChangesAsync();
        }

        public async Task UpdateBookModelAsync(Product product)
        {

            if (product != null)
            {
                Product? tempProduct = await _productsStore.FindElementAsync(product.Id);
                if (tempProduct != null)
                {
                    tempProduct.Book.Name = product.Book.Name;
                    tempProduct.Book.Author.Name = product.Book.Author.Name;
                    tempProduct.Book.Author.MiddleName = product.Book.Author.MiddleName;
                    tempProduct.Book.Author.LastName = product.Book.Author.LastName;
                    tempProduct.Book.Producer.Name = product.Book.Producer.Name;
                    tempProduct.Book.PageAmount = product.Book.PageAmount;
                    tempProduct.Book.Genre.Name = product.Book.Genre.Name;
                    tempProduct.Book.Year = product.Book.Year;
                    tempProduct.Book.IsContinuation = product.Book.IsContinuation;

                    tempProduct.Amount = product.Amount;
                    tempProduct.Cost = product.Cost;
                    tempProduct.Price = product.Price;

                    await _bookStoreContext.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteBookModelAsync(Product product)
        {
            if (product != null)
            {
                Product? tempProduct = await _productsStore.FindElementAsync(product.Id);
                if (tempProduct != null)
                {
                    if (tempProduct.Amount == 0)
                    {
                        _bookStoreContext.Database.ExecuteSqlRaw("DELETE FROM Products WHERE Id = {0}", product.Id);
                        await _bookStoreContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("You can't delete a book, which have amount more than 0!");
                    }
                }
            }
        }
    }
}