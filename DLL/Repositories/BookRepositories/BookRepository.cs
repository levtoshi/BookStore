using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories.BookRepositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _bookStoreContext;

        public BookRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
        }

        public async Task<List<Product>> GetAllBooksAsync()
        {
            return await _bookStoreContext.Products
                .AsNoTracking()
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Author)
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Producer)
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Genre)
                .ToListAsync();
        }

        public async Task<Product> AddBookModelAsync(Product product)
        {
            if (product != null)
            {
                _bookStoreContext.Products.Add(product);
                await _bookStoreContext.SaveChangesAsync();
                return product;
            }
            throw new Exception("Product is null!");
        }

        public async Task<Product> UpdateBookModelAsync(Product product)
        {
            if (product != null)
            {
                Product? tempProduct = await _bookStoreContext.Products
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Author)
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Producer)
                    .Include(p => p.Book)
                        .ThenInclude(b => b.Genre)
                    .FirstOrDefaultAsync(p => p.Id == product.Id);
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
                    return tempProduct;
                }
                throw new Exception("There is no such product in database!");
            }
            throw new Exception("Product is null!");
        }

        public async Task DeleteBookModelAsync(Product product)
        {
            if (product != null)
            {
                Product? tempProduct = await _bookStoreContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
                if (tempProduct != null)
                {
                    if (tempProduct.Amount == 0)
                    {
                        _bookStoreContext.Products.Remove(tempProduct);
                        await _bookStoreContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("You can't delete a book, which have amount more than 0!");
                    }
                }
                else
                {
                    throw new Exception("There is no such product in database!");
                }
            }
            else
            {
                throw new Exception("Product is null!");
            }
        }

        public async Task AddBookStockAsync(int productId, int amount)
        {
            Product? tempProduct = await _bookStoreContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (tempProduct != null)
            {
                tempProduct.Amount += amount;
                await _bookStoreContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no such product in database!");
            }
        }

        public async Task WriteOffBookAsync(int productId, int amount)
        {
            Product? tempProduct = await _bookStoreContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (tempProduct != null)
            {
                tempProduct.Amount -= (amount < tempProduct.Amount) ? amount : tempProduct.Amount;
                await _bookStoreContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no such product in database!");
            }
        }

        public async Task SellBookAsync(int productId, int amount, DateTime dateTime)
        {
            Product? tempProduct = await _bookStoreContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (tempProduct != null)
            {
                if (tempProduct.Amount != 0)
                {
                    int amountClear = (amount < tempProduct.Amount) ? amount : tempProduct.Amount;
                    _bookStoreContext.Sales.Add(new Sale()
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
            else
            {
                throw new Exception("There is no such product in database!");
            }
        }
    }
}