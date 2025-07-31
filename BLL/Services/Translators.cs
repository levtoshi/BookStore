using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DLL.Models;
using DLL.Repositories;

namespace BLL.Services
{
    public static class Translators
    {
        public static Product TranslateFromProductInfoToProduct(ProductInfo product)
        {
            Product tempProduct = new Product();
            tempProduct.Id = product.Id;
            tempProduct.Book = new Book();
            tempProduct.Book.Id = product.Book.Id;
            tempProduct.Book.Author = new FullName();
            tempProduct.Book.Author.Id = product.Book.Author.Id;
            tempProduct.Book.Producer = new Producer();
            tempProduct.Book.Producer.Id = product.Book.Producer.Id;
            tempProduct.Book.Genre = new Genre();
            tempProduct.Book.Genre.Id = product.Book.Genre.Id;

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
                tempProduct.Discount = new Discount();
                tempProduct.Discount.Id = product.Discount.Id;
                tempProduct.Discount.Name = product.Discount.Name;
                tempProduct.Discount.Interest = product.Discount.Interest;
                tempProduct.Discount.StartDate = product.Discount.StartDate;
                tempProduct.Discount.EndDate = product.Discount.EndDate;
            }

            if (product.DelayedForCustomer != null)
            {
                tempProduct.DelayedForCustomer = new Delay();
                tempProduct.DelayedForCustomer.Id = product.DelayedForCustomer.Id;
                tempProduct.DelayedForCustomer.Customer = new Customer();
                tempProduct.DelayedForCustomer.Customer.Id = product.DelayedForCustomer.Customer.Id;
                tempProduct.DelayedForCustomer.Customer.FullName = new FullName();
                tempProduct.DelayedForCustomer.Customer.FullName.Id = product.DelayedForCustomer.Customer.FullName.Id;

                tempProduct.DelayedForCustomer.Customer.FullName.Name = product.DelayedForCustomer.Customer.FullName.Name;
                tempProduct.DelayedForCustomer.Customer.FullName.MiddleName = product.DelayedForCustomer.Customer.FullName.MiddleName;
                tempProduct.DelayedForCustomer.Customer.FullName.LastName = product.DelayedForCustomer.Customer.FullName.LastName;
                tempProduct.DelayedForCustomer.Customer.Email = product.DelayedForCustomer.Customer.Email;
                tempProduct.DelayedForCustomer.Amount = product.DelayedForCustomer.Amount;
            }
            return tempProduct;
        }

        public static ProductInfo TranslateFromProductToProductInfo(Product product)
        {
            ProductInfo tempProduct = new ProductInfo();
            tempProduct.Id = product.Id;
            tempProduct.Book = new BookInfo();
            tempProduct.Book.Id = product.Book.Id;
            tempProduct.Book.Author = new FullNameInfo();
            tempProduct.Book.Author.Id = product.Book.Author.Id;
            tempProduct.Book.Producer = new ProducerInfo();
            tempProduct.Book.Producer.Id = product.Book.Producer.Id;
            tempProduct.Book.Genre = new GenreInfo();
            tempProduct.Book.Genre.Id = product.Book.Genre.Id;

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
                tempProduct.Discount = new DiscountInfo();
                tempProduct.Discount.Id = product.Discount.Id;
                tempProduct.Discount.Name = product.Discount.Name;
                tempProduct.Discount.Interest = product.Discount.Interest;
                tempProduct.Discount.StartDate = product.Discount.StartDate;
                tempProduct.Discount.EndDate = product.Discount.EndDate;
            }

            if (product.DelayedForCustomer != null)
            {
                tempProduct.DelayedForCustomer = new DelayInfo();
                tempProduct.DelayedForCustomer.Id = product.DelayedForCustomer.Id;
                tempProduct.DelayedForCustomer.Customer = new CustomerInfo();
                tempProduct.DelayedForCustomer.Customer.Id = product.DelayedForCustomer.Customer.Id;
                tempProduct.DelayedForCustomer.Customer.FullName = new FullNameInfo();
                tempProduct.DelayedForCustomer.Customer.FullName.Id = product.DelayedForCustomer.Customer.FullName.Id;

                tempProduct.DelayedForCustomer.Customer.FullName.Name = product.DelayedForCustomer.Customer.FullName.Name;
                tempProduct.DelayedForCustomer.Customer.FullName.MiddleName = product.DelayedForCustomer.Customer.FullName.MiddleName;
                tempProduct.DelayedForCustomer.Customer.FullName.LastName = product.DelayedForCustomer.Customer.FullName.LastName;
                tempProduct.DelayedForCustomer.Customer.Email = product.DelayedForCustomer.Customer.Email;
                tempProduct.DelayedForCustomer.Amount = product.DelayedForCustomer.Amount;
            }
            return tempProduct;
        }

        public static Delay TranslateFromDelayInfoToDelay(DelayInfo delayInfo)
        {
            Delay delay = new Delay();
            delay.Id = delayInfo.Id;
            delay.Customer = new Customer();
            delay.Customer.Id = delayInfo.Customer.Id;
            delay.Customer.FullName = new FullName();
            delay.Customer.FullName.Id = delayInfo.Customer.FullName.Id;

            delay.Customer.FullName.Name = delayInfo.Customer.FullName.Name;
            delay.Customer.FullName.MiddleName = delayInfo.Customer.FullName.MiddleName;
            delay.Customer.FullName.LastName = delayInfo.Customer.FullName.LastName;
            delay.Customer.Email = delayInfo.Customer.Email;
            delay.Amount = delayInfo.Amount;
            return delay;
        }

        public static DelayInfo TranslateFromDelayToDelayInfo(Delay delayInfo)
        {
            DelayInfo delay = new DelayInfo();
            delay.Id = delayInfo.Id;
            delay.Customer = new CustomerInfo();
            delay.Customer.Id = delayInfo.Customer.Id;
            delay.Customer.FullName = new FullNameInfo();
            delay.Customer.FullName.Id = delayInfo.Customer.FullName.Id;

            delay.Customer.FullName.Name = delayInfo.Customer.FullName.Name;
            delay.Customer.FullName.MiddleName = delayInfo.Customer.FullName.MiddleName;
            delay.Customer.FullName.LastName = delayInfo.Customer.FullName.LastName;
            delay.Customer.Email = delayInfo.Customer.Email;
            delay.Amount = delayInfo.Amount;
            return delay;
        }

        public static Discount TranslateFromDiscountInfoToDiscount(DiscountInfo discountInfo)
        {
            Discount discount = new Discount();
            discount.Id = discountInfo.Id;
            discount.Name = discountInfo.Name;
            discount.Interest = discountInfo.Interest;
            discount.StartDate = discountInfo.StartDate;
            discount.EndDate = discountInfo.EndDate;
            return discount;
        }

        public static DiscountInfo TranslateFromDiscountToDiscountInfo(Discount discountInfo)
        {
            DiscountInfo discount = new DiscountInfo();
            discount.Id = discountInfo.Id;
            discount.Name = discountInfo.Name;
            discount.Interest = discountInfo.Interest;
            discount.StartDate = discountInfo.StartDate;
            discount.EndDate = discountInfo.EndDate;
            return discount;
        }

        public static List<ProductInfo> TranslateFromProductListToProductInfoList(List<Product> products)
        {
            List<ProductInfo> temp1 = new List<ProductInfo>();
            List<Product> temp2 = products;
            foreach (Product item in temp2)
            {
                temp1.Add(Translators.TranslateFromProductToProductInfo(item));
            }
            return temp1;
        }
    }
}