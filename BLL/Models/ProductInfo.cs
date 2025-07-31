using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int Cost { get; set; }
        public int Price { get; set; }
        public BookInfo Book { get; set; }
        public DiscountInfo? Discount { get; set; }
        public DelayInfo? DelayedForCustomer { get; set; }
    }
}
