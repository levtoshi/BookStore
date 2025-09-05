using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
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