using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using DLL;
using DLL.Interfaces;
using DLL.Models;
using DLL.Repositories;

namespace BLL.Services
{
    public class BookStoreService : IService<ProductInfo>
    {
        private IRepository<Product> _bookStoreRepository;

        public string? BookName
        {
            get
            {
                return _bookStoreRepository.BookName;
            }
            set
            {
                _bookStoreRepository.BookName = value;
            }
        }
        public string? BookAuthor
        {
            get
            {
                return _bookStoreRepository.BookAuthor;
            }
            set
            {
                _bookStoreRepository.BookAuthor = value;
            }
        }
        public string? BookGenre
        {
            get
            {
                return _bookStoreRepository.BookGenre;
            }
            set
            {
                _bookStoreRepository.BookGenre = value;
            }
        }
        public bool? BookOrder
        {
            get
            {
                return _bookStoreRepository.BookOrder;
            }
            set
            {
                _bookStoreRepository.BookOrder = value;
            }
        }

        public int? ShowType
        {
            get
            {
                return _bookStoreRepository.ShowType;
            }
            set
            {
                _bookStoreRepository.ShowType = value;
            }
        }

        public int? ShowDuration
        {
            get
            {
                return _bookStoreRepository.ShowDuration;
            }
            set
            {
                _bookStoreRepository.ShowDuration = value;
            }
        }

        public BookStoreService(IRepository<Product> repository)
        {
            this._bookStoreRepository = repository;
        }

        public void AddNewBook(ProductInfo product)
        {
            _bookStoreRepository.AddNewBook(Translators.TranslateFromProductInfoToProduct(product));
        }

        public void DeleteBook(ProductInfo product)
        {
            _bookStoreRepository.DeleteBook(Translators.TranslateFromProductInfoToProduct(product));
        }

        public void UpdateBook(ProductInfo product)
        {
            _bookStoreRepository.UpdateBook(Translators.TranslateFromProductInfoToProduct(product));
        }

        public void AddDiscount(ProductInfo product, DiscountInfo discount)
        {
            _bookStoreRepository.AddDiscount(Translators.TranslateFromProductInfoToProduct(product), Translators.TranslateFromDiscountInfoToDiscount(discount));
        }


        public void AddBooks(ProductInfo product, int amount)
        {
            _bookStoreRepository.AddBooks(Translators.TranslateFromProductInfoToProduct(product), amount);
        }

        public void WriteOffBooks(ProductInfo product, int amount)
        {
            _bookStoreRepository.WriteOffBooks(Translators.TranslateFromProductInfoToProduct(product), amount);
        }

        public void DelayBooks(ProductInfo product, DelayInfo delay)
        {
            _bookStoreRepository.DelayBooks(Translators.TranslateFromProductInfoToProduct(product), Translators.TranslateFromDelayInfoToDelay(delay));
        }

        public void SellBooks(ProductInfo product, int amount, DateTime dateTime)
        {
            _bookStoreRepository.SellBooks(Translators.TranslateFromProductInfoToProduct(product), amount, dateTime);
        }

        public List<ProductInfo> GetAll()
        {
            return Translators.TranslateFromProductListToProductInfoList(_bookStoreRepository.GetAll());
        }

        public List<string> GetAllAuthors()
        {
            return _bookStoreRepository.GetAllAuthors();
        }

        public List<string> GetAllGenres()
        {
            return _bookStoreRepository.GetAllGenres();
        }

        public bool VerifyUser(List<string> data)
        {
            return _bookStoreRepository.VerifyUser(data);
        }

        public string GetUsername()
        {
            return _bookStoreRepository.GetUsername();
        }
    }
}