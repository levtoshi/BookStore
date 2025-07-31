using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        string? BookName { get; set; }
        string? BookAuthor { get; set; }
        string? BookGenre { get; set; }
        bool? BookOrder { get; set; }
        int? ShowType { get; set; }
        int? ShowDuration { get; set; }

        void AddNewBook(T product);
        void UpdateBook(T product);
        void DeleteBook(T product);
        void SellBooks(T product, int amount, DateTime dateTime);
        void WriteOffBooks(T product, int amount);
        void AddBooks(T product, int amount);
        void AddDiscount(T product, DiscountInfo discount);
        void DelayBooks(T product, DelayInfo delay);
        List<T> GetAll();
        List<string> GetAllAuthors();
        List<string> GetAllGenres();
        bool VerifyUser(List<string> data);
        string GetUsername();
    }
}