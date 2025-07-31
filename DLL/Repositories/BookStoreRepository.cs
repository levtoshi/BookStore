using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Interfaces;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace DLL.Repositories
{
    public class BookStoreRepository : IRepository<Product>
    {
        private BookStoreContext _bookStoreContext;
        private List<Product> _products;
        private List<Sales> _sales;

        public string? BookName { get; set; }
        public string? BookAuthor { get; set; }
        public string? BookGenre { get; set; }
        public bool? BookOrder { get; set; }

        public int? ShowType { get; set; }
        public int? ShowDuration { get; set; }

        public BookStoreRepository(BookStoreContext context)
        {
            this._bookStoreContext = context;
            _bookStoreContext.SaveChanges();
            _products = new List<Product>();
            _sales = new List<Sales>();
            SetDefaultProduct();
        }

        public void AddNewBook(Product product)
        {  
            _bookStoreContext.Products.Add(product);
            _bookStoreContext.SaveChanges();
        }

        public void DeleteBook(Product product)
        {
            if (product != null)
            {
                Product tempProduct = FindElement(product.Id);
                if (tempProduct != null)
                {
                    if (tempProduct.Amount == 0)
                    {
                        _bookStoreContext.Database.ExecuteSqlRaw("DELETE FROM Products WHERE Id = {0}", product.Id);
                        _bookStoreContext.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("You can't delete a book, which have amount more than 0!");
                    }
                }
            }
        }

        public void UpdateBook(Product product)
        {
            if (product != null)
            {
                Product tempProduct = FindElement(product.Id);
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

                    if (product.Discount != null)
                    {
                        if (tempProduct.Discount == null)
                        {
                            tempProduct.Discount = new Discount();
                        }
                        tempProduct.Discount.Name = product.Discount.Name;
                        tempProduct.Discount.Interest = product.Discount.Interest;
                        tempProduct.Discount.StartDate = product.Discount.StartDate;
                        tempProduct.Discount.EndDate = product.Discount.EndDate;
                    }

                    if (product.DelayedForCustomer != null)
                    {
                        if(tempProduct.DelayedForCustomer == null)
                        {
                            tempProduct.DelayedForCustomer = new Delay();
                            tempProduct.DelayedForCustomer.Customer = new Customer();
                            tempProduct.DelayedForCustomer.Customer.FullName = new FullName();
                        }
                        tempProduct.DelayedForCustomer.Customer.FullName.Name = product.DelayedForCustomer.Customer.FullName.Name;
                        tempProduct.DelayedForCustomer.Customer.FullName.MiddleName = product.DelayedForCustomer.Customer.FullName.MiddleName;
                        tempProduct.DelayedForCustomer.Customer.FullName.LastName = product.DelayedForCustomer.Customer.FullName.LastName;
                        tempProduct.DelayedForCustomer.Customer.Email = product.DelayedForCustomer.Customer.Email;
                        tempProduct.DelayedForCustomer.Amount = product.DelayedForCustomer.Amount;
                    }

                    _bookStoreContext.SaveChanges();
                }
            }
        }

        public void AddDiscount(Product product, Discount discount)
        {
            if (product != null)
            {
                Product tempProduct = FindElement(product.Id);
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
                    _bookStoreContext.SaveChanges();
                }
            }
        }

        public void AddBooks(Product product, int amount)
        {
            if (product != null)
            {
                Product tempProduct = FindElement(product.Id);
                if (tempProduct != null)
                {
                    tempProduct.Amount += amount;
                    _bookStoreContext.SaveChanges();
                }
            }
        }
        public void WriteOffBooks(Product product, int amount)
        {
            if (product != null)
            {
                Product tempProduct = FindElement(product.Id);
                if (tempProduct != null)
                {
                    tempProduct.Amount -= (amount < tempProduct.Amount) ? amount : tempProduct.Amount;
                    _bookStoreContext.SaveChanges();
                }
            }
        }
        public void DelayBooks(Product product, Delay delay)
        {
            if (product != null)
            {
                Product tempProduct = FindElement(product.Id);
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
                    _bookStoreContext.SaveChanges();
                }
            }
        }

        public void SellBooks(Product product, int amount, DateTime dateTime)
        {
            if (product != null)
            {
                Product tempProduct = FindElement(product.Id);
                _bookStoreContext.Sales.Add(new Sales()
                {
                    Amount = amount,
                    SoldTime = dateTime,
                    Product = tempProduct
                });
                tempProduct.Amount -= (amount < tempProduct.Amount) ? amount : tempProduct.Amount;
                _bookStoreContext.SaveChanges();
            }
        }

        private Product FindElement(int id)
        {
            return _bookStoreContext.Products.Include(a => a.Book).ThenInclude(b => b.Author).Include(a => a.Book).ThenInclude(b => b.Producer).Include(a => a.Book).ThenInclude(b => b.Genre).Include(a => a.DelayedForCustomer).ThenInclude(b => b.Customer).ThenInclude(c => c.FullName).Include(a => a.Discount).Where(p => p.Id == id).FirstOrDefault();
        }

        public void SetDefaultProduct()
        {
            _products = _bookStoreContext.Products.Include(a => a.Book).ThenInclude(b => b.Author).
                Include(a => a.Book).ThenInclude(b => b.Producer).
                Include(a => a.Book).ThenInclude(b => b.Genre).
                Include(a => a.DelayedForCustomer).ThenInclude(b => b.Customer).ThenInclude(c => c.FullName).
                Include(a => a.Discount).ToList();
        }

        private void SetDefaultSales()
        {
            _sales = _bookStoreContext.Sales.Include(a => a.Product).ThenInclude(a => a.Book).ThenInclude(b => b.Author).
                Include(a => a.Product).ThenInclude(a => a.Book).ThenInclude(b => b.Producer).
                Include(a => a.Product).ThenInclude(a => a.Book).ThenInclude(b => b.Genre).
                Include(a => a.Product).ThenInclude(a => a.DelayedForCustomer).ThenInclude(b => b.Customer).ThenInclude(c => c.FullName).
                Include(a => a.Product).ThenInclude(a => a.Discount).ToList();
            _products.Clear();
            switch (ShowDuration)
            {
                case 0:
                    var sales1 = _sales.Where(a => a.SoldTime.Date == DateTime.Now.Date).GroupBy(a => a.Product.Id).Select(group => new
                    {
                        BookId = group.Key,
                        TotalSales = group.Sum(s => s.Amount)
                    }).OrderBy(a => a.TotalSales).Reverse();
                    foreach (var item in sales1)
                    {
                        _products.Add(FindElement(item.BookId));
                    }
                    _products.DistinctBy(a => a.Id);
                    break;
                case 1:
                    var sales2 = _sales.Where(a => (a.SoldTime.DayOfYear > (a.SoldTime.DayOfYear - ((int)a.SoldTime.DayOfWeek)) && a.SoldTime.DayOfYear <= (a.SoldTime.DayOfYear + (7 -(int)a.SoldTime.DayOfWeek)))).GroupBy(a => a.Product.Id).Select(group => new
                    {
                        BookId = group.Key,
                        TotalSales = group.Sum(s => s.Amount)
                    }).OrderBy(a => a.TotalSales).Reverse();
                    foreach (var item in sales2)
                    {
                        _products.Add(FindElement(item.BookId));
                    }
                    _products.DistinctBy(a => a.Id);
                    break;
                case 2:
                    var sales3 = _sales.Where(a => a.SoldTime.Date.Month == DateTime.Now.Date.Month).GroupBy(a => a.Product.Id).Select(group => new
                    {
                        BookId = group.Key,
                        TotalSales = group.Sum(s => s.Amount)
                    }).OrderBy(a => a.TotalSales).Reverse();
                    foreach (var item in sales3)
                    {
                        _products.Add(FindElement(item.BookId));
                    }
                    _products.DistinctBy(a => a.Id);
                    break;
                case 3:
                    var sales4 = _sales.Where(a => a.SoldTime.Date.Year == DateTime.Now.Date.Year).GroupBy(a => a.Product.Id).Select(group => new
                    {
                        BookId = group.Key,
                        TotalSales = group.Sum(s => s.Amount)
                    }).OrderBy(a => a.TotalSales).Reverse();
                    foreach (var item in sales4)
                    {
                        _products.Add(FindElement(item.BookId));
                    }
                    _products.DistinctBy(a => a.Id);
                    break;
            }
        }

        public List<Product> GetAll()
        {
            if(ShowType == null)
            {
                SetDefaultProduct();
                if (BookName != null)
                    SearchWithName(BookName);
                if (BookAuthor != null)
                    FilterWithAuthor(BookAuthor);
                if (BookGenre != null)
                    FilterWithGenre(BookGenre);
                if (BookOrder != null)
                    FilterWithOrder();
            }
            else if (ShowType == 0)
            {
                SetDefaultSales();
            }
            return _products;
        }

        public void SearchWithName(string name)
        {
            List<Product> temp = new List<Product>();
            foreach (Product item in _products)
            {
                if (item.Book.Name.IndexOf(name) != -1)
                {
                    temp.Add(item);
                }
            }
            _products = temp;
        }
        public void FilterWithAuthor(string author)
        {
            List<string> tempStr = author.Split(' ').ToList();
            _products = _products.Where(a => a.Book.Author.Name == tempStr[0] && a.Book.Author.MiddleName == tempStr[1] && a.Book.Author.LastName == tempStr[2]).ToList();
        }

        public List<string> GetAllAuthors()
        {
            List<string> strings = new List<string>();
            if (ShowType != null && ShowType != null)
            {
                SetDefaultSales();
            }
            else
            {
                SetDefaultProduct();
            }
            foreach (Product item in _products)
            {
                strings.Add(item.Book.Author.Name + " " + item.Book.Author.MiddleName + " " + item.Book.Author.LastName);
            }
            return strings;
        }

        public void FilterWithGenre(string genre)
        {
            _products = _products.Where(a => a.Book.Genre.Name == genre).ToList();
        }

        public List<string> GetAllGenres()
        {
            List<string> strings = new List<string>();
            if (ShowType != null && ShowType != null)
            {
                SetDefaultSales();
                foreach (Product item in _products)
                {
                    strings.Add(item.Book.Genre.Name);
                }
            }
            else
            {
                foreach (Genre item in _bookStoreContext.Genres)
                {
                    strings.Add(item.Name);
                }
            }
            return strings;
        }


        public void FilterWithOrder()
        {
            if(BookOrder == true)
            {
                _products = _products.OrderBy(a => a.Book.Year).ToList();
            }
            else
            {
                _products = _products.OrderBy(a => a.Book.Year).Reverse().ToList();
            }
        }

        public bool VerifyUser(List<string> data)
        {
            if(!_bookStoreContext.Users.IsNullOrEmpty())
            {
                return (_bookStoreContext.Users.Where(a => a.IsAdmin == true && a.Username == data[0] && a.Password == data[1]).FirstOrDefault() != null);
            }
            return true;
        }

        public string GetUsername()
        {
            if (!_bookStoreContext.Users.IsNullOrEmpty())
            {
                return _bookStoreContext.Users.Where(a => a == a).FirstOrDefault().Username;
            }
            return "";
        }
    }
}