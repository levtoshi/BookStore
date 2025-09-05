using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime SoldTime { get; set; }
        [Required]
        public Product Product { get; set; }
    }
}