using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public Book Book { get; set; }
        public Discount? Discount { get; set; }
        public Delay? DelayedForCustomer { get; set; }
    }
}
